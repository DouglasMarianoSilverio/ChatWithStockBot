"use strict";



var connection = new signalR.HubConnectionBuilder().withUrl(window._signalRAddress+"/chathub").build();

connection.onclose(() => setTimeout(startSignalRConnection(connection), 5000));

const startSignalRConnection = connection => connection.start()
    .then(() => console.info('Websocket Connection Established'))
    .catch(err => console.error('SignalR Connection Error: ', err));


connection.on("ReceiveMessage", function (messages) {

    $("#messagesList").empty();

    
    messages.forEach(function (m) {
        var _date = new Date(m.date);

        var li = $("<li></li>").text(_date.toLocaleString("en-US") + " - " + m.user + ": " + m.text);        
        li.addClass("list-group-item");
        $("#messagesList").append(li);
    });
});

connection.start().then(function () {
    
}).catch(function (err) {
    return console.error(err.toString());
});
