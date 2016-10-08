(function() {
    var game = $.connection.gameHub;

    // Client methods called by the server (see IPlayer for interface)
    game.client.start = function (otherPlayer) {
        
    }

    $.connection.hub.start().done(function() {

    });
})();