
$(document).ready(function () {
    //$('body').addClass('sidebar-mini layout-fixed sidebar-collapse');
    getCompany();
    getProduct();
    $('#btnUpdate').hide();
    var mid = GetParameterValues('mid');
    var id = GetParameterValues('id');

    if (mid) {
        //  $('#btnNext').hide();
        //  $('#Body_MasterEntry').show();
        // $('#back').hide();
        //$('#Body_ItemEntry').show();
        // $('#btnUpdate').hide();
        FillDetails(mid);
    }
    else if (id) {

        $('#btnNext').hide();
        $('#Body_MasterEntry').show();
        $('#back').hide();
        $('#Body_ItemEntry').show();
    }
    else {
        //$('#Update').show();
        //$('#Body_MasterEntry').show();
        //$('#back').show();
        //$('#Body_ItemEntry').hide();
    }

});
function GetParameterValues(param) {
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        if (urlparam[0] == param) {
            return urlparam[1];
        }
    }
}

function getCompany() {
    $("[id*=Body_txtCompany]").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: 'FileRequestEntry.aspx/CompanyList',
                data: "{ 'prefix': '" + request.term + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.split('-')[0],
                            val: item.split('-')[1]
                        }
                    }))
                },
                error: function (response) {
                    // alert(response.responseText);
                },
                failure: function (response) {
                    //  alert(response.responseText);
                }
            });
        },
        select: function (e, i) {
            $('#Body_CompanyId').val(i.item.val);
            
        },
        change: function (event, ui) {
            if (ui.item === null) {
                tostpro('Please select from list', 'Error', 'error', 'top-right', '2000');
                $(this).val('');
                $('#Body_txtCompany').val('');
                $('#Body_txtCompany').focus();
            }
        },
        minLength: 1
    });

};

function getProduct() {
    $("[id*=Body_txtFile]").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: 'FileRequestEntry.aspx/ProductList',
                data: "{ 'prefix': '" + request.term + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.split('-')[0],
                            val: item.split('-')[1]
                        }
                    }))
                },
                error: function (response) {
                    // alert(response.responseText);
                },
                failure: function (response) {
                    //  alert(response.responseText);
                }
            });
        },
        select: function (e, i) {
            $('#Body_FileId').val(i.item.val);
        },
        change: function (event, ui) {
            if (ui.item === null) {
                tostpro('Please select from list', 'Error', 'error', 'top-right', '2000');
                $(this).val('');
                $('#Body_txtFile').val('');
                $('#Body_txtFile').focus();
            }
        },
        minLength: 1
    });

};
function SveNext(th) {
  
    var Date = $('#Body_txtDate').val();
    var RequestNo = $('#Body_txtRequestNo').val();
    var FileId = $("[id*=Body_FileId]").val();
    var CompanyId = $("[id*=Body_CompanyId]").val();
    var Qty = $("[id*=Body_txtQty]").val();

    var Action = th;
    var Id = "0";
    var idd = "0";
    idd = GetParameterValues('mid');
    if (idd) {
        idd = idd;
    }
    else
        idd = "0";

    if (Date != "" && FileId != "" && Qty != "") {
        $.ajax({
            type: "POST",
            contentType: "application/json;",
            url: 'FileRequestEntry.aspx/SaveNext',
            data: '{ Action:"' + Action + '",ID:"' + Id + '",Date: "' + Date + '", RequestNo: "' + RequestNo + '", CompanyId: "' + CompanyId + '", FileId: "' + FileId + '", Qty: "' + Qty + '"}',
            dataType: "json",
            success: function (data) {

                var dd = $.parseJSON(data.d);
                if (data.d == 'Wrong') {
                    alert('Wrong Entry !!!!!');
                }
                else {

                    var dd = $.parseJSON(data.d);

                    var id = GetParameterValues('mid');
                    if (id) {
                       $("[id*=Body_FileId]").val('0');
                        $("[id*=Body_CompanyId]").val('0');
                        $("[id*=Body_txtQty]").val('0');
                        $("[id*=Body_txtCompany]").val('');
                        $("[id*=Body_txtFile]").val('');
                        FillDetails(id);
                    }
                    else {
                        window.location = 'FileRequestEntry.aspx?mid=' + dd[0].MaxId;
                    }
                }
            },
            error: function ajaxerror(data) {

                alert(data.Status + " : " + data.StatusText);
            }
        });
    }
    else {
        tostpro('Please fill all details', 'Error', 'error', 'top-right', '2000');
    }
}


