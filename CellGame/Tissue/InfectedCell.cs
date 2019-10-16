﻿using System;
using CellGame.Germs;

namespace CellGame.Tissue
{
    public sealed class InfectedCell : ICell
    {
        private readonly bool _isAlive;
        private readonly bool _isInfected = true;
        private readonly ushort _selfSignal;
        private readonly ushort _alertSignal;
        private readonly IGerm _germ;


        public InfectedCell(bool isAlive, ushort selfSignal, ushort alertSignal, IGerm germ)
        {
            _isAlive = isAlive;
            _selfSignal = selfSignal;
            _alertSignal = alertSignal;
            _germ = germ ?? throw new ArgumentNullException(nameof(germ));
        }

        public ICell Clone()
        {
            IGerm nextGenGerm = new NullGerm();
            if(_isAlive)
                 nextGenGerm = _germ.Clone();
            
            return new InfectedCell(_isAlive, _selfSignal, _alertSignal, nextGenGerm);
        }

        public void Accept(ICellVisitor cellVisitor)
        {
            cellVisitor.VisitCell(_isAlive, _selfSignal, _alertSignal, _isInfected);
        }
    }
}
 
