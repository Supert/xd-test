using System.Linq;

namespace XdTest
{
    public class MockPuzzleGenerator : IPuzzleGenerator
    {
        public Puzzle Generate()
        {
            //14-15 problem
            int[] initialPositions = new int[Core.SIZE]
            {
                    1, 2, 3, 4,
                    5, 6, 7, 8,
                    9, 10, 11, 12,
                    13, 15, 14, 0
            };

            //Solution with empty tile in right bottom corner is not reachable for 14-15 problem.
            int[] solution = new int[Core.SIZE]
            {
                0, 1, 2, 3,
                4, 5, 6, 7,
                8, 9, 10, 11,
                12, 13, 14, 15
            };

            return new Puzzle()
            {
                InitialPositions = initialPositions,
                WinConditionPredicate = (field) => field.SequenceEqual(solution),
            };
        }
    }
}