using System;
using CellGame.Tissue;

namespace CellGame.Germs
{
    public interface IGerm : ICellVisitor
    {
        ICell InfectCell(ICell cellToInfect);
        IGerm Replicate();
    }
}
