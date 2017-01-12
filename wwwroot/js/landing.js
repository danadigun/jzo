
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

    //send message 
    $('form[name=send-message]').validate({

        rules: {
            email: { required: true },
            phone: { required: true, digits:true },
            message: { required: true },
        },

        messages: {
            phone: {
                digits: 'Only digits are allowed here',
                required: 'we need your phone number pls',
            }
        },

        submitHandler: function (form) {
            //alert('yey!');
            //TODO: Send Message Via Infobip SMS Gateway
            $('.drop-message').html('<p class=\"alert alert-success\"> Successfully sent message </p>')
        }
    })

    
});