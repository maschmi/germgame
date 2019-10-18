using System;
using CellGame.Helper;

namespace CellGame.Germs.Messages
{
    public class GermGrowthMessage : IMessage
    {
        public object Sender => Germ;
        public bool Lytic { get; }
        public bool Budding { get; }
        public IGerm Germ { get; }
        public int ReplicationMultiplier { get; }

        public GermGrowthMessage(bool lytic, bool budding, int replicationMultiplier, IGerm germ)
        {
            Lytic = lytic;
            Budding = budding;
            ReplicationMultiplier = replicationMultiplier;
            Germ = germ ?? throw new ArgumentNullException(nameof(germ));
        }
    }
}
