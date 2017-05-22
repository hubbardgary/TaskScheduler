function endsWith(name, pattern) {
    var patternIndex = name.lastIndexOf(pattern);
    if (patternIndex > -1 && patternIndex == name.length - pattern.length) {
        return true;
    }
    return false;
}

function checkAndSubmit() {
    var previousName = $('#PreviousName').val();
    var newName = $('#Name').val();

    if (newName != previousName && endsWith(previousName, linkedTaskSuffix)) {
        $.ajax({
            type:  "GET",
            url: linkedRecordingExistsEndpoint,
            contentType:  "application/json; charset=utf‐8",
            data:  {  shutdownName:  previousName  },
            dataType:  "json",
            success: function (linkedRecordingExists) {
                if (linkedRecordingExists) {
                    $('.bs-confirm-edit-linked-modal').modal('show');
                } else {
                    $('form').submit();
                }
            },
            error: function () { alert('error'); }
        });
    } else {
        $('form').submit();
    }
}