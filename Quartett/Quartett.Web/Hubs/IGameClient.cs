using Quartett.Web.Models;

namespace Quartett.Web.Hubs
{
    public interface IGameClient
    {
        #region Start
        void ReceivePlayer1(string name);
        void ReceivePlayer2(string name);
        #endregion

        #region Play
        void ReceiveNextCard(int yourNumberOfCardsRemaining, int theirNumberOfCardsRemaining, Card card);
        void MakeChoice();
        void AwaitChoice();
        void Reveal(bool didYouWin, Card opponentsCard, string winningCharacteristic);
        #endregion

        #region End
        void Win();
        void Lose(); 
        #endregion
    }
}