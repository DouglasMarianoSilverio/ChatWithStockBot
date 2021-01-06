"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44339/chathub").build();


connection.on("ReceiveMessage", function (messages) {

    $("#messagesList").empty();

    
    messages.forEach(function (m) {        
        var li = $("<li></li>").text(m.date + " - " + m.user + ": " + m.text);        
        li.addClass("list-group-item");
        $("#messagesList").append(li);
    });
});

connection.start().then(function () {
    
}).catch(function (err) {
    return console.error(err.toString());
});
