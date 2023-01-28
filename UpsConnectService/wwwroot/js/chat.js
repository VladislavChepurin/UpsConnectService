"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();


connection.on("ReceiveMessage", function (serial, name, status, inputVolt, outputVolt) {
    document.getElementById("serialNumber").innerHTML = serial;
    document.getElementById("nameDevice").innerHTML = name;
    document.getElementById("statusCode").innerHTML = status;
    document.getElementById("InputVoltage").innerHTML = inputVolt;
    document.getElementById("OutputVoltage").innerHTML = outputVolt;
});

connection.start().then(function () {
   
}).catch(function (err) {
    return console.error(err.toString());
});
