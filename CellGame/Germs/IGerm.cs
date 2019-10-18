using System;
using CellGame.Tissue;

namespace CellGame.Germs
{
    public interface IGerm : ICellVisitor
    {
        ICell InfectCell(ICell cellToInfect);
        void Accept(IGermVistor visitor);
        IGerm Replicate();
    }

    public interface IGermVistor
    {
        void Visit(bool isMature, bool isLytic, bool isBudding);
    }
}
