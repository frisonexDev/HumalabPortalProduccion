/*!
* Start Bootstrap - Simple Sidebar v6.0.6 (https://startbootstrap.com/template/simple-sidebar)
* Copyright 2013-2023 Start Bootstrap
* Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-simple-sidebar/blob/master/LICENSE)
*/
// 
// Scripts
// 

window.addEventListener('DOMContentLoaded', event => {

    // Toggle the side navigation
    const sidebarToggle = document.body.querySelector('#sidebarToggle');
    if (sidebarToggle) {
        // Uncomment Below to persist sidebar toggle between refreshes
        // if (localStorage.getItem('sb|sidebar-toggle') === 'true') {
        //     document.body.classList.toggle('sb-sidenav-toggled');
        // }
        sidebarToggle.addEventListener('click', event => {
            event.preventDefault();
            document.body.classList.toggle('sb-sidenav-toggled');
            localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sb-sidenav-toggled'));
        });
    }

});



function loadingShow() {
    $.LoadingOverlay("show",{
        image: "../Resources/images/Spinner/spinner.svg",
        fontawesome: "fa fa-cog fa-spin",
        imageColor: "#00558B",
        background: "rgba(129, 159, 128, 0.5)"
    });
}

function loadingHide() {
    $.LoadingOverlay("hide"); 
}


function ListarEstados(estado, Combo) {
    var html = '';
    $.ajax({

        url: '@Url.Action("ListarEstados", "Cliente")',
        type: 'GET',
        data: { NombreEstado: estado },

        success: function (response) {
            const listEstado = JSON.parse(response)

            if (listEstado.length > 0) {
                for (var i = 0; i < listEstado.length; i++) {
                    html += '<option value="' + listEstado[i].Valor + '"' + '>'
                        + listEstado[i].Nombre
                        + '</option>';
                }

                $('#' + Combo).append(html);

            }

        }

    });

}

const removeAccents = (str) => {
    return str.normalize("NFD").replace(/[\u0300-\u036f]/g, "");
}


function activarMenu(IdItem, className = 'activado') {    
    $('.list-group-item').removeClass(className);
    $('#' + IdItem).addClass(className);   
}