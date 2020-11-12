

    $('#PartyInput').hide();
    $('#OfficeInput').hide();

    $('AfterPartyEdit').hide();


    var partyItem = [];
    var officeItem = [];




    function GetSpecialOnDate() {

        var date = document.getElementById("billdate").value.toString();

          $.ajax({
        url: '@Url.Action("GetSpecialOnDate","AdminShowOrder")',
                     dataType: "json",
                     data: {Date: date},
                success: function (data) {
        console.log(data);

                    var editHTML = "";
                    var sl = 0;
                    var itmnm = "";
                    var itmq = 0;
                    if (data[5][0] == '0') {
        alert("Date is not allowed for printing.");
                    }
                    else {



        editHTML += '<table class="table table-striped table-bordered table-hover">';
                        editHTML += ' <thead><tr><th>#</th><th>Date</th><th>Person/Reference </th><th>Office</th><th>Item</th><th></th> ';
                        editHTML += '  </tr>  </thead> <tbody>'
                        for (var i = 0; i < data[{
            sl++;

                            var person = data[3].find(x => x.Id == data[1][i].UserId);

                            var name = "-";
                            var offname = "-";
                            var office = data[4].find(x => x.Id == data[1][i].OfficeId);
                            var d = data[1][i].OrderDate.slice(0, 10);
                            var splitstr = d.split("-");
                            var dt = splitstr[2] + "-" + splitstr[1] + "-" + splitstr[0];

                            if (person != undefined) {
            name = person.BUPFullName;
                            }
                            if (office != undefined) {
            offname = office.Name;
                            }










                            editHTML += '<tr><td>' + sl + '</td>';

                            editHTML += '<td> <div class="row"> <div class="col-sm-12">' + dt + '</div></div></td>';
                            //editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditOfficeDate(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td>';

editHTML += '<td> <div class="row"> <div class="col-sm-12">' + name + '</div></div></td>';

editHTML += '<td> <div class="row"> <div class="col-sm-12">' + offname + '</div></div></td><td>';
//editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditOfficeName(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td><td>';
for (var j = 0; j < data[0].length; j++) {
    var soi = data[2].find(x => x.Id == data[0][j].StoreOutItemId);
    if (data[1][i].Id == data[0][j].SpecialMenuParentId) {


        editHTML += '<div class="row"><div class="col-sm-8">' + soi.Name + '</div><div class="col-sm-4">' + data[0][j].UnitOrdered + '</div>';
        //editHTML += '<div class="col-sm-2"><button type="button" rel="tooltip" style="padding:2px;" onclick="EditOffice('+data[0][j].Id+')" class="btn"><i class="fas fa-edit"></i></button></div>';
        editHTML += '</div>';

    }

}



editHTML += '</td><td><button onclick="SpecialPrint(' + data[1][i].Id + ')" class="btn btn-white btn-round btn-just-icon">';   
editHTML += '<i class="material-icons">print</i><div class="ripple-container"></div> </button></td></tr>';
                        }


editHTML += '</tbody></table>'





$('#SpecialPrintDB').hide();
//$('#OfficeDB').hide();
//$('#OfficeEditOption').hide();
//$('#OfficeDateEditOption').hide();
//$('#OfficeNameEditOption').hide();
//                    $('#AfterOfficeBack').hide();

//$('#AfterOfficeEdit').hide();
//                                        $('#AfterOfficeNameEdit').hide();


$('#SpecialOnDate').show();


document.getElementById("SpecialOnDate").innerHTML = editHTML;
                    }
                    //document.getElementById('ei').value = itmnm;
                    //document.getElementById('eiq').value = itmq;





                            },
error: function (xhr, status, error) {
    alert("Error");
}
            });





    }


function SpecialPrint(Id) {

    $.ajax({
        url: '@Url.Action("SpecialPrint","AdminShowOrder")',
        dataType: "json",
        data: { Id: Id },
        success: function (data) {
            console.log(data);

            var editHTML = "";
            var sl = 0;
            var itmnm = "";
            var itmq = 0;
            var name = "";
            var dt = "";
            var offname = "";
            var splitprintdt = data[5][1].split("/");
            printdt = splitprintdt[1] + "-" + splitprintdt[0] + "-" + splitprintdt[2];
            var admin = data[5][0];

            editHTML += '<br><br><br><div align="center"><table class="table table-striped table-bordered table-hover">';
            editHTML += ' <thead><tr><th>Item</th><th>Quantity </th><th>Unit Price</th><th>Total Price</th> ';
            editHTML += '  </tr>  </thead> <tbody>'
            for (var i = 0; i < data[1].length; i++) {
                sl++;

                var person = data[3].find(x => x.Id == data[1][i].UserId);


                var office = data[4].find(x => x.Id == data[1][i].OfficeId);
                var d = data[1][i].OrderDate.slice(0, 10);
                var splitstr = d.split("-");
                dt = splitstr[2] + "-" + splitstr[1] + "-" + splitstr[0];

                if (person != undefined) {
                    name = person.BUPFullName;
                }
                if (office != undefined) {
                    offname = office.Name;
                }










                //editHTML += '<tr><td>' + sl + '</td>';

                //editHTML += '<td> <div class="row"> <div class="col-sm-12">' + dt + '</div></div></td>';
                //     //editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditOfficeDate(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td>';

                //editHTML += '<td> <div class="row"> <div class="col-sm-12">' + name + '</div></div></td>';

                //editHTML += '<td> <div class="row"> <div class="col-sm-12">' + offname + '</div></div></td><td>';
                //editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditOfficeName(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td><td>';

                var ttlPrice = 0;
                var ttlBill = 0;

                for (var j = 0; j < data[0].length; j++) {
                    var soi = data[2].find(x => x.Id == data[0][j].StoreOutItemId);
                    if (data[1][i].Id == data[0][j].SpecialMenuParentId) {
                        ttlPrice = data[0][j].UnitOrdered * soi.Price;
                        ttlBill += ttlPrice;
                        editHTML += '<tr><td>' + soi.Name + '</td><td>' + data[0][j].UnitOrdered + '</td><td>' + soi.Price + ' Tk</td><td>' + ttlPrice + ' Tk</td></tr>'

                        //editHTML += '<div class="col-sm-2"><button type="button" rel="tooltip" style="padding:2px;" onclick="EditOffice('+data[0][j].Id+')" class="btn"><i class="fas fa-edit"></i></button></div>';

                    }

                }



                editHTML += '<tr><td></td><td></td><td>Total Bill </td><td>' + ttlBill + '</td></tr>';
            }


            editHTML += '</tbody></table></div>';




            var mywindow = window.open('', 'PRINT', 'height=' + 1000 + ',width=' + 800);
            mywindow.document.write('<html>    ');
            mywindow.document.write('<head> <style>  td {    border-style: solid;border-width: thin;border-color:grey }</style> <head>')






            //mywindow.document.write(headerhtml.toString());
            mywindow.document.write('<body style="text-align: center; padding:1em;">');
            if (data[1][0].MealTypeId == 10006) {
                mywindow.document.write('<h2>' + 'Party Order' + '</h2>');

                mywindow.document.write('<h3> Person :' + name + '</h3>');
                mywindow.document.write('<h3> Date :' + dt + '</h3>');
            }
            else {
                mywindow.document.write('<h2>' + 'Office Order' + '</h2>');
                mywindow.document.write('<h3> Office :' + offname + '</h3>');

                mywindow.document.write('<h3> Reference :' + name + '</h3>');
                mywindow.document.write('<h3> Date :' + dt + '</h3>');

            }

            //mywindow.document.write('<table><tr><td>Session </td><td>' + s.toString() + '</td></tr><tr><td>Program </td><td>' + p.toString() + '</td></tr><tr><td>Course </td><td>' + c.toString() + '</td></tr></table></br>');
            //mywindow.document.write('<table><tr><td>Mean Marks </td><td>' + meanmark + '</td></tr><tr><td>Mean Grade Point </td><td>' + meanGradePOint + '</td></tr><tr><td>Faculty Performance Index </td><td>' + facultyPerformanceIndex + '</td></tr><tr><td>Standard Deviation Of Marks </td><td>' + stdDeviationMark + '</td></tr><tr><td>Standard Grade Points </td><td>' + stadGradePoint + '</td></tr><tr><td>Course Evaluation Score </td><td>' + courseEvaluationScore + '</td></tr><tr><td>Threshold Value </td><td>' + parsed.ThresholdValue +'</td></tr><tr><td>CO Level </td><td>'+LblCourseOutcomeValue + '</td></tr></table></br>')
            mywindow.document.write(editHTML);

            mywindow.document.write('<br><br><br><p style="float:left;">' + printdt + ' <p style="float:right">' + admin + '</p></p>')


            mywindow.document.write('<br><p style="float:left;margin-left:-115px;"> Printed Date <p style="float:right;margin-right:-80px;">Printed By</p></p>')
            //mywindow.document.write('<br style="line-height:50%;"><p class="b" style="float:left;line-height:2px;">Date:'+date+'</p>')


            //mywindow.document.write('<table><tr><td>Ammendment   </td><td> ' + amd + '</td></tr><tr><td>Recommendation & Proposal    </td><td>' + parsed.RecommendationAndProposal + '</td></tr><tr><td>Attainments Satisfaction   </td><td>' + atm + '</td></tr><tr><td>Attainment Comments   </td><td>' + parsed.AttainmentsComments + '</td></tr><tr><td>Instructor Comments&Feedback  </td><td>' + parsed.CommentAndFeedback + '</td></tr><tr><td>Recommendation1   </td><td>' + parsed.SuggestionAndRecommendation1 + '</td></tr><tr><td>Recommendation2   </td><td>' + parsed.SuggestionAndRecommendation2 + '</td></tr><tr><td>Recommendation3  </td><td>' + parsed.SuggestionAndRecommendation3 + '</td></tr></table></br>');

            mywindow.document.write('</body></html>');

            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10*/

            mywindow.print();
            //mywindow.close();













        },
        error: function (xhr, status, error) {
            alert("Error");
        }
    });


}



function SpecialTempPrint(Id) {

    $.ajax({
        url: '@Url.Action("SpecialPrint","AdminShowOrder")',
        dataType: "json",
        data: { Id: Id },
        success: function (data) {
            console.log(data);

            var editHTML = "";
            var sl = 0;
            var itmnm = "";
            var itmq = 0;
            var name = "";
            var dt = "";
            var offname = "";
            var splitprintdt = data[5][1].split("/");
            printdt = splitprintdt[1] + "-" + splitprintdt[0] + "-" + splitprintdt[2];
            var admin = data[5][0];

            editHTML += '<br><br><br><div align="center"><table class="table table-striped table-bordered table-hover">';
            editHTML += ' <thead><tr><th>Item</th><th>Quantity </th><th>Unit Price</th><th>Total Price</th> ';
            editHTML += '  </tr>  </thead> <tbody>'
            for (var i = 0; i < data[1].length; i++) {
                sl++;

                var person = data[3].find(x => x.Id == data[1][i].UserId);


                var office = data[4].find(x => x.Id == data[1][i].OfficeId);
                var d = data[1][i].OrderDate.slice(0, 10);
                var splitstr = d.split("-");
                dt = splitstr[2] + "-" + splitstr[1] + "-" + splitstr[0];

                if (person != undefined) {
                    name = person.BUPFullName;
                }
                if (office != undefined) {
                    offname = office.Name;
                }










                //editHTML += '<tr><td>' + sl + '</td>';

                //editHTML += '<td> <div class="row"> <div class="col-sm-12">' + dt + '</div></div></td>';
                //     //editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditOfficeDate(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td>';

                //editHTML += '<td> <div class="row"> <div class="col-sm-12">' + name + '</div></div></td>';

                //editHTML += '<td> <div class="row"> <div class="col-sm-12">' + offname + '</div></div></td><td>';
                //editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditOfficeName(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td><td>';

                var ttlPrice = 0;
                var ttlBill = 0;

                for (var j = 0; j < data[0].length; j++) {
                    var soi = data[2].find(x => x.Id == data[0][j].StoreOutItemId);
                    if (data[1][i].Id == data[0][j].SpecialMenuParentId) {
                        ttlPrice = data[0][j].UnitOrdered * soi.Price;
                        ttlBill += ttlPrice;
                        editHTML += '<tr><td>' + soi.Name + '</td><td>' + data[0][j].UnitOrdered + '</td><td>' + soi.Price + ' Tk</td><td>' + ttlPrice + ' Tk</td></tr>'

                        //editHTML += '<div class="col-sm-2"><button type="button" rel="tooltip" style="padding:2px;" onclick="EditOffice('+data[0][j].Id+')" class="btn"><i class="fas fa-edit"></i></button></div>';

                    }

                }



                editHTML += '<tr><td></td><td></td><td>Total Bill </td><td>' + ttlBill + '</td></tr>';
            }


            editHTML += '</tbody></table></div>';




            var mywindow = window.open('', 'PRINT', 'height=' + 1000 + ',width=' + 800);
            mywindow.document.write('<html>    ');
            mywindow.document.write('<head> <style>  td {    border-style: solid;border-width: thin;border-color:grey }</style> <head>')






            //mywindow.document.write(headerhtml.toString());
            mywindow.document.write('<body style="text-align: center; padding:1em;">');
            if (data[1][0].MealTypeId == 10006) {
                mywindow.document.write('<h2>' + 'Party Order' + '</h2>');

                mywindow.document.write('<h3> Person :' + name + '</h3>');
                mywindow.document.write('<h3> Date :' + dt + '</h3>');
            }
            else {
                mywindow.document.write('<h2>' + 'Office Order' + '</h2>');
                mywindow.document.write('<h3> Office :' + offname + '</h3>');

                mywindow.document.write('<h3> Reference :' + name + '</h3>');
                mywindow.document.write('<h3> Date :' + dt + '</h3>');

            }

            //mywindow.document.write('<table><tr><td>Session </td><td>' + s.toString() + '</td></tr><tr><td>Program </td><td>' + p.toString() + '</td></tr><tr><td>Course </td><td>' + c.toString() + '</td></tr></table></br>');
            //mywindow.document.write('<table><tr><td>Mean Marks </td><td>' + meanmark + '</td></tr><tr><td>Mean Grade Point </td><td>' + meanGradePOint + '</td></tr><tr><td>Faculty Performance Index </td><td>' + facultyPerformanceIndex + '</td></tr><tr><td>Standard Deviation Of Marks </td><td>' + stdDeviationMark + '</td></tr><tr><td>Standard Grade Points </td><td>' + stadGradePoint + '</td></tr><tr><td>Course Evaluation Score </td><td>' + courseEvaluationScore + '</td></tr><tr><td>Threshold Value </td><td>' + parsed.ThresholdValue +'</td></tr><tr><td>CO Level </td><td>'+LblCourseOutcomeValue + '</td></tr></table></br>')
            mywindow.document.write(editHTML);

            mywindow.document.write('<br><br><br><p style="float:left;">' + printdt + ' <p style="float:right">' + admin + '</p></p>')


            mywindow.document.write('<br><p style="float:left;margin-left:-115px;"> Printed Date <p style="float:right;margin-right:-80px;">Printed By</p></p>')
            //mywindow.document.write('<br style="line-height:50%;"><p class="b" style="float:left;line-height:2px;">Date:'+date+'</p>')


            //mywindow.document.write('<table><tr><td>Ammendment   </td><td> ' + amd + '</td></tr><tr><td>Recommendation & Proposal    </td><td>' + parsed.RecommendationAndProposal + '</td></tr><tr><td>Attainments Satisfaction   </td><td>' + atm + '</td></tr><tr><td>Attainment Comments   </td><td>' + parsed.AttainmentsComments + '</td></tr><tr><td>Instructor Comments&Feedback  </td><td>' + parsed.CommentAndFeedback + '</td></tr><tr><td>Recommendation1   </td><td>' + parsed.SuggestionAndRecommendation1 + '</td></tr><tr><td>Recommendation2   </td><td>' + parsed.SuggestionAndRecommendation2 + '</td></tr><tr><td>Recommendation3  </td><td>' + parsed.SuggestionAndRecommendation3 + '</td></tr></table></br>');

            mywindow.document.write('</body></html>');

            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10*/

            mywindow.print();
            //mywindow.close();













        },
        error: function (xhr, status, error) {
            alert("Error");
        }
    });


}



function Test() {
    console.log('Dip');
    //$('#fileUploadModal').modal('show');
    window.alert("lolololololo");
}

$(function () {
    $("#datepicker").datepicker({
        dateFormat: "dd-mm-yy"
        , duration: "fast"
    });
});

$(function () {
    $("#partydate").datepicker({
        dateFormat: "dd-mm-yy"
        , duration: "fast"
    });
});

$(function () {
    $("#billdate").datepicker({
        dateFormat: "dd-mm-yy"
        , duration: "fast"
    });
});

$(function () {
    $("#officedate").datepicker({
        dateFormat: "dd-mm-yy"
        , duration: "fast"
    });
});


$("#party").attr("aria-expanded", "true");



var input = $('#input-a');
input.clockpicker({
    autoclose: true
});

$("#person").on('change input', function () {
    fetchPerson();

});
$("#person2").on('change input', function () {
    fetchPerson2();
});
$("#person3").on('change input', function () {
    fetchPerson3();
});
$("#person4").on('change input', function () {
    fetchPerson4();
});

//   $('.clockpicker').clockpicker({
//  'default': 'now',
//  vibrate: true,
//  placement: "top",
//  align: "left",
//  autoclose: false,
//  twelvehour: true
//});
//$('.clockpicker').clockpicker();

//$('#datetimepicker3').datetimepicker({
//    format: 'LT'
//});

function OpenPartyInput() {

    $('#PartyEditOption').hide();
    $('#PartyPersonEditOption').hide();
    $('#PartyDateEditOption').hide();
    $('#AfterPartyDateEdit').hide();
    $('#AfterPartyNameEdit').hide();
    $('#AfterPartyEdit').hide();
    $('#PartyDB').hide();

    $('#AfterBack').hide();
    $('#PartyInput').show();

}
function OpenOfficeInput() {

    $('#OfficeEditOption').hide();
    $('#OfficeNameEditOption').hide();
    $('#OfficeDateEditOption').hide();
    $('#AfterOfficeDateEdit').hide();
    $('#AfterOfficeNameEdit').hide();
    $('#AfterOfficeEdit').hide();
    $('#OfficeDB').hide();

    $('#AfterOfficeBack').hide();
    $('#OfficeInput').show();

}

function CloseParty() {

    $.ajax({
        url: '@Url.Action("FetchSpecialMenu","AdminShowOrder")',
        dataType: "json",
        data: { Id: 10006 },
        success: function (data) {
            console.log(data);

            var editHTML = "";
            var sl = 0;
            var itmnm = "";
            var itmq = 0;


            editHTML += '<table class="table table-striped table-bordered table-hover">';
            editHTML += ' <thead><tr><th>#</th><th>Date</th><th>Name</th><th>Item</th> <th></th> ';
            editHTML += '  </tr>  </thead> <tbody>'
            for (var i = 0; i < data[1].length; i++) {
                sl++;

                var user = data[3].find(x => x.Id == data[1][i].UserId);
                var d = data[1][i].OrderDate.slice(0, 10);
                var splitstr = d.split("-");
                var dt = splitstr[2] + "-" + splitstr[1] + "-" + splitstr[0];







                editHTML += '<tr><td>' + sl + '</td>';

                editHTML += '<td> <div class="row"> <div class="col-sm-10">' + dt + '</div><div class="col-sm-2" style="padding-left: 0; margin-left: -10px;">';
                editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditPartyDate(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td>';


                editHTML += '<td> <div class="row"> <div class="col-sm-10">' + user.BUPFullName + '</div><div class="col-sm-2" style="padding-left: 0; margin-left: -10px;">';
                editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditPartyName(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td><td>';
                for (var j = 0; j < data[0].length; j++) {
                    var soi = data[2].find(x => x.Id == data[0][j].StoreOutItemId);
                    if (data[1][i].Id == data[0][j].SpecialMenuParentId) {


                        editHTML += '<div class="row"><div class="col-sm-6">' + soi.Name + '</div><div class="col-sm-2">' + data[0][j].UnitOrdered + '</div>';
                        editHTML += '<div class="col-sm-2"><button type="button" rel="tooltip" style="padding:2px;" onclick="EditParty(' + data[0][j].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div>';
                        editHTML += '<div class="col-sm-2"><button type="button" rel="tooltip" style="padding:2px;" onclick="DeleteParty(' + data[0][j].Id + ')" class="btn"><i class="fas fa-trash"></i></button></div></div>';

                    }

                }


                editHTML += '</td><td><button onclick="SpecialTempPrint(' + data[1][i].Id + ')" class="btn btn-white btn-round btn-just-icon">';
                editHTML += '<i class="material-icons">print</i><div class="ripple-container"></div> </button></td></tr>';

                //editHTML += '</td></tr>';
            }


            editHTML += '</tbody></table>'




            $('#PartyEditOption').hide();
            $('#PartyPersonEditOption').hide();
            $('#PartyDateEditOption').hide();
            $('#AfterPartyDateEdit').hide();
            $('#AfterPartyNameEdit').hide();
            $('#AfterPartyEdit').hide();
            $('#PartyDB').hide();
            $('#PartyInput').hide();

            $('#AfterBack').show();



            document.getElementById("AfterBack").innerHTML = editHTML;
            //document.getElementById('ei').value = itmnm;
            //document.getElementById('eiq').value = itmq;





        },
        error: function (xhr, status, error) {
            alert("Error");
        }
    });



}




function CloseOffice() {



    $.ajax({
        url: '@Url.Action("FetchSpecialMenu","AdminShowOrder")',
        dataType: "json",
        data: { Id: 10007 },
        success: function (data) {
            console.log(data);

            var editHTML = "";
            var sl = 0;
            var itmnm = "";
            var itmq = 0;


            editHTML += '<table class="table table-striped table-bordered table-hover">';
            editHTML += ' <thead><tr><th>#</th><th>Date</th><th>Name</th><th>Item</th><th></th> ';
            editHTML += '  </tr>  </thead> <tbody>'
            for (var i = 0; i < data[1].length; i++) {
                sl++;

                var office = data[3].find(x => x.Id == data[1][i].OfficeId);
                var d = data[1][i].OrderDate.slice(0, 10);
                var splitstr = d.split("-");
                var dt = splitstr[2] + "-" + splitstr[1] + "-" + splitstr[0];







                editHTML += '<tr><td>' + sl + '</td>';

                editHTML += '<td> <div class="row"> <div class="col-sm-10">' + dt + '</div><div class="col-sm-2" style="padding-left: 0; margin-left: -10px;">';
                editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditOfficeDate(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td>';


                editHTML += '<td> <div class="row"> <div class="col-sm-10">' + office.Name + '</div><div class="col-sm-2" style="padding-left: 0; margin-left: -10px;">';
                editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditOfficeName(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td><td>';
                for (var j = 0; j < data[0].length; j++) {
                    var soi = data[2].find(x => x.Id == data[0][j].StoreOutItemId);
                    if (data[1][i].Id == data[0][j].SpecialMenuParentId) {


                        editHTML += '<div class="row"><div class="col-sm-6">' + soi.Name + '</div><div class="col-sm-2">' + data[0][j].UnitOrdered + '</div>';
                        editHTML += '<div class="col-sm-2"><button type="button" rel="tooltip" style="padding:2px;" onclick="EditOffice(' + data[0][j].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div>';
                        editHTML += '<div class="col-sm-2"><button type="button" rel="tooltip" style="padding:2px;" onclick="DeleteOffice(' + data[0][j].Id + ')" class="btn"><i class="fas fa-trash"></i></button></div></div>';

                    }

                }



                editHTML += '</td><td><button onclick="SpecialTempPrint(' + data[1][i].Id + ')" class="btn btn-white btn-round btn-just-icon">';
                editHTML += '<i class="material-icons">print</i><div class="ripple-container"></div> </button></td></tr>';


            }


            editHTML += '</tbody></table>'

            $('#OfficeEditOption').hide();
            $('#OfficeNameEditOption').hide();
            $('#OfficeDateEditOption').hide();
            $('#AfterOfficeDateEdit').hide();
            $('#AfterOfficeNameEdit').hide();
            $('#AfterOfficeEdit').hide();
            $('#OfficeDB').hide();
            $('#OfficeInput').hide();





            $('#AfterOfficeBack').show();



            document.getElementById("AfterOfficeBack").innerHTML = editHTML;
            //document.getElementById('ei').value = itmnm;
            //document.getElementById('eiq').value = itmq;





        },
        error: function (xhr, status, error) {
            alert("Error");
        }
    });









}


function EditParty(id) {
    console.log(id);
    $.ajax({
        url: '@Url.Action("GetSpecialMenuEdit","AdminShowOrder")',
        dataType: "json",
        data: {},
        success: function (data) {
            console.log(data);

            $('#PartyDB').hide();
            var editHTML = "";
            var sl = 0;
            var itmnm = "";
            var itmq = 0;


            editHTML += '<table class="table table-striped table-bordered table-hover">';
            editHTML += ' <thead><tr><th>#</th><th>Date</th><th>Name</th><th>Item</th> ';
            editHTML += ' </tr>  </thead> <tbody>'
            for (var i = 0; i < data[1].length; i++) {
                sl++;

                var user = data[3].find(x => x.Id == data[1][i].UserId);
                var d = data[1][i].OrderDate.slice(0, 10);
                var splitstr = d.split("-");
                var dt = splitstr[2] + "-" + splitstr[1] + "-" + splitstr[0];

                editHTML += '<tr><td>' + sl + '</td><td>' + dt + '</td><td>' + user.BUPFullName + '</td><td>';
                for (var j = 0; j < data[0].length; j++) {
                    var soi = data[2].find(x => x.Id == data[0][j].StoreOutItemId);
                    if (data[1][i].Id == data[0][j].SpecialMenuParentId) {
                        if (data[0][j].Id == id) {

                            itmnm = soi.Name;
                            itmq = data[0][j].UnitOrdered;

                            editHTML += '<div class="row"><div class="col-sm-8"><div class="form-group"><input type="text" name="ei" id="ei" class="form-control" value="" /></div></div>';
                            editHTML += '<div id="edititembox"></div>';
                            editHTML += '<div class="col-sm-3"><input type="text" name="eiq" id="eiq" class="form-control" value="" /></div>';
                            editHTML += '<div class="col-sm-1"><button type="button" style="padding:2px;" rel="tooltip" onclick="SaveParty(' + id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div>';
                            // document.getElementById('ei').value = product(2, 3);
                        }
                        else {
                            editHTML += '<div class="row"><div class="col-sm-8">' + soi.Name + '</div><div class="col-sm-3">' + data[0][j].UnitOrdered + '</div>';
                            editHTML += '<div class="col-sm-1"></div></div>';
                        }
                    }

                }



                editHTML += '</td></tr>';
            }


            editHTML += '</tbody></table>'

            editHTML += '<div class="row"> <div class="col-md-9"></div><div class="col-md-3">';
            editHTML += ' <button id="myBtn" rel="tooltip" style="padding-left:15px;padding-right:15px;padding-top:10px;padding-bottom:10px;" onclick="CloseParty()" class="btn">Back</button>';
            editHTML += '</div></div>';











            $('#PartyEditOption').show();
            $('#PartyPersonEditOption').hide();
            $('#PartyDateEditOption').hide();
            $('#AfterPartyDateEdit').hide();
            $('#AfterPartyNameEdit').hide();
            $('#AfterPartyEdit').hide();
            $('#PartyDB').hide();
            $('#PartyInput').hide();


            $('#AfterBack').hide();



            document.getElementById("PartyEditOption").innerHTML = editHTML;
            document.getElementById('ei').value = itmnm;
            document.getElementById('eiq').value = itmq;
            $("#ei").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '@Url.Action("GetSearchItem","AdminShowOrder")',
                        dataType: "json",
                        data: { search: $("#ei").val() },
                        success: function (data) {
                            console.log('Hiiii');
                            console.log(data.length);
                            console.log(data);
                            var a = [];
                            for (var i = 0; i < data.length; i++) {

                                a.push(data[i].name);
                            }
                            //console.log(a);

                            response(a);
                            $("#edititembox").show();

                        },
                        error: function (xhr, status, error) {
                            alert("Error");
                        }
                    });
                }
            });





        },
        error: function (xhr, status, error) {
            alert("Error");
        }
    });

}


function EditPartyDate(id) {
    console.log(id);
    $.ajax({
        url: '@Url.Action("GetSpecialMenuEdit","AdminShowOrder")',
        dataType: "json",
        data: {},
        success: function (data) {
            console.log(data);

            $('#PartyDB').hide();
            var editHTML = "";
            var sl = 0;
            var orddate = "";
            var itmq = 0;


            editHTML += '<table class="table table-striped table-bordered table-hover">';
            editHTML += ' <thead><tr><th>#</th><th>Date</th><th>Name</th><th>Item</th> ';
            editHTML += '  </tr>  </thead> <tbody>'
            for (var i = 0; i < data[1].length; i++) {
                sl++;

                var user = data[3].find(x => x.Id == data[1][i].UserId);
                console.log(user);

                editHTML += '<tr><td>' + sl + '</td>';


                if (data[1][i].Id == id) {
                    editHTML += '<td><div class="row"><div class="col-sm-8"><input type="text" name="eod" id="eod" class="form-control" value="" /></div>';
                    editHTML += '<div class="col-sm-2"><button type="button" style="padding:5px;" rel="tooltip" onclick="SavePartyDate(' + id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td>';
                    orddate = data[1][i].OrderDate.toString();


                }
                else {
                    var d = data[1][i].OrderDate.slice(0, 10);
                    var splitstr = d.split("-");
                    editHTML += '<td>' + splitstr[2] + "-" + splitstr[1] + "-" + splitstr[0] + '</td>';


                }


                editHTML += '<td>' + user.BUPFullName + '</td><td>';

                for (var j = 0; j < data[0].length; j++) {
                    var soi = data[2].find(x => x.Id == data[0][j].StoreOutItemId);
                    if (data[1][i].Id == data[0][j].SpecialMenuParentId) {

                        editHTML += '<div class="row"><div class="col-sm-8">' + soi.Name + '</div><div class="col-sm-3">' + data[0][j].UnitOrdered + '</div>';
                        editHTML += '<div class="col-sm-1"></div></div>';

                    }

                }



                editHTML += '</td></tr>';
            }


            editHTML += '</tbody></table>'

            editHTML += '<div class="row"> <div class="col-md-9"></div><div class="col-md-3">';
            editHTML += ' <button id="myBtn" rel="tooltip" style="padding-left:15px;padding-right:15px;padding-top:10px;padding-bottom:10px;" onclick="CloseParty()" class="btn">Back</button>';
            editHTML += '</div></div>';




            $('#PartyEditOption').hide();
            $('#PartyPersonEditOption').hide();
            $('#AfterPartyDateEdit').hide();
            $('#AfterPartyNameEdit').hide();
            $('#AfterPartyEdit').hide();
            $('#PartyDB').hide();
            $('#PartyInput').hide();

            $('#AfterBack').hide();


            //$('#AfterPartyNameEdit').hide();

            console.log(orddate);
            var b = 'tbgrb rtbrtbbrtbrrrr btbrb';
            var d = orddate.slice(0, 10);
            var splitstr = d.split("-");
            console.log(d);
            $('#PartyDateEditOption').show();


            document.getElementById("PartyDateEditOption").innerHTML = editHTML;
            document.getElementById('eod').value = splitstr[2] + "-" + splitstr[1] + "-" + splitstr[0];


            $(function () {
                $("#eod").datepicker({
                    dateFormat: "dd-mm-yy"
                    , duration: "fast"
                });
            });





        },
        error: function (xhr, status, error) {
            alert("Error");
        }
    });

}




function EditPartyName(id) {
    console.log(id);
    $.ajax({
        url: '@Url.Action("GetSpecialMenuEdit","AdminShowOrder")',
        dataType: "json",
        data: {},
        success: function (data) {
            console.log(data);

            $('#PartyDB').hide();
            var editHTML = "";
            var sl = 0;
            var ordpr = "";
            var itmq = 0;


            editHTML += '<table class="table table-striped table-bordered table-hover">';
            editHTML += ' <thead><tr><th>#</th><th>Date</th><th>Name</th><th>Item</th> ';
            editHTML += '</tr>  </thead> <tbody>'
            for (var i = 0; i < data[1].length; i++) {
                sl++;

                var user = data[3].find(x => x.Id == data[1][i].UserId);
                console.log(user);

                var d = data[1][i].OrderDate.slice(0, 10);
                var splitstr = d.split("-");

                editHTML += '<tr><td>' + sl + '</td>';
                editHTML += '<td>' + splitstr[2] + "-" + splitstr[1] + "-" + splitstr[0] + '</td>';



                if (data[1][i].Id == id) {
                    editHTML += '<td><div class="row"><div class="col-sm-8"><input type="text" name="eop" id="eop" class="form-control" value="" /></div>';
                    editHTML += '<div id="eopbox"></div>';
                    editHTML += '<div class="col-sm-2"><button type="button" style="padding:5px;" rel="tooltip" onclick="SavePartyName(' + id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td>';
                    ordpr = user.BUPFullName;


                }
                else {
                    editHTML += '<td>' + user.BUPFullName + '</td>';



                }

                editHTML += '<td>';

                for (var j = 0; j < data[0].length; j++) {
                    var soi = data[2].find(x => x.Id == data[0][j].StoreOutItemId);
                    if (data[1][i].Id == data[0][j].SpecialMenuParentId) {

                        editHTML += '<div class="row"><div class="col-sm-8">' + soi.Name + '</div><div class="col-sm-2">' + data[0][j].UnitOrdered + '</div>';
                        editHTML += '<div class="col-sm-2"></div></div>';

                    }

                }



                editHTML += '</td></tr>';
            }


            editHTML += '</tbody></table>'

            editHTML += '<div class="row"> <div class="col-md-9"></div><div class="col-md-3">';
            editHTML += ' <button id="myBtn" rel="tooltip" style="padding-left:15px;padding-right:15px;padding-top:10px;padding-bottom:10px;" onclick="CloseParty()" class="btn">Back</button>';
            editHTML += '</div></div>';




            $('#PartyEditOption').hide();
            $('#PartyDateEditOption').hide();
            $('#AfterPartyDateEdit').hide();
            $('#AfterPartyNameEdit').hide();
            $('#AfterPartyEdit').hide();
            $('#PartyDB').hide();
            $('#PartyInput').hide();

            $('#AfterBack').hide();



            $('#PartyPersonEditOption').show();




            document.getElementById("PartyPersonEditOption").innerHTML = editHTML;
            document.getElementById('eop').value = ordpr;


            $("#eop").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '@Url.Action("GetSearchPerson","AdminShowOrder")',
                        dataType: "json",
                        data: { search: $("#eop").val() },
                        success: function (data) {
                            console.log('Hiiii');
                            console.log(data.length);
                            console.log(data);
                            var a = [];
                            for (var i = 0; i < data.length; i++) {

                                a.push(data[i].bupFullName);
                            }
                            //console.log(a);

                            response(a);
                            $("#eopbox").show();

                        },
                        error: function (xhr, status, error) {
                            alert("Error");
                        }
                    });
                }
            });





        },
        error: function (xhr, status, error) {
            alert("Error");
        }
    });

}






