﻿using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Quartett.WebApi.Services;

namespace Quartett.WebApi.Hubs
{
    public sealed class GameHub : Hub<IPlayer>, IGameHub
    {
        private readonly GameService _gameService = new GameService();
        public async Task RegisterPlayer1(string name)
        {
            Clients.All.ReceivePlayer1(name);
            //await _gameService.RegisterPlayer1(Context.ConnectionId, name).ConfigureAwait(false);
            await StartGameIfReady().ConfigureAwait(false);
        }

        public async Task RegisterPlayer2(string name)
        {
            Clients.All.ReceivePlayer2(name);
            await StartGameIfReady().ConfigureAwait(false);
        }

        public Task ReceiveChoice(string characteristicName)
        {
            throw new System.NotImplementedException();
        }

        private async Task StartGameIfReady()
        {
            if (await _gameService.GetIsGameReady().ConfigureAwait(false))
            {
                var game = await _gameService.GetGame().ConfigureAwait(false);
            }
        }
    }
}