using System.Threading.Tasks;

namespace CellGame.Helper
{
    internal interface IEventAggregator
    {
        Task Publish<TEvent>(TEvent message) where TEvent : IMessage;
        void Subscribe<TEvent>(IListenOn<TEvent> listenOn) where TEvent : IMessage;
    }
}
