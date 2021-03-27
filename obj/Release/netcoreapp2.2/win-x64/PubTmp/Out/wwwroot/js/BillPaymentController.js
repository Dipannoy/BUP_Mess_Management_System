

var globalParent = 0;
var globalBill = 0;


function MakePayment(parentId, bill) {
    globalParent = parentId;
    globalBill = bill;
  

    $.ajax({
        url: '/AdminBillGenerate/GetPaymentInfo',
        dataType: "json",
        data: { ParentId: parentId },
        success: function (data) {

            var modal = document.getElementById("billPayment");

            modal.style.display = "block";

            //var data = JSON.parse(datum);
            // console.log(data);
            //console.log(JSON.parse(data));
            //var y = JSON.parse(data);
            console.log(data);

        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });

}

function loadPaymentInfo() {

    var methodId = document.getElementById('paymentMethod').value;
    if (methodId == 1) {
        $('#checkField').hide();
        $('#cashField').hide();
        $('#bkashField').show();
    }
    else if (methodId == 2) {
        $('#checkField').show();
        $('#cashField').hide();
        $('#bkashField').hide();
    }
    else {
        $('#checkField').hide();
        $('#cashField').show();
        $('#bkashField').hide();
    }
}

function CloseModal() {

    var modal = document.getElementById("billPayment");


    modal.style.display = "none";


}

function ConfirmPayment() {
    var methodId = document.getElementById('paymentMethod').value;
    var moblNum = null;
    var transId = null;
    var amount = null;
    var bankName = null;
    var accountNum = null;
    var receiver = null;
    var selectedFile = null;
    var date = null;
    if (methodId == 1) {
        amount = document.getElementById('bkashAmnt').value;
        moblNum = document.getElementById('moblNum').value;
        transId = document.getElementById('tranId').value;
        receiver = document.getElementById('bkashReceiver').value;
        selectedFile = document.getElementById("inputFileBkash").files;
        date = document.getElementById("bkashDate").value;

    }
    else if (methodId == 2) {
        amount = document.getElementById('checkAmount').value;
        bankName = document.getElementById('bankName').value;
        accountNo = document.getElementById('ACNo').value;
        receiver = document.getElementById('checkReceiver').value;
        selectedFile = document.getElementById("inputFileCheck").files;
        date = document.getElementById("checkDate").value;


    }
    else {
        amount = document.getElementById('cashAmount').value;
        receiver = document.getElementById('cashReceiver').value;

    }

    var fileToLoad = selectedFile[0];
 
    var index = fileToLoad.name.lastIndexOf(".");
    var ext = fileToLoad.name.substring(index + 1, fileToLoad.name.length);
    var title = fileToLoad.name.substring(0, index);

    var fileReader = new FileReader();
    var base64;
    var encode;

    fileReader.onload = function (fileLoadedEvent) {
        base64 = fileLoadedEvent.target.result;
        //encode = base64.split(',');

        if (base64.includes(',')) {
            encode = base64.split(',');
        }
        else {
            encode = base64;
        }

        //  console.log(encode);



        $.ajax({
            url: '/AdminBillGenerate/ConfirmPayment',
            type: "POST",

            dataType: "json",
            data: {
                BillParent: globalParent, Method: methodId,
                Mobile: moblNum, TransId: transId, Bank: bankName, Account: accountNum,
                Receiver: receiver, Amount: amount, Due: globalBill,
                File: encode[1],
                FileExtension: ext,
                LinkUrl: null,
                PaymentDate : date
            },
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
    fileReader.readAsDataURL(fileToLoad);

}

$("#bkashDate").datepicker({
    dateFormat: "dd-mm-yy"
    , duration: "fast"
});

$("#checkDate").datepicker({
    dateFormat: "dd-mm-yy"
    , duration: "fast"
});