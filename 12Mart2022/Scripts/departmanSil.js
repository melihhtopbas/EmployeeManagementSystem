$(function () {
    $("#tblDepartmanlar").dataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
        }
    });
    $("#tblDepartmanlar").on("click", ".btnDepartmanSil", function () {
        gelenID = new Object();
        gelenID.ID = $(this).attr("data-id");
        var btn = $(this);



        Swal.fire({
            title: 'Emin misiniz?',
            text: "Sildikten sonra geri alamayacaksınız!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet, sil!'
        }).then((result) => {
            if (result.isConfirmed) {

                $.ajax({
                    type: "GET",
                    url: "/Departman/Sil/" + gelenID.ID,
                    success: function () {
                        btn.parent().parent().remove();


                    }
                });


                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'İşlem başarıyla tamamlandı!',
                    showConfirmButton: false,
                    timer: 1500,

                })


            }
        })



    });

});