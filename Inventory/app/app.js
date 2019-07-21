var hub = $.connection.ChatHub;
hub.client.message = function (msg) {
    $("#user").append("<li>" + msg + "</li>");
};
$.connection.hub.start(function () {
    $("#send").click(function () {
        alert("Hello");
        hub.server.send($("#txt").val());
        $("#txt").val();
    })
});