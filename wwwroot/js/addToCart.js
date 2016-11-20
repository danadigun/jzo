
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
            $.ajax({
                url: '/group/addToCart',
                type : 'POST',
                data : $(form).serialize()
            }).done(function (callback) {
                alert('added to cart. cartId: ' + callback.cartItem.cartId);
            }).fail(function (error) {
                alert('unable to add to cart');
                console.log(error);
            })
        }
    })
})