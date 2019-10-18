using System;
using CellGame.Tissue;

namespace CellGame.RunGame
{
    internal interface IPropagateInfection
    {
        ICell PropagateInfection(ICell cell);
    }
}
