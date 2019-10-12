using System;
using System.Reflection;
using AutoFixture;
using AutoFixture.Xunit2;

namespace CellGameTest.Tissue
{
    public class RatioAttribute : CustomizeAttribute
    {
        public override ICustomization GetCustomization(ParameterInfo parameter)
        {
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));
            
            return new RatioCustomization();
        }
    }
}