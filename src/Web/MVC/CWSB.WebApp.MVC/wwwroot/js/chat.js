"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44339/chathub").build();


connection.on("ReceiveMessage", function (user, message, when) {
    var date = Date.parse(when);
    console.log()
    var msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
    var li = $("<li></li>").text(when + " - " + user + ": " + msg);
    console.log(when + " - " + user + ": " + msg);
    li.addClass("list-group-item");
    $("#messagesList").append(li);
});

connection.start().then(function () {
    
}).catch(function (err) {
    return console.error(err.toString());
});
