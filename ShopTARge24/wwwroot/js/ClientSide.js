const connectionChat = new signalR.HubConnectionBuilder()

    .withUrl("/hubs/chat", signalR.HttpTransportType.WebSockets)
    .build();

const sendButton = document.getElementById("sendButton");
const userInput = document.getElementById("userInput");
const messageInput = document.getElementById("messageInput");
const messagesList = document.getElementById("messagesList");

// Saame sõnumi serverist
connectionChat.on("ReceiveMessage", (message) => {
    const li = document.createElement("li");
    li.textContent = message;
    li.className = "list-group-item";
    messagesList.appendChild(li);
});


connectionChat.on("ReceiveMessages", (messages) => {
    messagesList.innerHTML = ""; // puhastab listi
    messages.forEach(msg => {
        const li = document.createElement("li");
        li.textContent = msg;
        li.className = "list-group-item";
        messagesList.appendChild(li);
    });
});

sendButton.disabled = true;

connectionChat.start()
    .then(() => {
        console.log("Connected to ChatHub!");
        sendButton.disabled = false;
    })
    .catch(err => console.error(err.toString()));

// Saatmine
sendButton.addEventListener("click", () => {
    const user = userInput.value.trim();
    const message = messageInput.value.trim();

    if(user && message) {
        connectionChat.invoke("SendMessage", user, message)
            .catch(err => console.error(err.toString()));
        messageInput.value = '';
    }
});
