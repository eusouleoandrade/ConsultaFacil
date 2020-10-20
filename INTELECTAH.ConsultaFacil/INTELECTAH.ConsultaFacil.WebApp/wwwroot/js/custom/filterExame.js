document.querySelector('#TipoExameId').addEventListener("change", function (event) {

    if (event.target.value != '') {
        FilterExame(event.target.value);
    } else {
        var selectList = document.querySelector('#ExameId');
        selectList.innerText = null;
        var div_data = "<option value>Selecione o exame</option>";
        $(div_data).appendTo(selectList);
    }
});

function FilterExame(idTipoExame) {

    SetLoad();
    var form = $('#__AjaxAntiForgeryFormConsulta');
    var token = $('input[name="__RequestVerificationToken"]', form).val();

    $.ajax(
        {
            type: 'POST',
            url: '/Consulta/FilterExame',
            dataType: 'json',
            data: {
                __RequestVerificationToken: token,
                idTipoExame: idTipoExame
            },
            cache: false,
            async: true,
            success: function (data) {

                var selectList = document.querySelector('#ExameId');
                selectList.innerText = null;

                $.each(data, function (i, obj) {
                    var div_data = "<option value=" + obj.value + ">" + obj.text + "</option>";
                    $(div_data).appendTo(selectList);
                });
                SetLoad();
            },
            error: function () {
                SetLoad();
            }
        });
    return false;
};

function SetLoad() {

    $('#ibox1').children('.ibox-content').toggleClass('sk-loading');
};