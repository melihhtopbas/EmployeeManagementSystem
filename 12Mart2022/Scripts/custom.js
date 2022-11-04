$(function () {
    $("#tblDepartmanlar").on("click", ".btnDepartmanSil", function () {
        alert("click");

    });

});






function CheckDateTypeIsValıd(dateElement) {
    var value = $(dateElement).val();
    if (value == '') {
        $(dateElement).valid("false");
    }
    else {
        $(dateElement).valid();
    }

}