using System;
using System.Diagnostics.CodeAnalysis;
using CellGame.Germs;

namespace CellGame.Tissue
{
    public sealed class Cell : ICell 
    
    {
        private readonly bool _isAlive;
        private readonly bool _isInfected;
        private readonly ushort _selfSignal;
        private readonly ushort _alertSignal;
        private readonly IGerm _germ;
        
        public Cell()
        {
            _isAlive = true;
            _isInfected = false;
            _selfSignal = UInt16.MaxValue;
            _alertSignal = UInt16.MinValue;
            _germ = new NullGerm();
        }

        public Cell(bool isAlive, bool isInfected, ushort selfSignal, ushort alertSignal, IGerm germ)
        {
            _isAlive = isAlive;
            _isInfected = isInfected;
            _selfSignal = selfSignal;
            _alertSignal = alertSignal;
            _germ = germ ?? new NullGerm();
        }

        public ICell Clone()
        {
            return new Cell(_isAlive, _isInfected, _selfSignal, _alertSignal, _germ);
        }

        public void Accept(ICellVisitor cellVisitor)
        {
            cellVisitor.Visit(_isAlive, _selfSignal, _alertSignal, _isInfected);
        }
    }
}
