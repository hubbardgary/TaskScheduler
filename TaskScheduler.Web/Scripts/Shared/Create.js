$('document').ready(function () {
    showOrHideRecurrenceFields();
});

$('#IsRecurring').on('click', function () {
    addOrRemoveOneOffOption();
    showOrHideRecurrenceFields();
});


function addOrRemoveOneOffOption() {
    if ($('#IsRecurring')[0].checked) {
        $('#Recurrence option[value="1"]').remove();
    } else {
        $('#Recurrence').append('<option value="1">OneOff</option>');
    }
}

function showOrHideRecurrenceFields() {
    if ($('#IsRecurring')[0].checked) {
        $('#recurrence-fields').show();
    } else {
        $('#recurrence-fields').hide();
        $('#Recurrence').val("1").change();
    }
}
