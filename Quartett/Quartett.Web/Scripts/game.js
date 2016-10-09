﻿(function ($) {
    var game = $.connection.gameHub,
        $start = $('#start'),
        $waiting = $('#waiting'),
        $game = $('#game'),
        $me = $('#me'),
        $them = $('#them'),
        $send = $('#send');

    var Player = function () {
        var self = this;

        self.name = '';
        self.card = {};
        self.numberOfCards = 0;
        self.choice = '';
    }

    Player.prototype = {
        setName: function (name) {
            this.name = name;
        },

        receiveNextCard: function (nextCard) {
            this.card = nextCard;
        },

        setChoice: function (name) {
            this.choice = name;
        }
    }

    var player1 = new Player();
    var player2 = new Player();

    var me, them;

    var renderMap = function ($container, location) {
        new google.maps.Map($container.get(0),
        {
            center: { lat: location.latitude, lng: location.longitude },
            zoom: 8
        });
    };

    var renderCharacteristics = function ($container, characteristics) {
        $container.find('li').remove();
        var $ul = $container.find('ul');
        $.each(characteristics,
            function (index, character) {
                var $li = $('<li></li>');
                $ul.append($li);

                var $name = $('<span class="name"></span>');
                $name.text(character.Name);
                $li.append($name);

                var $value = $('<span class="value"></span>');
                $value.text(character.Value);
                $li.append($value);
            });
    };

    var renderCard = function ($container, card) {
        var $card = $container.find('.card');
        $card.find('.name').text(card.Name);

        //renderMap($card.find('.map'), card.Location);
        renderCharacteristics($card.find('.characters'), card.Characteristics);
        $send.addClass('hidden');
    };

    var updateMyCard = function () {
        renderCard($me, me.card);
    };

    var updateNumberOfCards = function () {
        $me.find('.numberOfCards').text(me.numberOfCards);
        $them.find('.numberOfCards').text(them.numberOfCards);
    };

    var updateGame = function () {
        updateNumberOfCards();
        updateMyCard();
    };

    var updateNames = function () {
        $me.find('.playerName').text(me.name);
        $them.find('.playerName').text(them.name);
    };

    var hideAll = function () {
        $start.toggleClass('hidden', true);
        $waiting.toggleClass('hidden', true);
        $game.toggleClass('hidden', true);
    };

    var showWinnerOfRound = function(winnerName) {
        renderCard($them, them.card);

        // Todo: show winner of the round
        alert("winner of the round");
    };

    var showGame = function () {
        hideAll();
        $game.toggleClass('hidden', false);
    };

    var showWait = function () {
        hideAll();
        $waiting.toggleClass('hidden', false);
    };

    var showStart = function () {
        hideAll();
        $start.toggleClass('hidden', false);
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

        game.client.receiveNextCard = function (yourNumberOfCardsRemaining, theirNumberOfCardsRemaining, card) {
            // Wait 2 seconds before showing next card
            setTimeout(function() {
                    me.numberOfCards = yourNumberOfCardsRemaining;
                    me.card = card;
                    them.numberOfCards = theirNumberOfCardsRemaining;

                    updateGame();
                    showGame();
                },
                2000);
        };

        game.client.makeChoice = function () {
            $me.addClass('makeChoice');
            $them.removeClass('makeChoice');

            $me.find('.card .characters ul')
            .on('click',
                'li',
                function () {
                    var $li = $(this);
                    $li.parent().find('li').removeClass('active');
                    $li.addClass('active');
                    me.setChoice($li.find('.name').text());
                    $send.removeClass('hidden');
                });

            $send
                .removeClass('hidden')
                .on('click',
                    function() {
                        $send.off('click');
                        game.server.applyChoice(me.choice);
                    });
        };

        game.client.awaitChoice = function () {
            $me.removeClass('makeChoice');
            $them.addClass('makeChoice');

            $send.addClass('hidden')
        };

        game.client.reveal = function (winnerName, opponentsCard) {
            $them.card = opponentsCard;
            showWinnerOfRound(winnerName);
        };

        game.client.win = function () {
            // Todo;
            alert("you are the winner");
        };

        game.client.lose = function () {
            // Todo;
            alert("you loose");
        };

        $.connection.hub.start()
            .done(function () {
                // Todo;
            });
    };

    var configureStart = function () {
        $('#player1')
            .on('click',
                function () {
                    if ($('#playerName').val() !== '') {
                        me = player1;
                        them = player2;
                        game.server.registerPlayer1($('#playerName').val());
                        showWait();
                    }
                });

        $('#player2')
            .on('click',
                function () {
                    if ($('#playerName').val() !== '') {
                        me = player2;
                        them = player1;
                        game.server.registerPlayer2($('#playerName').val());
                        showWait();
                    }
                });
    };

    var init = function () {
        configureClientHub();
        configureStart();
        showStart();
    };

    $(document)
        .ready(function() {
            init();
        });
})(jQuery);