$(function () {
    $("#tblDepartmanlar").on("click", ".hataliSilme", function () {
        Swal.fire({
            position: 'top',
            icon: 'error',
            title: 'Departmana ait personel olduğundan silme işlemi yapamazsınız!',
            showConfirmButton: false,
            timer: 1500
        })

    });

});