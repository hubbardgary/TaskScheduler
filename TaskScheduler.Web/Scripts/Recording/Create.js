$("#StartDate").on('input', function () {
    var startTime = $("#StartTime").val();
    var endTime = $("#EndTime").val();
    if (Date.parse('01/01/2011 ' + endTime + ':00') < Date.parse('01/01/2011 ' + startTime + ':00')) {
        $("#EndDate").val(formatDate(dateAddDays($("#StartDate").val(), 1)));
    } else {
        $("#EndDate").val($("#StartDate").val());
    }
});

$("#StartTime").on('input', function () {
    var startDate = $("#StartDate").val();
    var startTime = $("#StartTime").val();
    var timeComponents = startTime.split(":");

    var dateTime = new Date(startDate);
    dateTime.setHours(timeComponents[0]);
    dateTime.setMinutes(timeComponents[1]);

    var endDateTime = dateTime
    endDateTime.setMinutes(dateTime.getMinutes() + 30);

    $("#EndDate").val(formatDate(endDateTime));
    $("#EndTime").val(formatTime(endDateTime));
});

$("#EndTime").on('input', function () {
    setEndDate();
});

function setEndDate() {
    var startTime = $("#StartTime").val();
    var endTime = $("#EndTime").val();
    if (Date.parse('01/01/2011 ' + endTime + ':00') < Date.parse('01/01/2011 ' + startTime + ':00')) {
        $("#EndDate").val(formatDate(dateAddDays($("#StartDate").val(), 1)));
    } else {
        $("#EndDate").val($("#StartDate").val());
    }
}

function dateAddDays(startDate, daysToAdd) {
    var date = new Date(startDate);
    date.setDate(date.getDate() + daysToAdd);
    return date;
}

function formatDate(date) {
    return [date.getFullYear(),
            zeroPad(date.getMonth() + 1, 10),
            zeroPad(date.getDate(), 10)].join('-');
}

function formatTime(date) {
    return [zeroPad(date.getHours(), 10),
            zeroPad(date.getMinutes(), 10)]
            .join(':');
}

//function to add a leading zero to a number of shorter length than the specified units
function zeroPad(num, units) {
    var len = (String(units).length - String(num).length) + 1;
    return len > 0 ? new Array(len).join('0') + num : num;
}

$(function () {
    $('.focus :input').focus();
});