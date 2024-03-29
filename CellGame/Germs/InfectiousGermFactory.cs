﻿using CellGame.Helper;
using CellGame.Helper.EvenAggregator;

namespace CellGame.Germs
{
    internal class InfectiousGermFactory : IGermFactory
    {
        private readonly EventAggregator _eventaggregator;

        public InfectiousGermFactory(EventAggregator eventAggregator)
        {
            _eventaggregator = eventAggregator;
        }

        public IGerm CreateDefaultGerm()
        {
            return new LyticVirus(_eventaggregator);
        }
    }
}
