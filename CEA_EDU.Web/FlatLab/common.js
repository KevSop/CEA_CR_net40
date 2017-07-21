
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

function dateFormatter(value, row, index) {
    if (value == null || value == undefined || value == "") {
        return "";
    }

    if (value.indexOf("/Date(") == -1) {
        return value;
    }

    var date = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();

    var hours = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
    var minutes = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
    var seconds = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();

    return date.getFullYear() + "-" + month + "-" + currentDate + " " + hours + ":" + minutes + ":" + seconds;
}

function dateFormatterOnlyDate(value, row, index) {
    if (value == null || value == undefined || value == "") {
        return "";
    }

    if (value.indexOf("/Date(") == -1) {
        return value;
    }

    var date = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();

    return date.getFullYear() + "-" + month + "-" + currentDate;
}