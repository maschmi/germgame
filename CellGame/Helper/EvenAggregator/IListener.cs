using System.Threading.Tasks;

namespace CellGame.Helper
{
    internal interface IListener<in TMessage> where TMessage : IMessage
    {
        Task ProcessMessageAsync(TMessage message);
    }
}
