$('document').ready(function () {
    addOrRemoveOneOffOption();
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
        if (!recurrenceDropdownContainsOneOffOption()) {
            $('#Recurrence').append('<option value="1">OneOff</option>');
        }
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

function recurrenceDropdownContainsOneOffOption() {
    var containsOneOff = false;
    $('#Recurrence option').each(function () {
        if (this.value === '1') {
            containsOneOff = true;
        }
    });
    return containsOneOff;
}
