using CellGame.Helper;

namespace CellGame.RunGame.Messages
{
    internal class CellInfectionMessage : IMessage
    {
        public object Sender { get; }
        public CellInfectionMessage(object obj)
        {
            Sender = obj;
        }

        
    }
}
