$(document).ready(function () {
    setDuration();
});

$("#StartDate").on('input', function () {
    var startTime = $("#StartTime").val();
    var endTime = $("#EndTime").val();
    if (Date.parse('01/01/2011 ' + endTime + ':00') < Date.parse('01/01/2011 ' + startTime + ':00')) {
        $("#EndDate").val(formatDate(dateAddDays($("#StartDate").val(), 1)));
    } else {
        $("#EndDate").val($("#StartDate").val());
    }

    setDuration();
});

$("#EndDate").on('input', function () {
    setDuration();
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

    setDuration();
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

    setDuration();
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

function buildDateTime(dateString, hours, minutes) {
    var date = new Date(dateString);
    date.setHours(hours);
    date.setMinutes(minutes);
    return date;
}

function getStartDateTime() {
    var startDate = $("#StartDate").val();
    var startTime = $("#StartTime").val().split(":");
    return buildDateTime(startDate, startTime[0], startTime[1]);
}

function getEndDateTime() {
    var endDate = $("#EndDate").val();
    var endTime = $("#EndTime").val().split(":");
    return buildDateTime(endDate, endTime[0], endTime[1]);
}

function calculateDuration() {
    var minutes = (getEndDateTime() - getStartDateTime()) / 1000 / 60;
    var hours = Math.floor(minutes / 60);
    var days = Math.floor(hours / 24);

    if (isNaN(minutes)) {
        return "Invalid start or end date"
    }

    if (days === 0 && hours === 0) {
        return minutes + " minute" + (minutes !== 1 ? "s" : "");
    }
    
    if (days === 0) {
        minutes = minutes - (hours * 60);
        return hours + " hour" + (hours !== 1 ? "s " : " ") + minutes + " minute" + (minutes !== 1 ? "s" : "");
    }
    
    hours = hours - (days * 24);
    minutes = minutes - (days * 24 * 60) - (hours * 60);
    return days + " day" + (days !== 1 ? "s " : " ") + hours + " hour" + (hours !== 1 ? "s " : " ") + minutes + " minute" + (minutes !== 1 ? "s" : "");
}

function setDuration() {
    $("#duration").text(calculateDuration());
}

//function to add a leading zero to a number of shorter length than the specified units
function zeroPad(num, units) {
    var len = (String(units).length - String(num).length) + 1;
    return len > 0 ? new Array(len).join('0') + num : num;
}

$(function () {
    $('.focus :input').focus();
});