﻿namespace Contracts
{
    public class ObstructedCells
    {
        public ObstructedCells(int x, int y) 
        {
            X = x;
            Y = y;
        }
        public int X { get;set;}
        public int Y { get; set; }
    }
}