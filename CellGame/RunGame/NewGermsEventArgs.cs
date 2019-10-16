using System.Collections.Generic;
using CellGame.Germs;
using CellGame.Tissue;

namespace CellGame.RunGame
{
    internal class NewGermsEventArgs
    {
        public Location Origin { get; }
        public List<IGerm> Germs { get; }

        public NewGermsEventArgs(Location origin, List<IGerm> germs)
        {
            Origin = origin;
            Germs = germs ?? new List<IGerm>();
        }
    }
}
