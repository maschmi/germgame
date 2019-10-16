using System;
using CellGame.Tissue;

namespace CellGame.Germs
{
    public interface IGerm : ICellVisitor
    {
        event EventHandler<GermGrowthEventArgs> GermGrowth;
        ICell InfectCell(ICell cellToInfect);
        void Accept(IGermVisitor visitor);
        IGerm Replicate();
    }

    public class GermGrowthEventArgs
    {
        public bool Lytic { get; }
        public bool Budding { get; }
        public IGerm Germ { get; }
    }
}
