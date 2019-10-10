namespace CellGame.Germs
{
    internal class DefaultGermFactory : IGermFactory
    {
        public IGerm CreateDefaultGerm()
        {
            return new NullGerm();
        }
    }
}
