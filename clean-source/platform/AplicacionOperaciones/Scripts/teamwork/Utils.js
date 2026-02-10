function formatRutTypeSoftland(rut) {

    var formatRut = "";
    var emptyRut = rut.split(".").join("").split("-").join("");
    var lengthCadena = 0;

    /** 18425683-5 */

    lengthCadena = emptyRut.length;

    if (emptyRut.substr(0, 1) === "0") {
        lengthCadena = lengthCadena - 1;
    }

    if (emptyRut.substr(0, 2) === "00") {
        lengthCadena = lengthCadena - 1;
    }

    emptyRut = emptyRut.substr(emptyRut.length - lengthCadena, emptyRut.length);

    switch (lengthCadena) {
        case 9:

            formatRut = emptyRut.substr(0, 2) + "." + emptyRut.substr(2, 3) + "." + emptyRut.substr(5, 3) + "-" + emptyRut.substr(8, 1);

            formatRut = "0" + formatRut;

            break;
        case 8:

            formatRut = emptyRut.substr(0, 1) + "." + emptyRut.substr(1, 3) + "." + emptyRut.substr(4, 3) + "-" + emptyRut.substr(7, 1);

            formatRut = "00" + formatRut;

            break;
    }

    /** 9832783-2 */



    return formatRut;

}

function extensionFile(extension) {

    var ext = "";

    switch (extension.toLowerCase()) {
        case "xls":
            ext = "Archivo de tipo Excel";
            break;
        case "xlsx":
            ext = "Archivo de tipo Excel";
            break;
        case "xltm":
            ext = "Archivo de tipo Excel habilitado para macro";
            break;
        case "xlsm":
            ext = "Archivo de tipo Excel habilitado para macro";
            break;
        case "doc":
            ext = "Archivo de tipo Work";
            break;
        case "docx":
            ext = "Archivo de tipo Work";
            break;
        case "ppt":
            ext = "Archivo de tipo Power Point";
            break;
        case "pptx":
            ext = "Archivo de tipo Power Point";
            break;
        case "pdf":
            ext = "Archivo de tipo PDF";
            break;
        case "msg":
            ext = "Archivo de tipo Correo Outlook";
            break;
        case "txt":
            ext = "Archivo de tipo texto plano";
            break;
        case "png":
            ext = "Archivo de tipo imagen PNG";
            break;
        case "jpg":
            ext = "Archivo de tipo imagen JPG";
            break;
        case "jpge":
            ext = "Archivo de tipo imagen JPGE";
            break;
        case "gif":
            ext = "Archivo de tipo imagen GIF";
            break;
        case "7z":
            ext = "Archivo de tipo comprimido";
            break;
        case "zip":
            ext = "Archivo de tipo comprimido";
            break;
        case "rar":
            ext = "Archivo de tipo comprimido";
            break;
        default:
            ext = "Otro tipo de archivo";
            break;

    }

    return ext;
}

function extensionFileIcon(extension) {
    var extIcon = "";

    switch (extension.toLowerCase()) {
        case "xls":
            extIcon = "glyphicon-tw-7 glyphicon-tw-excel";
            break;
        case "xlsx":
            extIcon = "glyphicon-tw-7 glyphicon-tw-excel";
            break;
        case "xltm":
            extIcon = "glyphicon-tw-7 glyphicon-tw-excel";
            break;
        case "xlsm":
            extIcon = "glyphicon-tw-7 glyphicon-tw-excel";
            break;
        case "doc":
            extIcon = "glyphicon-tw-7 glyphicon-tw-word";
            break;
        case "docx":
            extIcon = "glyphicon-tw-7 glyphicon-tw-word";
            break;
        case "ppt":
            extIcon = "glyphicon-tw-7 glyphicon-tw-ppt";
            break;
        case "pptx":
            extIcon = "glyphicon-tw-7 glyphicon-tw-ppt";
            break;
        case "pdf":
            extIcon = "glyphicon-tw-7 glyphicon-tw-docpdf";
            break;
        case "msg":
            extIcon = "glyphicon-tw-7 glyphicon-tw-outlook";
            break;
        case "txt":
            extIcon = "glyphicon-tw-7 glyphicon-tw-txt";
            break;
        case "png":
            extIcon = "glyphicon-tw-7 glyphicon-tw-imagen";
            break;
        case "jpg":
            extIcon = "glyphicon-tw-7 glyphicon-tw-imagen";
            break;
        case "jpge":
            extIcon = "glyphicon-tw-7 glyphicon-tw-imagen";
            break;
        case "gif":
            extIcon = "glyphicon-tw-7 glyphicon-tw-imagen";
            break;
        case "7z":
            extIcon = "glyphicon-tw-7 glyphicon-tw-compress";
            break;
        case "zip":
            extIcon = "glyphicon-tw-7 glyphicon-tw-compress";
            break;
        case "rar":
            extIcon = "glyphicon-tw-7 glyphicon-tw-compress";
            break;
        default:
            extIcon = "glyphicon-tw-7 glyphicon-tw-default";
            break;

    }

    return extIcon;
}

