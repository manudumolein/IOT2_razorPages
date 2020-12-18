"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/GpioHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

// Luisteren naar ReceiveMessage
connection.on("ReceiveMessage", function (output1, output2, output3) {
    document.getElementById("pin13").checked = output1;
    document.getElementById("pin19").checked = output2;
    document.getElementById("pin26").checked = output3;
});

// Starten
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

// verzenden
document.getElementById("sendButton").addEventListener("click", function (event) {
    var output1 = document.getElementById("pin13").checked;
    var output2 = document.getElementById("pin19").checked;
    var output3 = document.getElementById("pin26").checked;
    connection.invoke("SendMessage", output1, output2,output3).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

