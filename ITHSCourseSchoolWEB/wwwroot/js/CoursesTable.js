var dataTable;

//$(document).ready(function () {
//    loadDataTable();
//});

function loadDataTable(id) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Auth/YourCourseList",
            "type": "GET",
            "datatype": "json",
            "data": { id: id },
        },
        "columns": [
            { "data": "id", "width": "15%" },
            { "data": "courseTitle", "width": "15%" },
            { "data": "courseStart", "width": "15%" },
            { "data": "courseEnd", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a onclick=Delete("/Auth/DeleteCourseFromList/${data}") class='btn btn-danger text-white'
                                    style='cursor:pointer;'> <i class='far fa-trash-alt'></i></a>
                                    &nbsp;
                                </div>
                                
                            `;
                }, "width": "20%"
            },
        ]
    });
}




function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}

