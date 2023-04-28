$(document).ready(function () {
    //GetReportTo();
    $('#TaskAssignedBy').change(function () {
        var id = $(this).val();
        $('#TaskAssignedTo').empty();
        $('#TaskAssignedTo').append('<Option>--Select--</Option>');
        GetReportTo(id);
    });
});

function GetReportTo(empID) {
    $.ajax({
        url: '/EmployeeMasterData/GetReportToEmployees?id=' + empID,
        success: function (result) {

            console.log(result);

            $.each(result, function (i, data) {
                console.log(data);
                $('#TaskAssignedTo').append('<Option value=' + data.employeeID + '>' + data.fullName + '</Option>');
            });
        }
    });
}