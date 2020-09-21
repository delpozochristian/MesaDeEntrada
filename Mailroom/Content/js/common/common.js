function ShowLoading() {
    var divResponse = $('#' + 'divLoading');
    divResponse.html('<div style="z-index:9999;" class="modalLoading"></div>');
}

function HideLoading() {
    var divResponse = $('#' + 'divLoading');
    divResponse.html('');
}

function LoadMaterialize()
{
    $('select').material_select();
    $('.collapsible').collapsible();
    $('.datepicker').pickadate({
        selectMonths: true, // Creates a dropdown to control month
        selectYears: 15 // Creates a dropdown of 15 years to control year
    });
    $('.modal').modal();
}

function LoadMaterializeDelay()
{
    setTimeout(function () { LoadMaterialize(); }, 1000);
}


function ShowAlertSuccess(texto) {
    swal({
        title: "",
        text: texto,
        type: "success",
        showCancelButton: false,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "OK",
        allowEscapeKey: false,
        closeOnConfirm: true
    },
        function () {
        });
}

function ShowAlertSuccessWithRedirect(texto, redirect) {
    swal({
        title: "",
        text: texto,
        type: "success",
        showCancelButton: false,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "OK",
        allowEscapeKey: false,
        closeOnConfirm: true
    },
        function () {
            window.location.replace(redirect);
        });
}

function ShowAlertError(texto) {
    HideLoading();
    sweetAlert('Error', texto, 'error');
}
