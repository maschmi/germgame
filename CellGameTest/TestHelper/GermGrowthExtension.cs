using CellGame.Germs.Messages;

namespace CellGameTest.TestHelper
{
    public static class GermGrowthExtension
    {
        public static bool IsEquivalentTo(this GermGrowthMessage parameter, GermGrowthMessage expectedMessage)
        {
            return parameter.Budding == expectedMessage.Budding
                   && parameter.Germ == expectedMessage.Germ
                   && parameter.Sender == expectedMessage.Sender
                   && parameter.Lytic == expectedMessage.Lytic
                   && parameter.ReplicationMultiplier == expectedMessage.ReplicationMultiplier;
        }
    }
}