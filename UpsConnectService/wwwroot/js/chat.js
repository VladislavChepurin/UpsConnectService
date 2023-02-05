"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();


connection.on("ReceiveMessage", function (request) {

    var element = document.getElementById(request.serialNumber);

    //  Make label
    var label = document.createElement('label');
    label.textContent = "Марка устойства: " + request.nameDevice;
    label.setAttribure('id', 'новый_айди');
    var br = document.createElement('br');

    element.appendChild(br);
    element.appendChild(label);
});

connection.start().then(function () {
   
}).catch(function (err) {
    return console.error(err.toString());
});
