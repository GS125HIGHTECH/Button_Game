$(function () {
    $(document).on("click", ".game-button", function (event) {
        event.preventDefault();
        var buttonNumber = $(this).val();
        doButtonUpdate(buttonNumber);
    })

    function doButtonUpdate(buttonNumber) {
        $.ajax({
            datatype: "json",
            method: "POST",
            url: "/button/ShowOneButton",
            data: {
                "buttonNumber": buttonNumber
            },
            success: function (data) {

                $("#" + buttonNumber).html(data.part1);
                $("#messageArea").html(data.part2);
            }
        })
    }
})