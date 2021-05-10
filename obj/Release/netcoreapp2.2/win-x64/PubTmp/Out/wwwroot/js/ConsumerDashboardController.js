
var BRKDateId = 0;
var LnDateId = 0;
var TBRKDateId = 0;
var BRKCount = 1;
var TBRKCount = 1;
var LnCount = 1;


function createMealDateInput(mealId,
    mealDateId,
    mealCheckBoxId,
    mealCheckBoxSpace,
    mealDateButtonId,
    mealDateButtonSpace,
    moreButtonId,
    moreButtonSpace,
    mealInitButton) {
    var mealDateCount = 0;
    var ID = "";
    var idPrefix = "";
    if (mealId == 1) {
        BRKDateId++;
        mealDateCount = BRKDateId;
        ID = 'Breakfast_' + mealDateCount;
        idPrefix = 'Breakfast_';
    }
    else if (mealId == 2) {
        LnDateId++;
        mealDateCount = LnDateId;
        ID = 'Lunch_' + mealDateCount;
        idPrefix = 'Lunch_';
    }
    else {
        TBRKDateId++;
        mealDateCount = TBRKDateId;
        ID = 'TBreak_' + mealDateCount;
        idPrefix = 'TBreak_';
    }
    var prevMealDateCount = mealDateCount - 1;
    if ((prevMealDateCount > 0
        && document.getElementById(idPrefix + prevMealDateCount).value.toString().length > 0)
        || prevMealDateCount == 0) {

        var input = document.createElement("input");
        input.setAttribute('type', 'text');
        input.setAttribute('id', ID);
        input.setAttribute('class', 'form-control col-sm-10');

        var childInpDiv = document.createElement("div");
        childInpDiv.setAttribute('id', mealDateId + mealDateCount);
        //if (mealDateCount > 1) {
        //    childInpDiv.style.paddingTop = "10px";
        //}
        var inpdivParent = document.getElementById(mealDateId);
        inpdivParent.appendChild(childInpDiv);
        //input.setAttribute('onchange', createBRKDateInput2())
        //input.onchange = function () { createMealDateInput(mealId, mealDateId, mealCheckBoxId, mealCheckBoxSpace, mealDateButtonId, mealDateButtonSpace); };
        var parent = document.getElementById(mealDateId + mealDateCount);
        parent.appendChild(input);
    }
    else {
        updateDateCount(mealId);
        alert("First fill up the empty date field");
        return;

    }
    

    if (mealDateCount > 1) {
        document.getElementById(moreButtonId).remove();

    }
    $("#" + mealInitButton).hide();
    var moreBtn = document.createElement("button");
    moreBtn.setAttribute('id', moreButtonId);
    moreBtn.setAttribute('name', moreButtonId);
    moreBtn.setAttribute('class', 'btn btn');
    moreBtn.textContent = 'Add More';
    moreBtn.onclick = function () {

            createMealDateInput(mealId, mealDateId, mealCheckBoxId,
            mealCheckBoxSpace, mealDateButtonId, mealDateButtonSpace,
            moreButtonId, moreButtonSpace, mealInitButton);
    };
    var childDiv = document.createElement("div");
    childDiv.setAttribute('id', moreButtonSpace + mealDateCount);
    if (mealDateCount > 1) {
        childDiv.style.paddingTop = "60px";
    }
    var divParent = document.getElementById(moreButtonSpace);
    divParent.appendChild(childDiv);

    var btnParent = document.getElementById(moreButtonSpace + mealDateCount);
    btnParent.appendChild(moreBtn);

    $("#"+ID).datepicker({
        dateFormat: "dd-mm-yy"
        , autoclose: true,

        minDate: 0 

    });
    if (mealDateCount == 1) {
        $("#" + mealCheckBoxSpace).append('<label for="name">On &nbsp;&nbsp;</label>');

        var input2 = document.createElement("input");
        input2.setAttribute('type', 'checkbox');
        input2.setAttribute('id', mealCheckBoxId);
        input2.setAttribute('name', mealCheckBoxId);
        input2.setAttribute('class', 'checkBoxClass');
        var parent2 = document.getElementById(mealCheckBoxSpace);
        parent2.appendChild(input2);
        //$("#" + mealCheckBoxSpace).append('<label for="name">Off</label>');

        document.getElementById(mealCheckBoxId).checked = false;

        var input3 = document.createElement("button");
        input3.setAttribute('id', mealDateButtonId);
        input3.setAttribute('name', mealDateButtonId);
        input3.setAttribute('class', 'btn btn');
        input3.textContent = 'Submit';
        input3.onclick = function () { submitMealDate(mealId); };

        var parent3 = document.getElementById(mealDateButtonSpace);
        parent3.appendChild(input3);

        //$("#" + mealCheckBoxSpace).append('<div class="row" style="padding-top:10px;">Check on in case of active meal.</div>');

    }

}
function createBRKDateInput2() {
    BRKDateId++;
    var ID = 'Breakfast_' + BRKDateId;
    var input = document.createElement("input");
    input.setAttribute('type', 'text');
    input.setAttribute('id', ID);
    input.setAttribute('class', 'form-control col-sm-10');
    input.onchange = function () { createBRKDateInput(); };
    var parent = document.getElementById("BRKDateSpace");
    parent.appendChild(input);

    $("#" + ID).datepicker({
        dateFormat: "dd-mm-yy"
        , duration: "fast"
    });
}

