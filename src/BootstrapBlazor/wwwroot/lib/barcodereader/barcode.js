(function () { 
    window.zxing = {
        start: function (autostop,wrapper) {
            console.log('autostop' + autostop);          
  
            let selectedDeviceId;
            //const codeReader = new ZXing.BrowserBarcodeReader()
            const codeReader = new ZXing.BrowserMultiFormatReader()
            console.log('ZXing code reader initialized')
            codeReader.getVideoInputDevices()
                .then((videoInputDevices) => {
                    const sourceSelect = document.getElementById('sourceSelect')
                    selectedDeviceId = videoInputDevices[0].deviceId
                    console.log('videoInputDevices:' + videoInputDevices.length);
                    if (videoInputDevices.length > 1) {
                        videoInputDevices.forEach((element) => {
                            const sourceOption = document.createElement('option')
                            sourceOption.text = element.label
                            sourceOption.value = element.deviceId
                            sourceSelect.appendChild(sourceOption)
                            selectedDeviceId = element.deviceId;
                        })

                        sourceSelect.onchange = () => {
                            selectedDeviceId = sourceSelect.value;
                            codeReader.reset();
                            StartScan();
                        }

                        const sourceSelectPanel = document.getElementById('sourceSelectPanel')
                        sourceSelectPanel.style.display = 'block'
                    }

                    StartScan(autostop);

                    document.getElementById('startButton').addEventListener('click', () => {
                        StartScan();
                    })

                    function StartScan(autostop) {
                        codeReader.decodeOnceFromVideoDevice(selectedDeviceId, 'video').then((result) => {
                            console.log(result)
                            document.getElementById('result').textContent = result.text

                            var supportsVibrate = "vibrate" in navigator;
                            if (supportsVibrate) navigator.vibrate(1000);

                            if (autostop) {
                                console.log('autostop');
                                codeReader.reset();
                                return wrapper.invokeMethodAsync("invokeFromJS", result.text);
                            } else {
                                console.log('None-stop');
                                codeReader.reset();
                                wrapper.invokeMethodAsync("invokeFromJS", result.text);
                            }

                        }).catch((err) => {
                            console.error(err)
                            document.getElementById('result').textContent = err
                        })
                        console.log(`Started continous decode from camera with id ${selectedDeviceId}`)
                    }

                    document.getElementById('resetButton').addEventListener('click', () => {
                        document.getElementById('result').textContent = '';
                        codeReader.reset();
                        console.log('Reset.')
                    })

                    document.getElementById('closeButton').addEventListener('click', () => {
                        document.getElementById('result').textContent = '';
                        codeReader.reset();
                        console.log('closeButton.')
                        wrapper.invokeMethodAsync("invokeFromJSClose");
                    })

                })
                .catch((err) => {
                    console.error(err)
                })
        }
    };
})();