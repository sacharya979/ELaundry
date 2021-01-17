
$(document).ready(function () {
    $('.payment').on('click', function () {

       
        var salesamount = $(this).closest('tr').find('.salesamount').val();
        var username = $(this).closest('tr').find('.username').val();
        var branchname = $(this).closest('tr').find('.branchname').val();
        var userid = $(this).closest('tr').find('.userid').val();
        var branchid = $(this).closest('tr').find('.userid').val();

        $("#mousername").val(username);
        $("#mosalesamount").val(salesamount);
        $("#bobranchname").val(branchname);


        $("#mouserid").val(userid);
        $("#mobranchid").val(branchid);
        $('#btnPrintdeno').prop('disabled', true);
        
        $('.modal').modal('show');


      
        
    });
});
function modalclosing() {
    $('.modal').modal('hide');
    window.location.href = "/Closing/MakeClosing/";
   
}
function InsertClosing() {

    var denoObj =
    {
        
        SalesAmount: $('#mosalesamount').val(),
        UserId: $('#mouserid').val(),
        BranchId: $('#mobranchid').val(),
        CashAmount: $('#totalamount').val(),
        
    };
    //Send the JSON array to Controller using AJAX.
    $.ajax({
        type: "POST",
        url: "/Closing/InsertClosing/",
        data: JSON.stringify(denoObj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
          
           
            sweetAlert
                ({
                    title: "Saved!",
                    text: "Closing of  " + $("#mousername").val()+"  Done",
                    type: "success"
                });
            $('.submitbutton').prop('disabled', true);
            $('#btnPrintdeno').prop('disabled', false);

            //window.location.href = "/Closing/MakeClosing/";
           

        }
    });
}

function PrintElem() {
   
    var divdeno = document.getElementById("printdiv").innerHTML;
    var mywindow = window.open('', 'PRINT', 'height=400,width=600');

    mywindow.document.write('<html><head><title>' + document.title + '</title>');
    mywindow.document.write('</head><body >');
    var htmlstring = '<table><thead><th>Denominator</th><th>Qty</th><th>Total</th></thead>';
    htmlstring += '<tbody><tr>';

    var thousandqty = $('#thousand').val();
    var thousandtotal = $('#thousandtotal').val();
    htmlstring += '<td>1000X</td><td>' + thousandqty + '</td><td>' + thousandtotal + '</td></tr>';

    var fivehundredqty = $('#fivehundred').val();
    var fivehundredtotal = $('#fivehundredtotal').val();
    htmlstring += '<tr><td>500X</td><td>' + fivehundredqty + '</td><td>' + fivehundredtotal + '</td></tr>';

    var onehundredqty = $('#hundred').val();
    var onehundredtotal = $('#hundredtotal').val();
    htmlstring += '<tr><td>100X</td><td>' + onehundredqty + '</td><td>' + onehundredtotal + '</td></tr>';

    var fiftyqty = $('#fifty').val();
    var fiftytotal = $('#fiftytotal').val();
    htmlstring += '<tr><td>50X</td><td>' + fiftyqty + '</td><td>' + fiftytotal + '</td></tr>';


    var twentyqty = $('#twenty').val();
    var twentytotal = $('#twentytotal').val();
    htmlstring += '<tr><td>20X</td><td>' + twentyqty + '</td><td>' + twentytotal + '</td></tr>';



    var tenqty = $('#ten').val();
    var tentotal = $('#tentotal').val();
    htmlstring += '<tr><td>10X</td><td>' + tenqty + '</td><td>' + tentotal + '</td></tr>';


    var fiveqty = $('#five').val();
    var fivetotal = $('#fivetotal').val();
    htmlstring += '<tr><td>5X</td><td>' + fiveqty + '</td><td>' + fivetotal + '</td></tr>';



    var twoqty = $('#two').val();
    var twototal = $('#twototal').val();
    htmlstring += '<tr><td>2X</td><td>' + twoqty + '</td><td>' + twototal + '</td></tr>';




    var oneqty = $('#one').val();
    var onetotal = $('#onetotal').val();
    htmlstring += '<tr><td>1X</td><td>' + oneqty + '</td><td>' + onetotal + '</td></tr>';


    var totalamount = $('#totalamount').val();
    
    htmlstring += '<tr><td colspan="2">TotalAmount:<td>' + totalamount + '</td></tr>';

    var username = $('#mousername').val();
    htmlstring += '<tr><td >Username: <td colspan="2">' + username + '</td></tr>';
    var salesamount = $('#mosalesamount').val();
    htmlstring += '<tr><td >Sales Amount: <td colspan="2">' + salesamount + '</td></tr>';
    var branchname = $('#bobranchname').val();
    htmlstring += '<tr><td >BranchName: <td colspan="2">' + branchname + '</td></tr>';

    mywindow.document.write(htmlstring);
    mywindow.document.write('</body></html>');

    mywindow.document.close(); // necessary for IE >= 10
    mywindow.focus(); // necessary for IE >= 10*/

    mywindow.print();
    mywindow.close();

    return true;
}

