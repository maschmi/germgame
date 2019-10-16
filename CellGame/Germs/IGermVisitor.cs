namespace CellGame.Germs
{
    public interface IGermVisitor
    {
        void Visit(bool isMature, int replicationMultiplier);
    }
}