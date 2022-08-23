const connection = new signalR.HubConnectionBuilder()
    .withUrl("/Models/ChatHub")
    .build();

connection.start().catch(err => console.error(err.toString()));

connection.on('Send', (nick, message) => {
    //appendLine(nick, message);
    console.log(nick + " : " + message);
});

$('#mess-send').on('click', function () {
    var noidung = document.getElementById('mess-new-text');
    connection.invoke("SendAll", "abc", noidung.value);
})