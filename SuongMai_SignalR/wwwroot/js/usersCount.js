//using Microsoft.signalR
// create a connecttion
var connectionUserCount = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/userCount", signalR.HttpTransportType.ServerSentEvents).build();

// connect to method that hub
connectionUserCount.on("updateTotalViews", (value) => {
    var newCountspan = document.getElementById("totalViewsCounter");
    newCountspan.innerText = value.toString();
});

connectionUserCount.on("updateTotalUser", (value) => {
    var newCountspan = document.getElementById("totalUsersCounter");
    newCountspan.innerText = value.toString();
});

function newWindowLoadOnClient(){
    connectionUserCount.send("NewWindownLoaded");
}
//Ok
function fulfilled(){
    console.log("Connection to USer hub successfully!!!");
    newWindowLoadOnClient();
}
// Fail
function rejected() {

}
// start connection 
connectionUserCount.start().then(fulfilled, rejected);
