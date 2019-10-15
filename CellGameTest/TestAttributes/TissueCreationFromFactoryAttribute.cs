using System.Reflection;
using AutoFixture;
using AutoFixture.Xunit2;
using CellGameTest.RunGame;

namespace CellGameTest.TestAttributes
{
    public class TissueCreationFromFactoryAttribute : CustomizeAttribute
    {
        private readonly int _max;

        public TissueCreationFromFactoryAttribute(int max)
        {
            _max = max;
        }

        public override ICustomization GetCustomization(ParameterInfo parameter)
        {
            return new TissueFromFactoryCustomization(_max);
        }
    }
}