"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44339/chathub").build();


connection.on("ReceiveMessage", function (messages) {

    $("#messagesList").empty();

    
    messages.forEach(function (m) {
        //console.log(m);
        //console.log(m.user);
        //console.log(m.text);
        //console.log(m.date);
        var li = $("<li></li>").text(m.date + " - " + m.user + ": " + m.text);        
        li.addClass("list-group-item");
        $("#messagesList").append(li);
    });


    console.log(list);

    /*var msg = message.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
    var li = $("<li></li>").text(when + " - " + user + ": " + msg);
    console.log(when + " - " + user + ": " + msg);
    li.addClass("list-group-item");
    $("#messagesList").append(li);*/
});

connection.start().then(function () {
    
}).catch(function (err) {
    return console.error(err.toString());
});
