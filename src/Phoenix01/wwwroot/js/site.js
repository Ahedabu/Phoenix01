// datepicker for user birthday
$(function () {
    $("#datepicker").datepicker({ changeMonth: true, changeYear: true, yearRange: '1900:2017' });
});


// fadein function for all pages
$(function () {
    $('div.hidden').fadeIn().removeClass('hidden');
});