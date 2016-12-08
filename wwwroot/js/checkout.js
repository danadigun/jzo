//checkout.js

$(function () {
    //checkout
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

    //Remove item from cart
    $('.remove-item-cart').click(function (e) {
        e.preventDefault();

        var id = $(this).attr('data-id');

        $(this).html('removing...');

        $.ajax({
            url: '../api/SelectedItems/'+id,
            type: 'DELETE'

        }).done(function (callback) {

            $('#item_' + id).fadeOut();

            //update total
            $.ajax({
                url: '../group/getTotal',
                type: 'GET'

            }).done(function (callback) {

                $('.cart_total_cost').html(callback.total + " [NGN]");
                $('.count-message').html("You have " + callback.noOfItems + " item(s) in your shopping cart");
                $('#cart_count').html(callback.noOfItems);

            }).fail(function (error) {

                console.log(error);
            })
            
        })
    })
})