function updateDateCount(mealId) {
    if (mealId == 1) {
        BRKDateId--;
    }
    else if (mealId == 2) {
        LnDateId--;
    }
    else {
        TBRKDateId--;
    }
}
function submitMealDate(mealId) {
    var idPrefix = "";
    var loopCounter = 0;
    var dateList = [];
    var check = false;
    if (mealId == 1) {
        idPrefix = "Breakfast_";
        loopCounter = BRKDateId;
        check = document.getElementById("BRKCheckBox").checked;
    }
    else if (mealId == 2) {
        idPrefix = "Lunch_";
        loopCounter = LnDateId;
        check = document.getElementById("LnCheckBox").checked;

    }
    else {
        idPrefix = "TBreak_";
        loopCounter = TBRKDateId;
        check = document.getElementById("TBRKCheckBox").checked;


    }

    for (var i = 1; i <= loopCounter; i++) {
        var date = document.getElementById(idPrefix + i).value.toString();
        if (date.length > 0) {
            var obj = { Date: date };
            dateList.push(obj);
        }
     

    }
    if (dateList.length == 0) {
        alert("Please select any date");
        return;
    }
    var jsonDateList = JSON.stringify(dateList);
    $.ajax({
        url: '/Consumer/AddConsumerOrderDate',
        type: "POST",
        data: { DateList: jsonDateList, MealId: mealId, Check: check },
        success: function (response) {
            if (response.success) {

                alert(response.responseText);
                window.location.reload(true);


            } else {
                // DoSomethingElse()
                //if (response.responseText == "Expire") {
                //    window.location.href = "https://ucam.bup.edu.bd/Security/LogOut.aspx";
                //}
                //else {
                //    alert(response.responseText);
                //    window.location.reload(true);

                //}

            }
        }


    })


}

function deleteDate(dateDetailId) {

    $("#ConfirmDelete").empty();
    var input3 = document.createElement("button");
   
    input3.setAttribute('class', 'btn btn');
    input3.textContent = 'Yes';
    input3.onclick = function () { confirmDelete(dateDetailId); };

    var parent3 = document.getElementById("ConfirmDelete");
    parent3.appendChild(input3);
    var modal = document.getElementById("deleteModal");
    modal.style.display = "block";


}

function confirmDelete(dateDetailId) {

    $.ajax({
        url: '/Consumer/DeleteDate',
        type: "POST",
        data: { DetailId: dateDetailId },
        success: function (response) {
            if (response.success) {

                alert(response.responseText);
                window.location.reload(true);


            } else {
                // DoSomethingElse()
                //if (response.responseText == "Expire") {
                //    window.location.href = "https://ucam.bup.edu.bd/Security/LogOut.aspx";
                //}
                //else {
                //    alert(response.responseText);
                //    window.location.reload(true);

                //}

            }
        }


    })

}

