function modificarPerfil(id) {
    $.get("/Perfil/Modificar/" + id, function (data) {
        $("#PartialModal").html(data);
        $("#country_selector2").countrySelect({
            defaultCountry: "cl",
            preferredCountries: ['cl', 'us', 'mx']
        });
        var nacionalidad = document.getElementById("nac");
        $("#country_selector2").countrySelect("setCountry", nacionalidad.value );
        $("#PartialModal").show();
    });
}
function eliminarPerfil(id){
    $.get("/Perfil/Eliminar/" + id, function (data) {
        $("#PartialModal").html(data);
        $("#PartialModal").show();
    });
}
function closeModal() {
    var modal = document.getElementById("PartialModal");
    modal.style.display = 'none';
}