//Item.js file

$(function () {

    /**
     * Add new item using the create-wizard
     */
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

    //next-wizard
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

    //complete-wizard
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
    
    //dropzone for Add Item
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
                       "<b>Success! </b>You have successfully created an item. Click <b>finish</b> to finish!" +
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


    /**
     * remove item
     */
    $('.remove-item').click(function (e) {
        e.preventDefault();

        var itemId = $(this).attr('data-id');

        swal(
         {
             title: "Remove an item",
             text: "Are you sure you want to delete this item from the store?",
             type: "warning",
             showCancelButton: true,
             closeOnConfirm: false,
             confirmButtonColor: "#DD6B55",
             confirmButtonText: "Yes, delete it!",
             showLoaderOnConfirm: true,
         },
        function () {
            $.ajax({
                url: "../api/items/" + itemId,
                type: 'DELETE'
            }).done(function () {
                swal("success", "successfully removed item: " + itemId, "success");
                $('#tr_item_' + itemId).fadeOut();
            }).fail(function (error) {
                swal("error", "unable to delete item:  " + itemId, "error");
                console.log(error);
            })
        }
       );

    })



    /**
     * Edit Item using the the Edit/Update wizard
     */
    
    //Show Update modal and populate form
    var editing_item = 0;
    $('#EditModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var recipient = button.data('whatever') // Extract info from data-* attributes -> recipient = Id
        editing_item = recipient;

        //start wizard
        edit_item_start();

        //populate modal form with item properties selected 
        var modal = $(this)
        populateEditForm(recipient, modal);

        //validate modal form and update
        $('form[name=update-item]').submit(function (e) { e.preventDefault() }).validate({

            rules: {
                edit_name: { required: true },
                edit_description: { required: true },
                edit_price: { required: true, digits: true },
                edit_quantity: { required: true, digits: true },
                edit_group_id: { required: true },

            },

            messages: {
                name: {
                    required: 'Please you have to name your item'
                },
                description: {
                    required: 'Please describe this item briefly'
                },
                price: {
                    required: 'You have to enter the price of this item',
                    digits: 'Only numbers are allowed here'
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
               
                updateItem(form, recipient)
            }
        })

    });

    //Get current data of item and populate form
    function populateEditForm(recipient, modal) {
        $.ajax({
            url: 'api/Items/' + recipient,
            type: 'GET'
        }).done(function (data) {

            //update modal form with data
            modal.find('.modal-title').text('Update ' + data.name)
           // modal.find('.modal-body select[name=edit_group_id]').val(data.ItemGroupId.toString())
            modal.find('.modal-body input[name=edit_name]').val(data.name)
            modal.find('.modal-body input[name=edit_description]').val(data.description)
            modal.find('.modal-body input[name=edit_price]').val(data.price)
            modal.find('.modal-body input[name=edit_quantity]').val(data.quantity)
            modal.find('.modal-body input[name=edit_group_id]').val(data.itemGroupId)
            modal.find('.modal-body input[name=edit_img_url]').val(data.image_url)

        }).fail(function (error) {
            alert(error);
        })
    }

    //updates Item
    function updateItem(form, recipient) {

        var data = $(form).serializeFormJSON(); //from jsonSerialize.js

        var _data = {
            Id: recipient,
            name: data.edit_name,
            description : data.edit_description,
            price: data.edit_price,
            quantity: data.edit_quantity,
            ItemGroupId: $('.modal-body input[name=edit_group_id]').val(),
            image_url: $('.modal-body input[name=edit_img_url]').val(),
            
        }
        
        $.ajax({
            url: 'api/Items/' + recipient,
            type:'PUT',
            data:JSON.stringify(_data),
            contentType : 'application/json',
           
        }).done(function () {
            edit_item_next();
        })
    }

    //start edit wizard
    function edit_item_start() {
        $('#edit-item-dropzone').hide();
        $('.item_update_complete').hide();
        //$('.item-add').hide();
    }

    //next edit-wizard
    function edit_item_next() {
        $('form[name=update-item] .form-group input').prop('disabled', true);
        $('form[name=update-item] .form-group select').prop('disabled', true);
        $('#edit-item-dropzone').show();
        $('form[name=update-item].item_update_changes').hide();
        $('form[name=update-item].item_update_complete').show();
        //$('.item-add').show();
        $('form[name=update-item] .edit-item-drop-message')
             .html(
                 "<div class =\"alert alert-info\">" +
                 "<b>Step 1 Complete. </b>Next upload item thumbnail image to finish!" +
                 "</div>"

             )
        $('form[name=update-item] .item_update_complete').prop('disabled', true);

    }

    //complete-wizard
    $('.item_update_complete').click(function () {
        item_complete();
    });

    //dropzone for Update Item
    Dropzone.options.editItemDropzone = {
        url: 'api/ItemImage',
        maxFilesize: 1.5,
        maxfiles: 1,
        acceptedFiles: "image/*",
        uploadMultiple: false,


        init: function () {

            this.on('sending', function (data, xhr, formData) {
                //handle before sending
                formData.append("item_id", editing_item);
            });

            this.on('addedfile', function (file) {
                if (this.files.length > 1) {
                    this.removeFile(this.files[0]);
                }
            })

            this.on('success', function (file, response) {
                //on complete
                $('.edit-item-drop-message')
                   .html(
                       "<div class =\"alert alert-success\">" +
                       "<b>Success! </b>You have successfully created an item. Click <b>finish</b> to finish!" +
                       "</div>" +
                       "<div class=\"alert alert-info \">" +
                       "You can simply replace image by uploading a new one" +
                       "</div>"
                   )
                $('.item_update_complete').prop('disabled', false);
                $('.item_update_changes').hide();
                $('.item_update_complete').fadeIn();
                $('.item_close-dismiss').hide();
            })

            this.on('error', function (file, response) {
                //if error
            })
        }


    }

})