

var onSpotList = [];
var selectCount = 1;
var selectCountOffice = 1;
var numOfEdittedOptions = 0;
var editCount = 0;
function loadOnSpotSpace() {
    if (document.getElementById("personal").checked) {
        $("#personOrder").css("display", "block");
        $("#officeOrder").css("display", "none");
    }
    else {
        $("#personOrder").css("display", "none");
        $("#officeOrder").css("display", "block");

    }

}

function Change() {
    document.getElementById("onspotPerson").removeAttribute("disabled");
    document.getElementById("onspotPerson").value = "";
}

$("#onspotPerson").autocomplete({
    source: function (request, response) {
        $.ajax({
            url: '/Consumer/GetSearchPerson',
            dataType: "json",
            data: { search: $("#onspotPerson").val() },
            success: function (data) {
                var filterPerson = data.filter(x => x.employeeRank === 1 || x.employeeRank === 2);
                var a = [];
                for (var i = 0; i < filterPerson.length; i++) {

                    a.push("[" + filterPerson[i].officeName + "]-" + filterPerson[i].bupNumber + "-" + filterPerson[i].bupFullName);
                }

                response(a);
                $("#suggesstion-box").show();

            },
            error: function (xhr, status, error) {
                alert("Error");
            }
        });
    }
});

$("#onspotBearer").autocomplete({
    source: function (request, response) {
        $.ajax({
            url: '/Consumer/GetSearchPerson',
            dataType: "json",
            data: { search: $("#onspotBearer").val() },
            success: function (data) {
                var filterPerson = data.filter(x => x.employeeRank === 1 || x.employeeRank === 2);
                var a = [];
                for (var i = 0; i < filterPerson.length; i++) {

                    a.push("[" + filterPerson[i].officeName + "]-" + filterPerson[i].bupNumber + "-" + filterPerson[i].bupFullName);
                }


                response(a);
                $("#suggesstion-box3").show();

            },
            error: function (xhr, status, error) {
                alert("Error");
            }
        });
    }
});

$("#onspotOfficeBearer").autocomplete({
    source: function (request, response) {
        $.ajax({
            url: '/Consumer/GetSearchPerson',
            dataType: "json",
            data: { search: $("#onspotOfficeBearer").val() },
            success: function (data) {
                var filterPerson = data.filter(x => x.employeeRank === 1 || x.employeeRank === 2);
                var a = [];
                for (var i = 0; i < filterPerson.length; i++) {

                    a.push("[" + filterPerson[i].officeName + "]-" + filterPerson[i].bupNumber + "-" + filterPerson[i].bupFullName);
                }



                response(a);
                $("#suggesstion-box4").show();

            },
            error: function (xhr, status, error) {
                alert("Error");
            }
        });
    }
});



$("#onspotOffice").autocomplete({
    source: function (request, response) {
        $.ajax({
            url: '/Consumer/GetSearchOffice',
            dataType: "json",
            data: { search: $("#onspotOffice").val() },
            success: function (data) {
                var a = [];
                for (var i = 0; i < data.length; i++) {

                    a.push(data[i].name);
                }
                console.log(a);

                response(a);
                $("#suggesstion-box2").show();

            },
            error: function (xhr, status, error) {
                alert("Error");
            }
        });
    }
});


function addMoreOnSpot(type) {


    $.ajax({
        url: '/Consumer/GetExtraChit',
        dataType: "json",
        data: { },
        success: function (data) {
            onSpotList = data;
            formMoreHTMLString(type);
        },
        error: function (xhr, status, error) {
            alert("Error");
        }
    });


  

}

function addMoreOnSpotEdit() {


    $.ajax({
        url: '/Consumer/GetExtraChit',
        dataType: "json",
        data: {},
        success: function (data) {
            onSpotList = data;
            formMoreEditableHTMLString();
        },
        error: function (xhr, status, error) {
            alert("Error");
        }
    });




}

