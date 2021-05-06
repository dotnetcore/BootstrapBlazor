(function ($) {
    $.extend({
        bb_download_wasm: function (name, contentType, content) {
            // Convert the parameters to actual JS types
            var nameStr = BINDING.conv_string(name);
            var contentTypeStr = BINDING.conv_string(contentType);
            var contentArray = Blazor.platform.toUint8Array(content);

            // Create the URL
            var file = new File([contentArray], nameStr, { type: contentTypeStr });
            var exportUrl = URL.createObjectURL(file);

            // Create the <a> element and click on it
            var a = document.createElement("a");
            document.body.appendChild(a);
            a.href = exportUrl;
            a.download = nameStr;
            a.target = "_self";
            a.click();

            // We don't need to keep the url, let's release the memory
            // On Safari it seems you need to comment this line... (please let me know if you know why)
            URL.revokeObjectURL(exportUrl);
        },
        bb_download: function (filename, contentType, content) {
            var exportUrl = $.bb_create_url(filename, contentType, content)
            // Create the <a> element and click on it
            var a = document.createElement("a");
            document.body.appendChild(a);
            a.href = exportUrl;
            a.download = filename;
            a.target = "_self";
            a.click();

            // We don't need to keep the url, let's release the memory
            // On Safari it seems you need to comment this line... (please let me know if you know why)
            URL.revokeObjectURL(exportUrl);
        },
        bb_create_url_wasm: function (filename, contentType, content) {
            // Convert the parameters to actual JS types
            var nameStr = BINDING.conv_string(filename);
            var contentTypeStr = BINDING.conv_string(contentType);
            var contentArray = Blazor.platform.toUint8Array(content);

            // Create the URL
            var file = new File([contentArray], nameStr, { type: contentTypeStr });
            return URL.createObjectURL(file);
        },
        bb_create_url: function (filename, contentType, content) {
            // Blazor marshall byte[] to a base64 string, so we first need to convert the string (content) to a Uint8Array to create the File
            var data = $.base64DecToArr(content);

            // Create the URL
            var file = new File([data], filename, { type: contentType });
            return  URL.createObjectURL(file);
        },
        // Convert a base64 string to a Uint8Array. This is needed to create a blob object from the base64 string.
        // The code comes from: https://developer.mozilla.org/fr/docs/Web/API/WindowBase64/D%C3%A9coder_encoder_en_base64
        b64ToUint6: function (nChr) {
            return nChr > 64 && nChr < 91 ? nChr - 65 : nChr > 96 && nChr < 123 ? nChr - 71 : nChr > 47 && nChr < 58 ? nChr + 4 : nChr === 43 ? 62 : nChr === 47 ? 63 : 0;
        },
        base64DecToArr: function (sBase64, nBlocksSize) {
            var
                sB64Enc = sBase64.replace(/[^A-Za-z0-9\+\/]/g, ""),
                nInLen = sB64Enc.length,
                nOutLen = nBlocksSize ? Math.ceil((nInLen * 3 + 1 >> 2) / nBlocksSize) * nBlocksSize : nInLen * 3 + 1 >> 2,
                taBytes = new Uint8Array(nOutLen);

            for (var nMod3, nMod4, nUint24 = 0, nOutIdx = 0, nInIdx = 0; nInIdx < nInLen; nInIdx++) {
                nMod4 = nInIdx & 3;
                nUint24 |= $.b64ToUint6(sB64Enc.charCodeAt(nInIdx)) << 18 - 6 * nMod4;
                if (nMod4 === 3 || nInLen - nInIdx === 1) {
                    for (nMod3 = 0; nMod3 < 3 && nOutIdx < nOutLen; nMod3++, nOutIdx++) {
                        taBytes[nOutIdx] = nUint24 >>> (16 >>> nMod3 & 24) & 255;
                    }
                    nUint24 = 0;
                }
            }
            return taBytes;
        }
    });
})(jQuery);
