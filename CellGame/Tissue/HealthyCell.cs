using System;
using System.Diagnostics.CodeAnalysis;
using CellGame.Germs;

namespace CellGame.Tissue
{
    public sealed class HealthyCell : ICell

    {
        private readonly bool _isAlive;
        private readonly bool _isInfected = false;
        private readonly ushort _selfSignal;
        private readonly ushort _alertSignal;
        private readonly IGerm _germ;

        public HealthyCell()
        {
            _isAlive = true;
            _selfSignal = ushort.MaxValue;
            _alertSignal = ushort.MinValue;
            _germ = new NullGerm();
        }

        public HealthyCell(bool isAlive, ushort selfSignal, ushort alertSignal)
        {
            _isAlive = isAlive;
            _selfSignal = selfSignal;
            _alertSignal = alertSignal;
            _germ = new NullGerm();
        }

        public ICell Clone()
        {
            return new HealthyCell(_isAlive, _selfSignal, _alertSignal);
        }

        public void Accept(ICellVisitor cellVisitor)
        {
            cellVisitor.VisitCell(_isAlive, _selfSignal, _alertSignal);
        }
    }
}
