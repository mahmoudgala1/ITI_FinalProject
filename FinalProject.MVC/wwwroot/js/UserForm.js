$(document).ready(function () {
    $('#Image').on('change', function () {
        $('.image-preview').attr('src', window.URL.createObjectURL(this.files[0])).removeClass('d-none');
    });
});