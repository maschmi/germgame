using System;
using System.Runtime.InteropServices;
using CellGame.Germs;

namespace CellGame.Tissue
{
    internal class DefaultCellFactory : ICellFactory
    {
        public Cell CreateHealthyCell() =>
            new Cell();

        public Cell CreateInfectedCell(IGerm germ) =>
            new Cell(true,
                true,
                UInt16.MinValue,
                UInt16.MaxValue,
                germ);
    }
}