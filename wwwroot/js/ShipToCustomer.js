//ShipToCustomer.js

$(function () {
    //ship to customer
    $('.ship-to').click(function (e) {
        e.preventDefault();

        var referenceId = $(this).attr('data-reference')

        //busy button
        $(this).html("please wait..")

        //dispay shipping address confirmation
        $('#shipping_' + referenceId).html(
           "<p class=\"alert alert-default\"><b><i>please confirm customer shipping details:<i><b></p>"
           //"<p class=\"well well_"+referenceId+"\"></p>"+
          //"<p></p>"
        );

        //populate shipping details
        $.ajax({
            type: 'GET',
            url: '../Group/GetShippingDetails?orderReference=' + referenceId
        }).done(function (callback) {
            if (callback.error) {
                alert(callback.error);
            } else {

                var ship_address = $("<p class=\"well-ref well_" + referenceId + "\">" +
                    "<b><i class=\"ion ion-ios-browsers-outline\"></i>&nbsp;Customer Shipping address:</b><br><br>" +
                    "<b><i class=\"ion ion-android-drafts\"></i>&nbsp;&nbsp;</b>" + callback.user.mailingAddress +
                    "<br><b><i class=\"ion ion-android-pin\"></i>&nbsp;&nbsp;</b>" + callback.user.city +
                    ", " + callback.user.country +
                    " </p>")

                $('#shipping_' + referenceId).append(ship_address);

                //proceed
                var proceed = $("<button class=\"btn btn-primary proceed-ship\" data-ship=\""
                                + referenceId + " \">Proceed</button>")
                                    .click(function () {

                                        //get ref no.
                                        var ref = $(this).attr("data-ship");

                                        //process shipping for ref no.
                                        ship(ref);

                                    });

                //cancel
                var cancel = $(" <button class=\"btn btn-default cancel-ship\" data-cancel=\""
                                 + referenceId + " \">cancel</button>")
                                    .click(function () {
                                        var ref = $(this).attr("data-cancel");

                                        $('#shipping_' + ref).html(" ");
                                        $(".ship_" + ref).html("ship order to customer <i class=\"ion ion-chevron-right\"></i>")
                                    })

                //append controls
                var control_buttons = $("<p><p>");

                $('#shipping_' + referenceId).append(control_buttons);

                $('#shipping_' + referenceId).find("p:last").append(proceed);
                $('#shipping_' + referenceId).find("p:last").append(cancel);

                $(this).html("Processing Shipping..")
            }
        }).fail(function (error) {
            alert('unable to proceed')
            console.log(error)
        })


    })

    //proceed to ship function
    function ship(ref) {
        swal({
            title: "Ship process",
            text: "Are you sure you want to process shipping for Order "+ref,
            type: "info",
            showCancelButton: true,
            closeOnConfirm: false,
            showLoaderOnConfirm: true,
            confirmButtonColor: "#0047AB",
            confirmButtonText: "Yes, Proceed!",
            cancelButtonText: "cancel",
        },
        function () {
            //setTimeout(function () {
            //    swal("Order shipping processed");
            //}, 2000);
            $.ajax({
                type: 'POST',
                url: '../Group/ship?refId='+ref
            }).done(function (response) {
                if (response.status == true) {
                    swal("Order shipping processed");
                } else {
                   alert("This order does not exist")
                }
            }).fail(function (error) {
                alert("unable to processes shipping")
            })
        });
    }
})