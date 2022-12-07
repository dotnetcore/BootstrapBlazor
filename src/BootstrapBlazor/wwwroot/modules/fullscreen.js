import SimpleComponent from "./base/simple-component.js"
import { getElementById, isElement } from "./base/index.js"

export class FullScreen extends SimpleComponent {
    _execute(args) {
        this._toggleElement = args[0]
        if (this._toggleElement && !isElement(this._toggleElement)) {
            this._toggleElement = getElementById(this._toggleElement)
        }
        else {
            this._toggleElement = document.documentElement
        }
        this._toggle()
    }

    _toggle() {
        if(this._isFullscreen()) {
            this._exit()
        }
        else {
            this._enter()
        }
        this._toggleElement.classList.toggle('fs-open')
    }

    _enter() {
        this._toggleElement.requestFullscreen() ||
        this._toggleElement.webkitRequestFullscreen ||
        this._toggleElement.mozRequestFullScreen ||
        this._toggleElement.msRequestFullscreen;
    }

    _exit()  {
        if (document.exitFullscreen) {
            document.exitFullscreen();
        }
        else if (document.mozCancelFullScreen) {
            document.mozCancelFullScreen();
        }
        else if (document.webkitExitFullscreen) {
            document.webkitExitFullscreen();
        }
        else if (document.msExitFullscreen) {
            document.msExitFullscreen();
        }
    }

    _isFullscreen() {
        return document.fullscreen ||
            document.webkitIsFullScreen ||
            document.webkitFullScreen ||
            document.mozFullScreen ||
            document.msFullScreent
    }
}