function EditOffice(id) {
    console.log(id);
    $.ajax({
        url: '@Url.Action("GetSpecialOfficeMenuEdit","AdminShowOrder")',
        dataType: "json",
        data: {},
        success: function (data) {
            console.log(data);

            var editHTML = "";
            var sl = 0;
            var itmnm = "";
            var itmq = 0;


            editHTML += '<table class="table table-striped table-bordered table-hover">';
            editHTML += ' <thead><tr><th>#</th><th>Date</th><th>Name</th><th>Item</th> ';
            editHTML += '  </tr>  </thead> <tbody>'
            for (var i = 0; i < data[1].length; i++) {
                sl++;

                var office = data[3].find(x => x.Id == data[1][i].OfficeId);
                var d = data[1][i].OrderDate.slice(0, 10);
                var splitstr = d.split("-");
                var dt = splitstr[2] + "-" + splitstr[1] + "-" + splitstr[0];

                editHTML += '<tr><td>' + sl + '</td><td>' + dt + '</td><td>' + office.Name + '</td><td>';
                for (var j = 0; j < data[0].length; j++) {
                    var soi = data[2].find(x => x.Id == data[0][j].StoreOutItemId);
                    if (data[1][i].Id == data[0][j].SpecialMenuParentId) {
                        if (data[0][j].Id == id) {

                            itmnm = soi.Name;
                            itmq = data[0][j].UnitOrdered;

                            editHTML += '<div class="row"><div class="col-sm-8"><div class="form-group"><input type="text" name="oei" id="oei" class="form-control" value="" /></div></div>';
                            editHTML += '<div id="edititembox"></div>';
                            editHTML += '<div class="col-sm-3"><input type="text" name="oeiq" id="oeiq" class="form-control" value="" /></div>';
                            editHTML += '<div class="col-sm-1"><button type="button" style="padding:2px;" rel="tooltip" onclick="SaveOffice(' + id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div>';
                            // document.getElementById('ei').value = product(2, 3);
                        }
                        else {
                            editHTML += '<div class="row"><div class="col-sm-8">' + soi.Name + '</div><div class="col-sm-3">' + data[0][j].UnitOrdered + '</div>';
                            editHTML += '<div class="col-sm-1"></div></div>';
                        }
                    }

                }



                editHTML += '</td></tr>';
            }


            editHTML += '</tbody></table>';

            editHTML += '<div class="row"> <div class="col-md-9"></div><div class="col-md-3">';
            editHTML += ' <button id="myBtn" rel="tooltip" style="padding-left:15px;padding-right:15px;padding-top:10px;padding-bottom:10px;" onclick="CloseOffice()" class="btn">Back</button>';
            editHTML += '</div></div>';




            $('#OfficeEditOption').show();
            $('#OfficeNameEditOption').hide();
            $('#OfficeDateEditOption').hide();
            $('#AfterOfficeDateEdit').hide();
            $('#AfterOfficeNameEdit').hide();
            $('#AfterOfficeEdit').hide();
            $('#OfficeDB').hide();
            $('#OfficeInput').hide();

            $('#AfterOfficeDelete').hide();

            $('#AfterOfficeBack').hide();
            //document.getElementById("OfficeEditOption")
            document.getElementById("OfficeEditOption").innerHTML = editHTML;
            document.getElementById('oei').value = itmnm;
            document.getElementById('oeiq').value = itmq;
            $("#oei").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '@Url.Action("GetSearchItem","AdminShowOrder")',
                        dataType: "json",
                        data: { search: $("#oei").val() },
                        success: function (data) {
                            console.log('Hiiii');
                            console.log(data.length);
                            console.log(data);
                            var a = [];
                            for (var i = 0; i < data.length; i++) {

                                a.push(data[i].name);
                            }
                            //console.log(a);

                            response(a);
                            $("#editofficeitembox").show();

                        },
                        error: function (xhr, status, error) {
                            alert("Error");
                        }
                    });
                }
            });





        },
        error: function (xhr, status, error) {
            alert("Error");
        }
    });

}


