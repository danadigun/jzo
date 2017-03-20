//code for group.js
'use strict';

$(function () {

    start();

    var group_id = 0;
    $('form[name=add-group]').validate({

        rules: {
            name: { required: true },
            description: { required: true }
        },
        messages: {
            name: {
                required: 'you have to specify the name of this group'
            },
            description: {
                required: 'this group needs a good discription'
            }
        },
        submitHandler: function submitHandler(form) {
            addGroup(form);
        }

    });

    function addGroup(form) {

        $('button[name=save-group]').html("please wait ....");

        $.ajax({
            url: "api/ItemGroups",
            type: "POST",
            data: $(form).serialize()

        }).done(function (callback) {

            next();
            group_id = callback.id;
        }).fail(function (callback) {

            alert('unable to add group');
        });
    }

    function next() {
        $('.form-group input').prop('disabled', true);
        $('#my-dropzone').show();
        $('.save-changes').hide();
        $('.complete').show();
        $('.item-add').show();
        $('.drop-message').html("<div class =\"alert alert-info\">" + "<b>Step 1 Complete. </b>Next upload group thumbnail image to finish!" + "</div>");
        $('.complete').prop('disabled', true);
    }

    function start() {
        $('#my-dropzone').hide();
        $('.complete').hide();
        $('.item-add').hide();
    }

    $('.complete').click(function () {
        complete();
    });

    function complete() {
        $('.form-group input').val('');
        $('.form-group input').prop('disabled', false);

        //refresh
        $(location).attr("href", "/group");
    }

    //dropzone
    Dropzone.options.myDropzone = {

        url: "api/ItemGroupImage",
        maxFilesize: 1.5,
        maxfiles: 1,
        acceptedFiles: "image/*",
        uploadMultiple: false,

        init: function init() {

            this.on('sending', function (data, xhr, formData) {
                //get form data here
                formData.append("group_id", group_id);
            });
            this.on('addedfile', function (file) {
                if (this.files.length > 1) {
                    this.removeFile(this.files[0]);
                }
            });
            this.on('success', function (file, response) {
                //get success here
                $('.drop-message').html("<div class =\"alert alert-success\">" + "<b>Success! </b>You have successfully created a new group. Click <b>exit</b> to finish!" + "</div>" + "<div class=\"alert alert-info \">" + "You can simply replace image by uploading a new one" + "</div>");
                $('.complete').prop('disabled', false);
                $('.close-dismiss').hide();
            });
            this.on('error', function (file) {
                //get error here
            });
        }
    };

    //remove group
    $('.remove-group').click(function (e) {
        e.preventDefault();

        var groupId = $(this).attr('data-id');

        swal({
            title: "Remove a group/collection",
            text: "Are you sure you want to delete this entire collection with all its associated items?",
            type: "warning",
            showCancelButton: true,
            closeOnConfirm: false,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete it!",
            showLoaderOnConfirm: true
        }, function () {
            $.ajax({
                url: "../api/itemgroups/" + groupId,
                type: 'DELETE'
            }).done(function () {
                swal("success", "successfully removed group/collection with Id: " + groupId, "success");
                $('#group_' + groupId).fadeOut();
            }).fail(function (error) {
                swal("error", "unable to delete group with id:  " + groupId, "error");
                console.log(error);
            });
        });

        //alert('removing group: ' + groupId)
    });
});

