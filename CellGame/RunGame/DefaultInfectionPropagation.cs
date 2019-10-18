using System;
using CellGame.Tissue;

namespace CellGame.RunGame
{
    internal class DefaultInfectionPropagation : IPropagateInfection
    {

        public ICell PropagateInfection(ICell cell)
        {
            return cell.Clone();
        }
    }
}
