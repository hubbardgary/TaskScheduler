$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});

$('#deleteSelected').on('click', function () {
    var tasks = '';
    $(':checkbox[name*="Selected"]:checked').each(function () {
        tasks += '<li>' + decodeURIComponent(this.id) + '</li>';
    });
    $('#deleteConfirmMsg').html('Are you sure you wish to delete these scheduled tasks?<br /><br /><ul>' + tasks + '</ul>');
});

$(':checkbox').on('click', function () {
    var selectedItems = $(':checkbox[name*="Selected"]:checked').length;
    switch (selectedItems) {
        case 0:
            $('#deleteSelected').addClass('disabled');
            $('#editSelected').addClass('disabled');
            break;
        case 1:
            $('#deleteSelected').removeClass('disabled');
            $('#editSelected').removeClass('disabled');
            break;
        default:
            $('#deleteSelected').removeClass('disabled');
            $('#editSelected').addClass('disabled');
            break;
    }
});