function OnOffModify(MealId) {
    var checked;
    if (MealId == 1) {
        checked = document.getElementById("BrkOnOff").checked;
    }
    if (MealId == 2) {
        checked = document.getElementById("LncOnOff").checked;
    }
    if (MealId == 3) {
        checked = document.getElementById("TOnOff").checked;
    }


    $.ajax({

        url: '/Consumer/OnOffModify',
        type: "POST",
        //dataType: 'json',
        data: { Checked: checked, MealId: MealId },
        //contentType: 'application/json; charset=utf-8',
        success: function (response) {
            if (response.success) {


                alert(response.responseText);

                window.location.reload(true);


            } else {
                // DoSomethingElse()
                if (response.responseText == "Expire") {
                    window.location.href = "https://ucam.bup.edu.bd/Security/LogOut.aspx";
                }
                else {
                    alert(response.responseText);
                    window.location.reload(true);

                }
            }
        }


    })



}

//function OnOffModify(MealId) {

//    var checked;
//    if (MealId == 1) {
//        checked = document.getElementById("BrkOnOff").checked;
//    }
//    if (MealId == 2) {
//        checked = document.getElementById("LncOnOff").checked;
//    }
//    if (MealId == 3) {
//        checked = document.getElementById("TOnOff").checked;
//    }


//    $.ajax({

//        url: '@Url.Action("OnOffModify", "Consumer")',
//        type: "POST",
//        //dataType: 'json',
//        data: { Checked: checked, MealId: MealId },
//        //contentType: 'application/json; charset=utf-8',
//        success: function (response) {
//            if (response.success) {


//                alert(response.responseText);
//                //window.location.reload(true);


//            } else {
//                // DoSomethingElse()
//                alert(response.responseText);
//                window.location.reload(true);
//            }
//        }


//    })




//} 

function ExtraItemUpdate(mealId) {

        var addedItem = [];

        var textsAdded;

        var checkBoxAdded;
        var checkBoxLengthAdded;
        if (mealId == 1) {
            textsAdded = document.getElementsByName("EitmBrkQ");
            checkBoxAdded = document.getElementsByName("itemcheck");
            checkBoxLengthAdded = checkBoxAdded.length;

        }
        if (mealId == 2) {
            textsAdded = document.getElementsByName("EitmLnQ");
            checkBoxAdded = document.getElementsByName("itemcheck2");
            checkBoxLengthAdded = checkBoxAdded.length;

        }
        if (mealId == 3) {
            textsAdded = document.getElementsByName("EitmTQ");
            checkBoxAdded = document.getElementsByName("itemcheck3");
            checkBoxLengthAdded = checkBoxAdded.length;

        }
        var tempItemValueAdded = "";
        var tempPriceValueAdded = "";
        var date = document.getElementById("menudate").value.toString();
        console.log(date);
        //console.log(checkBoxAdded);
        //console.log(textsAdded);




        for (var i = 0; i < checkBoxLengthAdded; i++) {

            if (checkBoxAdded[i].checked == true) {

                tempPriceValueAdded = textsAdded[i].value.toString();
                tempItemValueAdded = checkBoxAdded[i].value.toString();
                console.log(checkBoxAdded[i].value.toString() + "  " + textsAdded[i].value.toString());

                if (parseInt(tempPriceValueAdded) <= 0 || tempPriceValueAdded === "") {
                    alert("Quantity should be greater than 0");
                    return;
                }

                var obj = { ItemId: tempItemValueAdded, Quantity: tempPriceValueAdded };
                addedItem.push(obj);
            }

        }
        //-------------------
        extraItemList = JSON.stringify(addedItem);

        $.ajax({

            url: '@Url.Action("UpdateExtraItemList", "Consumer")',
            type: "POST",
            data: { ExtraItemList: extraItemList, MealId: mealId, Date: date },
            success: function (response) {
                if (response.success) {

                    alert(response.responseText);
                    // window.location.reload(true);


                } else {
                    // DoSomethingElse()
                    if (response.responseText == "Expire") {
                        window.location.href = "https://ucam.bup.edu.bd/Security/LogOut.aspx";
                    }
                    else {
                        alert(response.responseText);
                        window.location.reload(true);

                    }

                }
            }


        })

}

