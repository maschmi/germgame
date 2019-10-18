using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CellGame.Germs;
using CellGame.Helper;
using CellGame.Helper.Shuffle;
using CellGame.RunGame;
using CellGame.Tissue;

namespace CellGame
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            int maxX = 50;
            int maxY = 10;
            var eventAggregator = new EventAggregator();
            var cellFactory = new DefaultCellFactory();

            var germFactory = new InfectiousGermFactory(eventAggregator);
            var tissueMap = new Tissue2DFactory(cellFactory, germFactory, new FisherYatesShuffle())
                .Create(maxX, maxY, 0.7f, 0.05f);
            var cellStringEncoder = new CellStringEncoder();
            var roundBasedGame = new RoundBasedGame(new TissueGrowthMechanism(), new RandomInfectionPropagation(germFactory, eventAggregator), 
                eventAggregator, false);

            var rounds = new List<TissuePrinter>();
            for (int i = 0; i < 50; i++)
            {
                var printer = new TissuePrinter(tissueMap, cellStringEncoder);                
                rounds.Add(printer);
                tissueMap = roundBasedGame.Advance(tissueMap);
            }

            foreach (var round in rounds)
            {
                Console.Clear();
                round.PrintTissue();
                Thread.Sleep(500);
            }

            Console.ReadKey();
        }
    }
}
