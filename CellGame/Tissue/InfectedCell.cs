using System;
using CellGame.Germs;

namespace CellGame.Tissue
{
    public sealed class InfectedCell : ICell, IGermVistor
    {
        private readonly bool _isAlive;
        private readonly ushort _selfSignal;
        private readonly ushort _alertSignal;
        private readonly IGerm _germ;
        private bool _germReplicationKillsCell;

        public InfectedCell(bool isAlive, ushort selfSignal, ushort alertSignal, IGerm germ)
        {
            _isAlive = isAlive;
            _selfSignal = selfSignal;
            _alertSignal = alertSignal;
            _germ = germ ?? throw new ArgumentNullException(nameof(germ));
        }

        public ICell Clone()
        {
            if (!_isAlive) 
                return new NullCell();
            
            var nextGenGerm = _germ.Replicate();
            nextGenGerm.Accept(this);
            return new InfectedCell(!_germReplicationKillsCell, _selfSignal, _alertSignal, nextGenGerm);
        }

        public void Accept(ICellVisitor cellVisitor)
        {
            cellVisitor?.VisitCell(_isAlive, _selfSignal, _alertSignal);
        }

        public void Visit(bool isMature, bool isLytic, bool isBudding)
        {
            _germReplicationKillsCell = isMature && isLytic;
        }
    }
}
