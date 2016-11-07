//Item.js file

$(function () {
    $('form[name=add-item]').validate({
        
        rules:{
            name: { required: true },
            description: { required: true },
            price: { required: true, digits: true },
            quantity: { required: true, digits: true },
            group_id: { required: true },
            
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
            group_id: {
                required: 'please select the group this item belongs to'
            }
        },
        
        submitHandler: function (form) {
            addItem(form)
        }
    })

    var item_id = 0;

    item_start();

    function addItem(form) {

        $('.save-item').html('please wait..');

        var _data = $(form).serializeFormJSON();

        var item_data = {
            name: _data.name,
            price: _data.price,
            description: _data.description,
            ItemGroupId: _data.group_id,
            quantity : _data.quantity,
        }

        addToGroup(item_data);
     }

    function addToGroup(data) {
   
        $.ajax({
            url: 'api/items',
            type: 'POST',
            data: data
        }).done(function (callback) {
            item_id = callback.id;
            item_next();

        }).fail(function (error) {
            alert('unable to add new item');
            console.log(erro);
        });
       
    }

    function item_next() {
        $('.form-group input').prop('disabled', true);
        $('.form-group select').prop('disabled', true);
        $('#item-dropzone').show();
        $('.item_save-changes').hide();
        $('.item_complete').show();
        //$('.item-add').show();
        $('.item-drop-message')
             .html(
                 "<div class =\"alert alert-info\">" +
                 "<b>Step 1 Complete. </b>Next upload item thumbnail image to finish!" +
                 "</div>"

             )
        $('.complete').prop('disabled', true);

    }

    function item_start() {
        $('#item-dropzone').hide();
        $('.item_complete').hide();
        $('.item-add').hide();
    }

    $('.item_complete').click(function () {
        item_complete();
    });

    function item_complete() {
        $('.form-group input').val('');
        $('.form-group input').prop('disabled', false);
        $('.form-group select').prop('disabled', false);

        //refresh
        $(location).attr("href", "/group");
    }

    Dropzone.options.itemDropzone = {
        url: 'api/ItemImage',
        maxFilesize: 1.5,
        maxfiles: 1,
        acceptedFiles: "image/*",
        uploadMultiple: false,


        init: function () {

            this.on('sending', function (data, xhr, formData) {
                //handle before sending
                formData.append("item_id", item_id);
            });

            this.on('addedfile', function (file) {
                if (this.files.length > 1) {
                    this.removeFile(this.files[0]);
                }
            })

            this.on('success', function (file, response) {
                //on complete
                $('.item-drop-message')
                   .html(
                       "<div class =\"alert alert-success\">" +
                       "<b>Success! </b>You have successfully created a item. Click <b>finish</b> to finish!" +
                       "</div>" +
                       "<div class=\"alert alert-info \">" +
                       "You can simply replace image by uploading a new one" +
                       "</div>"
                   )
                $('.item_complete').prop('disabled', false);
                $('.item_close-dismiss').hide();
            })

            this.on('error', function (file, response) {
                //if error
            })
        }

        
    }
})