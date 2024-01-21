console.log("Chat Scripts initiated.");

var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

connection.start().catch(function (error) {
    return console.error(error.toString())
});

//window.onload = function () {
//    var messages = @Html.Raw(Json.Serialize(Model.Messages));
//    if (messages && messages.length > 0) {
//        messages.forEach(function (message) {
//            console.log(message); // Add this line
//            var div = document.createElement('div');
//            div.className = 'mb-3';
//            div.innerHTML = `<strong>${message.senderName}</strong>: ${message.text}`;
//            document.getElementById("chatContainer").appendChild(div);
//        });
//        // Scroll to the bottom of the chat
//        var container = document.getElementById("chatContainer");
//        container.scrollTop = container.scrollHeight;
//    }
//};



document.getElementById('messageForm').addEventListener("submit", function (event) {
    event.preventDefault();
    debugger;
    var user = document.getElementById("username").textContent;
    var userId = document.getElementById("userId").value;
    var message = document.getElementById("message").value;
    var div = document.createElement('div');
    document.getElementById("warning").innerHTML = "";

    if (user && !message) {
        div.className = 'mb-3 mt-3 alert alert-warning text-center';
        div.innerHTML = `You must enter a message!`;
        document.getElementById("warning").appendChild(div);
        return;
    } else if (!user) {
        div.className = 'mb-3 mt-3 alert alert-warning text-center';
        div.innerHTML = `You must enter a name!`;
        document.getElementById("warning").appendChild(div);
        return;
    }

    // Session Id
    var sessionId = getQueryVariable("sessionId");

    // Clear the message input field
    document.getElementById("message").value = "";

    connection.invoke("SendMessage", sessionId, user, userId, message).catch((err) => {
        console.error(err.toString())
    });
});

connection.on("RecieveMessage", function (user, userId, message) {
    var div = document.createElement('div');
    div.className = 'mb-3';
    div.innerHTML = `<strong>${user}</strong>: ${message}`;
    document.getElementById("chatContainer").appendChild(div);
    // Scroll to the bottom of the chat
    var container = document.getElementById("chatContainer");
    container.scrollTop = container.scrollHeight;
});

function getQueryVariable(variable) {
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        if (pair[0] == variable) {
            return pair[1];
        }
    }
    alert('Query Variable ' + variable + ' not found');
}