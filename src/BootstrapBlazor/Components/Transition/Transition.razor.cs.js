(function ($) {
  $.extend({
    bb_transition: function (el, obj) {
      $(el).on('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oAnimationEnd', function () {
        obj.invokeMethodAsync('TransitionAsync')
      });
    },
  });
})(jQuery);
