using System;
using System.Runtime.InteropServices;
using CellGame.Germs;

namespace CellGame.Tissue
{
    internal class DefaultCellFactory : ICellFactory
    {
        public ICell CreateHealthyCell() =>
            new Cell();

        public ICell CreateInfectedCell(IGerm germ) =>
            new Cell(true,
                true,
                UInt16.MinValue,
                UInt16.MaxValue,
                germ);

        public ICell CreateNullCell()
        {
            return new NullCell();
        }
    }
}