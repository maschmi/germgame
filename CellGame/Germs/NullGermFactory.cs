namespace CellGame.Germs
{
    internal class NullGermFactory : IGermFactory
    {
        public IGerm CreateDefaultGerm()
        {
            return new NullGerm();
        }
    }
}