function CreteDailyMenu2() {
    var mod = document.getElementById("loadModal");
    mod.style.display = "block";
    var dayList = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday"];
    var itemInpPrefix = "ExItemInp_";
    var itemChkPrefix = "ExItemChk_";
    var addedItem = [];
    var quantityList = [];
    var checkBoxAdded = [];
    var totalItemList;
    for (var i = 0; i < 5; i++) {
        var day = i + 1;
        for (var j = 0; j < 3; j++) {
            var meal = j + 1;
            quantityList = document.getElementsByName(itemInpPrefix + dayList[i] + "_" + meal);
            checkBoxAdded = document.getElementsByName(itemChkPrefix + dayList[i] + "_" + meal);
            var checkBoxLengthAdded = checkBoxAdded.length;
            for (var k = 0; k < checkBoxLengthAdded; k++) {

                if (checkBoxAdded[k].checked == true) {

                    var tempQuantityValueAdded = quantityList[k].value.toString();
                    var tempItemValueAdded = checkBoxAdded[k].value.toString();

                    if (parseInt(tempQuantityValueAdded) <= 0 || tempQuantityValueAdded === "") {
                        alert("Quantity should be greater than 0");
                        mod.style.display = "none";
                        break;
                        return;
                    }

                    var obj = { ItemId: tempItemValueAdded, Quantity: tempQuantityValueAdded, Day: day, Meal: meal };
                    addedItem.push(obj);
                }

            }

            quantityList = [];
            checkBoxAdded = [];
        }
    }

    totalItemList = JSON.stringify(addedItem);
    console.log(addedItem);
    $.ajax({
        url: '/Consumer/CreateDailyMenu',
        type: "POST",
        data: { ItemList: totalItemList },
        success: function (response) {
            if (response.success) {
                mod.style.display = "none";

                alert(response.responseText);
                // window.location.reload(true);


            } else {
                // DoSomethingElse()
                //if (response.responseText == "Expire") {
                //    window.location.href = "https://ucam.bup.edu.bd/Security/LogOut.aspx";
                //}
                //else {
                //    alert(response.responseText);
                //    window.location.reload(true);

                //}

            }
        }


    })
}

//function CreteDailyMenu() {
//    var mod = document.getElementById("loadModal");
//    mod.style.display = "block";
//    var dayList = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday"];
//    var itemInpPrefix = "ExItemInp_";
//    var itemChkPrefix = "ExItemChk_";
//    var addedItem = [];
//    var quantityList = [];
//    var checkBoxAdded = [];
//    var totalItemList;
//    for (var i = 0; i < 5; i++) {
//        var day = i + 1;
//        for (var j = 0; j < 3; j++) {
//            var meal = j + 1;
//             quantityList = document.getElementsByName(itemInpPrefix + dayList[i] + "_" + meal);
//             checkBoxAdded = document.getElementsByName(itemChkPrefix + dayList[i] + "_" + meal);
//            var checkBoxLengthAdded = checkBoxAdded.length;
//            for (var k = 0; k < checkBoxLengthAdded; k++) {

//                if (checkBoxAdded[k].checked == true) {

//                    var tempQuantityValueAdded = quantityList[k].value.toString();
//                    var tempItemValueAdded = checkBoxAdded[k].value.toString();

//                    if (parseInt(tempQuantityValueAdded) <= 0 || tempQuantityValueAdded === "") {
//                        mod.style.display = "none";

//                        alert("Quantity should be greater than 0");
//                        return;
//                    }

//                    var obj = { ItemId: tempItemValueAdded, Quantity: tempQuantityValueAdded, Day:day, Meal:meal };
//                    addedItem.push(obj);
//                }

//            }

//            quantityList = [];
//            checkBoxAdded = [];
//        }
//    }

//    totalItemList = JSON.stringify(addedItem);
//    console.log(addedItem);
//    $.ajax({
//        url: '/Consumer/CreateDailyMenu',
//        type: "POST",
//        data: { ItemList: totalItemList },
//        success: function (response) {
//            if (response.success) {
//                mod.style.display = "none";

//                alert(response.responseText);
//                // window.location.reload(true);


