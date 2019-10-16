using System;
using CellGame.Tissue;

namespace CellGame.RunGame
{
    internal interface IPropagateInfection
    {
        event EventHandler<NewGermsEventArgs> GermReproduction;
        ICell PropagateInfection(InfectedCell infectedCell);
    }
}
