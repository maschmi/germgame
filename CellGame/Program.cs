using System.Linq;
using CellGame.Germs;
using CellGame.ListShuffle;
using CellGame.RunGame;
using CellGame.Tissue;

namespace CellGame
{
    public static class Program
    {
        static void Main(string[] args)
        {
            int maxX = 10;
            int maxY = 10;
            var cellFactory = new DefaultCellFactory();
            var germFactory = new DefaultGermFactory();
            var tissueMap = new Tissue2DFactory(cellFactory, germFactory, new FisherYatesShuffle())
                .Create(maxX, maxY, 0.7f, 0.05f);
            var cellStringEncoder = new CellStringEncoder();
            var roundBasedGame = new RoundBasedGame(new TissueGrowthMechanism(), new NullInfectionPropagation());
            
            var printerRoundOne = new TissuePrinter(tissueMap, cellStringEncoder);

            tissueMap = roundBasedGame.Advance(tissueMap);
            var printerRoundTwo = new TissuePrinter(tissueMap, cellStringEncoder);
            
            printerRoundOne.PrintTissue();
            printerRoundTwo.PrintTissue();
        }
    }
}