function formMoreHTMLString(type) {
    var moreDropId = "";
    var selectId = "";
    var count = 0;
    var itemId = "";
    var itemInpId = "";
    var itemRemoveId = "";
    var removeFieldID = "";
    var spaceId = "";
    if (type == 1) {
        if (parseInt(document.getElementById("onspotItem" + selectCount).value) == 0
            || document.getElementById("itmQ" + selectCount).value === "") {
       
            alert("First fill up the empty input field");
            return;
        }

        selectCount++;
        moreDropId = "moreItemDrop";
        selectId = "onspotItem";
        count = selectCount;
        itemId = "moreItemQuantity";
        itemInpId = "itmQ";
        itemRemoveId = "removeMore";
        removeFieldID = "removeField";
        spaceId = "personSpace";

    }
    else {
        if (parseInt(document.getElementById("onspotOfficeItem" + selectCountOffice).value) == 0
            || document.getElementById("itmOfficeQ" + selectCountOffice).value === "") {
            alert("First fill up the empty input field");

            return;
        }
        selectCountOffice++;
        moreDropId = "moreOfficeItemDrop";
        selectId = "onspotOfficeItem";
        count = selectCountOffice;
        itemId = "moreOfficeItemQuantity";
        itemInpId = "itmOfficeQ";
        itemRemoveId = "removeOfficeMore";
        removeFieldID = "removeOfficeField";
        spaceId = "officeSpace";



    }
    //var htmlString = '<div class="form-group">';
    //var htmlString = '<label class="bmd-label-floating">Item</label>';
    //htmlString += '<div style="display:inline-flex">';
    //htmlString += '<i class="material-icons" style="padding-top:6px;">local_dining</i>';

    //document.getElementById(moreDropId).innerHTML += htmlString;
    //$("#" + moreDropId).append('<div><label for="name">Item</label></div>');
    if (count > 2) {
        $("#" + moreDropId).append('<div id=' + selectId + spaceId + count+ ' style="padding-top:32px;"></div>');

    }
    var moreSelect = document.createElement("select");
    moreSelect.setAttribute('id', selectId + count);
    moreSelect.setAttribute('name', selectId + count);
    moreSelect.setAttribute('class', "form-control border-radius-none");
    if (type == 1) {
        moreSelect.setAttribute('onchange', 'checkRepeatValue(1)');
    }
    else {
        moreSelect.setAttribute('onchange', 'checkRepeatValue(2)');
    }
    
    var selectParent = document.getElementById(moreDropId);
    selectParent.appendChild(moreSelect); 
    //document.getElementById(moreDropId).innerHTML += '</div></div>';

    var Select = document.getElementById(selectId + count);
    var el = document.createElement("option");
    var padTopRemove = "";
    el.textContent = "Select Item";
    el.value = 0;
    Select.appendChild(el);

    for (var i = 0; i < onSpotList.length; i++) {
        var optn = onSpotList[i];
        var el = document.createElement("option");
        el.textContent = optn.itemName;
        el.value = optn.id;
        Select.appendChild(el);

    } 

    //var htmlString2 = '<div class="form-group">';
    //var htmlString2 = '<label class="bmd-label-floating">Item</label>';
    //htmlString2 += '<div style="display:inline-flex">';
    //htmlString2 += '<i class="material-icons" style="padding-top:6px;">double_arrow</i>';
   // document.getElementById(itemId).innerHTML += htmlString2;
    //$("#" + itemId).append('<div><label for="name">Quantity</label></div>');


    if (count == 3) {
        $("#" + itemId).append('<div id=' + itemInpId + spaceId + count + ' style="padding-top:20px;"></div>');
        padTopRemove = ((count - 1) * 24);

    }
    else if (count > 3)
    {
        $("#" + itemId).append('<div id=' + itemInpId + spaceId + count + ' style="padding-top:20px;"></div>');
        padTopRemove = ((count - 1) * 24) + (24 * (count-2));
    }
    else {
        //$("#" + itemId).append('<div style="margin-top:-10px;"></div>');
        padTopRemove = 1;

    }
    var input = document.createElement("input");
    input.setAttribute('type', 'number');
    input.setAttribute('id', itemInpId + count);
    input.setAttribute('class', 'form-control');
    input.setAttribute('placeholder', 'quantity');

    input.setAttribute('min', 1);
    var quantutyParent = document.getElementById(itemId);
    quantutyParent.appendChild(input);
    //document.getElementById(itemId).innerHTML += '</div></div>';

    var moreRmvBtn = document.createElement("button");
    moreRmvBtn.setAttribute('id', removeFieldID + count);
    moreRmvBtn.setAttribute('name', removeFieldID + count);
    moreRmvBtn.setAttribute('class', 'btn-sm btn-success  btn-just-icon');
    moreRmvBtn.style.padding = "0px";

    moreRmvBtn.onclick = function () {
        removeField(selectId, count, itemInpId, type, removeFieldID, spaceId); 
       
    };
    moreRmvBtn.innerHTML = '<i class="material-icons">close</i>';

    var childDiv = document.createElement("div");
    childDiv.setAttribute('id', removeFieldID + count);
    if (count > 1) {
        //var padTop = (count - 1) * 1;
        childDiv.style.paddingTop = padTopRemove.toString() + "px";
    }
    var divParent = document.getElementById(itemRemoveId);
    divParent.appendChild(childDiv);

    var btnParent = document.getElementById(removeFieldID + count);
    btnParent.appendChild(moreRmvBtn);

    $("#" + removeFieldID + (count-1)).remove();


}

