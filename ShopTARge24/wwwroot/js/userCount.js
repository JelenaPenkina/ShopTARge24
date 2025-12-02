// Create connection
var connectionUserCount = new signalR.HubConnectionBuilder()
    //.configureLogging(signalR.LogLevel.Information)
    .withUrl("/hubs/userCount", signalR.HttpTransportType.WebSocket).build();

//configureLogging(signalR.LogLevel.Information) Saab panna ka TRACE ja see abistab leida viga, kui süsteem ei näita
// ServerSentEvents asemel saab ka kasutada LongPolling == F12 - Network - Saab vaadata status, kas on 200 või mitte
// ServerSentEvents asemel saab ka kasutada WebSocket == F12 - Network - Saab vaadata Type alt, millega tegu on 


// Connect to methods that HUB invokes receive notifications FROM HUB.
connectionUserCount.on("updateTotalViews", (value) => {
    var newCountSpan = document.getElementById("totalViewsCounter");
    newCountSpan.innerText = value.toString();
});

connectionUserCount.on("updateTotalUsers", (value) => {
    var newCountSpan = document.getElementById("totalUsersCounter");
    newCountSpan.innerText = value.toString();
});

// Invoke HUB methods send notification to HUB
function newWindowLoadedOnClient() {
    connectionUserCount.invoke("NewWindowLoaded", "Gervin").then((value) => console.log(value));
    // connectionUserCount.send == F12 - console - value is undefined
}

// START connection
function fulfilled() {
    // Do something to start
    console.log("Connection to User Hub Successful!");
    newWindowLoadedOnClient();
}
function rejected() {
    // Rejected logs
}
connectionUserCount.start().then(fulfilled, rejected);