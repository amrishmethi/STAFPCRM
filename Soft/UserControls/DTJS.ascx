<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DTJS.ascx.cs" Inherits="Admin_UserControls_DTJS" %>
<%--<script src="https://code.jquery.com/jquery-3.5.1.js"></script>--%>
<script src="https://cdn.datatables.net/1.12.1/js/jquery.dataTables.min.js"></script>
<script src="https://cdn.datatables.net/buttons/2.2.3/js/dataTables.buttons.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
<script src="https://cdn.datatables.net/buttons/2.2.3/js/buttons.html5.min.js"></script>
<script src="https://cdn.datatables.net/buttons/2.2.3/js/buttons.print.min.js"></script>
<script src="https://cdn.datatables.net/buttons/2.2.3/js/buttons.colVis.min.js"></script>
<script src="https://cdn.datatables.net/rowreorder/1.2.8/js/dataTables.rowReorder.min.js"></script>
<script src="https://cdn.datatables.net/responsive/2.3.0/js/dataTables.responsive.min.js"></script>


<script type="text/javascript">
    $(document).ready(function () {
        //$('#example').DataTable({ 
        //    dom: 'Bfrtip',
        //    buttons: [
        //        'copy', 'csv', 'excel', 'pdf', 'print',
        //        'colvis'
        //    ]
        //}); 
    });

    $(document).ready(function () {
        $('#ExportTbl').DataTable({
            //  dom: '<"dt-top-container"<l><"dt-center-in-div"B><f>r>t<"dt-filter-spacer"f><ip>',
            // dom: 'lBfrtip',
            dom: '<"dt-top-container"<l><"dt-center-in-div"B><f>r>t<ip>',
            "processing": true,
            //    "serverSide": true,
            // paging:false,
            rowReorder: {
                selector: 'td:nth-child(1)'
            },
            //responsive: true,
            buttons: [
                //{
                //    extend: 'copyhtml5',
                //    exportoptions: {
                //        columns: [0, ':visible']
                //    }
                //},
                {
                    extend: 'excelHtml5', footer: true,
                    exportOptions: {
                        columns: ':visible',
                        format: {
                            body: function (inner, rowidx, colidx, node) {
                              
                                if ($(node).children("input").length > 0) {
                                    return $(node).children("input").first().innerText;
                                } else {
                                    return inner;
                                }
                            }
                        }
                    }
                },
                {
                    extend: 'pdfHtml5', footer: true, orientation: 'landscape',
                    pageSize: 'A4',
                    exportOptions: {
                        //columns: [0, 1, 2, 5]
                        columns: ':visible',
                    }
                },
                'colvis'
            ],
            pageLength: -1,
            "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]]
        });
    });


    $(document).ready(function () {

        $('#LOkesh').DataTable({
            dom: 'Bfrtip',

            buttons: [
                {
                    extend: 'excel',
                    exportOptions: {
                        format: {

                            body: function (inner, rowidx, colidx, node) {
                                if ($(node).children("input").length > 0) {
                                    return $(node).children("input").first().val();
                                } else {
                                    return inner;
                                }
                            }
                        }
                    }
                }
            ]

        });

    });

</script>
