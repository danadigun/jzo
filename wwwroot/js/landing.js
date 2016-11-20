
//landing page.js

$(function () {

    //carousel
    $('.carousel').carousel({
        pause : 'hover'
    })
    $('.carousel-control').hide();

    $('.carousel').mouseenter(function () {
        $('.carousel-control').fadeIn();

    })

    $('.carousel').mouseleave(function () {
        $('.carousel-control').fadeOut();

    })

    //image overlay
    $('.wrap').mouseenter(function () {
        $('.image-overlay').show();

    }).mouseleave(function () {
        $('.image-overlay').hide();
    })
});