function extensionFileType(extension) {
    var type = "";

    switch (extension.toLowerCase()) {
        case "xls":
            type = "btn btn-success";
            break;
        case "xlsx":
            type = "btn btn-success";
            break;
        case "xltm":
            type = "btn btn-success";
            break;
        case "xlsm":
            type = "btn btn-success";
            break;
        case "doc":
            type = "btn btn-primary";
            break;
        case "docx":
            type = "btn btn-primary";
            break;
        case "ppt":
            type = "btn btn-warning";
            break;
        case "pptx":
            type = "btn btn-warning";
            break;
        case "pdf":
            type = "btn btn-danger";
            break;
        case "msg":
            type = "btn btn-info";
            break;
        case "txt":
            type = "btn btn-info";
            break;
        case "png":
            type = "btn btn-info";
            break;
        case "jpg":
            type = "btn btn-info";
            break;
        case "jpge":
            type = "btn btn-info";
            break;
        case "gif":
            type = "btn btn-info";
            break;
        case "7z":
            type = "btn btn-info";
            break;
        case "zip":
            type = "btn btn-info";
            break;
        case "rar":
            type = "btn btn-info";
            break;
        default:
            type = "btn btn-info";
            break;

    }

    return type;
}

function convertFechaPalabra(fecha) {
    var convertFecha = "";

    var splitFecha = fecha.split('-');

    convertFecha = splitFecha[0] + ' de ';

    switch (parseInt(splitFecha[1])) {
        case 1:
            convertFecha = convertFecha + 'Enero';
            break;
        case 2:
            convertFecha = convertFecha + 'Febrero';
            break;
        case 3:
            convertFecha = convertFecha + 'Marzo';
            break;
        case 4:
            convertFecha = convertFecha + 'Abril';
            break;
        case 5:
            convertFecha = convertFecha + 'Mayo';
            break;
        case 6:
            convertFecha = convertFecha + 'Junio';
            break;
        case 7:
            convertFecha = convertFecha + 'Julio';
            break;
        case 8:
            convertFecha = convertFecha + 'Agosto';
            break;
        case 9:
            convertFecha = convertFecha + 'Septiembre';
            break;
        case 10:
            convertFecha = convertFecha + 'Octubre';
            break;
        case 11:
            convertFecha = convertFecha + 'Noviembre';
            break;
        case 12:
            convertFecha = convertFecha + 'Diciembre';
            break;
    }

    convertFecha = convertFecha + ' de ' + splitFecha[2];

    return convertFecha;
}

function standarFileName(fileName) {
    var standarFileNames = "";

    var caracteres = ["'"];

    for (var i = 0; i < caracteres.length; i++) {
        if (fileName.split(caracteres[i]).length > 0) {
            standarFileNames = fileName.split(caracteres[i]).join(" ");
        }
    }

    return standarFileNames;
}

function diffDays(fecha) {
    var date = new Date();

    var dateToday = new Date(date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate());

    var dateToValid = new Date(fecha.split("-")[2] + '-' + fecha.split('-')[1] + '-' + fecha.split('-')[0]);

    var diff = dateToValid.getTime() - dateToday.getTime();

    var days = Math.round(diff / (1000 * 60 * 60 * 24));

    return days;
}

