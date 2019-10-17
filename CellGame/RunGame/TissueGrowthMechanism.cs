using System;
using CellGame.Tissue;

namespace CellGame.RunGame
{
    internal sealed class TissueGrowthMechanism : IGrowTissue
    {
        private Random _rnd = new Random();

        public ICell GrowTissue(Tissue2D input, Location location)
            => GetRandomNeighbour(input, location).Clone();

        private ICell GetRandomNeighbour(Tissue2D input, Location currentLocation)
        {
            var maxTries = 10;
            for (int i = 0; i < maxTries; i++)
            {
                int rndY = ConvertToOffset(_rnd.Next(0, 2));
                int rndX = ConvertToOffset(_rnd.Next(0, 2));
                var pickLocation = new Location(currentLocation.X - rndX, currentLocation.Y - rndY);
                if (input.Tissue.TryGetValue(pickLocation, out ICell neigbour))
                    return neigbour;
            }

            return new NullCell();
        }

        private int ConvertToOffset(int next)
        {
            if (next == 0)
                return -1;

            return next;
        }
    }
}
