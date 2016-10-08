(function() {
    var game = $.connection.gameHub;
    
    // See IGameHub for interface to server.
    // See IPlayer for interface to be implemented by clients.

    // Client methods
    game.client.start = function (otherPlayer) {
        
    }

    $.connection.hub.start().done(function() {

    });
})();