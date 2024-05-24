selectUser = function (selectedRow, txt) {
    var searchBox = document.getElementById(txt);
    if (selectedRow) {
        selectedRow.className = "selectedRow";
        var sid = selectedRow.cells[2].innerHTML;
        var fullName = selectedRow.cells[0].innerHTML + ' ' + selectedRow.cells[1].innerHTML;
        var selectedUser = fullName + ' | ' + sid;
        searchBox.value = selectedUser;
        $("#gvUsers.documents").hide();
    }
};

function findAdUser(txt, div) {
    var searchtext = document.getElementById(txt).value;
    if (searchtext.length > 1) {
        SearchActiveDirectoryUser(searchtext, div, txt);
    }
}
function SearchActiveDirectoryUser(searchName, container, txt) {
    window.IncidentTrackingService.SearchUsers(
        searchName, function (result) {
            processServiceResults(eval(result), container, txt);
        }, RequestFailedCallback);
}

processServiceResults = function (data, container, txt) {

    var content = '<table style="width:100%;" class="documents" id="gvUsers">' +
        '<tr>' +
            '<th>First Name</th>' +
            '<th>Surname</th>' +
            '<th>SID</th>' +
        '</tr>';

    if (data) {

        for (var i = 0, len = data.length; i < len; ++i) {
            var html = createRow(data[i], txt);
            content = content + ' ' + html;

        }
    }
    else {
        content = '<table style="width:100%;" class="tableSytle"><tr><td>THERE IS NO USER FOUND FOR YOUR CRITERIA</td></tr> </table>';
    }
    content = content + ' </table>';
    container.innerHTML = content;
};


createRow = function (aduser, txt) {
    var html = '<tr onclick="selectUser(this,\'' + txt + '\');" onmouseover="style.cursor=\'cursor\'">' +
        '<td>' + aduser.Name + '</td>' +
        '<td>' + aduser.Surname + '</td>' +
        '<td>' + aduser.SID + '</td>' +
        '</tr>';

    return html;

};




function RequestFailedCallback(error) {
    var stackTrace = error.get_stackTrace();
    var message = error.get_message();
    var statusCode = error.get_statusCode();
    var exceptionType = error.get_exceptionType();
    var timedout = error.get_timedOut();


    var errorMessage = "Stack Trace: " + stackTrace + "<br/>" +
                 "Service Error: " + message + "<br/>" +
                 "Status Code: " + statusCode + "<br/>" +
                 "Exception Type: " + exceptionType + "<br/>" +
                 "Timedout: " + timedout;

    alert(errorMessage);
}