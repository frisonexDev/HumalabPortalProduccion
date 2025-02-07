function MensajeAlerta(tipo, mensaje) {

    var message = mensaje;
    var type = tipo;
    var duration = 3000;
    var ripple = true;
    var dismissible = false;
    var positionX = "right";
    var positionY = "top";
    window.notyf.open({
        type,
        message,
        duration,
        ripple,
        dismissible,
        position: {
            x: positionX,
            y: positionY
        }
    });

}


function MensajeAlerta1(tipo, mensaje)
{
    const notyf = new Notyf({
        duration: 5000,    
        ripple : true,
        dismissible : true,
        position: {
            x: 'right',
            y: 'top',
        },
        types: [{ type: "default", backgroundColor: "#3B7DDD", icon: { className: "notyf__icon--success", tagName: "i" } },
            { type: "success", backgroundColor: "#28a745", icon: { className: "notyf__icon--success", tagName: "i" } },
            { type: "warning", backgroundColor: "#ffc107", icon: { className: "notyf__icon--error", tagName: "i" } },
            { type: "danger", backgroundColor: "#dc3545", icon: { className: "notyf__icon--error", tagName: "i" } }
        ]
    });

    notyf.open({
        type: tipo,
        message: mensaje
    });
}