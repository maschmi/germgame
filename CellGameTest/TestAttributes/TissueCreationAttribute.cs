using System;
using System.Reflection;
using AutoFixture;
using AutoFixture.Xunit2;

namespace CellGameTest.TestAttributes
{
    public class TissueCreationAttribute : CustomizeAttribute
    {
        private readonly int _maxValue;

        public TissueCreationAttribute(int maxValue = 200)
        {
            _maxValue = maxValue;
        }
        public override ICustomization GetCustomization(ParameterInfo parameter)
        { 
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));
            
            return new Tissue2DCustomization(_maxValue);
        }
    }
}