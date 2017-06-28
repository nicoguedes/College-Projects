$(function () {
    $('#QuantidadeQuestoes').change(function () {
        $('#ErrosPermitidos').attr('max', $(this).val());
    });
});