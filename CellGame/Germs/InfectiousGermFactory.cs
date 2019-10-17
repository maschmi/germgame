using CellGame.Helper;

namespace CellGame.Germs
{
    internal class InfectiousGermFactory : IGermFactory
    {
        private readonly IEventAggregator _eventaggregator;

        public InfectiousGermFactory(IEventAggregator eventAggregator)
        {
            _eventaggregator = eventAggregator;
        }
        public IGerm CreateDefaultGerm()
        {
            return new LyticVirus(_eventaggregator);
        }
    }
}
