'use strict';

$(function () {
    $('form[name=CustomOrderForm]').validate({

        rules: {
            neck: { required: true },
            shoulder: { required: true },
            mid: { required: true },
            chest: { required: true },
            waist: { required: true },
            longSleeve: { required: true },
            shortSleeve: { required: true },
            forearm: { required: true },
            wrist: { required: true },
            bicep: { required: true },
            bodyLength: { required: true },
            kaftanLength: { required: true },
            trouserWaist: { required: true },
            hip: { required: true },
            thigh: { required: true },
            knee: { required: true },
            feet: { required: true },
            trouserLength: { required: true }

        },

        messages: {

            neck: { required: 'neck  is required' },
            shoulder: { required: 'shoulder is required' },
            mid: { required: 'mid field is required' },
            chest: { required: 'chest is required' },
            waist: { required: 'waist is required' },
            longSleeve: { required: 'Long Sleeve is required' },
            shortSleeve: { required: 'short Sleeve is required' },
            forearm: { required: 'forearm is required' },
            wrist: { required: 'wrist is required' },
            bicep: { required: 'bicep is required' },
            bodyLength: { required: 'body Length is required' },
            kaftanLength: { required: 'kaftan Length is required' },
            trouserWaist: { required: 'This field is required' },
            hip: { required: 'hip field is required' },
            thigh: { required: 'thigh field is required' },
            knee: { required: 'knee is required' },
            feet: { required: 'feet is required' },
            trouserLength: { required: 'Trouser Length is required' }
        }
    });
    $('.custom-order-form').hide();

    //customize order
    $('.customize-order').click(function () {
        // swal("This feature is comming soon...we're working on it!");
        loadCustomOrder();
    });

    //regular order
    $('.regular-order-button').click(function () {
        loadRegularOrder();
    });

    /**
     * this loads custom order form
     */
    function loadCustomOrder() {

        //TODO: if all forms are filled show submit botton
        if ($('form[name=CustomOrderForm]').valid()) {

            $('.regular-order-form').hide();
            $('.custom-order-form').fadeIn();
        } else {
            $('.regular-order-form').hide();
            $('.custom-order-form').fadeIn();

            //hide submit button
            $('.submit_custom_order').hide();
        }
    }

    /**
     * this loads regular order form
     */
    function loadRegularOrder() {
        $('.regular-order-form').fadeIn();
        $('.custom-order-form').hide();
    }

    /*
     * custom  order form wizard
     */
    $('.submit_custom_order').hide();
    var accordions = ['#NeckChest', '#WaistSeat', '#shirtShoulder', '#armsWrist', '#hipInSeam', '#sleaves'];

    var wizardService = {

        open: function open(id) {
            $(id).collapse({
                toggle: true
            });
        },

        close: function close(id) {
            $(id).collapse({
                toggle: false
            });

            //$('a[data-target='+'\"'+id+'\"'+']').prop("disabled", true);
        },

        openOneCloseAll: function openOneCloseAll(indexToOpen) {
            //open the selected index
            var id = accordions[indexToOpen];
            //wizardService.open(id);
            $(id).collapse('show');

            //close others
            for (var i = 0; i < accordions.length; i++) {
                if (i !== indexToOpen) {
                    $(accordions[i]).collapse('hide');
                    wizardService.close(accordions[i]);
                }
            }
        },

        unlock: function unlock(indexToUnlock) {
            var id = accordions[indexToUnlock];
            $('a[data-target=' + '\"' + id + '\"' + ']').prop("disabled", false);
        },

        edit: function edit(index) {
            if (index === 0) {
                //unlock
                this.unlock(index);

                //show
                //open the selected index
                var id = accordions[indexToOpen];
                //wizardService.open(id);
                $(id).collapse('show');
            }
        }
    };

    //step 1
    wizardService.openOneCloseAll(0);
    $('.next_1').click(function (e) {
        e.preventDefault();
        var empty = $(this).parent().find("input").filter(function () {
            return this.value === "";
        });
        if (empty.length) {
            //At least one input is empty
            //alert('empty')
            $('.title_1').html("Neck & Shoulder  <i class=\"ion ion-close-round\" style=\"float:right; color:red\"></i><br>" + "<i style=\"padding-top:5px; color:red; text-align:center\">One or more fields are still empty</i>");
        } else {

            //fields are valid
            $('.title_1').html("Neck & Shoulder  <i class=\"ion ion-checkmark-round\" style=\"float:right; color:green\"></i><br>");

            //move to step 2 and unlock validated and completed sections
            wizardService.openOneCloseAll(1);
            wizardService.unlock(0);
        }
    });

    //step 2
    $('.next_2').click(function (e) {
        e.preventDefault();
        var empty = $(this).parent().find("input").filter(function () {
            return this.value === "";
        });
        if (empty.length) {
            //At least one input is empty
            //alert('empty')
            $('.title_2').html("Chest & Mid  <i class=\"ion ion-close-round\" style=\"float:right; color:red\"></i><br>" + "<i style=\"padding-top:5px; color:red; text-align:center\">One or more fields are still empty</i>");
        } else {

            //fields are valid
            $('.title_2').html("Chest & Mid  <i class=\"ion ion-checkmark-round\" style=\"float:right; color:green\"></i><br>");

            //move to step 3 and unlock validated and completed sections
            wizardService.openOneCloseAll(2);
            wizardService.unlock(1);
        }
    });

    //step 3
    $('.next_3').click(function (e) {
        e.preventDefault();
        var empty = $(this).parent().find("input").filter(function () {
            return this.value === "";
        });
        if (empty.length) {
            //At least one input is empty
            //alert('empty')
            $('.title_3').html("Waist & Arms  <i class=\"ion ion-close-round\" style=\"float:right; color:red\"></i><br>" + "<i style=\"padding-top:5px; color:red; text-align:center\">One or more fields are still empty</i>");
        } else {

            //fields are valid
            $('.title_3').html("Waist & Arms  <i class=\"ion ion-checkmark-round\" style=\"float:right; color:green\"></i><br>");

            //move to step 4 and unlock validated and completed sections
            wizardService.openOneCloseAll(3);
            wizardService.unlock(2);
        }
    });

    //step 4
    $('.next_4').click(function (e) {
        e.preventDefault();
        var empty = $(this).parent().find("input").filter(function () {
            return this.value === "";
        });
        if (empty.length) {
            //At least one input is empty
            //alert('empty')
            $('.title_4').html("Length <i class=\"ion ion-close-round\" style=\"float:right; color:red\"></i><br>" + "<i style=\"padding-top:5px; color:red; text-align:center\">One or more fields are still empty</i>");
        } else {

            //fields are valid
            $('.title_4').html("Length  <i class=\"ion ion-checkmark-round\" style=\"float:right; color:green\"></i><br>");

            //move to step 4 and unlock validated and completed sections
            wizardService.openOneCloseAll(4);
            wizardService.unlock(3);
        }
    });

    //step 5
    $('.next_5').click(function (e) {
        e.preventDefault();
        var empty = $(this).parent().find("input").filter(function () {
            return this.value === "";
        });
        if (empty.length) {
            //At least one input is empty
            //alert('empty')
            $('.title_5').html("Waist, Hip & Thigh  <i class=\"ion ion-close-round\" style=\"float:right; color:red\"></i><br>" + "<i style=\"padding-top:5px; color:red; text-align:center\">One or more fields are still empty</i>");
        } else {

            //fields are valid
            $('.title_5').html("Waist, Hip & Thigh  <i class=\"ion ion-checkmark-round\" style=\"float:right; color:green\"></i><br>");

            //move to step 4 and unlock validated and completed sections
            wizardService.openOneCloseAll(5);
            wizardService.unlock(4);
        }
    });

    //step 6
    $('.next_6').click(function (e) {
        e.preventDefault();
        var empty = $(this).parent().find("input").filter(function () {
            return this.value === "";
        });
        if (empty.length) {
            //At least one input is empty
            //alert('empty')
            $('.title_6').html("Knee, feet & trouser Length  <i class=\"ion ion-close-round\" style=\"float:right; color:red\"></i><br>" + "<i style=\"padding-top:5px; color:red; text-align:center\">One or more fields are still empty</i>");
        } else {

            //fields are valid
            $('.title_6').html("Knee, feet & trouser Length  <i class=\"ion ion-checkmark-round\" style=\"float:right; color:green\"></i><br>");

            //move to step 4 and unlock validated and completed sections
            $(accordions[5]).collapse('hide');

            //show submit button
            $('.submit_custom_order').fadeIn();
        }
    });

    //submit form
    $('form[name=CustomOrderForm]').submit(function (e) {
        e.preventDefault();

        //TODO: API post to create custom order
        var CustomOrder = {
            ItemId: $('input[name=itemId]').val(),
            ItemName: $('input[name=itemName]').val(),
            qty: $('input[name=qty]').val(),
            neck: $('input[name=neck]').val(),
            shoulder: $('input[name=shoulder]').val(),
            mid: $('input[name=mid]').val(),
            chest: $('input[name=chest]').val(),
            waist: $('input[name=waist]').val(),
            longSleeve: $('input[name=longSleeve]').val(),
            shortSleeve: $('input[name=shortSleeve]').val(),
            forearm: $('input[name=forearm]').val(),
            wrist: $('input[name=wrist]').val(),
            bicep: $('input[name=bicep]').val(),
            bodyLength: $('input[name=bodyLength]').val(),
            kaftanLength: $('input[name=kaftanLength]').val(),
            trouserWaist: $('input[name=trouserWaist]').val(),
            hip: $('input[name=hip]').val(),
            thigh: $('input[name=thigh]').val(),
            knee: $('input[name=knee]').val(),
            feet: $('input[name=feet]').val(),
            trouserLength: $('input[name=trouserLength]').val()

        };
        swal('Order = ' + CustomOrder.ItemName + ', qty=' + CustomOrder.qty + 'pcs');
    });
});