//            } else {
//                // DoSomethingElse()
//                //if (response.responseText == "Expire") {
//                //    window.location.href = "https://ucam.bup.edu.bd/Security/LogOut.aspx";
//                //}
//                //else {
//                //    alert(response.responseText);
//                //    window.location.reload(true);

//                //}

//            }
//        }


//    })
//}

function PeriodModify(MealId) {
    console.log(MealId);
    console.log(document.getElementById("BrkPeriod").checked);
    var from;
    var to;
    var checked;
    if (MealId == 1) {
        from = document.getElementById("datepicker").value.toString();
        to = document.getElementById("datepicker2").value.toString();
        checked = document.getElementById("BrkPeriod").checked;
    }
    if (MealId == 3) {
        from = document.getElementById("datepicker3").value.toString();
        to = document.getElementById("datepicker4").value.toString();
        checked = document.getElementById("TBrkPeriod").checked;
    }
    if (MealId == 2) {
        from = document.getElementById("datepicker5").value.toString();
        to = document.getElementById("datepicker6").value.toString();
        checked = document.getElementById("LnPeriod").checked;
    }

    if (from.length == 0 || to.length == 0) {
        //jQuery.noConflict();

        var modal = document.getElementById("myModal");

        // Get the button that opens the modal
        var btn = document.getElementById("myBtn");

        // Get the <span> element that closes the modal
        var span = document.getElementsByClassName("closeModify")[0];
        modal.style.display = "block";


        // When the user clicks the button, open the modal


        // When the user clicks on <span> (x), close the modal
        span.onclick = function () {
            modal.style.display = "none";
        }

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal) {
                modal.style.display = "none";
            }
        }




    }
    else {

        $.ajax({

            url: '@Url.Action("UserPeriodModify", "Consumer")',
            type: "POST",
            //dataType: 'json',
            data: { From: from, To: to, Checked: checked, MealId: MealId },
            //contentType: 'application/json; charset=utf-8',
            success: function (response) {
                if (response.success) {


                    alert(response.responseText);
                    //window.location.reload(true);


                } else {
                    // DoSomethingElse()
                    alert(response.responseText);
                    window.location.href = "https://ucam.bup.edu.bd/Security/LogOut.aspx";
                }
            }


        })

    }









}

$(function () {
    $("#datepicker").datepicker({
        dateFormat: "dd-mm-yy"
        , duration: "fast"
    });
    $("#datepicker2").datepicker({
        dateFormat: "dd-mm-yy"
        , duration: "fast"
    });
    $("#datepicker3").datepicker({
        dateFormat: "dd-mm-yy"
        , duration: "fast"
    });
    $("#datepicker4").datepicker({
        dateFormat: "dd-mm-yy"
        , duration: "fast"
    });
    $("#datepicker5").datepicker({
        dateFormat: "dd-mm-yy"
        , duration: "fast"
    });
    $("#datepicker6").datepicker({
        dateFormat: "dd-mm-yy"
        , duration: "fast"
    });
    $("#menudate").datepicker({
        dateFormat: "dd-mm-yy"
        , duration: "fast"
    });
});


$(document).ready(function () {
    var switchStatus = false;
    $("#checkboxBreakfastOnOffId").on('change', function () {
        if ($(this).is(':checked')) {
            switchStatus = $(this).is(':checked');
            //.... Switch On
            // alert(switchStatus);// To verify

            $.ajax({

                url: '@Url.Action("UpdateMealActivation", "Consumer")',
                type: "POST",
                //dataType: 'json',
                data: { CheckBoxValue: switchStatus, categoryIdValue: 1 },
                //contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    if (response.success) {

                        alert(response.responseText);
                        window.location.reload(true);


                    } else {
                        // DoSomethingElse()
                        alert(response.responseText);
                        window.location.reload(true);
                    }
                }


            })



        }
        else {
            switchStatus = $(this).is(':checked');
            //.... Switch Off
            //alert(switchStatus);// To verify


            $.ajax({

                url: '@Url.Action("UpdateMealActivation", "Consumer")',
                type: "POST",
                //dataType: 'json',
                data: { CheckBoxValue: switchStatus, categoryIdValue: 1 },
                //contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    if (response.success) {

                        //alert(response.responseText);
                        window.location.reload(true);


                    } else {
                        // DoSomethingElse()
                        //alert(response.responseText);
                        window.location.reload(true);
                    }
                }


            })



        }
    })
});


