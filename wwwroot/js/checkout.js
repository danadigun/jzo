//checkout.js

$(function () {
    $('.checkout').click(function () {
       
        $('.checkout').html('please wait...')

        $.ajax({

            url: '/group/getCart',
            type: 'POST'

        }).done(function (callback) {

            if (callback.noOfItems == 0) {
                alert('no items in your cart');
            } else {
                $.ajax({
                    url: '../api/orders?cartId=' + callback.cartId,
                    type: 'POST'
                }).done(function (callback) {
                    //callback here
                    $('.checkout').html('Checkout')

                    //alert('Paystack URL: ' + callback.url)
                    //redirect to paystack payment url

                    $(location).attr('href', callback.url);

                }).fail(function (error) {
                    //error callbaack here
                    alert(error);
                })
            }

        }).fail(function (error) {

            console.log(error);

        })
        
    })

     
})
