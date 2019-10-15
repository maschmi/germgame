using System;
using System.Reflection;
using AutoFixture;
using AutoFixture.Xunit2;

namespace CellGameTest.TestAttributes
{
    public class MockCellTissueCreationAttribute : CustomizeAttribute
    {
        private readonly int _maxValue;

        public MockCellTissueCreationAttribute(int maxValue = 200)
        {
            _maxValue = maxValue;
        }
        public override ICustomization GetCustomization(ParameterInfo parameter)
        { 
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));
            
            return new Tissue2DMockCellCustomization(_maxValue);
        }
    }
}