function calculatetotal() {
    var sum = 0;
    $(".totalprice").each(function () {
        sum += parseFloat($(this).val());
    });
    $("#totalamount").val(sum.toFixed(2));
}
function calculateThousand() {
    var totalno = $('#thousand').val();
    if (totalno == "") {
        $("#thousandtotal").val('0');
    }
    else {

        var subtotal = parseInt(totalno) * parseFloat('1000');
        $("#thousandtotal").val(subtotal);
        calculatetotal();
    }

}
function calculateFiveHundred() {
    var totalno = $('#fivehundred').val();
    if (totalno == "") {
        $("#fivehundredtotal").val('0');
    }
    else {

        var subtotal = parseInt(totalno) * parseFloat('500');
        $("#fivehundredtotal").val(subtotal);
        calculatetotal();
    }

}
function calculateOneHundred() {
    var totalno = $('#hundred').val();
    if (totalno == "") {
        $("#hundredtotal").val('0');
    }
    else {

        var subtotal = parseInt(totalno) * parseFloat('100');
        $("#hundredtotal").val(subtotal);
        calculatetotal();
    }

}
function calculateFifty() {
    var totalno = $('#fifty').val();
    if (totalno == "") {
        $("#fiftytotal").val('0');
    }
    else {

        var subtotal = parseInt(totalno) * parseFloat('50');
        $("#fiftytotal").val(subtotal);
        calculatetotal();
    }

}
function calculateTwenty() {
    var totalno = $('#twenty').val();
    if (totalno == "") {
        $("#twentytotal").val('0');
    }
    else {

        var subtotal = parseInt(totalno) * parseFloat('20');
        $("#twentytotal").val(subtotal);
        calculatetotal();
    }

}
function calculateTen() {
    var totalno = $('#ten').val();
    if (totalno == "") {
        $("#tentotal").val('0');
    }
    else {

        var subtotal = parseInt(totalno) * parseFloat('10');
        $("#tentotal").val(subtotal);
        calculatetotal();
    }

}
function calculateFive() {
    var totalno = $('#five').val();
    if (totalno == "") {
        $("#fivetotal").val('0');
    }
    else {

        var subtotal = parseInt(totalno) * parseFloat('5');
        $("#fivetotal").val(subtotal);
        calculatetotal();
    }

}
function calculateTwo() {
    var totalno = $('#two').val();
    if (totalno == "") {
        $("#twototal").val('0');
    }
    else {

        var subtotal = parseInt(totalno) * parseFloat('2');
        $("#twototal").val(subtotal);
        calculatetotal();
    }

}
function calculateOne() {
    var totalno = $('#one').val();
    if (totalno == "") {
        $("#onetotal").val('0');
    }
    else {

        var subtotal = parseInt(totalno) * parseFloat('1');
        $("#onetotal").val(subtotal);
        calculatetotal();
    }

}

function GetTotalCash(elem) {
    // This will give the tr of the Element Which was changed
    var $container = $(elem).parent().parent();
    var cashamount = $('#totalamount').val(); 

   var amount= $container.find('.cashamount').val();
   
   
}