//using Microsoft.signalR
var cloakspan = document.getElementById("cloakCounter");
var stonespan = document.getElementById("stoneCounter");
var wandspan = document.getElementById("wandCounter");
// create a connecttion
var connectionDeathlyHallows = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/deathyhallows").build();

// connect to method that hub
connectionDeathlyHallows.on("updateDealthyHallowsCount", (cloak, stone, wand) => {
    cloakspan.innerText = cloak.toString();
    stonespan.innerText = stone.toString();
    wandspan.innerText = wand.toString();
});


//Ok
function fulfilled() {
    connectionDeathlyHallows.invoke("GetRaceStatus").then((racounter) => {
        cloakspan.innerText = racounter.cloak.toString();
        stonespan.innerText = racounter.stone.toString();
        wandspan.innerText = racounter.wand.toString();
    });
    console.log("Connection to USer hub successfully!!!");
    
}
// Fail
function rejected() {

}
// start connection 
connectionDeathlyHallows.start().then(fulfilled, rejected);
