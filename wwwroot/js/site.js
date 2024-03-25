$(function () {
    $(document).on("mousedown", ".game-button", function (event) {
        event.preventDefault();
        switch (event.which) {
            case 1:
                var buttonNumber = $(this).val();
                doButtonUpdate(buttonNumber, "button/ShowOneButton");
                break;
            case 2:
                break;
            case 3:
                var buttonNumber = $(this).val();
                doButtonUpdate(buttonNumber, "button/RightClickOneButton");
                break;
            default:
                alert("Nothing");
        }
    })

    $(document).bind("contextmenu", function (e) {
        e.preventDefault();
       
    })

    function doButtonUpdate(buttonNumber, urlString) {
        $.ajax({
            datatype: "json",
            method: "POST",
            url: urlString,
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