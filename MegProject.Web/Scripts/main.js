$(document).ready(function() {
    


});


var showModal = function(type, header, message, url) {

    $('#modalTitle').html(header);
    $('#modalLabel').html(message);
    $('#alertModal').modal('show');
    if (url != null) {
        setTimeout(function() { window.location.href = url; }, 3000);
    }
}