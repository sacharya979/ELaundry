/// <reference path="jquery-3.4.1.intellisense.js" />
$(document).ready(function () {
    var id = 0;
    var sPageURL = window.location.href;
    var indexOfLastSlash = sPageURL.lastIndexOf("/");

    if (indexOfLastSlash > 0 && sPageURL.length - 1 != indexOfLastSlash)
        id =sPageURL.substring(indexOfLastSlash + 1);
   
    //$(".tokenno").val(id);
    //getbyTokenNo();
});
$('.keyboard').on('input', function (e) {
    var paidamt = $('.keyboard').val();
    var totalamt = $('#grandtotal').val();


    var subtotal = parseInt(totalamt) - parseFloat(paidamt);
    $(".returnamount").val(subtotal);
});
function calculateRemaining() {
    var paidamt = $('.keyboard').val();
    var totalamt = $('#grandtotal').val();
    if (paidamt>0) {

        var subtotal = parseInt(totalamt) - parseFloat(paidamt);
        $(".returnamount").val(subtotal);
    }
    else {
        $(".returnamount").val('0');
    }
    
}
//function getbyTokenNo() {
  
   
//    $.ajax({
//        url: "/Payment/GetOrderDetailsbyTokenNo/",
//        type: "GET",
//        contentType: "json",
//        dataType: "json",
//        data: {
//            tokenno: $(".tokenno").val()
//        },
//        beforeSend: function () { // Before we send the request, remove the .hidden class from the spinner and default to inline-block.
//            $(".loader").show();
//        },
//        success: function (result) {
          
//            var i = 1;
//            var html = '';
//            var htmlprint = '';
//                $.each(result, function (key, item) {
//                    html += '<tr>';
//                    html += '<td>' + item.GarmentName + '</td>';
//                    html += '<td>' + item.Quantity + '</td>';
//                    html += '<td>' + item.UnitPrice + '</td>';
//                    html += '<td><input type="text" class="totalprice" style="border:none; width:20px" value="' + item.Total + '"></td>';
                  
//                    html += '<td>'+item.TW+'</td>';


                  
//                    html += '</tr>';

//                    htmlprint += '<tr>';
//                    htmlprint += '<td>' + i + '</td>';
//                    htmlprint += '<td>' + item.GarmentName + '</td>';
//                    htmlprint += '<td>' + item.Quantity + '</td>';
//                    htmlprint += '<td>' + item.UnitPrice + '</td>';
//                    htmlprint += '<tr>';
//                    i++;

//                });
          
//                $('.tbody').html(html);
           
//            totalPrice();

//            htmlprint += '<tr><td class="quantity"></td><td class="description">TOTAL</td><td class="price">' + $("#grandtotal").val()+'</td> </tr>';

//            $('.tbodyprint').html(htmlprint);
            

//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        },
//        complete: function () { // Set our complete callback, adding the .hidden class and hiding the spinner.
//            $(".loader").hide();
//        },
//    });
//    return false;

//}
//function getbyTokenNo() {


//    $.ajax({
//        url: "/Payment/GetOrderDetailsbyTokenNo/",
//        type: "GET",
//        contentType: "json",
//        dataType: "json",
//        data: {
//            tokenno: $(".tokenno").val()
//        },
//        beforeSend: function () { // Before we send the request, remove the .hidden class from the spinner and default to inline-block.
//            $(".loader").show();
//        },
//        success: function (result) {

//            var i = 1;
//            var html = '';
//            var htmlprint = '';
//            $.each(result, function (key, item) {
//                html += '<tr>';
//                html += '<td>' + item.GarmentName + '</td>';
//                html += '<td>' + item.Quantity + '</td>';
//                html += '<td>' + item.UnitPrice + '</td>';
//                html += '<td><input type="text" class="totalprice" style="border:none; width:20px" value="' + item.Total + '"></td>';

//                html += '<td>' + item.TW + '</td>';



//                html += '</tr>';

//                htmlprint += '<tr>';
//                htmlprint += '<td>' + i + '</td>';
//                htmlprint += '<td>' + item.GarmentName + '</td>';
//                htmlprint += '<td>' + item.Quantity + '</td>';
//                htmlprint += '<td>' + item.UnitPrice + '</td>';
//                htmlprint += '<tr>';
//                i++;

//            });

//            $('.tbody').html(html);

//            totalPrice();

//            htmlprint += '<tr><td class="quantity"></td><td class="description">TOTAL</td><td class="price">' + $("#grandtotal").val() + '</td> </tr>';

//            $('.tbodyprint').html(htmlprint);


//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        },
//        complete: function () { // Set our complete callback, adding the .hidden class and hiding the spinner.
//            $(".loader").hide();
//        },
//    });
//    return false;

//}
function myPrint() {
    InsertRecord();//temporder insert
    InsertOrder();// tblOrder insert
   
   //InsertRecord();//temporder insert
    
   

    var divContents = document.getElementById("ticket").innerHTML;
    var a = window.open('', '', 'height=500, width=500');
    a.document.write('<html>');
    a.document.write('<body >');
    a.document.write(divContents);
    a.document.write('</body></html>');
    a.document.close();
    a.print(); 
   
}
function Add(orderid) {
   
    var invObj =
    {
        OrderId: orderid,
        GrossAmount: $('#grandtotal').val(),
      
    };
    $.ajax({
        url: "/Invoice/InsertInvoice/",
        data: JSON.stringify(invObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
           
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


function totalPrice() {
    var sum = 0;
    $(".totalprice").each(function () {
        sum += parseFloat($(this).val());
    });
    $("#grandtotal").val(sum.toFixed(2));
}
function InsertRecord() {

    var customers = new Array();
    var tokenno = $('.tokenno').val();
    $("#tableorderitems TBODY TR").each(function () {
        var row = $(this);
        var GarmentId = row.find('.GarmentId').val();

        var quantity = row.find('.quantity').val();
        var price = row.find('.unitprice').val();
        var tw = row.find('.tww').val();

        var subtotal = parseInt(quantity) * parseFloat(price);

        customers.push({
            GarmentId: GarmentId,
            Quantity: quantity,
            UnitPrice: price,
            Total: subtotal,
            TokenNo: tokenno,
            TW: tw

        });
    });

    //Send the JSON array to Controller using AJAX.
    $.ajax({
        type: "POST",
        url: "/Payment/InsertGarment/",
        data: JSON.stringify(customers),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {

            

        }
    });
}
function InsertOrder() {
    var tokenno = $('.tokenno').val();
    $.ajax({
        url: "/Payment/InsertOrder/" + tokenno,
       
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            Add(result);


        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function CheckTokenStatus() {
    if ($(".tokenno").val() !== "") {
        $.ajax({
            url: "/Payment/CheckTokenTaken/",
            type: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            data: {
                id: $(".tokenno").val()
            },
            success: function (result) {

                if (result == "Active") {
                    sweetAlert
                        ({
                            title: "Information!",
                            text: "Token Already Taken",
                            type: "warning"
                        });
                    $('#submitRecord').prop('disabled', true);

                }
                else {
                    $('#submitRecord').prop('disabled', false);
                }


            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    else {
        $('#submitRecord').prop('disabled', true);
    }

}