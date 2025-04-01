$(document).ready(function () {
    $("#WorkingDays, #SubjectsPerDay").on("input", function () {
        let workingDays = parseInt($("#WorkingDays").val()) || 0;
        let subjectsPerDay = parseInt($("#SubjectsPerDay").val()) || 0;
        $("#TotalHours").val(workingDays * subjectsPerDay);
    });
});
