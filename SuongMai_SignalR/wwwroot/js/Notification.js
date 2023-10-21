// create a connecttion
var connectionNotification = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/notificationHub").build();

document.getElementById("sendButton").disabled = true;

connectionNotification.on("LoadNotification", function (message, counter) {
    document.getElementById("messageList").innerHTML = "";
    var notificationCounter = document.getElementById("notificationCounter");
    notificationCounter.innerHTML = "<span>(" + counter + ")</span>";
    for (let i = message.length - 1; i >= 0; i--) {
        var li = document.createElement("li");
        li.textContent = "Notification - " + message[i];
        document.getElementById("messageList").appendChild(li);

    }

})

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("notificationInput").value;
    connectionNotification.send("SendMessage", message).then(function(){
        document.getElementById("notificationInput").value = "";
    });
});

connectionNotification.start().then(function () {
    connectionNotification.send("LoadMessage");
    document.getElementById("sendButton").disabled = false;
});


