$(function () {

    $("#SelectedWidget").change(function () {

        var t = $(this).val();

        if (t !== "") {
            $.post("@Url.Action("GetDefault", "Manage")?val=" + t, function (res) {
                if (res.Success === "true") {

                    //enable the text boxes and set the value

                    $("#Width").prop('disabled', false).val(res.Data.Width);
                    $("#Height").prop('disabled', false).val(res.Data.Height);

                } else {
                    alert("Error getting data!");
                }
            });
        } else {
            //Let's clear the values and disable :)
            $("input.editableItems").val('').prop('disabled', true);
        }

    });
});