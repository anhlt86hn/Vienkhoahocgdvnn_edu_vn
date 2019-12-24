$(document).ready(function () {
    //Default Configuration: This is the default configuration. 
    $(".default .carousel").jCarouselLite({  
        btnNext: ".default .next",
        btnPrev: ".default .prev"
    });

    //Auto - Scroll:  Automatically scrolls content based on the time period given in the auto option. 
    $(".auto .carousel").jCarouselLite({
        auto: 800,
        speed: 1000
    });

 //Scroll more items: You can scroll more than one item (the default), using this option.  
    $(".scrollMore .carousel").jCarouselLite({
        btnNext: ".scrollMore .next",
        btnPrev: ".scrollMore .prev",
        scroll: 2
    });

    //Mouse wheel navigation:
    //   Note: You need the Mouse Wheel plugin for this to work. 
    $(".mouseWheel .carousel").jCarouselLite({
        mouseWheel: true
    });

    //Mouse wheel and Navigation buttons together.
    //Note: You need the Mouse Wheel plugin for this to work. 
    $(".mouseWheelButtons .carousel").jCarouselLite({
        btnNext: ".mouseWheelButtons .next",
        btnPrev: ".mouseWheelButtons .prev",
        mouseWheel: true
    });

    //Custom Animation. Easing Effects.
    //    Note: You need the easing plugin for this to work. 
    $(".bounceout .carousel").jCarouselLite({
            btnNext: ".bounceout .next",
            btnPrev: ".bounceout .prev",
            easing: "easeOutBounce",
                speed: 1000
    });

    //Custom Animation. Speed Control.
    $(".slower .carousel").jCarouselLite({
        btnNext: ".slower .next",
        btnPrev: ".slower .prev",
        speed: 800
    });

    //Custom Widget.
    //This opportunity to use another easing effect - "backout" 
    $(".widget .carousel").jCarouselLite({
        btnNext: ".widget .next",
        btnPrev: ".widget .prev",
        speed: 800,
        easing: "backout"
    });

    //$(".widget img").click(function () {
        $(".widget .mid img").attr("src", $(this).attr("src"));
    });

    //More or Less
    $(".moreItems .carousel").jCarouselLite({
        btnNext: ".moreItems .next",
        btnPrev: ".moreItems .prev",
        visible: 2
    });

    //Any Content - Not just Images
    $(".nonImageContent .carousel").jCarouselLite({
        btnNext: ".nonImageContent .next",
        btnPrev: ".nonImageContent .prev"
    });

    //Mixed Content - Not just Images
    $(".mixedContent .carousel").jCarouselLite({
        btnNext: ".mixedContent .next",
        btnPrev: ".mixedContent .prev"
    });

    //Vertical
    $(".vertical .carousel").jCarouselLite({
        btnNext: ".vertical .next",
        btnPrev: ".vertical .prev",
        vertical: true
    });

    //External Controls
    //Buttons, buttons and more buttons. Feel free... 
    $(".externalControl .carousel").jCarouselLite({
        visible: 3,
        start: 0,
        btnNext: ".externalControl .next",
        btnPrev: ".externalControl .prev",
        btnGo:
        [".externalControl .1", ".externalControl .2",
        ".externalControl .3", ".externalControl .4",
        ".externalControl .5", ".externalControl .6",
        ".externalControl .7", ".externalControl .8",
        ".externalControl .9", ".externalControl .10",
        ".externalControl .11", ".externalControl .12"]
    });

    //Callbacks too!
    $(".callback .carousel").jCarouselLite({
        btnNext: ".callback .next",
        btnPrev: ".callback .prev",
        beforeStart: function(a) {
            alert("Before animation starts:" + a);
        },
        afterEnd: function(a) {
            alert("After animation ends:" + a);
        }
    });

    //Slider
    $(".imageSlider .carousel").jCarouselLite({
        btnNext: ".imageSlider .next",
        btnPrev: ".imageSlider .prev",
        visible: 1,
        speed: 500
    });

    //Image Slider with external controls.
    $(".imageSliderExt .carousel").jCarouselLite({
        btnNext: ".imageSliderExt .next",
        btnPrev: ".imageSliderExt .prev",
        visible: 1,
        btnGo:
        [".imageSliderExt .1", ".imageSliderExt .2",
        ".imageSliderExt .3", ".imageSliderExt .4",
        ".imageSliderExt .5", ".imageSliderExt .6",
        ".imageSliderExt .7", ".imageSliderExt .8",
        ".imageSliderExt .9", ".imageSliderExt .10",
        ".imageSliderExt .11", ".imageSliderExt .12"]
    });

    //Fraction Configuration
    $(".fraction .carousel").jCarouselLite({
        btnNext: ".fraction .next",
        btnPrev: ".fractions .prev",
        visible: 2.5,
        circular: false
    });

});
