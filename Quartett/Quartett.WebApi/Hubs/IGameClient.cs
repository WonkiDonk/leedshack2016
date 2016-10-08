using Quartett.WebApi.Contexts.Entities;

namespace Quartett.WebApi.Hubs
{
    public interface IGameClient
    {
        #region Start
        void ReceivePlayer1(string name);
        void ReceivePlayer2(string name);
        #endregion

        #region Play
        void ReceiveNextCard(int numberOfCardRemaining, Card card);
        void MakeChoice();
        void AwaitChoice();
        #endregion

        #region End
        void Win();
        void Lose(); 
        #endregion
    }
}