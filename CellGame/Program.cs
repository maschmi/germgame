﻿using System.Linq;
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
            var germFactory = new DefaultGermFactory();
            var tissueMap = new Tissue2DFactory(cellFactory, germFactory, new FisherYatesShuffle()).
                Create(maxX, maxY, 0.8f, 0.15f);
            
            var printer = new TissuePrinter(maxX, maxY, tissueMap, new CellPrinter());

            printer.PrintTissue();
        }
    }
}
