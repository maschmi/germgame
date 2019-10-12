using AutoFixture;

namespace CellGameTest.TestAttributes
{
    public class RatioCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<float>(c => c.FromFactory<ushort>(i =>
            {
                if(i > 0)
                    return 1f / i;

                return 0f;
            }));
        }
    }
}