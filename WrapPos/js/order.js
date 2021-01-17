/// <reference path="jquery-3.4.1.intellisense.js" />
$(document).ready(function () {
    $('.searchlist').hide();
});
function GetGarmentByCategoryId() {
    $(".tablist").hide(1000);
    $('.searchlist').show();
    var search = $('#search').val();
    $.ajax({
        url: "/Order/GetGarmentByGarmentName/",
        type: "GET",
        contentType: "json",
        dataType: "json",
        data: {
            search: $('#search').val()
        },
        success: function (result) {
            var html = '';
            if (!$.trim(result)) {
                html += ' <div class="alert bg-danger" role="alert"><em class="fa fa-lg fa-warning">&nbsp;</em>';
                html += 'No Item Found';


                html += ' <a href="#" class="pull-right"><em class="fa fa-lg fa-close"></em></a></div>';
                $('.garmentlist').html(html);

            }
            else {

                $.each(result, function (key, item) {

                    html += ' <button type="button" onclick="return getbyID(' + item.GarmentId + ')" class="btn btn-lg btn-warning">';
                    html += item.GarmentName;


                    html += '</button>';
                });
                $('.garmentlist').html(html);
            }

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;

}
var orderItems = [];
var grantotal = 0;
function getbyID(EmpID) {


    $.ajax({
        url: "/Order/getbyID/" + EmpID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {


            if (CheckGarmentAdded(result.GarmentName)) {
                $.notify("garment Already Exist", {
                    globalPosition: "top center",
                    className: "warning"
                })
            }
            else {
                orderItems.push({
                    ItemName: result.GarmentName,
                    Quantity: 1,
                    Rate: result.UnitPrice,
                    TotalAmount: result.UnitPrice
                });


                $(".tbody").append("<tr data-name='" + result.GarmentName + "'><td><input class='GarmentName' type='hidden' value='" + result.GarmentName + "'><input class='GarmentId' type='hidden' value='" + result.GarmentId + "'>" + result.GarmentName + "</td><td><input type='text' class='quantity form-control' onkeyup='UpdateTotals(this)' id='quantity' value='1'></td><td><input type='text' class='unitprice form-control' readonly  value=" + result.UnitPrice + "></td><td><input type='text' class='totalprice form-control' readonly value=" + result.UnitPrice + "></td><td><input type='checkbox' id='check' ></td ><td><button onclick='deleteRow(this)' class='btn btn-danger btn-xs btn-delete '>Delete</button></td ></tr>");

                totalPrice();
                $.notify("garment Added", {
                    globalPosition: "top center",
                    className: "success"
                })
            }

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;

}

function calculfac() {

    const parent = $(this).parent('tr');
    var quantity = parseInt(parent.find('.quantity').val()) || 0;
    var price = parseInt(parent.find('.unitprice').val()) || 0;
    parent.find('.total').val(quantity * price);

}
function deleteRow(btn) {
    swal({
        title: "Are you sure?",
        text: "Are you sure that you want to delete?",
        type: "warning",
        showCancelButton: true,
        closeOnConfirm: false,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Delete",
        confirmButtonColor: "#ec6c62"
    },


        function (isConfirm) {

            if (isConfirm) {
                //var row = btn.parentNode.parentNode;

                //var i = btn.parentNode.parentNode.rowIndex;
                //document.getElementById("tableitems").deleteRow(i);
                //orderItems.splice(i, 1);
                //const indx =   orderItems.findIndex(v => v.name === "Michael");
                //orderItems.splice(indx, indx >= 0 ? 1 : 0);
                var $container = $(btn).parent().parent();
                var val = $container.find('.GarmentName').val();
                var val = $(this).closest('tr').find(".GarmentName").text();
                var index = orderItems.findIndex(function (item) { return item.ItemName == val });
                orderItems.splice(index, 1);
                var row = btn.parentNode.parentNode;

                var i = btn.parentNode.parentNode.rowIndex;
                document.getElementById("tableitems").deleteRow(i);

                totalPrice();
                sweetAlert
                    ({
                        title: "Deleted!",
                        text: "Employee Deleted Successfully",
                        type: "warning"
                    });

            }
        }
        
    )
}
function totalPrice() {
    var sum = 0;
    $(".totalprice").each(function () {
        sum += parseFloat($(this).val());
    });
    $("#grandtotal").val(sum.toFixed(2));
}
function UpdateTotals(elem) {
    // This will give the tr of the Element Which was changed
    var $container = $(elem).parent().parent();
    var quantity = $container.find('.quantity').val();
    var price = $container.find('.unitprice').val();
    var subtotal = parseInt(quantity) * parseFloat(price);
    $container.find('.totalprice').val(subtotal.toFixed(2));
    totalPrice();
}
function CloseListgarment() {
    $('.searchlist').hide(1000);
    $(".tablist").show(1000);
}

//function InsertRecord() {

//    var customers = new Array();
//    var tokenno = $('.tokenno').val();
//    $("#tableitems TBODY TR").each(function () {
//        var row = $(this);
//        var GarmentId = row.find('.GarmentId').val();

//        var quantity = row.find('.quantity').val();
//        var price = row.find('.unitprice').val();
//        var tw = row.find('#check').is(":checked")

//        var subtotal = parseInt(quantity) * parseFloat(price);

//        customers.push({
//            GarmentId: GarmentId,
//            Quantity: quantity,
//            UnitPrice: price,
//            Total: subtotal,
//            TokenNo: tokenno,
//            TW: tw

//        });
//    });

//    //Send the JSON array to Controller using AJAX.
//    $.ajax({
//        type: "POST",
//        url: "/Order/InsertGarment",
//        data: JSON.stringify(customers),
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (r) {
//            sweetAlert
//                ({
//                    title: "Saved!",
//                    text: "Record Added To Order",
//                    type: "success"
//                });
//            deleteRows();
//            //$('.tokenno').val('');
//            //$("#grandtotal").val('');
//            window.location.href = "/Payment/MakePayment/" + $('.tokenno').val();

//        }
//    });
//}
function InsertRecordOrder() {

        var customers = new Array();
    var tokenno = $('.tokenno').val();
    $("#tableitems TBODY TR").each(function () {
        var row = $(this);
        var GarmentId = row.find('.GarmentId').val();

        var quantity = row.find('.quantity').val();
        var price = row.find('.unitprice').val();
        var tw = row.find('#check').is(":checked")

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
        url: "/Order/ManageOrder",
        data: JSON.stringify(customers),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
           
            window.location.href = "/Payment/MakePayment/";

        }
    });
}
function deleteRows() {
    //var table = document.getElementById("tableitems");
    //for (var i = table.rows.length - 1; i > 0; i--) {
    //    table.deleteRow(i);
    //}
}

function CheckGarmentAdded(name) {

    var found = false;
    for (var i = 0; i < orderItems.length; i++) {
        if (orderItems[i].ItemName == name) {
            found = true;
            break;
        }
    }
    return found;


}
