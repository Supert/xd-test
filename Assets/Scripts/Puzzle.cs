using System;

namespace XdTest
{
    public struct Puzzle
    {
        public int[] InitialPositions;
        public Func<int[], bool> WinConditionPredicate;
    }
}