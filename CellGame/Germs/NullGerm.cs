using System;
using System.ComponentModel.DataAnnotations;
using CellGame.Tissue;

namespace CellGame.Germs
{
    internal sealed class NullGerm : IGerm
    {
       
        public ICell InfectCell(ICell cellToInfect)
        {
            return new HealthyCell();
        }

        public void Accept(IGermVistor visitor)
        {
            //do nothing, null implementation
        }

        public IGerm Replicate()
        {
            return new NullGerm();
        }
    }
}
