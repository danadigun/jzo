//code for group.js
$(function () {
    $('form[name=add-group]').validate({
        
        rules: {
            name: { required: true },           
            description: { required: true }
        },
        messages : {
            name: {
                required: 'you have to specify the name of this group'
            },
            description: {
                required : 'this group needs a good discription'
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

            $('button[name=save-group]').html("Save Changes");
            alert('successfully added group')

        }).fail(function (callback) {

            alert('unable to add group')
        })
    }

    //dropzone
    
})