function removeField(selectID, Count, itemInputID, type, removeFieldID, spaceId) {
    $("#" + selectID + Count).remove();
    $("#" + itemInputID + Count).remove();
    $("#" + removeFieldID + Count).remove();
    $("#" + selectID + spaceId + Count).remove();
    $("#" + itemInputID + spaceId + Count).remove();

    if (type == 1) {
        selectCount--;
    }
    else {
        selectCountOffice--;
    }

}
function checkRepeatValue(type) {
    var count = 0;
    var selectId = "";
    var lastItem = 0;
    console.log(type);
    if (type == 1) {
        count = selectCount;
        selectId = "onspotItem";
    }
    else {
        count = selectCountOffice;
        selectId = "onspotOfficeItem";
    }

    if (count > 0) {
        lastItem = document.getElementById(selectId + count).value;
    }

    for (var i = 0; i < count - 1; i++) {
        var itemId = document.getElementById(selectId + (i + 1)).value;
        if (lastItem == itemId) {
            alert("This item has been selected before");
            document.getElementById(selectId + count).value = "0";
        }

    }
        



}

function formMoreEditableHTMLString() {
    var moreDropId = "";
    var selectId = "";
    var count = 0;
    var itemId = "";
    var itemInpId = "";
    if (editCount > 0) {
        if (parseInt(document.getElementById("onspotItemEdit" + editCount).value) == 0
            || document.getElementById("itmQEdit" + editCount).value === "") {

            alert("First fill up the empty input field");
            return;
        }
    }

        editCount++;
        moreDropId = "moreItemDropEdit";
    selectId = "onspotItemEdit";
    count = editCount;
        itemId = "moreItemQuantityEdit";
        itemInpId = "itmQEdit";

    
    //var htmlString = '<div class="form-group">';
    //var htmlString = '<label class="bmd-label-floating">Item</label>';
    //htmlString += '<div style="display:inline-flex">';
    //htmlString += '<i class="material-icons" style="padding-top:6px;">local_dining</i>';

    //document.getElementById(moreDropId).innerHTML += htmlString;
    $("#" + moreDropId).append('<div><label for="name">Item</label></div>');

    var moreSelect = document.createElement("select");
    moreSelect.setAttribute('id', selectId + count);
    moreSelect.setAttribute('name', selectId + count);
    moreSelect.setAttribute('class', "form-control border-radius-none");


    var selectParent = document.getElementById(moreDropId);
    selectParent.appendChild(moreSelect);
    //document.getElementById(moreDropId).innerHTML += '</div></div>';

    var Select = document.getElementById(selectId + count);
    var el = document.createElement("option");
    el.textContent = "Select Item";
    el.value = 0;
    Select.appendChild(el);

    for (var i = 0; i < onSpotList.length; i++) {
        var optn = onSpotList[i];
        var el = document.createElement("option");
        el.textContent = optn.itemName;
        el.value = optn.id;
        Select.appendChild(el);

    }

    //var htmlString2 = '<div class="form-group">';
    //var htmlString2 = '<label class="bmd-label-floating">Item</label>';
    //htmlString2 += '<div style="display:inline-flex">';
    //htmlString2 += '<i class="material-icons" style="padding-top:6px;">double_arrow</i>';
    // document.getElementById(itemId).innerHTML += htmlString2;
    $("#" + itemId).append('<div><label for="name">Quantity</label></div>');

    var input = document.createElement("input");
    input.setAttribute('type', 'number');
    input.setAttribute('id', itemInpId + count);
    input.setAttribute('class', 'form-control');
    input.setAttribute('min', 1);
    var quantutyParent = document.getElementById(itemId);
    quantutyParent.appendChild(input);
    //document.getElementById(itemId).innerHTML += '</div></div>';

}


