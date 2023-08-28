$(document).ready(function () {
    var taskIdToDelete;

    $(".open-delete-modal").click(function () {
        taskIdToDelete = $(this).data("task-id");
        $("#delete-modal").show();
    });

    $("#cancel-delete").click(function () {
        $("#delete-modal").hide();
    });

    $("#confirm-delete").click(function () {
        if (taskIdToDelete) {
            window.location.href = "/Objective/DeleteTask/" + taskIdToDelete;
        }
    });
});