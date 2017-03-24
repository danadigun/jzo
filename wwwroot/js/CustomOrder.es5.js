﻿'use strict';

$(function () {
    $('form[name=CustomOrderForm]').validate({

        rules: {
            neck: { required: true },
            chest: { required: true }
        },

        messages: {
            neck: {
                required: 'please enter the neck measurement in inches'
            },

            chest: {
                required: 'please enter the chest measurement in inches'
            }
        }
    });
    $('.custom-order-form').hide();

    //customize order
    $('.customize-order').click(function () {
        //swal("This feature is comming soon...we're working on it!");
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
        $('.regular-order-form').hide();
        $('.custom-order-form').fadeIn();
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
});
