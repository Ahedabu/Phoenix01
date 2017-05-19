// fadein function for all pages
$(function () {
    $('div.hidden').fadeIn(600).removeClass('hidden');
});


$(function () {
    $('[data-toggle="tooltip"]').tooltip();
});

//choose user language
$(function () {
    $("#AddUserLanguage").change(function () {
        var lang = $(this).val();
        if (lang !== "") {
            $.post("/Manage/AddLang?lang=" + lang, "", function (res) {
                if (res.Success === "true") {
                    //update the ChosenLanguages div element
                    $("#ChosenLang").append('<input class="btn btn-info btn-md" data-toggle="tooltip" title="Click to remove" type="button" value=' + lang + ' id="RemoveUserLanguage" name="RemoveUserLanguage" /><a> </a>');

                } else {
                    alert("Error getting data!");
                }
            });
        }
    });
});

//remove user language
$(function () {
    $(document).on('click', "input[type='button']", function () {
        var lang = $(this).val();
        $(this).remove();
        //var data = { RemoveUserLanguage : lang}
        if (lang !== "") {
            $.post("/Manage/RemoveLang?lang=" + lang, function (res) {
                if (res.Success === "false") {
                    alert("Error getting data!");
                }
            });
        }
    });
});