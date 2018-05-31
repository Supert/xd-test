using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XdTest
{
    public class Field : MonoBehaviour
    {
        private RectTransform rect;

        private Puzzle puzzle;

        //Left to right, up to down.
        //I can work with multidimentional arrays, it's just matter of my personal preference.
        //0 is an empty tile.
        private int[] field = new int[Core.SIZE];
        private Tile[] tiles = new Tile[Core.SIZE];

        private void Awake()
        {
            rect = GetComponent<RectTransform>();

            //Can hurt you in a long run.
            Tile prefab = transform.GetChild(0).GetComponent<Tile>();
            for (int i = 0; i < field.Length; i++)
            {
                field[i] = i;
            }

            for (int i = 0; i < field.Length - 1; i++)
            {
                tiles[i] = Instantiate(prefab, transform);

                tiles[i].Initialize(i, GetTileAnchoredPositionFromIndex(i));

                //I'm aware about closures.
                int tileValue = i + 1;
                tiles[i].OnClick += () => OnTileClicked(tileValue);
            }
        }

        private void OnTileClicked(int tileValue)
        {
            int index = 0;
            while (field[index] != tileValue)
                index++;

            AllowedDirections direction = GetMovingDirection(index);

            if (direction == AllowedDirections.None)
                return;

            List<int> indices = GetMovingIndices(index, direction);

            foreach (var i in indices)
            {
                switch (direction)
                {
                    case AllowedDirections.Up:
                        MoveTile(i, i - Core.SIDE_SIZE);
                        break;

                    case AllowedDirections.Right:
                        MoveTile(i, i + 1);
                        break;

                    case AllowedDirections.Down:
                        MoveTile(i, i + Core.SIDE_SIZE);
                        break;

                    case AllowedDirections.Left:
                        MoveTile(i, i - 1);
                        break;

                    case AllowedDirections.None:
                        Debug.LogError("You are trying to move tiles that can't be moved. How did you end up in there?");
                        break;
                }
            }

            if (puzzle.WinConditionPredicate(field))
                Core.Instance.FSM.Event(FSM.Events.PuzzleSolved);
        }

        private void MoveTile(int from, int to)
        {
            field[to] = field[from];
            field[from] = 0;

            tiles[field[to] - 1].TargetPosition = GetTileAnchoredPositionFromIndex(to);
        }

        private AllowedDirections GetMovingDirection(int index)
        {
            int tileX = index % Core.SIDE_SIZE;
            int tileY = index / Core.SIDE_SIZE;

            //Go to each direction and see if there is an empty tile;

            for (int y = tileY - 1; y >= 0; y--)
            {
                if (field[tileX + y * Core.SIDE_SIZE] == 0)
                    return AllowedDirections.Up;
            }

            for (int x = tileX + 1; x < Core.SIDE_SIZE; x++)
            {
                if (field[x + tileY * Core.SIDE_SIZE] == 0)
                    return AllowedDirections.Right;
            }

            for (int y = tileY + 1; y < Core.SIDE_SIZE; y++)
            {
                if (field[tileX + y * Core.SIDE_SIZE] == 0)
                    return AllowedDirections.Down;
            }

            for (int x = tileX - 1; x >= 0; x--)
            {
                if (field[x + tileY * Core.SIDE_SIZE] == 0)
                    return AllowedDirections.Left;
            }

            return AllowedDirections.None;
        }

        private List<int> GetMovingIndices(int index, AllowedDirections direction)
        {
            List<int> result = new List<int>();
            int currentIndex = index;
            switch (direction)
            {
                case AllowedDirections.Up:
                    while (currentIndex >= 0 && field[currentIndex] != 0)
                    {
                        result.Add(currentIndex);
                        currentIndex -= Core.SIDE_SIZE;
                    }
                    break;

                case AllowedDirections.Right:
                    while (currentIndex % Core.SIDE_SIZE < Core.SIZE && field[currentIndex] != 0)
                    {
                        result.Add(currentIndex);
                        currentIndex++;
                    }
                    break;

                case AllowedDirections.Down:
                    while (currentIndex < Core.SIZE && field[currentIndex] != 0)
                    {
                        result.Add(currentIndex);
                        currentIndex += Core.SIDE_SIZE;
                    }
                    break;

                case AllowedDirections.Left:
                    while (currentIndex % Core.SIDE_SIZE >= 0 && field[currentIndex] != 0)
                    {
                        result.Add(currentIndex);
                        currentIndex--;
                    }
                    break;
            }
            result.Reverse();
            return result;
        }

        private Vector2 GetTileAnchoredPositionFromIndex(int index)
        {
            return new Vector2(index % Core.SIDE_SIZE + 0.5f, - index / Core.SIDE_SIZE - 0.5f) * rect.rect.width / Core.SIDE_SIZE;
        }

        public void Initialize()
        {
            puzzle = Core.Instance.PuzzleGenerator.Generate();
            for (int i = 0; i < puzzle.InitialPositions.Length; i++)
            {
                field[i] = puzzle.InitialPositions[i];
                if (field[i] != 0)
                tiles[field[i] - 1].TargetPosition = GetTileAnchoredPositionFromIndex(i);
            }
        }
    }
}