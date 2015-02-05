function confirmExit() { }

window.onbeforeunload = null;

window.errors = [];
window.onerror = function (e, url, lineNumber) {
    window.errors.push(e + ' - url: ' + url + ' - line: ' + lineNumber);
}

window.ajaxRequests = 0;
function incrementAjaxRequests(conn, options) {
    if (console) {
        console.log(" -- START " + options.url + " @ " + window.ajaxRequests);
    }
    window.ajaxRequests++;
}

function decrementAjaxRequests(conn, response, options) {
    if (window.ajaxRequests == 0) return;
    if (console) {
        console.log(" -- STOP " + options.url + " @ " + window.ajaxRequests);
    }
    window.ajaxRequests--;
}

_SaveGuiState = false;
Ext.ux.desktop.MessageFactory.timeout = false;

Ext.Ajax.on('beforerequest', incrementAjaxRequests);
Ext.Ajax.on('requestcomplete', decrementAjaxRequests);
Ext.Ajax.on('requestexception', decrementAjaxRequests);