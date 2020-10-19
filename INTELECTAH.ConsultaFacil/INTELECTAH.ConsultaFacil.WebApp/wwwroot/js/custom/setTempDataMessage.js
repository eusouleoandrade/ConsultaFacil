function setTempDataMessage(message) {
    var tempDataMessage = message;

    if (tempDataMessage != '') {
        setToastr();
        toastr.success(tempDataMessage, 'Notificação');
    };
}

function setTempDataMessageError(message) {
    var tempDataMessage = message;

    if (tempDataMessage != '') {
        setToastr();
        toastr.error(tempDataMessage, 'Erro');
    };
}