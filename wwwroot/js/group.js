//code for group.js
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
        submitHandler: function (form) {
            addGroup(form);
        }

    })

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

            alert('unable to add group')
        })
    }

    function next() {
        $('.form-group input').prop('disabled', true);
        $('.dropzone').show();
        $('.save-changes').hide();
        $('.complete').show();

                $('.drop-message')
                     .html(
                         "<div class =\"alert alert-info\">" +
                         "<b>Step 1 Complete. </b>Next upload group thumbnail image to finish!" +
                         "</div>" 
                        
                     )
                $('.complete').prop('disabled', true);

    }

    function start() {
        $('.dropzone').hide();
        $('.complete').hide();
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

        init: function () {
            //init here

            this.on('sending', function (data, xhr, formData) {
                //get form data here 
                formData.append("group_id", group_id);
            })
            this.on('addedfile', function (file) {
                if (this.files.length > 1) {
                    this.removeFile(this.files[0]);
                }
            })
            this.on('success', function (file, response) {
                //get success here
                $('.drop-message')
                    .html(
                        "<div class =\"alert alert-success\">" +
                        "<b>Success! </b>You have successfully created an item group. click <b>complete</b> to finish!" +
                        "</div>"+
                        "<div class=\"alert alert-info \">"+
                        "You can simply replace image by uploading a new one"+
                        "</div>"
                    )
                $('.complete').prop('disabled', false);
                $('.close-dismiss').hide();
            })

            this.on('error', function (file) {
                //get error here
            })
        }
    }

})