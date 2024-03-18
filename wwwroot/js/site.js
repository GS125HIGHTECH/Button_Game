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

                $("#" + buttonNumber).html(data);
            }
        })
        $.ajax({
            url: "/button/GetModelData",
            method: "GET",
            dataType: "json",
            success: function (data) {
                var allGreen = checkAllButtonsGreen(data);
                if (allGreen) {
                    $("#allMatch").text("Congratulations. All buttons are green!");
                } else {
                    $("#allMatch").text("Not all buttons are green. See if you can make them all match.");
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error("Error fetching model data:", textStatus, errorThrown);
            }
        });
    }
    function checkAllButtonsGreen(modelData) {
        return modelData.every(button => button.buttonState === 0);
    }
})