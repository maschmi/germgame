using System.Threading.Tasks;

namespace CellGame.Helper
{
    internal interface IListenOn<in TMessage> where TMessage : IMessage
    {
        Task ProcessMessageAsync(TMessage message);
    }
}
