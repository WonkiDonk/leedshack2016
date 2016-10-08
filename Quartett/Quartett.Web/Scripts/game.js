(function ($) {
    var game = $.connection.gameHub,
        $start = $('#start'),
        $waiting = $('#waiting'),
        $game = $('#game'),
        $me = $('#me'),
        $them = $('#them');

    var Player = function () {
        var self = this;

        self.name = '';
        self.card = {};
        self.numberOfCards = 0;
    }

    Player.prototype = {
        setName: function (name) {
            this.name = name;
        },

        receiveNextCard: function (nextCard) {
            this.card = nextCard;
        }
    }

    var player1 = new Player();
    var player2 = new Player();

    var me, them;

    var updateNames = function () {
        $me.find('.playerName').text(me.name);
        $them.find('.playerName').text(them.name);
    };

    var showGame = function () {
        $start.toggleClass('hidden', true);
        $waiting.toggleClass('hidden', true);
        $game.toggleClass('hidden', false);
    };

    var showWait = function () {
        $start.toggleClass('hidden', true);
        $waiting.toggleClass('hidden', false);
        $game.toggleClass('hidden', true);
    };

    var showStart = function () {
        $start.toggleClass('hidden', false);
        $waiting.toggleClass('hidden', true);
        $game.toggleClass('hidden', true);
    };

    var configureClientHub = function () {
        game.client.receivePlayer1 = function (name) {
            player1.setName(name);
            updateNames();
        };

        game.client.receivePlayer2 = function (name) {
            player2.setName(name);
            updateNames();
        };

        game.client.receiveNextCard = function () {
            showGame();
        };

        game.client.makeChoice = function () {

        };

        game.client.awaitChoice = function () {

        };

        game.client.win = function () {

        };

        game.client.lose = function () {

        };

        $.connection.hub.start()
            .done(function () {
            });

        //game.server.registerPlayer1(name);
    };

    var configureStart = function () {
        $('#player1')
            .on('click',
                function (ev) {
                    ev.preventDefault();
                    me = player1;
                    them = player2;
                    game.server.registerPlayer1($('#playerName').val());
                    showWait();
                });

        $('#player2')
            .on('click',
                function (ev) {
                    ev.preventDefault();
                    me = player2;
                    them = player1;
                    game.server.registerPlayer2($('#playerName').val());
                    showWait();
                });
    };

    var init = function () {
        configureClientHub();
        configureStart();
        showStart();
    };

    init();
})(jQuery);