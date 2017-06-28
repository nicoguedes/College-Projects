$(function () {
    $('#Tag').autocomplete({
        source: JSON.parse(tagsSource)
    });
});