function EditOfficeDate(id) {
    $.ajax({
        url: '@Url.Action("GetSpecialOfficeMenuEdit","AdminShowOrder")',
        dataType: "json",
        data: {},
        success: function (data) {
            console.log(data);

            var editHTML = "";
            var sl = 0;
            var orddate = "";
            var itmq = 0;


            editHTML += '<table class="table table-striped table-bordered table-hover">';
            editHTML += ' <thead><tr><th>#</th><th>Date</th><th>Name</th><th>Item</th> ';
            editHTML += '  </tr>  </thead> <tbody>'
            for (var i = 0; i < data[1].length; i++) {
                sl++;

                var office = data[3].find(x => x.Id == data[1][i].OfficeId);

                editHTML += '<tr><td>' + sl + '</td>';


                if (data[1][i].Id == id) {
                    editHTML += '<td><div class="row"><div class="col-sm-8"><input type="text" name="eofd" id="eofd" class="form-control" value="" /></div>';
                    editHTML += '<div class="col-sm-2"><button type="button" style="padding:5px;" rel="tooltip" onclick="SaveOfficeDate(' + id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td>';
                    orddate = data[1][i].OrderDate.toString();


                }
                else {
                    var d = data[1][i].OrderDate.slice(0, 10);
                    var splitstr = d.split("-");
                    editHTML += '<td>' + splitstr[2] + "-" + splitstr[1] + "-" + splitstr[0] + '</td>';


                }


                editHTML += '<td>' + office.Name + '</td><td>';

                for (var j = 0; j < data[0].length; j++) {
                    var soi = data[2].find(x => x.Id == data[0][j].StoreOutItemId);
                    if (data[1][i].Id == data[0][j].SpecialMenuParentId) {

                        editHTML += '<div class="row"><div class="col-sm-8">' + soi.Name + '</div><div class="col-sm-3">' + data[0][j].UnitOrdered + '</div>';
                        editHTML += '<div class="col-sm-1"></div></div>';

                    }

                }



                editHTML += '</td></tr>';
            }


            editHTML += '</tbody></table>'

            editHTML += '<div class="row"> <div class="col-md-9"></div><div class="col-md-3">';
            editHTML += ' <button id="myBtn" rel="tooltip" style="padding-left:15px;padding-right:15px;padding-top:10px;padding-bottom:10px;" onclick="CloseOffice()" class="btn">Back</button>';
            editHTML += '</div></div>';




            $('#OfficeEditOption').hide();
            $('#OfficeNameEditOption').hide();
            $('#AfterOfficeDateEdit').hide();
            $('#AfterOfficeNameEdit').hide();
            $('#AfterOfficeEdit').hide();
            $('#OfficeDB').hide();
            $('#OfficeInput').hide();
            $('#AfterOfficeDelete').hide();

            $('#AfterOfficeBack').hide();



            var d = orddate.slice(0, 10);
            var splitstr = d.split("-");
            $('#OfficeDateEditOption').show();


            document.getElementById("OfficeDateEditOption").innerHTML = editHTML;
            document.getElementById('eofd').value = splitstr[2] + "-" + splitstr[1] + "-" + splitstr[0];


            $(function () {
                $("#eofd").datepicker({
                    dateFormat: "dd-mm-yy"
                    , duration: "fast"
                });
            });





        },
        error: function (xhr, status, error) {
            alert("Error");
        }
    });

}


