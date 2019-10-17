using System;
using System.Collections.Generic;
using System.Linq;
using CellGame.Germs;
using CellGame.Helper;
using CellGame.Helper.Shuffle;
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
            var eventAggregator = new EventAggregator();
            var cellFactory = new DefaultCellFactory();
            
            var germFactory = new InfectiousGermFactory(eventAggregator);
            var tissueMap = new Tissue2DFactory(cellFactory, germFactory, new FisherYatesShuffle())
                .Create(maxX, maxY, 0.7f, 0.05f);
            var cellStringEncoder = new CellStringEncoder();
            var roundBasedGame = new RoundBasedGame(new TissueGrowthMechanism(), new NullInfectionPropagation(), eventAggregator);
            
            var  rounds = new List<TissuePrinter>();
            for(int i = 0; i < 5; i++) 
            {
                Console.WriteLine($"We are in round {(i + 1).ToString()}:");
                var printer = new TissuePrinter(tissueMap, cellStringEncoder);
                printer.PrintTissue();
                tissueMap = roundBasedGame.Advance(tissueMap);
            }
        }
    }
}
