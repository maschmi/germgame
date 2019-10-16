using System;
using System.Runtime.InteropServices;
using CellGame.Tissue;

namespace CellGame.Germs
{
    public class LyticVirus : IGerm
    {
        private const int GenerationsToMature = 10;
        private readonly int _generation;
        private readonly bool  _isMature;
        private readonly int _replicationMultiplier = 10;
        
        private bool _canInfectCell = false;
        private (ushort selfSignal, ushort alertSignal) _originCellSignals;

        public LyticVirus()
        {
            _generation = 0;
            _isMature = false;
        }

        internal LyticVirus(int currentGeneration)
        {
            _generation = currentGeneration;
            _isMature = _generation >= GenerationsToMature;
        }
        
        public void VisitCell(bool isAlive, ushort selfSignal, ushort alertSignal, bool isInfected)
        {
            _canInfectCell = isAlive && !isInfected;
            _originCellSignals = (selfSignal, alertSignal);
        }
        
        public ICell InfectCell(ICell cellToInfect)
        {
            cellToInfect.Accept(this);
            if (cellToInfect is HealthyCell cell && _canInfectCell)
                    return new InfectedCell(true,
                        (ushort)Math.Floor(_originCellSignals.selfSignal * 0.7),
                        (ushort)(_originCellSignals.alertSignal + (ushort) Math.Floor((ushort.MaxValue - _originCellSignals.alertSignal) * 0.2)),
                        NewInfection());
            
            return cellToInfect;
        }

        public void Accept(IGermVisitor visitor)
        {
            visitor.Visit(_isMature, _replicationMultiplier);
        }

        public IGerm Clone()
        {
            return new LyticVirus(_generation+1);
        }

        private IGerm NewInfection()
        {
            return new LyticVirus(0);
        }
    }
}