function maskRutTypeSoftland(rut) {
    var mask = "";

    if (rut !== "") {
        switch (rut.length - 1) {
            case 0:
                mask = '0' + rut.substr(rut.length - 1, 1);
                break;
            case 1:
                mask = rut.substr(0, rut.length - 1) + rut.substr(rut.length - 1, 1);
                break;
            case 2:
                mask = rut.substr(0, rut.length - 1) + rut.substr(rut.length - 1, 1);
                break;
            case 3:
                mask = rut.substr(0, rut.length - 1) + '.' + rut.substr(rut.length - 1, 1);
                break;
            case 4:
                mask = rut.substr(0, rut.length - 1) + '.' + rut.substr(rut.length - 1, 1);
                break;
            case 5:

                break;
            case 6:

                break;
            case 7:

                break;
            case 8:

                break;
        }
    }

    return mask;
}

function daysList() {
    var html = '';

    html = '<option value="0">Día</option>';
    html = html + '<option value="01">01</option>';
    html = html + '<option value="02">02</option>';
    html = html + '<option value="03">03</option>';
    html = html + '<option value="04">04</option>';
    html = html + '<option value="05">05</option>';
    html = html + '<option value="06">06</option>';
    html = html + '<option value="07">07</option>';
    html = html + '<option value="08">08</option>';
    html = html + '<option value="09">09</option>';
    html = html + '<option value="10">10</option>';
    html = html + '<option value="11">11</option>';
    html = html + '<option value="12">12</option>';
    html = html + '<option value="13">13</option>';
    html = html + '<option value="14">14</option>';
    html = html + '<option value="15">15</option>';
    html = html + '<option value="16">16</option>';
    html = html + '<option value="17">17</option>';
    html = html + '<option value="18">18</option>';
    html = html + '<option value="19">19</option>';
    html = html + '<option value="20">20</option>';
    html = html + '<option value="21">21</option>';
    html = html + '<option value="22">22</option>';
    html = html + '<option value="23">23</option>';
    html = html + '<option value="24">24</option>';
    html = html + '<option value="25">25</option>';
    html = html + '<option value="26">26</option>';
    html = html + '<option value="27">27</option>';
    html = html + '<option value="28">28</option>';
    html = html + '<option value="29">29</option>';
    html = html + '<option value="30">30</option>';
    html = html + '<option value="31">31</option>';

    return html;
}

function MonthList() {
    var html = '';

    html = '<option value="0">Mes</option>';
    html = html + '<option value="01">Ene</option>';
    html = html + '<option value="02">Feb</option>';
    html = html + '<option value="03">Mar</option>';
    html = html + '<option value="04">Abr</option>';
    html = html + '<option value="05">May</option>';
    html = html + '<option value="06">Jun</option>';
    html = html + '<option value="07">Jul</option>';
    html = html + '<option value="08">Ago</option>';
    html = html + '<option value="09">Sep</option>';
    html = html + '<option value="10">Oct</option>';
    html = html + '<option value="11">Nov</option>';
    html = html + '<option value="12">Dic</option>';

    return html;
}

function YearList() {
    var html = '';
    var date = new Date();

    html = '<option value="0">Año</option>';
    for (var i = date.getFullYear(); i >= 1900; i--) {
        html = html + '<option value="' + i + '">' + i + '</option>';
    }

    return html;
}

function validIsNumeric(caracter) {
    var retorno = "";

    switch (caracter) {
        case "1":
            retorno = "S";
            break;
        case "2":
            retorno = "S";
            break;
        case "3":
            retorno = "S";
            break;
        case "4":
            retorno = "S";
            break;
        case "5":
            retorno = "S";
            break;
        case "6":
            retorno = "S";
            break;
        case "7":
            retorno = "S";
            break;
        case "8":
            retorno = "S";
            break;
        case "9":
            retorno = "S";
            break;
        case "0":
            retorno = "S";
            break;
        default:
            retorno = "N";
            break;
    }

    return retorno;
}