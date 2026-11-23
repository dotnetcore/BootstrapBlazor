import { getClientInfo } from "./client.js"
import Data from "./data.js"
import EventHandler from "./event-handler.js";

export async function init(id, options) {
    const { invoke, method, interval = 3000, url, connectionId } = options;
    if (Data.get(id)) {
        return;
    }

    const pollInterval = Math.min(2147483647, Math.max(4, interval / 8));
    const leaseInterval = pollInterval * 2;
    const connectionIdKey = 'bb_hub_connection_id';
    const leaseKey = 'bb_hub_leader_lease';

    if (localStorage.getItem(connectionIdKey) === null) {
        localStorage.setItem(connectionIdKey, connectionId);
    }

    const hub = { active: true, handler: null, info: null, nextBeat: 0, retryAfter: 0, inFlight: false };
    const getLease = () => {
        const lease = localStorage.getItem(leaseKey) || '';
        const separator = lease.lastIndexOf(':');
        return { owner: lease.substring(0, separator), expires: Number(lease.substring(separator + 1)) };
    };
    const releaseLease = () => {
        if (getLease().owner === connectionId) {
            localStorage.removeItem(leaseKey);
        }
    };
    const callback = async () => {
        const now = Date.now();
        if (!hub.active || now < hub.retryAfter) {
            return;
        }

        const lease = getLease();
        const ownsLease = lease.owner === connectionId;
        if (!ownsLease && lease.expires > now) {
            return;
        }

        // Give another tab time to report if this heartbeat is still pending halfway to the next one.
        if (hub.inFlight && (!ownsLease || now >= hub.nextBeat - interval / 2)) {
            return;
        }

        if (!ownsLease) {
            hub.nextBeat = 0;
        }
        localStorage.setItem(leaseKey, `${connectionId}:${now + leaseInterval}`);
        if (hub.inFlight || now < hub.nextBeat || getLease().owner !== connectionId) {
            return;
        }

        hub.nextBeat = now + interval;
        hub.inFlight = true;
        try {
            hub.info.id = localStorage.getItem(connectionIdKey);
            await invoke.invokeMethodAsync(method, hub.info);
        }
        catch {
            // Let another tab take over before retrying a disconnected circuit.
            hub.retryAfter = Date.now() + interval;
            releaseLease();
        }
        finally {
            hub.inFlight = false;
        }
    };
    const start = () => {
        if (hub.active && hub.info && hub.handler === null) {
            hub.handler = setInterval(callback, pollInterval);
            callback();
        }
    };
    hub.hide = () => {
        hub.active = false;
        clearInterval(hub.handler);
        hub.handler = null;
        releaseLease();
    };
    hub.show = event => {
        if (event.persisted && !hub.active) {
            hub.active = true;
            start();
        }
    };
    EventHandler.on(window, 'pagehide', hub.hide);
    EventHandler.on(window, 'pageshow', hub.show);
    Data.set(id, hub);

    try {
        hub.info = await getClientInfo(url);
        start();
    }
    catch (error) {
        if (Data.get(id) === hub) {
            dispose(id);
        }
        throw error;
    }
}

export function dispose(id) {
    const hub = Data.get(id);
    if (hub) {
        hub.hide();
        EventHandler.off(window, 'pagehide', hub.hide);
        EventHandler.off(window, 'pageshow', hub.show);
        Data.remove(id);
    }
}
