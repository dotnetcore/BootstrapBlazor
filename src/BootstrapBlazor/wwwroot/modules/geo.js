Number.prototype.toRadians = function () {
    return this * Math.PI / 180;
}

export default {
    distance: function (latitude1, longitude1, latitude2, longitude2) {
        // R is the radius of the earth in kilometers
        var R = 6371;

        var deltaLatitude = (latitude2 - latitude1).toRadians();
        var deltaLongitude = (longitude2 - longitude1).toRadians();
        latitude1 = latitude1.toRadians();
        latitude2 = latitude2.toRadians();

        var a = Math.sin(deltaLatitude / 2) *
            Math.sin(deltaLatitude / 2) +
            Math.cos(latitude1) *
            Math.cos(latitude2) *
            Math.sin(deltaLongitude / 2) *
            Math.sin(deltaLongitude / 2);

        var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
        var d = R * c;
        return d;
    },

    updateLocaltion: function (position, currentDistance = 0.0, totalDistance = 0.0, lastLat, lastLong) {
        // 纬度
        var latitude = position.coords.latitude;
        // 经度
        var longitude = position.coords.longitude;
        // 位置精度
        var accuracy = position.coords.accuracy;
        // 海拔高度
        var altitude = position.coords.altitude;
        // 位置的海拔精度
        var altitudeAccuracy = position.coords.altitudeAccuracy;
        // 方向，从正北开始以度计
        var heading = position.coords.heading;
        // 速度
        var speed = position.coords.speed;
        // 响应的日期/时间
        var timestamp = position.timestamp;

        // sanity test... don't calculate distance if accuracy
        // value too large
        if (accuracy >= 500) {
            console.warn("Need more accurate values to calculate distance.");
        }

        // calculate distance
        if (lastLat != null && lastLong != null) {
            currentDistance = $.bb_geo_distance(latitude, longitude, lastLat, lastLong);
            totalDistance += currentDistance;
        }

        lastLat = latitude;
        lastLong = longitude;

        if (altitude == null) {
            altitude = 0;
        }
        if (altitudeAccuracy == null) {
            altitudeAccuracy = 0;
        }
        if (heading == null) {
            heading = 0;
        }
        if (speed == null) {
            speed = 0;
        }
        return {
            latitude,
            longitude,
            accuracy,
            altitude,
            altitudeAccuracy,
            heading,
            speed,
            timestamp,
            currentDistance,
            totalDistance,
            lastLat,
            lastLong,
        };
    },
    handleLocationError: function (error) {
        switch (error.code) {
            case 0:
                console.error("There was an error while retrieving your location: " + error.message);
                break;
            case 1:
                console.error("The user prevented this page from retrieving a location.");
                break;
            case 2:
                console.error("The browser was unable to determine your location: " + error.message);
                break;
            case 3:
                console.error("The browser timed out before retrieving the location.");
                break;
        }
    },
    getCurrnetPosition: function (obj, method) {
        var ret = false;
        if (navigator.geolocation) {
            ret = true;
            navigator.geolocation.getCurrentPosition(position => {
                var info = $.bb_geo_updateLocaltion(position);
                obj.invokeMethodAsync(method, info);
            }, $.bb_geo_handleLocationError);
        }
        else {
            console.warn("HTML5 Geolocation is not supported in your browser");
        }
        return ret;
    },
    watchPosition: function (obj, method) {
        var id = 0;
        var currentDistance = 0.0;
        var totalDistance = 0.0;
        var lastLat;
        var lastLong;
        if (navigator.geolocation) {
            id = navigator.geolocation.watchPosition(position => {
                var info = $.bb_geo_updateLocaltion(position, currentDistance, totalDistance, lastLat, lastLong);
                currentDistance = info.currentDistance;
                totalDistance = info.totalDistance;
                lastLat = info.lastLat;
                lastLong = info.lastLong;
                obj.invokeMethodAsync(method, info);
            }, $.bb_geo_handleLocationError, {
                maximumAge: 20000
            });
        }
        else {
            console.warn("HTML5 Geolocation is not supported in your browser");
        }
        return id;
    },
    clearWatchLocation: function (id) {
        var ret = false;
        if (navigator.geolocation) {
            ret = true;
            navigator.geolocation.clearWatch(id);
        }
        return ret;
    }
}
