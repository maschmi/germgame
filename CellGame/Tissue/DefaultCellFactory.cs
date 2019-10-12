using System;
using System.Runtime.InteropServices;
using CellGame.Germs;

namespace CellGame.Tissue
{
    internal class DefaultCellFactory : ICellFactory
    {
        public ICell CreateHealthyCell() =>
            new HealthyCell();

        public ICell CreateInfectedCell(IGerm germ) =>
            new InfectedCell(true,
                UInt16.MaxValue / 2,
                UInt16.MaxValue / 2,
                germ);

        public ICell CreateNullCell()
        {
            return new NullCell();
        }
    }
}