function EditOfficeName(id) {
    $.ajax({
        url: '@Url.Action("GetSpecialOfficeMenuEdit","AdminShowOrder")',
        dataType: "json",
        data: {},
        success: function (data) {

            var editHTML = "";
            var sl = 0;
            var ordpr = "";
            var itmq = 0;


            editHTML += '<table class="table table-striped table-bordered table-hover">';
            editHTML += ' <thead><tr><th>#</th><th>Date</th><th>Name</th><th>Item</th> ';
            editHTML += '</tr>  </thead> <tbody>'
            for (var i = 0; i < data[1].length; i++) {
                sl++;

                var office = data[3].find(x => x.Id == data[1][i].OfficeId);

                var d = data[1][i].OrderDate.slice(0, 10);
                var splitstr = d.split("-");

                editHTML += '<tr><td>' + sl + '</td>';
                editHTML += '<td>' + splitstr[2] + "-" + splitstr[1] + "-" + splitstr[0] + '</td>';



                if (data[1][i].Id == id) {
                    editHTML += '<td><div class="row"><div class="col-sm-8"><input type="text" name="eof" id="eof" class="form-control" value="" /></div>';
                    editHTML += '<div id="eofbox"></div>';
                    editHTML += '<div class="col-sm-2"><button type="button" style="padding:5px;" rel="tooltip" onclick="SaveOfficeName(' + id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td>';
                    ordpr = office.Name;


                }
                else {
                    editHTML += '<td>' + office.Name + '</td>';

                }

                editHTML += '<td>';

                for (var j = 0; j < data[0].length; j++) {
                    var soi = data[2].find(x => x.Id == data[0][j].StoreOutItemId);
                    if (data[1][i].Id == data[0][j].SpecialMenuParentId) {

                        editHTML += '<div class="row"><div class="col-sm-8">' + soi.Name + '</div><div class="col-sm-2">' + data[0][j].UnitOrdered + '</div>';
                        editHTML += '<div class="col-sm-2"></div></div>';

                    }

                }



                editHTML += '</td></tr>';
            }


            editHTML += '</tbody></table>'

            editHTML += '<div class="row"> <div class="col-md-9"></div><div class="col-md-3">';
            editHTML += ' <button id="myBtn" rel="tooltip" style="padding-left:15px;padding-right:15px;padding-top:10px;padding-bottom:10px;" onclick="CloseOffice()" class="btn">Back</button>';
            editHTML += '</div></div>';




            $('#OfficeEditOption').hide();
            $('#OfficeDateEditOption').hide();
            $('#AfterOfficeDateEdit').hide();
            $('#AfterOfficeNameEdit').hide();
            $('#AfterOfficeEdit').hide();
            $('#OfficeDB').hide();
            $('#OfficeInput').hide();
            $('#AfterOfficeDelete').hide();

            $('#AfterOfficeBack').hide();



            $('#OfficeNameEditOption').show();




            document.getElementById("OfficeNameEditOption").innerHTML = editHTML;
            document.getElementById('eof').value = ordpr;


            $("#eof").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '@Url.Action("GetSearchOffice","AdminShowOrder")',
                        dataType: "json",
                        data: { search: $("#eof").val() },
                        success: function (data) {
                            console.log('Hiiii');
                            console.log(data.length);
                            console.log(data);
                            var a = [];
                            for (var i = 0; i < data.length; i++) {

                                a.push(data[i].name);
                            }
                            //console.log(a);

                            response(a);
                            $("#eofbox").show();

                        },
                        error: function (xhr, status, error) {
                            alert("Error");
                        }
                    });
                }
            });





        },
        error: function (xhr, status, error) {
            alert("Error");
        }
    });

}





