"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();


connection.on("ReceiveMessage", function (serial, name) {
    document.getElementById("serialNumber").innerHTML = serial;
    document.getElementById("nameDevice").innerHTML = name;
});

connection.start().then(function () {
   
}).catch(function (err) {
    return console.error(err.toString());
});
