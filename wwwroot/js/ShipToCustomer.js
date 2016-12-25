//ShipToCustomer.js

$(function () {
    //ship to customer
    $('.ship-to').click(function (e) {
        e.preventDefault();

        var referenceId = $(this).attr('data-reference')

        //busy button
        $(this).html("Processing shipping..")

        //dispay shipping address confirmation
        $('#shipping_' + referenceId).html(
           "<p class=\"alert alert-default\"><b><i>please confirm customer shipping details:<i><b></p>"+
           "<p class=\"well well_"+referenceId+"\"></p>"+
          "<p></p>"
        );

        //populate shipping details
        $.ajax({
            type: 'GET',
            url: '../Group/GetShippingDetails?orderReference=' + referenceId
        }).done(function (callback) {
            if (callback.error) {
                alert(callback.error);
            } else {
                $('.well_' + referenceId).html(callback.user)
            }
        }).fail(function (error) {
            alert('unable to proceed')
            console.log(error)
        })
        //proceed
        var proceed = $("<button class=\"proceed-ship\" data-ship=\""
                        + referenceId + " \">Proceed</button>")

                            .click(function () {

                                var ref = $(this).attr("data-ship");
                               

                            });

        //cancel
        var cancel = $("<button class=\"cancel-ship\" data-cancel=\""
                         + referenceId + " \">cancel</button>")

                            .click(function () {
                                var ref = $(this).attr("data-cancel");

                                $('#shipping_' + ref).html(" ");
                                $(".ship_"+ref).html("ship order to customer <i class=\"ion ion-chevron-right\"></i>")
                            })

        //append controls
        $('#shipping_' + referenceId).find("p:last").append(proceed);
        $('#shipping_' + referenceId).find("p:last").append(cancel);
    })

    
})