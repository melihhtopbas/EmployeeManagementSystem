$(function () {
    $("#tblPersoneller").on("click", ".btnPersonelSil", function () {
        gelenID = new Object();
        gelenID.ID = $(this).attr("data-id");
        var btn = $(this);

        Swal.fire({
            title: 'Are you sure?',
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


                Swal.fire(
                    'Deleted!',
                    'Your file has been deleted.',
                    'success'
                )
            }
        })



    });

});