function fetchPerson() {
    $('#FetchPersonOrder').submit();
}
function fetchPerson2() {
    $('#FetchPersonOrder2').submit();
}
function fetchPerson3() {
    $('#FetchPersonOrder3').submit();
}
function fetchPerson4() {
    $('#FetchPersonOrder4').submit();
}

$("#item").on('change input', function () {
    fetchItem();
});
$("#item2").on('change input', function () {
    fetchItem2();
});
$("#item3").on('change input', function () {
    fetchItem3();
});

$("#item4").on('change input', function () {
    fetchItem4();
});
function Print(id, height, width, meal, day, month, year) {


    var mywindow = window.open('', 'PRINT', 'height=' + height + ',width=' + width);
    mywindow.document.write('<html>    ');
    mywindow.document.write('<head> <style>  td {    border-style: solid;border-width: thin;border-color:grey }</style> <head>')


    console.log(day);
    console.log(month);
    console.log(year);



    //mywindow.document.write(headerhtml.toString());
    mywindow.document.write('<body style="text-align: center; padding:1em;">');
    mywindow.document.write('<h2>' + meal + '</h2>');
    mywindow.document.write('<h3>' + day + '.' + month + '.' + year + '</h3>');
    //mywindow.document.write('<table><tr><td>Session </td><td>' + s.toString() + '</td></tr><tr><td>Program </td><td>' + p.toString() + '</td></tr><tr><td>Course </td><td>' + c.toString() + '</td></tr></table></br>');
    //mywindow.document.write('<table><tr><td>Mean Marks </td><td>' + meanmark + '</td></tr><tr><td>Mean Grade Point </td><td>' + meanGradePOint + '</td></tr><tr><td>Faculty Performance Index </td><td>' + facultyPerformanceIndex + '</td></tr><tr><td>Standard Deviation Of Marks </td><td>' + stdDeviationMark + '</td></tr><tr><td>Standard Grade Points </td><td>' + stadGradePoint + '</td></tr><tr><td>Course Evaluation Score </td><td>' + courseEvaluationScore + '</td></tr><tr><td>Threshold Value </td><td>' + parsed.ThresholdValue +'</td></tr><tr><td>CO Level </td><td>'+LblCourseOutcomeValue + '</td></tr></table></br>')
    mywindow.document.write(document.getElementById(id).innerHTML);

    //mywindow.document.write('<table><tr><td>Ammendment   </td><td> ' + amd + '</td></tr><tr><td>Recommendation & Proposal    </td><td>' + parsed.RecommendationAndProposal + '</td></tr><tr><td>Attainments Satisfaction   </td><td>' + atm + '</td></tr><tr><td>Attainment Comments   </td><td>' + parsed.AttainmentsComments + '</td></tr><tr><td>Instructor Comments&Feedback  </td><td>' + parsed.CommentAndFeedback + '</td></tr><tr><td>Recommendation1   </td><td>' + parsed.SuggestionAndRecommendation1 + '</td></tr><tr><td>Recommendation2   </td><td>' + parsed.SuggestionAndRecommendation2 + '</td></tr><tr><td>Recommendation3  </td><td>' + parsed.SuggestionAndRecommendation3 + '</td></tr></table></br>');

    mywindow.document.write('</body></html>');

    mywindow.document.close(); // necessary for IE >= 10
    mywindow.focus(); // necessary for IE >= 10*/

    mywindow.print();
    mywindow.close();


}
function fetchItem() {
    $('#FetchItemOrder').submit();
}
function fetchItem2() {
    $('#FetchItemOrder2').submit();
}
function fetchItem3() {
    $('#FetchItemOrder3').submit();
}
function fetchItem4() {
    $('#FetchItemOrder4').submit();
}
jQuery.noConflict();

$("#officeperson").autocomplete({
    source: function (request, response) {
        $.ajax({
            url: '@Url.Action("GetSearchPerson","AdminShowOrder")',
            dataType: "json",
            data: { search: $("#officeperson").val() },
            success: function (data) {
                console.log('Hiiii');
                console.log(data.length);
                console.log(data);
                var a = [];
                for (var i = 0; i < data.length; i++) {

                    a.push(data[i].bupFullName);
                }
                //console.log(a);

                response(a);
                $("#officepersonbox").show();

            },
            error: function (xhr, status, error) {
                alert("Error");
            }
        });
    }
});
$("#officename").autocomplete({
    source: function (request, response) {
        $.ajax({
            url: '@Url.Action("GetSearchOffice","AdminShowOrder")',
            dataType: "json",
            data: { search: $("#officename").val() },
            success: function (data) {
                console.log('Hiiii');
                console.log(data.length);
                console.log(data);
                var a = [];
                for (var i = 0; i < data.length; i++) {

                    a.push(data[i].name);
                }
                //console.log(a);

                response(a);
                $("#officebox").show();

            },
            error: function (xhr, status, error) {
                alert("Error");
            }
        });
    }
});

$("#partyperson").autocomplete({
    source: function (request, response) {
        $.ajax({
            url: '@Url.Action("GetSearchPerson","AdminShowOrder")',
            dataType: "json",
            data: { search: $("#partyperson").val() },
            success: function (data) {
                console.log('Hiiii');
                console.log(data.length);
                console.log(data);
                var a = [];
                for (var i = 0; i < data.length; i++) {

                    a.push(data[i].bupFullName);
                }
                //console.log(a);

                response(a);
                $("#partypersonbox").show();

            },
            error: function (xhr, status, error) {
                alert("Error");
            }
        });
    }
});

$("#searchInput").autocomplete({
    source: function (request, response) {
        $.ajax({
            url: '@Url.Action("GetSearchPerson","AdminShowOrder")',
            dataType: "json",
            data: { search: $("#searchInput").val() },
            success: function (data) {
                console.log('Hiiii');
                console.log(data);
                var a = [];
                for (var i = 0; i < data.length; i++) {

                    a.push(data[i].bupFullName);
                }
                console.log(a);

                response(a);
                $("#suggesstion-box").show();

            },
            error: function (xhr, status, error) {
                alert("Error");
            }
        });
    }
});


$("#partyitem").autocomplete({
    source: function (request, response) {
        $.ajax({
            url: '@Url.Action("GetSearchItem","AdminShowOrder")',
            dataType: "json",
            data: { search: $("#partyitem").val() },
            success: function (data) {
                console.log('Hiiii');
                console.log(data.length);
                console.log(data);
                var a = [];
                partyItem = [];
                if (data.length == 0) {
                    $("#PriceDiv").css("display", "block");
                    partyItem = [];
                }
                else {
                    $("#PriceDiv").css("display", "none");
                    partyItem = data;
                }
                for (var i = 0; i < data.length; i++) {

                    a.push(data[i].name);
                }
                //console.log(a);

                response(a);
                $("#partyitembox").show();

            },
            error: function (xhr, status, error) {
                alert("Error");
            }
        });
    }
});


$("#officeitem").autocomplete({
    source: function (request, response) {
        $.ajax({
            url: '@Url.Action("GetSearchItem","AdminShowOrder")',
            dataType: "json",
            data: { search: $("#officeitem").val() },
            success: function (data) {
                console.log('Hiiii');
                console.log(data.length);
                console.log(data);
                var a = [];
                officeItem = [];
                if (data.length == 0) {
                    $("#OffPriceDiv").css("display", "block");
                    officeItem = [];
                }
                else {
                    $("#OffPriceDiv").css("display", "none");
                    officeItem = data;
                }
                for (var i = 0; i < data.length; i++) {

                    a.push(data[i].name);
                }
                //console.log(a);

                response(a);
                $("#officeitembox").show();

            },
            error: function (xhr, status, error) {
                alert("Error");
            }
        });
    }
});



function AddPartyItem() {


    var person = document.getElementById("partyperson").value.toString();
    var item = document.getElementById("partyitem").value.toString();
    var date = document.getElementById("partydate").value.toString();

    var q = document.getElementById("piq").value.toString();
    var price = 0;
    console.log('check ' + partyItem.length);
    if (partyItem.length == 0) {
        price = document.getElementById("pr").value.toString();

    }



    $.ajax({
        url: '@Url.Action("AddPartyItem","AdminShowOrder")',
        dataType: "json",
        data: { Person: person, Item: item, Date: date, Quantity: q, Price: price },
        success: function (data) {
            console.log(data[1]);
            var order = "";

            order += "<p>";
            for (var i = 0; i < data[0].length; i++) {

                var sto = data[1].find(x => x.Id == data[0][i].StoreOutItemId);
                console.log(sto);
                order += "  " + sto.Name + " - " + data[0][i].UnitOrdered + " , ";
            }

            order += "</p>"

            document.getElementById("InputDetail").innerHTML = order;
            //console.log(JSON.parse(response.d));
            //console.log('Hiiii');
            //console.log(data.length);
            //console.log(data);
            //var a = [];
            //for (var i = 0; i < data.length; i++) {

            //    a.push(data[i].bupFullName);
            //}
            ////console.log(a);

            //response(a);
            //$("#officepersonbox").show();

        },
        error: function (xhr, status, error) {
            console.log(error);
            alert(error);
        }
    });


    console.log(person + item + q);


}
function AddOfficeItem() {


    var person = document.getElementById("officeperson").value.toString();
    var item = document.getElementById("officeitem").value.toString();
    var q = document.getElementById("oiq").value.toString();
    var office = document.getElementById("officename").value.toString();
    var officeDate = document.getElementById("officedate").value.toString();
    var price = 0;

    if (officeItem.length == 0) {
        price = document.getElementById("offpr").value.toString();
    }


    $.ajax({
        url: '@Url.Action("AddOfficeItem","AdminShowOrder")',
        dataType: "json",
        data: { Person: person, Office: office, Item: item, Date: officeDate, Quantity: q, Price: price },
        success: function (data) {
            console.log(data[1]);
            var order = "";

            order += "<p>";
            for (var i = 0; i < data[0].length; i++) {

                var sto = data[1].find(x => x.Id == data[0][i].StoreOutItemId);
                console.log(sto);
                order += "  " + sto.Name + " - " + data[0][i].UnitOrdered + " , ";
            }

            order += "</p>"

            document.getElementById("OfficeInputDetail").innerHTML = order;
            //console.log(JSON.parse(response.d));
            //console.log('Hiiii');
            //console.log(data.length);
            //console.log(data);
            //var a = [];
            //for (var i = 0; i < data.length; i++) {

            //    a.push(data[i].bupFullName);
            //}
            ////console.log(a);

            //response(a);
            //$("#officepersonbox").show();

        },
        error: function (xhr, status, error) {
            console.log(error);
            alert(error);
        }
    });





    console.log(person + item + q);


}

