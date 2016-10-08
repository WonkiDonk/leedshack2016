(function($) {
    var game = $.connection.gameHub,
        $start = $('#start'),
        $waiting = $('#waiting'),
        $game = $('#game');

    //var Player = function() {
    //    var self = this;

    //    self.name = '';
    //    self.card = {};
    //    self.numberOfCards = 0;
    //}

    //Player.prototype = {
    //    setName: function(name) {
    //        this.name = name;
    //    },

    //    receiveNextCard: function(nextCard) {
    //        this.card = nextCard;
    //    }
    //}

    //var me = new Player();
    //var them = new Player();

    //var player1, player2;

    var configureClientHub = function() {
        game.client.receivePlayer1 = function() {
            
        };

        game.client.receivePlayer2 = function() {
            
        };

        game.client.receiveNextCard = function() {

        };

        game.client.makeChoice = function () {

        };

        game.client.awaitChoice = function () {

        };

        game.client.win = function () {

        };

        game.client.lose = function () {

        };

        $.connection.hub.start().done(function () {
        });

        //game.server.registerPlayer1(name);
    }
    
    var wait = function() {
        $start.toggleClass('hidden', true);
        $waiting.toggleClass('hidden', false);
    };

    var configureStart = function() {
        $('#player1')
            .on('click',
                function (ev) {
                    ev.preventDefault();
                    game.server.registerPlayer1($('#playerName').val());
                    wait();
                });

        $('#player2')
            .on('click',
                function (ev) {
                    ev.preventDefault();
                    game.server.registerPlayer2($('#playerName').val());
                    wait();
                });
    };

    var init = function () {
        configureClientHub();
        configureStart();
    };
        
    init();
})(jQuery);