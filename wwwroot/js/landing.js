﻿
//landing page.js

$(function () {
    $('.carousel-control').hide();

    $('.carousel').mouseenter(function () {
        $('.carousel-control').fadeIn();

    })

    $('.carousel').mouseleave(function () {
        $('.carousel-control').fadeOut();

    })
});