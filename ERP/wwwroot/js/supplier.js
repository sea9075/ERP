var dataTable;
$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/BackEnd/Partner/GetAll'
        },
        "columns": [
            {
                data: null,
                "render": function (data, type, row, meta) {
                    return meta.row + 1;
                },
                "width": "5%"
            },
            { data: 'partnerName', "width": "10%", "type": "string" },
            { data: 'partnerPhone', "width": "10%", "type": "string" },
            { data: 'partnetIDCard', "width": "10%", "type": "string" },
            {
                data: 'partnerPhoto',
                "render": function (data) {
                    if (data) {
                        return `<img src="${data}" alt="Photo" style="width: 100px; height: auto; border-radius: 5px;" />`;
                    } else {
                        return `NoPhoto`;
                    }
                },
                "width": "10%"
            },
            {
                data: 'partnerId',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/BackEnd/Partner/upsert?id=${data}" type="button" class="btn btn-inverse-info btn-fw">修改</a>
                    <a onClick=Delete('/BackEnd/Partner/Delete/${data}') type="button" class="btn btn-danger btn-fw">刪除</a>
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