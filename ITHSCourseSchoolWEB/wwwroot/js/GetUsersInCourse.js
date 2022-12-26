var dataTable;

function loadDataTable(id) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Courses/ListOfUsersInCourse",
            "type": "GET",
            "datatype": "json",
            "data": { id: id },
        },
        "columns": [
            { "data": "id", "width": "15%" },
            { "data": "userName", "width": "15%" },
            { "data": "name", "width": "15%" },
        ]
    });
}