namespace CellGame.Germs
{
    public class GermFactory : IGermFactory
    {
        public IGerm CreateDefaultGerm()
        {
            return new NullGerm();
        }
    }
}