(function (document) {
  const getHours = value => Math.trunc((value / 60 / 60) % 60, 10);
  const getMinutes = value => Math.trunc((value / 60) % 60, 10);
  const getSeconds = value => Math.trunc(value % 60, 10);

  function isNumber (obj) {
    var type = typeof obj;
    return (type === "number" || type === "string") &&
      !isNaN(obj - parseFloat(obj));
  }

  // Format time to UI friendly string
  function formatTime (time = 0, displayHours = false, inverted = false) {
    // Bail if the value isn't a number
    if (!isNumber(time)) {
      return formatTime(null, displayHours, inverted);
    }

    // Format time component to add leading zero
    const format = value => `0${value}`.slice(-2);

    // Breakdown to hours, mins, secs
    let hours = getHours(time);
    const mins = getMinutes(time);
    const secs = getSeconds(time);

    // Do we need to display hours?
    if (displayHours || hours > 0) {
      hours = `${hours}:`;
    } else {
      hours = '';
    }

    // Render
    return `${inverted && time > 0 ? '-' : ''}${hours}${format(mins)}:${format(secs)}`;
  }

  class Thumbnails {
    /**
     * PreviewThumbnails constructor.
     * @param {Plyr} player
     * @return {PreviewThumbnails}
     */
    constructor(player) {
      this.player = player;
      this.loaded = false;
      this.lastMouseMoveTime = Date.now();
      this.mouseDown = false;
      this.loadedImages = [];

      this.elements = {
        thumb: {},
        scrubbing: {},
      };

      this.load();
    }

    get enabled () {
      return this.player.isHTML5 && this.player.isVideo && this.player.config.thumbnail.enabled;
    }
    get config () {
      return this.player.config.thumbnail;
    }

    load () {
      // Togglethe regular seek tooltip
      if (this.player.elements.display.seekTooltip) {
        this.player.elements.display.seekTooltip.hidden = this.enabled;
      }

      if (!this.enabled) {
        return;
      }

      // Render DOM elements
      this.render();

      // Check to see if thumb container size was specified manually in CSS
      this.determineContainerAutoSizing();

      this.loaded = true;

      this.listeners();
    }

    startMove (event) {
      if (!this.loaded) {
        return;
      }

      if (!event instanceof Event || !['touchmove', 'mousemove'].includes(event.type)) {
        return;
      }

      // Wait until media has a duration
      if (!this.player.media.duration) {
        return;
      }

      if (event.type === 'touchmove') {
        // Calculate seek hover position as approx video seconds
        this.seekTime = this.player.media.duration * (this.player.elements.inputs.seek.value / 100);
      } else {
        // Calculate seek hover position as approx video seconds
        const clientRect = this.player.elements.progress.getBoundingClientRect();
        const percentage = (100 / clientRect.width) * (event.pageX - clientRect.left);
        this.seekTime = this.player.media.duration * (percentage / 100);

        if (this.seekTime < 0) {
          // The mousemove fires for 10+px out to the left
          this.seekTime = 0;
        }

        if (this.seekTime > this.player.media.duration - 1) {
          // Took 1 second off the duration for safety, because different players can disagree on the real duration of a video
          this.seekTime = this.player.media.duration - 1;
        }

        this.mousePosX = event.pageX;

        // Set time text inside image container
        this.elements.thumb.time.innerText = formatTime(this.seekTime);
      }

      // Download and show image
      this.showImageAtCurrentTime();
    }

    endMove () {
      this.toggleThumbContainer(false, true);
    }

    startScrubbing (event) {
      // Only act on left mouse button (0), or touch device (event.button is false)
      if (event.button === false || event.button === 0) {
        this.mouseDown = true;

        // Wait until media has a duration
        if (this.player.media.duration) {
          this.toggleScrubbingContainer(true);
          this.toggleThumbContainer(false, true);

          // Download and show image
          this.showImageAtCurrentTime();
        }
      }
    }

    endScrubbing () {
      this.mouseDown = false;

      // Hide scrubbing preview. But wait until the video has successfully seeked before hiding the scrubbing preview
      if (Math.ceil(this.lastTime) === Math.ceil(this.player.media.currentTime)) {
        // The video was already seeked/loaded at the chosen time - hide immediately
        this.toggleScrubbingContainer(false);
      } else {
        // The video hasn't seeked yet. Wait for that
        this.player.media.addEventListener('timeupdate', () => {
          // Re-check mousedown - we might have already started scrubbing again
          if (!this.mouseDown) {
            this.toggleScrubbingContainer(false);
          }
        }, { once: true });
      }
    }

    /**
     * Setup hooks for Plyr and window events
     */
    listeners () {
      const { player } = this;

      // Hide thumbnail preview - on mouse click, mouse leave (in listeners.js for now), and video play/seek. All four are required, e.g., for buffering
      player.on('play', () => {
        this.toggleThumbContainer(false, true);
      });

      player.on('seeked', () => {
        this.toggleThumbContainer(false);
      });

      player.on('timeupdate', () => {
        this.lastTime = this.player.media.currentTime;
      });

      let elements = this.player.elements;
      // Preview thumbnails plugin
      // TODO: Really need to work on some sort of plug-in wide event bus or pub-sub for this
      ['mousemove', 'touchmove',
        'mouseleave', 'click',
        'mousedown', 'touchstart',
        'mouseup', 'touchend'].forEach(item => {
          elements.progress.addEventListener(item, event => {
            const { thumbnails } = player;

            if (thumbnails && thumbnails.loaded) {
              switch (item) {
                case 'mousemove':
                case 'touchmove':
                  thumbnails.startMove(event);
                  break;
                case 'mouseleave':
                case 'click':
                  thumbnails.endMove(false, true);
                  break;
                case 'mousedown':
                case 'touchstart':
                  thumbnails.startMove(event);
                  break;
                case 'mouseup':
                case 'touchend':
                  thumbnails.endScrubbing(event);
                  break;
              }
            }
          });
        });
    }

    /**
     * Create HTML elements for image containers
     */
    render () {
      // Create HTML element: plyr__preview-thumbnail-container
      this.elements.thumb.container = document.createElement('div');
      this.elements.thumb.container.classList.add(this.player.config.classNames.previewThumbnails.thumbContainer);

      // Wrapper for the image for styling
      this.elements.thumb.imageContainer = document.createElement('div');
      this.elements.thumb.imageContainer.className = this.player.config.classNames.previewThumbnails.imageContainer;

      this.elements.thumb.container.appendChild(this.elements.thumb.imageContainer);

      // Create HTML element, parent+span: time text (e.g., 01:32:00)
      const timeContainer = document.createElement('div');
      timeContainer.className = this.player.config.classNames.previewThumbnails.timeContainer;

      this.elements.thumb.time = document.createElement('span');
      this.elements.thumb.time.textContent = '00:00';

      timeContainer.appendChild(this.elements.thumb.time);

      this.elements.thumb.container.appendChild(timeContainer);

      // Inject the whole thumb
      if (this.player.elements.progress instanceof Element) {
        this.player.elements.progress.appendChild(this.elements.thumb.container);
      }

      // Create HTML element: plyr__preview-scrubbing-container
      this.elements.scrubbing.container = document.createElement('div');
      this.elements.scrubbing.container.className = this.player.config.classNames.previewThumbnails.scrubbingContainer;

      this.player.elements.wrapper.appendChild(this.elements.scrubbing.container);
    }

    showImageAtCurrentTime () {
      if (this.mouseDown) {
        this.setScrubbingContainerSize();
      } else {
        this.setThumbContainerSizeAndPos();
      }

      // Find the desired thumbnail index
      // TODO: Handle a video longer than the thumbs where thumbNum is null
      let config = this.config;
      let interval = this.player.duration / config.pic_num;
      const thumbNum = Math.floor(this.seekTime / interval);
      // console.dir('thumbNum---'+thumbNum)

      let qualityIndex = Math.ceil((thumbNum + 1) / (config.col * config.row)) - 1;

      const hasThumb = thumbNum >= 0;

      // Show the thumb container if we're not scrubbing
      if (!this.mouseDown) {
        this.toggleThumbContainer(hasThumb);
      }

      // No matching thumb found
      if (!hasThumb) {
        return;
      }

      // Only proceed if either thumbnum or thumbfilename has changed
      if (thumbNum !== this.showingThumb) {
        this.showingThumb = thumbNum;
        this.loadImage(qualityIndex);
      }
    }

    // Show the image that's currently specified in this.showingThumb
    loadImage (qualityIndex = 0) {
      const thumbNum = this.showingThumb;
      const thumbUrl = this.config.urls[qualityIndex];

      if (!this.currentImageElement || this.currentImageElement.src !== thumbUrl) {
        // If we're already loading a previous image, remove its onload handler - we don't want it to load after this one
        // Only do this if not using sprites. Without sprites we really want to show as many images as possible, as a best-effort
        if (this.loadingImage) {
          this.loadingImage.onload = null;
        }

        // We're building and adding a new image. In other implementations of similar functionality (YouTube), background image
        // is instead used. But this causes issues with larger images in Firefox and Safari - switching between background
        // images causes a flicker. Putting a new image over the top does not
        const previewImage = new Image();
        previewImage.src = thumbUrl;
        previewImage.dataset.index = thumbNum;

        this.player.debug.log(`Loading image: ${thumbUrl}`);

        // For some reason, passing the named function directly causes it to execute immediately. So I've wrapped it in an anonymous function...
        previewImage.onload = () =>
          this.showImage(previewImage, qualityIndex, thumbNum, thumbUrl, true);
        this.loadingImage = previewImage;
        this.removeOldImages(previewImage);
      } else {
        // Update the existing image
        this.showImage(this.currentImageElement, qualityIndex, thumbNum, thumbUrl, false);
        this.currentImageElement.dataset.index = thumbNum;
        this.removeOldImages(this.currentImageElement);
      }
    }

    showImage (previewImage, qualityIndex, thumbNum, thumbFilename, newImage = true) {
      this.player.debug.log(
        `Showing thumb: ${thumbFilename}. num: ${thumbNum}. qual: ${qualityIndex}. newimg: ${newImage}`,
      );
      this.setImageSizeAndOffset(previewImage, thumbNum);

      if (newImage) {
        this.currentImageContainer.appendChild(previewImage);
        this.currentImageElement = previewImage;

        if (!this.loadedImages.includes(thumbFilename)) {
          this.loadedImages.push(thumbFilename);
        }
      }
    }

    // Remove all preview images that aren't the designated current image
    removeOldImages (currentImage) {
      // Get a list of all images, convert it from a DOM list to an array
      Array.from(this.currentImageContainer.children).forEach(image => {
        if (image.tagName.toLowerCase() !== 'img') {
          return;
        }

        const removeDelay = 500;

        if (image.dataset.index !== currentImage.dataset.index && !image.dataset.deleting) {
          // Wait 200ms, as the new image can take some time to show on certain browsers (even though it was downloaded before showing). This will prevent flicker, and show some generosity towards slower clients
          // First set attribute 'deleting' to prevent multi-handling of this on repeat firing of this function
          image.dataset.deleting = true;
          // This has to be set before the timeout - to prevent issues switching between hover and scrub
          const { currentImageContainer } = this;

          setTimeout(() => {
            currentImageContainer.removeChild(image);
            this.player.debug.log(`Removing thumb: ${image.dataset.filename}`);
          }, removeDelay);
        }
      });
    }

    get currentImageContainer () {
      if (this.mouseDown) {
        return this.elements.scrubbing.container;
      }

      return this.elements.thumb.imageContainer;
    }

    get thumbAspectRatio () {
      return this.config.width / this.config.height;
    }

    get thumbContainerHeight () {
      if (this.mouseDown) {
        // Can't use media.clientHeight - HTML5 video goes big and does black bars above and below
        return Math.floor(this.player.media.clientWidth / this.thumbAspectRatio);
      }

      return Math.floor(this.player.media.clientWidth / this.thumbAspectRatio / 4);
    }

    get currentImageElement () {
      if (this.mouseDown) {
        return this.currentScrubbingImageElement;
      }

      return this.currentThumbnailImageElement;
    }

    set currentImageElement (element) {
      if (this.mouseDown) {
        this.currentScrubbingImageElement = element;
      } else {
        this.currentThumbnailImageElement = element;
      }
    }

    toggleThumbContainer (toggle = false, clearShowing = false) {
      const className = this.player.config.classNames.previewThumbnails.thumbContainerShown;
      this.elements.thumb.container.classList.toggle(className, toggle);

      if (!toggle && clearShowing) {
        this.showingThumb = null;
        this.showingThumbFilename = null;
      }
    }

    toggleScrubbingContainer (toggle = false) {
      const className = this.player.config.classNames.previewThumbnails.scrubbingContainerShown;
      this.elements.scrubbing.container.classList.toggle(className, toggle);

      if (!toggle) {
        this.showingThumb = null;
        this.showingThumbFilename = null;
      }
    }

    determineContainerAutoSizing () {
      if (this.elements.thumb.imageContainer.clientHeight > 20) {
        // This will prevent auto sizing in this.setThumbContainerSizeAndPos()
        this.sizeSpecifiedInCSS = true;
      }
    }

    // Set the size to be about a quarter of the size of video. Unless option dynamicSize === false, in which case it needs to be set in CSS
    setThumbContainerSizeAndPos () {
      if (!this.sizeSpecifiedInCSS) {
        const thumbWidth = Math.floor(this.thumbContainerHeight * this.thumbAspectRatio);
        this.elements.thumb.imageContainer.style.height = `${this.thumbContainerHeight}px`;
        this.elements.thumb.imageContainer.style.width = `${thumbWidth}px`;
      }

      this.setThumbContainerPos();
    }

    setThumbContainerPos () {
      const seekbarRect = this.player.elements.progress.getBoundingClientRect();
      const plyrRect = this.player.elements.container.getBoundingClientRect();
      const { container } = this.elements.thumb;

      // Find the lowest and highest desired left-position, so we don't slide out the side of the video container
      const minVal = plyrRect.left - seekbarRect.left + 10;
      const maxVal = plyrRect.right - seekbarRect.left - container.clientWidth - 10;

      // Set preview container position to: mousepos, minus seekbar.left, minus half of previewContainer.clientWidth
      let previewPos = this.mousePosX - seekbarRect.left - container.clientWidth / 2;

      if (previewPos < minVal) {
        previewPos = minVal;
      }

      if (previewPos > maxVal) {
        previewPos = maxVal;
      }

      container.style.left = `${previewPos}px`;
    }

    // Can't use 100% width, in case the video is a different aspect ratio to the video container
    setScrubbingContainerSize () {
      this.elements.scrubbing.container.style.width = `${this.player.media.clientWidth}px`;
      // Can't use media.clientHeight - html5 video goes big and does black bars above and below
      this.elements.scrubbing.container.style.height = `${this.player.media.clientWidth / this.thumbAspectRatio}px`;
    }

    // Sprites need to be offset to the correct location
    setImageSizeAndOffset (previewImage, frame) {
      // Find difference between height and preview container height
      let config = this.config;
      let indexInPage = frame + 1 - (config.col * config.row) * (Math.ceil((frame + 1) / (config.col * config.row)) - 1)
      let tnaiRowIndex = Math.ceil(indexInPage / config.row) - 1
      let tnaiColIndex = indexInPage - tnaiRowIndex * config.row - 1
      // console.dir('indexinpage---'+indexInPage)
      // console.dir('tnaiRowIndex---'+tnaiRowIndex)
      // console.dir('tnaiColIndex---'+tnaiColIndex)

      const multiplier = this.thumbContainerHeight / config.height;

      previewImage.style.height = `${Math.floor(previewImage.naturalHeight * multiplier)}px`;
      previewImage.style.width = `${Math.floor(previewImage.naturalWidth * multiplier)}px`;
      previewImage.style.left = `-${tnaiColIndex * config.width * multiplier}px`;
      previewImage.style.top = `-${tnaiRowIndex * config.height * multiplier}px`;
    }
  }

  document.addEventListener('ready', event => {
    const curPlayer = event.detail.plyr;

    curPlayer.thumbnails = new Thumbnails(curPlayer);
  });
})(document);