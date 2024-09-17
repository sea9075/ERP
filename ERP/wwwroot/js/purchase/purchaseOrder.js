var dataTable;
$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/Purchase/PurchaseOrder/GetAll'
        },
        "columns": [
            {
                data: null,
                "render": function (data, type, row, meta) {
                    return meta.row + 1;
                },
                "width": "5%"
            },
            {
                data: 'orderNumber',
                "width": "10%",
                "type": "string",
                "render": function (data, type, row) {
                    return `<a href="/Purchase/PurchaseDetail/${row.purchaseDetailId}">${data}</a>`;
                }
            },
            {
                data: 'date',
                "width": "10%",
                "type": "string",
                "render": function (data) {
                    var date = new Date(data);
                    return date.toLocaleDateString('zh-TW', { year: 'numeric', month: '2-digit', day: '2-digit' });
                }
            },
            { data: 'totalPrice', "width": "10%", "type": "string" },
            { data: 'supplier.name', "width": "10%", "type": "string" },
            {
                data: 'purchaseOrderId',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/Purchase/PurchaseOrder/upsert?id=${data}" type="button" class="btn btn-inverse-info btn-fw">修改</a>
                    <a onClick=Delete('/Purchase/PurchaseOrder/Delete/${data}') type="button" class="btn btn-danger btn-fw">刪除</a>
                    </div>`
                },
                "width": "5%"
            }
        ],
        "createdRow": function (row, data, dataIndex) {
            $(row).addClass('table-primary');
        }
    });
}


function Delete(url) {
    Swal.fire({
        title: "你確定要刪除此資料嗎?",
        text: "刪除後你將無法回復此資料!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "確定",
        cancelButtonText: "取消"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    console.log(data);
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}