$(document).ready(function () {
    var switchStatusV2 = false;
    $("#checkboxLunchOnOffId").on('change', function () {
        if ($(this).is(':checked')) {
            switchStatusV2 = $(this).is(':checked');
            // alert(switchStatus);// To verify

            $.ajax({

                url: '@Url.Action("UpdateMealActivation", "Consumer")',
                type: "POST",
                //dataType: 'json',
                data: { CheckBoxValue: switchStatusV2, categoryIdValue: 2 },
                //contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    if (response.success) {

                        //alert(response.responseText);
                        window.location.reload(true);
                        window.location.reload(true);

                    } else {
                        // DoSomethingElse()
                        //alert(response.responseText);
                        window.location.reload(true);
                    }
                }


            })



        }
        else {
            switchStatusV2 = $(this).is(':checked');
            //alert(switchStatus);// To verify


            $.ajax({

                url: '@Url.Action("UpdateMealActivation", "Consumer")',
                type: "POST",
                //dataType: 'json',
                data: { CheckBoxValue: switchStatusV2, categoryIdValue: 2 },
                //contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    if (response.success) {

                        //alert(response.responseText);
                        window.location.reload(true);


                    } else {
                        // DoSomethingElse()
                        //alert(response.responseText);
                        window.location.reload(true);
                    }
                }


            })



        }
    })
});

$(document).ready(function () {
    var switchStatusV3 = false;
    $("#checkboxTeaBreakOnOffId").on('change', function () {
        if ($(this).is(':checked')) {
            switchStatusV3 = $(this).is(':checked');
            // alert(switchStatus);// To verify

            $.ajax({

                url: '@Url.Action("UpdateMealActivation", "Consumer")',
                type: "POST",
                //dataType: 'json',
                data: { CheckBoxValue: switchStatusV3, categoryIdValue: 3 },
                //contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    if (response.success) {

                        //alert(response.responseText);
                        window.location.reload(true);


                    } else {
                        // DoSomethingElse()
                        //alert(response.responseText);
                        window.location.reload(true);
                    }
                }


            })



        }
        else {
            switchStatusV3 = $(this).is(':checked');
            //alert(switchStatus);// To verify


            $.ajax({

                url: '@Url.Action("UpdateMealActivation", "Consumer")',
                type: "POST",
                //dataType: 'json',
                data: { CheckBoxValue: switchStatusV3, categoryIdValue: 3 },
                //contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    if (response.success) {

                        //alert(response.responseText);
                        window.location.reload(true);


                    } else {
                        // DoSomethingElse()
                        //alert(response.responseText);
                        window.location.reload(true);
                    }
                }


            })



        }
    })
});