function SaveParty(Id) {
    console.log('dip');
    // var person = document.getElementById("partyperson").value.toString();
    var item = document.getElementById("ei").value.toString();
    // var date = document.getElementById("partydate").value.toString();

    var q = document.getElementById("eiq").value.toString();

    $.ajax({
        url: '@Url.Action("SaveSpecialMenuEdit","AdminShowOrder")',
        dataType: "json",
        data: { Id: Id, Item: item, Quantity: q },
        success: function (data) {
            console.log(data);

            $('#PartyDB').hide();
            var editHTML = "";
            var sl = 0;
            var itmnm = "";
            var itmq = 0;



            editHTML += '<table class="table table-striped table-bordered table-hover">';
            editHTML += ' <thead><tr><th>#</th><th>Date</th><th>Name</th><th>Item</th> ';
            editHTML += ' <th></th></tr>  </thead> <tbody>'
            for (var i = 0; i < data[1].length; i++) {
                sl++;

                var user = data[3].find(x => x.Id == data[1][i].UserId);
                var d = data[1][i].OrderDate.slice(0, 10);
                var splitstr = d.split("-");
                var dt = splitstr[2] + "-" + splitstr[1] + "-" + splitstr[0];

                editHTML += '<tr><td>' + sl + '</td>';

                editHTML += '<td> <div class="row"> <div class="col-sm-10">' + dt + '</div><div class="col-sm-2" style="padding-left: 0; margin-left: -10px;">';
                editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditPartyDate(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td>';


                editHTML += '<td> <div class="row"> <div class="col-sm-10">' + user.BUPFullName + '</div><div class="col-sm-2" style="padding-left: 0; margin-left: -10px;">';
                editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditPartyName(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td><td>';
                for (var j = 0; j < data[0].length; j++) {
                    var soi = data[2].find(x => x.Id == data[0][j].StoreOutItemId);
                    if (data[1][i].Id == data[0][j].SpecialMenuParentId) {


                        editHTML += '<div class="row"><div class="col-sm-6">' + soi.Name + '</div><div class="col-sm-2">' + data[0][j].UnitOrdered + '</div>';
                        editHTML += '<div class="col-sm-2"><button type="button" rel="tooltip" style="padding:2px;" onclick="EditParty(' + data[0][j].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div>';
                        editHTML += '<div class="col-sm-2"><button type="button" rel="tooltip" style="padding:2px;" onclick="DeleteParty(' + data[0][j].Id + ')" class="btn"><i class="fas fa-trash"></i></button></div></div>';


                    }

                }



                editHTML += '</td><td><button onclick="SpecialTempPrint(' + data[1][i].Id + ')" class="btn btn-white btn-round btn-just-icon">';
                editHTML += '<i class="material-icons">print</i><div class="ripple-container"></div> </button></td></tr>';


            }


            editHTML += '</tbody></table>'




            $('#PartyInput').hide();
            $('#PartyDB').hide();
            $('#PartyEditOption').hide();

            $('#AfterPartyEdit').show();



            document.getElementById("AfterPartyEdit").innerHTML = editHTML;
            //document.getElementById('ei').value = itmnm;
            //document.getElementById('eiq').value = itmq;





        },
        error: function (xhr, status, error) {
            alert("Error");
        }
    });

    //console.log(item + q);

}


function SavePartyDate(Id) {
    console.log('dip');
    // var person = document.getElementById("partyperson").value.toString();
    var date = document.getElementById("eod").value.toString();
    // var date = document.getElementById("partydate").value.toString();


    $.ajax({
        url: '@Url.Action("SaveSpecialMenuDateEdit","AdminShowOrder")',
        dataType: "json",
        data: { Date: date, Id: Id },
        success: function (data) {
            console.log(data);

            $('#PartyDB').hide();
            var editHTML = "";
            var sl = 0;
            var itmnm = "";
            var itmq = 0;


            editHTML += '<table class="table table-striped table-bordered table-hover">';
            editHTML += ' <thead><tr><th>#</th><th>Date</th><th>Name</th><th>Item</th> ';
            editHTML += '<th></th>  </tr>  </thead> <tbody>'
            for (var i = 0; i < data[1].length; i++) {
                sl++;

                var user = data[3].find(x => x.Id == data[1][i].UserId);
                var d = data[1][i].OrderDate.slice(0, 10);
                var splitstr = d.split("-");
                var dt = splitstr[2] + "-" + splitstr[1] + "-" + splitstr[0];







                editHTML += '<tr><td>' + sl + '</td>';

                editHTML += '<td> <div class="row"> <div class="col-sm-10">' + dt + '</div><div class="col-sm-2" style="padding-left: 0; margin-left: -10px;">';
                editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditPartyDate(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td>';


                editHTML += '<td> <div class="row"> <div class="col-sm-10">' + user.BUPFullName + '</div><div class="col-sm-2" style="padding-left: 0; margin-left: -10px;">';
                editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditPartyName(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td><td>';
                for (var j = 0; j < data[0].length; j++) {
                    var soi = data[2].find(x => x.Id == data[0][j].StoreOutItemId);
                    if (data[1][i].Id == data[0][j].SpecialMenuParentId) {


                        editHTML += '<div class="row"><div class="col-sm-6">' + soi.Name + '</div><div class="col-sm-2">' + data[0][j].UnitOrdered + '</div>';
                        editHTML += '<div class="col-sm-2"><button type="button" rel="tooltip" style="padding:2px;" onclick="EditParty(' + data[0][j].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div>';
                        editHTML += '<div class="col-sm-2"><button type="button" rel="tooltip" style="padding:2px;" onclick="DeleteParty(' + data[0][j].Id + ')" class="btn"><i class="fas fa-trash"></i></button></div></div>';

                    }

                }



                editHTML += '</td><td><button onclick="SpecialTempPrint(' + data[1][i].Id + ')" class="btn btn-white btn-round btn-just-icon">';
                editHTML += '<i class="material-icons">print</i><div class="ripple-container"></div> </button></td></tr>';


            }


            editHTML += '</tbody></table>'




            $('#PartyInput').hide();
            $('#PartyDB').hide();
            $('#PartyEditOption').hide();
            $('#PartyDateEditOption').hide();


            $('#AfterPartyDateEdit').show();


            document.getElementById("AfterPartyDateEdit").innerHTML = editHTML;
            //document.getElementById('ei').value = itmnm;
            //document.getElementById('eiq').value = itmq;





        },
        error: function (xhr, status, error) {
            alert("Error");
        }
    });

    //console.log(item + q);

}



function SavePartyName(Id) {
    console.log('dip');
    // var person = document.getElementById("partyperson").value.toString();
    var person = document.getElementById("eop").value.toString();
    // var date = document.getElementById("partydate").value.toString();


    $.ajax({
        url: '@Url.Action("SaveSpecialMenuNameEdit","AdminShowOrder")',
        dataType: "json",
        data: { Person: person, Id: Id },
        success: function (data) {
            console.log(data);

            $('#PartyDB').hide();
            var editHTML = "";
            var sl = 0;
            var itmnm = "";
            var itmq = 0;


            editHTML += '<table class="table table-striped table-bordered table-hover">';
            editHTML += ' <thead><tr><th>#</th><th>Date</th><th>Name</th><th>Item</th> ';
            editHTML += ' <th></th></tr>  </thead> <tbody>'
            for (var i = 0; i < data[1].length; i++) {
                sl++;

                var user = data[3].find(x => x.Id == data[1][i].UserId);
                var d = data[1][i].OrderDate.slice(0, 10);
                var splitstr = d.split("-");
                var dt = splitstr[2] + "-" + splitstr[1] + "-" + splitstr[0];

                editHTML += '<tr><td>' + sl + '</td>';

                editHTML += '<td> <div class="row"> <div class="col-sm-10">' + dt + '</div><div class="col-sm-2" style="padding-left: 0; margin-left: -10px;">';
                editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditPartyDate(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td>';


                editHTML += '<td> <div class="row"> <div class="col-sm-10">' + user.BUPFullName + '</div><div class="col-sm-2" style="padding-left: 0; margin-left: -10px;">';
                editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditPartyName(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td><td>';
                for (var j = 0; j < data[0].length; j++) {
                    var soi = data[2].find(x => x.Id == data[0][j].StoreOutItemId);
                    if (data[1][i].Id == data[0][j].SpecialMenuParentId) {


                        editHTML += '<div class="row"><div class="col-sm-6">' + soi.Name + '</div><div class="col-sm-2">' + data[0][j].UnitOrdered + '</div>';
                        editHTML += '<div class="col-sm-2"><button type="button" rel="tooltip" style="padding:2px;" onclick="EditParty(' + data[0][j].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div>';
                        editHTML += '<div class="col-sm-2"><button type="button" rel="tooltip" style="padding:2px;" onclick="DeleteParty(' + data[0][j].Id + ')" class="btn"><i class="fas fa-trash"></i></button></div></div>';


                    }

                }



                editHTML += '</td><td><button onclick="SpecialTempPrint(' + data[1][i].Id + ')" class="btn btn-white btn-round btn-just-icon">';
                editHTML += '<i class="material-icons">print</i><div class="ripple-container"></div> </button></td></tr>';


            }


            editHTML += '</tbody></table>'




            $('#PartyInput').hide();
            $('#PartyDB').hide();
            $('#PartyEditOption').hide();
            $('#PartyDateEditOption').hide();
            $('#PartyPersonEditOption').hide();
            $('#AfterBack').hide();

            $('#AfterPartyNameEdit').show();







            document.getElementById("AfterPartyNameEdit").innerHTML = editHTML;
            //document.getElementById('ei').value = itmnm;
            //document.getElementById('eiq').value = itmq;





        },
        error: function (xhr, status, error) {
            alert("Error");
        }
    });

    //console.log(item + q);

}



function DeleteParty(Id) {

    // var date = document.getElementById("partydate").value.toString();


    $.ajax({
        url: '@Url.Action("DeleteSpecialMenu","AdminShowOrder")',
        dataType: "json",
        data: { Id: Id },
        success: function (data) {
            console.log(data);

            $('#PartyDB').hide();
            var editHTML = "";
            var sl = 0;
            var itmnm = "";
            var itmq = 0;


            editHTML += '<table class="table table-striped table-bordered table-hover">';
            editHTML += ' <thead><tr><th>#</th><th>Date</th><th>Name</th><th>Item</th> ';
            editHTML += ' <th></th> </tr>  </thead> <tbody>'
            for (var i = 0; i < data[1].length; i++) {
                sl++;

                var user = data[3].find(x => x.Id == data[1][i].UserId);
                var d = data[1][i].OrderDate.slice(0, 10);
                var splitstr = d.split("-");
                var dt = splitstr[2] + "-" + splitstr[1] + "-" + splitstr[0];







                editHTML += '<tr><td>' + sl + '</td>';

                editHTML += '<td> <div class="row"> <div class="col-sm-10">' + dt + '</div><div class="col-sm-2" style="padding-left: 0; margin-left: -10px;">';
                editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditPartyDate(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td>';


                editHTML += '<td> <div class="row"> <div class="col-sm-10">' + user.BUPFullName + '</div><div class="col-sm-2" style="padding-left: 0; margin-left: -10px;">';
                editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditPartyName(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td><td>';
                for (var j = 0; j < data[0].length; j++) {
                    var soi = data[2].find(x => x.Id == data[0][j].StoreOutItemId);
                    if (data[1][i].Id == data[0][j].SpecialMenuParentId) {


                        editHTML += '<div class="row"><div class="col-sm-6">' + soi.Name + '</div><div class="col-sm-2">' + data[0][j].UnitOrdered + '</div>';
                        editHTML += '<div class="col-sm-2"><button type="button" rel="tooltip" style="padding:2px;" onclick="EditParty(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div>';
                        editHTML += '<div class="col-sm-2"><button type="button" rel="tooltip" style="padding:2px;" onclick="DeleteParty(' + data[1][i].Id + ')" class="btn"><i class="fas fa-trash"></i></button></div></div>';

                    }

                }



                editHTML += '</td><td><button onclick="SpecialTempPrint(' + data[1][i].Id + ')" class="btn btn-white btn-round btn-just-icon">';
                editHTML += '<i class="material-icons">print</i><div class="ripple-container"></div> </button></td></tr>';


            }


            editHTML += '</tbody></table>'




            $('#PartyInput').hide();
            $('#PartyDB').hide();
            $('#PartyEditOption').hide();
            $('#PartyDateEditOption').hide();





            document.getElementById("AfterPartyDelete").innerHTML = editHTML;
            //document.getElementById('ei').value = itmnm;
            //document.getElementById('eiq').value = itmq;





        },
        error: function (xhr, status, error) {
            alert("Error");
        }
    });

    //console.log(item + q);

}







function SaveOffice(Id) {
    console.log('dip');
    // var person = document.getElementById("partyperson").value.toString();
    var item = document.getElementById("oei").value.toString();
    // var date = document.getElementById("partydate").value.toString();

    var q = document.getElementById("oeiq").value.toString();

    $.ajax({
        url: '@Url.Action("SaveSpecialOfficeMenuEdit","AdminShowOrder")',
        dataType: "json",
        data: { Id: Id, Item: item, Quantity: q },
        success: function (data) {
            console.log(data);

            $('#OfficeDB').hide();
            var editHTML = "";
            var sl = 0;
            var itmnm = "";
            var itmq = 0;


            editHTML += '<table class="table table-striped table-bordered table-hover">';
            editHTML += ' <thead><tr><th>#</th><th>Date</th><th>Name</th><th>Item</th> ';
            editHTML += '<th></th></tr>  </thead> <tbody>'
            for (var i = 0; i < data[1].length; i++) {
                sl++;

                var office = data[3].find(x => x.Id == data[1][i].OfficeId);


                var d = data[1][i].OrderDate.slice(0, 10);
                var splitstr = d.split("-");
                var dt = splitstr[2] + "-" + splitstr[1] + "-" + splitstr[0];

                editHTML += '<tr><td>' + sl + '</td>';
                editHTML += '<td> <div class="row"> <div class="col-sm-10">' + dt + '</div><div class="col-sm-2" style="padding-left: 0; margin-left: -10px;">';
                editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditOfficeDate(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td>';


                editHTML += '<td> <div class="row"> <div class="col-sm-10">' + office.Name + '</div><div class="col-sm-2" style="padding-left: 0; margin-left: -10px;">';
                editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditOfficeName(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td><td>';


                for (var j = 0; j < data[0].length; j++) {
                    var soi = data[2].find(x => x.Id == data[0][j].StoreOutItemId);
                    if (data[1][i].Id == data[0][j].SpecialMenuParentId) {


                        editHTML += '<div class="row"><div class="col-sm-6">' + soi.Name + '</div><div class="col-sm-2">' + data[0][j].UnitOrdered + '</div>';
                        editHTML += '<div class="col-sm-2"><button type="button" rel="tooltip" style="padding:2px;" onclick="EditOffice(' + data[0][j].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div>';
                        editHTML += '<div class="col-sm-2"><button type="button" rel="tooltip" style="padding:2px;" onclick="DeleteOffice(' + data[0][j].Id + ')" class="btn"><i class="fas fa-trash"></i></button></div></div>';


                    }

                }



                editHTML += '</td><td><button onclick="SpecialTempPrint(' + data[1][i].Id + ')" class="btn btn-white btn-round btn-just-icon">';
                editHTML += '<i class="material-icons">print</i><div class="ripple-container"></div> </button></td></tr>';


            }


            editHTML += '</tbody></table>'





            $('#OfficeInput').hide();
            $('#OfficeDB').hide();
            $('#OfficeEditOption').hide();
            $('#OfficeDateEditOption').hide();
            $('#OfficeNameEditOption').hide();
            $('#AfterOfficeBack').hide();

            $('#AfterOfficeDateEdit').hide();
            $('#AfterOfficeNameEdit').hide();




            $('#AfterOfficeEdit').show();
            document.getElementById("AfterOfficeEdit").innerHTML = editHTML;
            //document.getElementById('ei').value = itmnm;
            //document.getElementById('eiq').value = itmq;





        },
        error: function (xhr, status, error) {
            alert("Error");
        }
    });

    //console.log(item + q);

}


function SaveOfficeDate(Id) {
    // var person = document.getElementById("partyperson").value.toString();
    var date = document.getElementById("eofd").value.toString();
    // var date = document.getElementById("partydate").value.toString();


    $.ajax({
        url: '@Url.Action("SaveSpecialMenuDateEdit","AdminShowOrder")',
        dataType: "json",
        data: { Date: date, Id: Id },
        success: function (data) {
            console.log(data);

            $('#PartyDB').hide();
            var editHTML = "";
            var sl = 0;
            var itmnm = "";
            var itmq = 0;


            editHTML += '<table class="table table-striped table-bordered table-hover">';
            editHTML += ' <thead><tr><th>#</th><th>Date</th><th>Name</th><th>Item</th> ';
            editHTML += ' <th></th> </tr>  </thead> <tbody>'
            for (var i = 0; i < data[1].length; i++) {
                sl++;

                var office = data[3].find(x => x.Id == data[1][i].OfficeId);
                var d = data[1][i].OrderDate.slice(0, 10);
                var splitstr = d.split("-");
                var dt = splitstr[2] + "-" + splitstr[1] + "-" + splitstr[0];







                editHTML += '<tr><td>' + sl + '</td>';

                editHTML += '<td> <div class="row"> <div class="col-sm-10">' + dt + '</div><div class="col-sm-2" style="padding-left: 0; margin-left: -10px;">';
                editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditOfficeDate(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td>';


                editHTML += '<td> <div class="row"> <div class="col-sm-10">' + office.Name + '</div><div class="col-sm-2" style="padding-left: 0; margin-left: -10px;">';
                editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditOfficeName(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td><td>';
                for (var j = 0; j < data[0].length; j++) {
                    var soi = data[2].find(x => x.Id == data[0][j].StoreOutItemId);
                    if (data[1][i].Id == data[0][j].SpecialMenuParentId) {


                        editHTML += '<div class="row"><div class="col-sm-6">' + soi.Name + '</div><div class="col-sm-2">' + data[0][j].UnitOrdered + '</div>';
                        editHTML += '<div class="col-sm-2"><button type="button" rel="tooltip" style="padding:2px;" onclick="EditOffice(' + data[0][j].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div>';
                        editHTML += '<div class="col-sm-2"><button type="button" rel="tooltip" style="padding:2px;" onclick="DeleteOffice(' + data[0][j].Id + ')" class="btn"><i class="fas fa-trash"></i></button></div></div>';

                    }

                }



                editHTML += '</td><td><button onclick="SpecialTempPrint(' + data[1][i].Id + ')" class="btn btn-white btn-round btn-just-icon">';
                editHTML += '<i class="material-icons">print</i><div class="ripple-container"></div> </button></td></tr>';


            }


            editHTML += '</tbody></table>'





            $('#OfficeInput').hide();
            $('#OfficeDB').hide();
            $('#OfficeEditOption').hide();
            $('#OfficeDateEditOption').hide();
            $('#OfficeNameEditOption').hide();
            $('#AfterOfficeBack').hide();

            $('#AfterOfficeEdit').hide();
            $('#AfterOfficeNameEdit').hide();


            $('#AfterOfficeDateEdit').show();


            document.getElementById("AfterOfficeDateEdit").innerHTML = editHTML;
            //document.getElementById('ei').value = itmnm;
            //document.getElementById('eiq').value = itmq;





        },
        error: function (xhr, status, error) {
            alert("Error");
        }
    });

    //console.log(item + q);

}


function SaveOfficeName(Id) {
    // var person = document.getElementById("partyperson").value.toString();
    var person = document.getElementById("eof").value.toString();
    // var date = document.getElementById("partydate").value.toString();


    $.ajax({
        url: '@Url.Action("SaveSpecialMenuNameEdit","AdminShowOrder")',
        dataType: "json",
        data: { Person: person, Id: Id },
        success: function (data) {
            console.log(data);

            var editHTML = "";
            var sl = 0;
            var itmnm = "";
            var itmq = 0;


            editHTML += '<table class="table table-striped table-bordered table-hover">';
            editHTML += ' <thead><tr><th>#</th><th>Date</th><th>Name</th><th>Item</th> ';
            editHTML += ' <th></th></tr>  </thead> <tbody>'
            for (var i = 0; i < data[1].length; i++) {
                sl++;

                var office = data[3].find(x => x.Id == data[1][i].OfficeId);
                var d = data[1][i].OrderDate.slice(0, 10);
                var splitstr = d.split("-");
                var dt = splitstr[2] + "-" + splitstr[1] + "-" + splitstr[0];

                editHTML += '<tr><td>' + sl + '</td>';

                editHTML += '<td> <div class="row"> <div class="col-sm-10">' + dt + '</div><div class="col-sm-2" style="padding-left: 0; margin-left: -10px;">';
                editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditOfficeDate(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td>';


                editHTML += '<td> <div class="row"> <div class="col-sm-10">' + office.Name + '</div><div class="col-sm-2" style="padding-left: 0; margin-left: -10px;">';
                editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditOfficeName(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td><td>';
                for (var j = 0; j < data[0].length; j++) {
                    var soi = data[2].find(x => x.Id == data[0][j].StoreOutItemId);
                    if (data[1][i].Id == data[0][j].SpecialMenuParentId) {


                        editHTML += '<div class="row"><div class="col-sm-6">' + soi.Name + '</div><div class="col-sm-2">' + data[0][j].UnitOrdered + '</div>';
                        editHTML += '<div class="col-sm-2"><button type="button" rel="tooltip" style="padding:2px;" onclick="EditOffice(' + data[0][j].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div>';
                        editHTML += '<div class="col-sm-2"><button type="button" rel="tooltip" style="padding:2px;" onclick="DeleteOffice(' + data[0][j].Id + ')" class="btn"><i class="fas fa-trash"></i></button></div></div>';


                    }

                }



                editHTML += '</td><td><button onclick="SpecialTempPrint(' + data[1][i].Id + ')" class="btn btn-white btn-round btn-just-icon">';
                editHTML += '<i class="material-icons">print</i><div class="ripple-container"></div> </button></td></tr>';


            }


            editHTML += '</tbody></table>'




            $('#OfficeInput').hide();
            $('#OfficeDB').hide();
            $('#OfficeEditOption').hide();
            $('#OfficeDateEditOption').hide();
            $('#OfficeNameEditOption').hide();
            $('#AfterOfficeBack').hide();

            $('#AfterOfficeEdit').hide();
            $('#AfterOfficeDateEdit').hide();

            $('#AfterOfficeNameEdit').show();







            document.getElementById("AfterOfficeNameEdit").innerHTML = editHTML;
            //document.getElementById('ei').value = itmnm;
            //document.getElementById('eiq').value = itmq;





        },
        error: function (xhr, status, error) {
            alert("Error");
        }
    });

    //console.log(item + q);

}


function DeleteOffice(Id) {

    // var date = document.getElementById("partydate").value.toString();


    $.ajax({
        url: '@Url.Action("DeleteSpecialMenu","AdminShowOrder")',
        dataType: "json",
        data: { Id: Id },
        success: function (data) {
            console.log(data);

            var editHTML = "";
            var sl = 0;
            var itmnm = "";
            var itmq = 0;


            editHTML += '<table class="table table-striped table-bordered table-hover">';
            editHTML += ' <thead><tr><th>#</th><th>Date</th><th>Name</th><th>Item</th> ';
            editHTML += '<th></th>  </tr>  </thead> <tbody>'
            for (var i = 0; i < data[1].length; i++) {
                sl++;

                var office = data[3].find(x => x.Id == data[1][i].OfficeId);
                var d = data[1][i].OrderDate.slice(0, 10);
                var splitstr = d.split("-");
                var dt = splitstr[2] + "-" + splitstr[1] + "-" + splitstr[0];







                editHTML += '<tr><td>' + sl + '</td>';

                editHTML += '<td> <div class="row"> <div class="col-sm-10">' + dt + '</div><div class="col-sm-2" style="padding-left: 0; margin-left: -10px;">';
                editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditOfficeDate(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td>';


                editHTML += '<td> <div class="row"> <div class="col-sm-10">' + office.Name + '</div><div class="col-sm-2" style="padding-left: 0; margin-left: -10px;">';
                editHTML += ' <button type="button" style="padding:2px;" rel="tooltip" onclick="EditOfficeName(' + data[1][i].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div></div></td><td>';
                for (var j = 0; j < data[0].length; j++) {
                    var soi = data[2].find(x => x.Id == data[0][j].StoreOutItemId);
                    if (data[1][i].Id == data[0][j].SpecialMenuParentId) {


                        editHTML += '<div class="row"><div class="col-sm-6">' + soi.Name + '</div><div class="col-sm-2">' + data[0][j].UnitOrdered + '</div>';
                        editHTML += '<div class="col-sm-2"><button type="button" rel="tooltip" style="padding:2px;" onclick="EditOffice(' + data[0][j].Id + ')" class="btn"><i class="fas fa-edit"></i></button></div>';
                        editHTML += '<div class="col-sm-2"><button type="button" rel="tooltip" style="padding:2px;" onclick="DeleteOffice(' + data[0][j].Id + ')" class="btn"><i class="fas fa-trash"></i></button></div></div>';

                    }

                }



                editHTML += '</td><td><button onclick="SpecialTempPrint(' + data[1][i].Id + ')" class="btn btn-white btn-round btn-just-icon">';
                editHTML += '<i class="material-icons">print</i><div class="ripple-container"></div> </button></td></tr>';


            }


            editHTML += '</tbody></table>'




            $('#OfficeInput').hide();
            $('#OfficeDB').hide();
            $('#OfficeEditOption').hide();
            $('#OfficeDateEditOption').hide();
            $('#OfficeNameEditOption').hide();
            $('#AfterOfficeBack').hide();

            $('#AfterOfficeNameEdit').hide();
            $('#AfterOfficeDateEdit').hide();
            $('#AfterOfficeEdit').hide();


            $('#AfterOfficeDelete').show();





            document.getElementById("AfterOfficeDelete").innerHTML = editHTML;
            //document.getElementById('ei').value = itmnm;
            //document.getElementById('eiq').value = itmq;





        },
        error: function (xhr, status, error) {
            alert("Error");
        }
    });

    //console.log(item + q);

}




$(document).ready(function () {

    var date = new Date();

    var day = date.getDate();
    var month = date.getMonth() + 1;
    var year = date.getFullYear();

    if (month < 10) month = "0" + month;
    if (day < 10) day = "0" + day;

    var today = year + "-" + month + "-" + day;
    // document.getElementById('from').value = today;

});



function OnspotOrder() {
    var x = document.getElementById('Label1').innerText;
    var ddlCategory = document.getElementById("DDLCategory");
    var categoryId = ddlCategory.options[ddlCategory.selectedIndex].value.toString();
    console.log(categoryId);
    console.log(x);


    $.ajax({

        url: '@Url.Action("CreateOnspotOrder", "AdminShowOrder")',
        type: "POST",
        data: { MealTypeId: categoryId, ItemId: x },
        success: function (response) {
            if (response.success) {

                //jQuery("#messageUp").append('<div class="alert alert-success" role="alert"><b>"' + response.responseText + '"</b></div>');

                //window.alert(response.responseText);
                //        //jQuery("#messageUp").append('<div class="alert alert-success" role="alert"><b>"' + response.responseText + '"</b></div>');
                //window.location.reload(true);


            } else {
                // DoSomethingElse()
                alert(response.responseText);
            }
        }


    })


}
function test() {

}

function UpdateSetMenuList() {

    //=============== Set Menu Id =================
    var setMenuIdTemp = $('#setMenuId').val();
    //alert(setMenuIdTemp);
    //=============== Set Menu Id =================


    //=============== Set Menu Item Id =================
    var setMenuArray = [];
    var setMenuPrice = document.getElementById("setMenuPrice").value.toString();

    var checkBoxSetMenu = document.getElementsByName("checkMainItem");
    var checkBoxLengthSetMenu = checkBoxSetMenu.length;
    var tempItemValueSetMenu = "";
    for (var i = 0; i < checkBoxLengthSetMenu; i++) {
        if (checkBoxSetMenu[i].checked == true) {

            tempItemValueSetMenu = checkBoxSetMenu[i].value.toString();
            var obj = { SetMenuItemId: tempItemValueSetMenu };
            setMenuArray.push(obj);
        }
    }
    //=============== Set Menu Item Id =================


    //=============== Extra Item Id =================
    var addedItem = [];
    var checkBoxAdded = document.getElementsByName("checkExtraItem");
    var checkBoxLengthAdded = checkBoxAdded.length;
    var textsAdded = document.getElementsByName("extraItemPrice");
    var tempItemValueAdded = "";
    var tempPriceValueAdded = "";
    for (var i = 0; i < checkBoxLengthAdded; i++) {
        if (checkBoxAdded[i].checked == true) {
            tempPriceValueAdded = textsAdded[i].value.toString();
            tempItemValueAdded = checkBoxAdded[i].value.toString();
            var obj = { AddedItemId: tempItemValueAdded, AddedItemPrice: tempPriceValueAdded };
            addedItem.push(obj);
        }
    }
    //=============== Extra Item Id =================

    var tempExtraItemList = "";
    var tempSetMenuList = "";
    tempExtraItemList = JSON.stringify(addedItem);
    tempSetMenuList = JSON.stringify(setMenuArray);



    $.ajax({

        url: '@Url.Action("UpdateSetMenuAndAddedItemList", "SetMenu")',
        type: "POST",
        data: { SetMenuList: tempSetMenuList, ExtraItemList: tempExtraItemList, SetMenuId: setMenuIdTemp, SetMenuPrice: setMenuPrice },
        success: function (response) {
            if (response.success) {

                //alert(response.responseText);
                //window.location.reload(true);




                jQuery("#messageUp").append('<div class="alert alert-success" role="alert"><b>"' + response.responseText + '"</b></div>');
                jQuery("#messageDown").append('<div class="alert alert-success" role="alert"><b>"' + response.responseText + '"</b></div>');


            } else {
                // DoSomethingElse()
                //alert(response.responseText);
                jQuery("#messageUp").append('<div class="alert alert-danger" role="alert"><b>"' + response.responseText + '"</b></div>');
                jQuery("#messageDown").append('<div class="alert alert-danger" role="alert"><b>"' + response.responseText + '"</b></div>');
            }
        }


    })




}







//<<<<<<<<<<<<<<<<<< CleanPriceFieldOfExtraItem >>>>>>>>>>>>>>>>>>>>>>>
function CleanPriceFieldOfExtraItem(storeOutItemId) {

    var isChecked = $('#checkadded_' + storeOutItemId).is(":checked");
    if (isChecked == true) {
        //alert('is checked');
    } else {
        //alert('is not checked');
        $('#priceadded_' + storeOutItemId).val('');

    }

}

//fixed_plugin_open = $('.sidebar .sidebar-wrapper .nav li.active a p').html();

//    if (window_width > 767 && fixed_plugin_open == 'Dashboard') {
//      if ($('.fixed-plugin .dropdown').hasClass('show-dropdown')) {
//        $('.fixed-plugin .dropdown').addClass('open');
//      }

//    }

//    $('.fixed-plugin a').click(function(event) {
//      // Alex if we click on switch, stop propagation of the event, so the dropdown will not be hide, otherwise we set the  section active
//      if ($(this).hasClass('switch-trigger')) {
//        if (event.stopPropagation) {
//          event.stopPropagation();
//        } else if (window.event) {
//          window.event.cancelBubble = true;
//        }
//      }
//    });
//<<<<<<<<<<<<<<<<<< CleanPriceFieldOfExtraItem >>>>>>>>>>>>>>>>>>>>>>>

function OpenPartyModal() {
    var modal = document.getElementById("myModal");

    // Get the button that opens the modal

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];

    // When the user clicks the button, open the modal
    modal.style.display = "block";


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


