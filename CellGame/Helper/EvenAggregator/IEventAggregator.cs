using System.Threading.Tasks;

namespace CellGame.Helper
{
    internal interface IEventAggregator
    {
        Task Publish<TEvent>(TEvent message) where TEvent : IMessage;
        void Subscribe<TEvent>(IListener<TEvent> listener) where TEvent : IMessage;
    }
}