function FillDetails(mid) {
    $.ajax({
        url: 'FileRequestEntry.aspx/FillDetails',
        data: '{ID: "' + mid + '"}',
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
          
            var dd = $.parseJSON(data.d);
            if (data.d == 'Wrong') {
                alert('Wrong Entry !!!!!');
            }
            else {
                //$("#Body_txtInvoiceNo").val(dd[0].InvoiceNo);
                //$('#Body_txtParty').val(dd[0].FirmName);
                //$('#Body_txtMobile').val(dd[0].MobileNo);
                //$('#Body_txtDate').val(dd[0].dd);
                //$('#Body_txtAddress').val(dd[0].Address);
                //$('#Body_txtState').val(dd[0].State);
                //$('#Body_PartyId').val(dd[0].PartyId);
                //$('#Body_PartyType').val(dd[0].PartyType);
                //$('#Body_GstType').val(dd[0].GstType);
                //$('#Body_lblGstType').text(dd[0].GstType);
                //$('#Body_txtRemark').text(dd[0].Remark);
                // updateId = mid;

                $('#tt tbody').empty();
                for (var i = 0; i < dd.length; i++) {

                    if (dd[i].FirmName == null) {
                        //alert(dd[i].FileId);
                    } else {
                        $("#tt").append(
                            "<tr><td>" + dd[i].SNO + "</td><td>" + dd[i].FirmName + "</td><td>" + dd[i].FileName + "</td><td>" + dd[i].Qty + "</td>"
                            + "<td><a href='javascript:void(0);' onclick='RemoveItem(" + dd[i].ID + ");'>Remove</a></td></tr>");

                    }
                }
            }
        }
    })
}


function editDetails(id) {
    // alert(id);
    $('#btnUpdate').show();
    $('#btnAdd').hide();

    $.ajax({
        url: 'FileRequestEntry.aspx/EditDetails',
        data: '{id: "' + id + '"}',

        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            var dd = $.parseJSON(data.d);
            if (data.d == 'Wrong') {
                alert('Wrong Entry !!!!!');
            }
            else {
                $('#Body_txtFile').val(dd[0].Name);
                $('#Body_txtChassisNo').val(dd[0].ChassisNo);
                $('#Body_txtbatteryNo').val(dd[0].BatteryNo);
                $('#Body_txtMotorNo').val(dd[0].MotorNo);
                $('#Body_txtKyeNo').val(dd[0].KeyNo);
                $('#Body_txtQty').val(dd[0].Qty);
                $('#Body_txtMrp').val(dd[0].MRP);
                $('#Body_txtGstDis').val(dd[0].AllGst);
                $('#Body_txtDiscount').val(dd[0].Discount);
                $('#Body_txtAmount').val(dd[0].NetAmount);
                $('#Body_txtDescription').val(dd[0].Description);
                $('#Body_FileId').val(dd[0].FileId);
                $('#Body_DeteailId').val(id);
            }
        }
    })
}
function RemoveItem(ID) {
    $.ajax({
        url: 'FileRequestEntry.aspx/DeleteEntry',
        data: "{ 'ID': '" + ID + "'}",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var dd = $.parseJSON(data.d);
            var mid = GetParameterValues('mid');
            FillDetails(mid);
            if (data.d == 'Wrong') {
                alert('Wrong Entry !!!!!');
            }
            else {
                var id = GetParameterValues('mid');
                //FillDetails(id);
            }
        }
    })
}

