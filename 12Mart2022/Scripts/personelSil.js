$(function () {
    $("#tblPersoneller").dataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
        }
    });
    $("#tblPersoneller").on("click", ".btnPersonelSil", function () {
        gelenID = new Object();
        gelenID.ID = $(this).attr("data-id");
        var btn = $(this);

        Swal.fire({
            title: 'Eminmisinnnniz?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {

                $.ajax({
                    type: "GET",
                    url: "/Personel/Sil/" + gelenID.ID,
                    success: function () {
                        btn.parent().parent().remove();


                    }
                });


                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'Your work has been saved',
                    showConfirmButton: false,
                    timer: 1500,

                })


            }
        })



    });

});