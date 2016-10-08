(function() {
    var game = $.connection.gameHub;

    var Player = function() {
        var self = this;

        self.name = "";
        self.card = {};
        self.numberOfCards = 0;
    }

    Player.prototype = {
        setName: function(name) {
            this.name = name;
        },

        receiveNextCard: function(nextCard) {
            this.card = nextCard;
        }
    }

    var me = new Player();
    var them = new Player();
    var players = [me, them];

    var player1 = me;
    var player2 = them;
    
    // See IGameHub for interface to server.
    // See IPlayer for interface to be implemented by clients.

    // Client methods
    game.client.receivePlayer1 = player1.setName;
    game.client.receivePlayer2 = player2.setName;
    game.client.receiveNextCard = me.receiveNextCard;
    game.client.makeChoice = function() {

    };
    game.client.awaitChoice = function() {

    };
    game.client.win = function() {

    };
    game.client.lose = function() {

    };


    $.connection.hub.start().done(function() {
    });

    $(function () {
        ko.applyBindings(players);
    });
})();