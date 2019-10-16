using System;
using CellGame.Tissue;

namespace CellGame.RunGame
{
    internal class DefaultInfectionPropagation : IPropagateInfection
    {
        public event EventHandler<NewGermsEventArgs> GermReproduction;
        public ICell PropagateInfection(InfectedCell infectedCell)
        {
            return infectedCell.Clone();
        }
    }
}