function addOnSpotItem(type) {
    var moreDropId = "";
    var selectId = "";
    var count = 0;
    var itemId = "";
    var itemInpId = "";
    var itemList = [];
    var isOffice = false;
    var bearer = "";
    var office = "No";
    var user = "No";
    if (type == 1) {
        selectId = "onspotItem";
        itemInpId = "itmQ";
        count = selectCount;
        bearer = document.getElementById("onspotBearer").value === "" ? "No" : document.getElementById("onspotBearer").value ;
        user =  document.getElementById("onspotPerson").value === "" ? "No" : document.getElementById("onspotPerson").value ;

    }
    else {

        selectId = "onspotOfficeItem";
        itemInpId = "itmOfficeQ";
        count = selectCountOffice;
        isOffice = true;
        bearer = document.getElementById("onspotOfficeBearer").value === "" ? "No" : document.getElementById("onspotOfficeBearer").value;
        office = document.getElementById("onspotOffice").value;
        if (office === "") {
            alert("Selelct Office");
        }

    }

    for (var i = 0; i < count; i++) {
        var itemId = document.getElementById(selectId + (i + 1)).value;
        var q = document.getElementById(itemInpId + (i + 1)).value;
        if (parseInt(itemId) == 0 || q === "") {

        }
        else {
            var obj = { Id: itemId, Quantity: q };
            itemList.push(obj);
        }
   
    }
    $.ajax({
        url: '/Consumer/AddOnSpotItem',
        type: "POST",

        dataType: "json",
        data: { ItemList: JSON.stringify(itemList), IsOffice: isOffice, Person: user, Office: office, Bearer: bearer },
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


function editOrder(onSpotParentId) {


    $("#" + "removeCheck").empty();
    $("#" + "removeLabel").empty();
    $("#" + "itemFld").empty();
    $("#" + "itemQntFld").empty();
    
    

    $.ajax({
        url: '/Consumer/GetOnSpotItemList',
        dataType: "json",
        data: { ParentId: onSpotParentId },
        success: function (data) {

            //var data = JSON.parse(datum);
           // console.log(data);
            //console.log(JSON.parse(data));
            //var y = JSON.parse(data);

            numOfEdittedOptions = data[1].length;
            for (var i = 0; i < data[1].length; i++) {
                var x = document.createElement("INPUT");
                x.setAttribute("type", "checkbox");
                x.setAttribute("class", "form-check-input");
                x.setAttribute("name", "CheckBox_"+ (i+1)  );
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
                moreSelect.setAttribute('id', "RemoveSelect_" + (i+1));
                moreSelect.setAttribute('name', "RemoveSelect_" + (i+1));
                moreSelect.setAttribute('class', "form-control border-radius-none");


                var selectParent = document.getElementById("itemFld");
                selectParent.appendChild(moreSelect);

                var Select = document.getElementById("RemoveSelect_" + (i+1));
                var el = document.createElement("option");
                var ex = data[0].find(x => x.Id === data[1][i].StoreOutItemId.toString());
                el.textContent = ex.ItemName;
                el.value = ex.Id;
                Select.appendChild(el);
                var val = ex.Id;

                for (var j = 0; j < data[0].length; j++) {
                    var optn = data[0][j];
                    if (data[0][j].Id == val) {

                    }
                    else {
                        var el = document.createElement("option");
                        el.textContent = data[0][j].ItemName;
                        el.value = data[0][j].Id;
                        Select.appendChild(el);
                    }

                } 
                $("#" + "itemQntFld").append('<div><label for="name">Quantity</label></div>');


                var input = document.createElement("input");
                input.setAttribute('type', 'number');
                input.setAttribute('id', 'EditQnt_'+(i+1));
                input.setAttribute('class', 'form-control');
                //input.setAttribute('min', 1);
             

                var quantityParent = document.getElementById("itemQntFld");
                quantityParent.appendChild(input);
                document.getElementById('EditQnt_'+(i+1)).value = data[1][i].quantity;
            }
        
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

        var obj = { IsDelete: isDelete, OrderId: checkValue, ItemId: item, Quantity: quantity, IsNew:0 };
        list.push(obj);

    }
    for (var j = 0; j < editCount; j++) {
        var itemId = document.getElementById("onspotItemEdit" + (j + 1)).value;
        var q = document.getElementById("itmQEdit" + (j + 1)).value;
        if (parseInt(itemId) == 0 || q === "") {

        }
        else {
            var obj = { IsDelete: false, OrderId: 0, ItemId: itemId, Quantity: q, IsNew: 1 };
            list.push(obj);
        }
    }
    $.ajax({
        url: '/Consumer/EditOnSpotItem',
        type: "POST",

        dataType: "json",
        data: { ItemList: JSON.stringify(list)},
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