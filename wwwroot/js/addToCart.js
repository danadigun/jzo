
//addToCart.js
$(function () {
    
    $('form[name=add-to-cart]').validate({
        
        rules: {
            size: { required: true },
            quantity: { required: true, digits: true },
            
        },

        messages: {
            size: {
                required:"Select a required size please"
            },
            quantity: {
                required: "Enter quantity please",
                digits:"Numbers only please"
            }
        },

        submitHandler: function (form) {
            $('.add-to-cart').html("Please wait....")
            $.ajax({
                url: '/group/addToCart',
                type : 'POST',
                data: $(form).serialize()

            }).done(function (callback) {
                alert('item has successfully been added to cart');
                $('.add-to-cart').html("Add to cart")

                $(location).attr('href', '/store');

            }).fail(function (error) {

                alert('unable to add to cart');
                console.log(error);
            })
        }
    })

    //on page load update cart_item count

    $.ajax({

        url: '/group/getCart',
        type: 'POST'

    }).done(function (callback) {

        $('#cart_count').html(callback.noOfItems);
        $('#cart_count_2').html(callback.noOfItems);

        console.log(callback);

    }).fail(function (error) {

        console.log(error);

    })

})