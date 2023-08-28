$(document).ready(function () {
    var taskId = /* Get the task ID from the URL or another source */;



    $.get(`/Objective/Details/${taskId}`, function (data) {
        $("#task-title").text(data.title);
        $("#task-description").text(data.description);
        $("#complete-by-date").text(data.completeByDate);



        $("#created-on-date").text(data.createdDate.split(" ")[0]);
        $("#created-on-time").text(data.createdDate.split(" ")[1]);



        $("#updated-on-date").text(data.updatedDate.split(" ")[0]);
        $("#updated-on-time").text(data.updatedDate.split(" ")[1]);



        if (data.completedDate) {
            $("#completed-on-date").text(data.completedDate.split(" ")[0]);
            $("#completed-on-time").text(data.completedDate.split(" ")[1]);
        } else {
            $("#completed-on-date").text("Not completed yet");
            $("#completed-on-time").text("");
        }
    });
});