function AddMoreExtra(type,parentID, selectID, inputParentID, inputID,buttonParentId,buttonID,btnParent,extraChitDrop) {
   
    var count = 0;
 
    if (type == 1) {
        BRKCount++;
        count = BRKCount;
    }
    else if (type == 2) {
        LnCount++;
        count = LnCount;
    }
    else {
        TBRKCount++;
        count = TBRKCount;
    }
    if (parseInt(document.getElementById(selectID + (count-1)).value) == 0
        || document.getElementById(inputID + (count - 1)).value === "") {
        updateMealCount(type);
        alert("First fill up the empty input field");
        return;
    }
    $.ajax({
        url: '/Consumer/GetAllItems',
        dataType: "json",
        data: {},
        success: function (data) {
            var onSpotList = data;

            //$("#" + parentID).append('<div><label for="name">Item</label></div>');

            var div1 = document.createElement("div");
            //div1.setAttribute('class', 'form-group');
            div1.setAttribute('id', extraChitDrop + count);
            div1.style.paddingTop = '10px';
            var selectParent = document.getElementById(parentID);

            selectParent.appendChild(div1);

            var moreSelect = document.createElement("select");
            moreSelect.setAttribute('id', selectID + count);
            moreSelect.setAttribute('name', selectID + count);
            moreSelect.setAttribute('class', "form-control border-radius-none");


            var selectParent2 = document.getElementById(extraChitDrop + count);
            selectParent2.appendChild(moreSelect);
            //document.getElementById(moreDropId).innerHTML += '</div></div>';

            var Select = document.getElementById(selectID + count);
            var el = document.createElement("option");
            el.textContent = "Select Extra Chit";
            el.value = 0;
            Select.appendChild(el);

            for (var i = 0; i < onSpotList.length; i++) {
                var optn = onSpotList[i];
                var el = document.createElement("option");
                el.textContent = optn.name;
                el.value = optn.id;
                Select.appendChild(el);

            }


            //$("#" + inputParentID).append('<div><label for="name">Quantity</label></div>');

            var div = document.createElement("div");
            div.setAttribute('Id', buttonParentId + count);
            div.style.paddingBottom = '35px';
            var prnt = document.getElementById(btnParent);
            prnt.appendChild(div);

            var input = document.createElement("input");
            input.setAttribute('type', 'number');
            input.setAttribute('id', inputID + count);
            input.setAttribute('class', 'form-control');
            input.setAttribute('min', 1);
            var quantutyParent = document.getElementById(inputParentID);
            quantutyParent.appendChild(input);

            var input3 = document.createElement("button");
            input3.setAttribute('id', buttonID + count);
            input3.setAttribute('name', buttonID + count);
            input3.setAttribute('class', 'btn btn');
            input3.textContent = 'Add More';
            input3.onclick = function () { AddMoreExtra(type, parentID, selectID, inputParentID, inputID, buttonParentId, buttonID, btnParent, extraChitDrop); };

            var parent3 = document.getElementById(buttonParentId + count);
            parent3.appendChild(input3);
            $("#" + buttonID + (count - 1)).hide();


        },
        error: function (xhr, status, error) {
            alert("Error");
        }
    });
   
    //document.getElementById(itemId).innerHTML += '</div></div>';

}

function updateMealCount(mealId)
{
    if (mealId == 1) {
        BRKCount--;
    }
    else if (mealId == 2) {
        LnCount--;
    }
    else {
        TBRKCount--;
    }
}


