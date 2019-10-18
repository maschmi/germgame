using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using CellGame.Germs;
using CellGame.Germs.Messages;
using CellGame.Helper;
using CellGame.Tissue;

namespace CellGame.RunGame
{
    internal class NullInfectionPropagation : IPropagateInfection
    {
        public NullInfectionPropagation(IGermFactory germFactory, EventAggregator eventAggregator)
        {
            //null implementation
        }
        public ICell PropagateInfection(ICell cell)
        {
            return cell.Clone();
        }

        public async Task ProcessMessageAsync(GermGrowthMessage message)
        {
            await Task.CompletedTask;
        }
    }
}
