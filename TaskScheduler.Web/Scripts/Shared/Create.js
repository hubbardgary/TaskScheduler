$('document').ready(function () {
    showOrHideRecurrenceFields();
});

$('#IsRecurring').on('click', function () {
    showOrHideRecurrenceFields();
});

function showOrHideRecurrenceFields() {
    if ($('#IsRecurring')[0].checked) {
        $('#recurrence-fields').show();
    } else {
        $('#Recurrence').val(1);
        $('#recurrence-fields').hide();
    }
}
