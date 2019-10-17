using System;
using System.Reflection;
using AutoFixture;
using AutoFixture.Xunit2;

namespace CellGameTest.TestAttributes
{
    public class MinMaxIntAttribute : CustomizeAttribute
    {
        private readonly int _maxInt;
        private readonly int _minInt;

        public MinMaxIntAttribute(int minInt = int.MinValue, int maxInt = int.MaxValue)
        {
            _maxInt = maxInt;
            _minInt = minInt;
        }

        public override ICustomization GetCustomization(ParameterInfo parameter)
        {
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));

            return new MinMaxIntCustomization(_minInt, _maxInt);
        }
    }
}
