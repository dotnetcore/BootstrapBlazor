Number.prototype.toRadians = function () {
    return this * Math.PI / 180
}

const handleLocationError = error => {
    switch (error.code) {
        case 0:
            console.error("There was an error while retrieving your location: " + error.message)
            break
        case 1:
            console.error("The user prevented this page from retrieving a location.")
            break
        case 2:
            console.error("The browser was unable to determine your location: " + error.message)
            break
        case 3:
            console.error("The browser timed out before retrieving the location.")
            break
    }
}

const updateLocaltion = (position, currentDistance = 0.0, totalDistance = 0.0, lastLat, lastLong) => {
    // 纬度
    const latitude = position.coords.latitude
    // 经度
    const longitude = position.coords.longitude
    // 位置精度
    const accuracy = position.coords.accuracy
    // 海拔高度
    let altitude = position.coords.altitude
    // 位置的海拔精度
    let altitudeAccuracy = position.coords.altitudeAccuracy
    // 方向，从正北开始以度计
    let heading = position.coords.heading
    // 速度
    let speed = position.coords.speed
    // 响应的日期/时间
    const timestamp = position.timestamp

    // calculate distance
    if (lastLat != null && lastLong != null) {
        currentDistance = distance(latitude, longitude, lastLat, lastLong)
        totalDistance += currentDistance
    }

    lastLat = latitude
    lastLong = longitude

    if (altitude == null) {
        altitude = 0
    }
    if (altitudeAccuracy == null) {
        altitudeAccuracy = 0
    }
    if (heading == null) {
        heading = 0
    }
    if (speed == null) {
        speed = 0
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
    }
}

const distance = (latitude1, longitude1, latitude2, longitude2) => {
    // R is the radius of the earth in kilometers
    const R = 6371

    const deltaLatitude = (latitude2 - latitude1).toRadians()
    const deltaLongitude = (longitude2 - longitude1).toRadians()
    latitude1 = latitude1.toRadians()
    latitude2 = latitude2.toRadians()

    const a = Math.sin(deltaLatitude / 2) *
        Math.sin(deltaLatitude / 2) +
        Math.cos(latitude1) *
        Math.cos(latitude2) *
        Math.sin(deltaLongitude / 2) *
        Math.sin(deltaLongitude / 2)

    const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a))
    const d = R * c
    return d
}

export function getPosition() {
    let async = false
    let pos = null
    if (navigator.geolocation) {
        async = true
        navigator.geolocation.getCurrentPosition(position => {
            pos = updateLocaltion(position)
        }, handleLocationError)
    }
    else {
        console.warn("HTML5 Geolocation is not supported in your browser")
    }
    return new Promise((resolve, reject) => {
        if (async) {
            const handler = setInterval(() => {
                if (pos != null) {
                    clearInterval(handler)
                    resolve(pos)
                }
            }, 20)
        }
        else {
            resolve(null)
        }
    })
}

export function watchPosition(invoke, callback) {
    let id = 0
    let currentDistance = 0.0
    let totalDistance = 0.0
    let lastLat
    let lastLong
    if (navigator.geolocation) {
        id = navigator.geolocation.watchPosition(position => {
            const info = updateLocaltion(position, currentDistance, totalDistance, lastLat, lastLong)
            currentDistance = info.currentDistance
            totalDistance = info.totalDistance
            lastLat = info.lastLat
            lastLong = info.lastLong
            invoke.invokeMethodAsync(callback, info)
        }, handleLocationError, {
            maximumAge: 20000
        })
    }
    else {
        console.warn("HTML5 Geolocation is not supported in your browser")
    }
    return id
}

export function clearWatchLocation(id) {
    let ret = false
    if (navigator.geolocation) {
        ret = true
        navigator.geolocation.clearWatch(id)
    }
    return ret
}
