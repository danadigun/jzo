//Item.js file

$(function () {
    $('form[name=add-item]').validate({
        
        rules:{
            name: { required: true },
            description: { required: true },
            price: { required: true, digits: true },
            quantity: { required: true, digits: true }         
        },
        
        messages: {
            name: {
                required: 'Please you have to name your item'
            },
            description: {
                required:'Please describe this item briefly'
            },
            price: {
                required: 'You have to enter the price of this item',
                digits:'Only numbers are allowed here'
            },
            quantity: {
                required: 'You have to enter the quantity of this item',
                digits: 'Only numbers are allowed here'
            },
        },
        
        submitHandler: function (form) {
            addItem(form)
        }
    })

    function addItem(form) {

        $('.save-item').html('please wait..');

        var _data = $(form).serializeFormJSON();

        var item_data = {
            name: _data.name,
            price: _data.price,
            description: _data.description,
            ItemGroupId: _data.group_id,
        }

        addToGroup(item_data);
     }

    function addToGroup(data) {
   
        $.ajax({
            url: 'api/items',
            type: 'POST',
            data: data
        }).done(function (callback) {
            $('.save-item').html('Save changes');
            alert('successfully added new item')

        }).fail(function (error) {
            alert('unable to add new item');
            console.log(erro);
        });
       
    }
})