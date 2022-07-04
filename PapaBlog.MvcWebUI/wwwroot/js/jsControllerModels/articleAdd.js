$(document).ready(function () {

    // trumbowyg - start
    $('#text-editor').trumbowyg({
        btns: [
            ['viewHTML'],
            ['undo', 'redo'], // Only supported in Blink browsers
            ['formatting'],
            ['strong', 'em', 'del'],
            ['superscript', 'subscript'],
            ['link'],
            ['insertImage'],
            ['justifyLeft', 'justifyCenter', 'justifyRight', 'justifyFull'],
            ['unorderedList', 'orderedList'],
            ['horizontalRule'],
            ['removeformat'],
            ['fullscreen'],
            ['foreColor', 'backColor'],
            ['emoji'],
            ['fontfamily'],
            ['fontsize']
        ],
        plugins: {
            colors: {
                foreColorList: [
                    'ff0000', '00ff00', '0000ff', 'ff1493'
                ],
                backColorList: [
                    '000', '555', 'ff0000'
                ],
                displayAsList: false
            }
        }
    });

    // trumbowyg - end

    // select2 - start
    $('#categoryList').select2({
        placeholder: "Bir kategori seçiniz..",
        allowClear: true,
        theme: 'bootstrap4',
    });

    // select2 - end

    // datepicker - start
    $(function () {
        $("#datepicker").datepicker(
            {
                closeText: "kapat",
                prevText: "&#x3C;geri",
                nextText: "ileri&#x3e",
                currentText: "bugün",
                monthNames: ["Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"],
                monthNamesShort: ["Oca", "Şub", "Mar", "Nis", "May", "Haz", "Tem", "Ağu", "Eyl", "Eki", "Kas", "Ara"],
                dayNames: ["Pazar", "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi"],
                dayNamesShort: ["Pz", "Pt", "Sa", "Ça", "Pe", "Cu", "Ct"],
                dayNamesMin: ["Pz", "Pt", "Sa", "Ça", "Pe", "Cu", "Ct"],
                weekHeader: "Hf",
                dateFormat: "dd.mm.yy",
                firstDay: 1,
                isRTL: false,
                showMonthAfterYear: false,
                yearSuffix: "",
                duration: 500,
                minDate: -3,
                maxDate: +3
            });
    });

    // datepicker - end
});