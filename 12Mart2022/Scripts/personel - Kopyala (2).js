$(function () {
    
    $("#tblPersoneller").on("click", ".btnPersonelSil", function () {
        var btn = $(this);
        bootbox.confirm("Personeli silmek istediğinize emin misiniz? ", function (result) {
            if (result) {
                var id =btn.data("id");

                $.ajax({
                    type: "GET",
                    url: "/Personel/Sil/" + id,
                    success: function () {
                        btn.parent().parent().remove();

                    }
                });
            }

        })
       

       
    });

});