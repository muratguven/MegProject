$(document).ready(function() {
    $('#btnCreate').on('click', function() {
        $('#userGroupModal').modal('show');
    });

 

});


var alertUserGroupMessages=function(data) {
    showModal(data.result, data.header, data.message, data.url);
}

