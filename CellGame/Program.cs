using System.Linq;
using CellGame.Germs;
using CellGame.ListShuffle;
using CellGame.Tissue;

namespace CellGame
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxX = 10;
            int maxY = 10;
            var cellFactory = new DefaultCellFactory();
            var germFactory = new GermFactory();
            var tissueMap = new Tissue2D(maxX, maxY, cellFactory, germFactory);
            tissueMap.FillTissue(0.8f, 0.1f, new FisherYatesShuffle());
            var printer = new TissuePrinter(tissueMap.Tissue);

            printer.PrintTissue(maxX, maxY);

        }
    }
}