function SubmitExtra(type, dropId, inputId, remarkId) {
    var count = 0;
    var remark = "";
    var itemList = [];

    if (type == 1) {
        count = BRKCount;
    }
    else if (type == 2) {
        count = LnCount;
    }
    else {
        count = TBRKCount;
    }
    if (document.getElementById(remarkId).value === "") {

    }
    else {
        remark = document.getElementById(remarkId).value;
    }
    for (var i = 0; i < count; i++) {
        var itemId = document.getElementById(dropId + (i + 1)).value;
        var q = document.getElementById(inputId + (i + 1)).value;
        if (parseInt(itemId) == 0 || q === "") {

        }
        else {
            var obj = { ItemId: itemId, Quantity: q };
            itemList.push(obj);
        }

    }

    $.ajax({
        url: '/Consumer/AddExtraChitItem',
        type: "POST",

        dataType: "json",
        data: { ItemList: JSON.stringify(itemList), MealType: type, Remarks: remark },
        success: function (response) {
            if (response.success) {

                alert(response.responseText);
                window.location.reload(true);


            } else {
                // DoSomethingElse()
                if (response.responseText == "Expire") {
                    window.location.href = "https://ucam.bup.edu.bd/Security/LogOut.aspx";
                }
                else {
                    alert(response.responseText);
                    window.location.reload(true);

                }

            }
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}


function editOrder(parentId) {


    $("#" + "removeCheck").empty();
    $("#" + "removeLabel").empty();
    $("#" + "itemFld").empty();
    $("#" + "itemQntFld").empty();



    $.ajax({
        url: '/Consumer/GetExtraItemList',
        dataType: "json",
        data: { ConsumerExtraChitParentId: parentId },
        success: function (data) {

            //var data = JSON.parse(datum);
            // console.log(data);
            //console.log(JSON.parse(data));
            //var y = JSON.parse(data);
            console.log(data);
            numOfEdittedOptions = data[1].length;
            for (var i = 0; i < data[1].length; i++) {
                var x = document.createElement("INPUT");
                x.setAttribute("type", "checkbox");
                x.setAttribute("class", "form-check-input");
                x.setAttribute("name", "CheckBox_" + (i + 1));
                x.setAttribute("Id", "CheckBox_" + (i + 1));
                //x.setAttribute("class", "selectStl");
                var checkParent = document.getElementById("removeCheck");
                checkParent.appendChild(x);
                document.getElementById("CheckBox_" + (i + 1)).style.marginTop = "35px";

                $("#" + "removeCheck").append('</br>');
                $("#" + "CheckBox_" + (i + 1)).val(data[1][i].Id.toString());


                $("#" + "removeLabel").append('<div><label class="labelStl" for="name">Remove Item</label></div>');

                $("#" + "itemFld").append('<div><label for="name">Item</label></div>');


                var moreSelect = document.createElement("select");
                moreSelect.setAttribute('id', "RemoveSelect_" + (i + 1));
                moreSelect.setAttribute('name', "RemoveSelect_" + (i + 1));
                moreSelect.setAttribute('class', "form-control border-radius-none");


                var selectParent = document.getElementById("itemFld");
                selectParent.appendChild(moreSelect);

                var Select = document.getElementById("RemoveSelect_" + (i + 1));
                var el = document.createElement("option");
                var ex = data[0].find(x => x.Id === data[1][i].StoreOutItemId);
                el.textContent = ex.Name;
                el.value = ex.Id;
                Select.appendChild(el);
                var val = ex.Id;

                for (var j = 0; j < data[0].length; j++) {
                    var optn = data[0][j];
                    if (data[0][j].Id == val) {

                    }
                    else {
                        var el = document.createElement("option");
                        el.textContent = data[0][j].Name;
                        el.value = data[0][j].Id;
                        Select.appendChild(el);
                    }

                }
                $("#" + "itemQntFld").append('<div><label for="name">Quantity</label></div>');


                var input = document.createElement("input");
                input.setAttribute('type', 'number');
                input.setAttribute('id', 'EditQnt_' + (i + 1));
                input.setAttribute('class', 'form-control');
                //input.setAttribute('min', 1);


                var quantityParent = document.getElementById("itemQntFld");
                quantityParent.appendChild(input);
                document.getElementById('EditQnt_' + (i + 1)).value = data[1][i].quantity;
            }



            document.getElementById("editRemarks").value = data[2][0].Remarks;

        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
    var modal = document.getElementById("onSpotEdit");

    //var span = document.getElementsByClassName("close")[0];
    modal.style.display = "block";

    //document.getElementById("modalStrInDel").innerHTML = htmlString;



    //span.onclick = function () {
    //    modal.style.display = "none";
    //}
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }
}

function ConfirmEdit() {
    var list = [];
    var remarks = document.getElementById("editRemarks").value;
    for (var i = 0; i < numOfEdittedOptions; i++) {
        var checkValue = document.getElementById("CheckBox_" + (i + 1)).value.toString();
        var checked = document.getElementById("CheckBox_" + (i + 1)).checked;
        var item = document.getElementById("RemoveSelect_" + (i + 1)).value.toString();
        var quantity = document.getElementById('EditQnt_' + (i + 1)).value.toString();
        if (quantity === "" && checked == false) {
            alert("Empty Quantity");
            return;
        }

        var isDelete = checked == true ? 1 : 0;

        var obj = { IsDelete: isDelete, OrderId: checkValue, ItemId: item, Quantity: quantity, IsNew: 0 };
        list.push(obj);

    }

    console.log(list);
    $.ajax({
        url: '/Consumer/EditExtraItem',
        type: "POST",

        dataType: "json",
        data: { ItemList: JSON.stringify(list), Remarks : remarks },
        success: function (response) {
            if (response.success) {

                alert(response.responseText);
                window.location.reload(true);


            } else {
                // DoSomethingElse()
                if (response.responseText == "Expire") {
                    window.location.href = "https://ucam.bup.edu.bd/Security/LogOut.aspx";
                }
                else {
                    alert(response.responseText);
                    window.location.reload(true);

                }

            }
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
}

function CloseModal() {

    var modal = document.getElementById("onSpotEdit");


    modal.style.display = "none";


}
function closeDeleteModal() {
    var modal = document.getElementById("deleteModal");


    modal.style.display = "none";
}
