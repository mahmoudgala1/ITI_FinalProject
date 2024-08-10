$.validator.addMethod('filesize', function (value, element, param) {
    var isVaild = this.optional(element) || element.files[0].size <= param;
    return isVaild;
});