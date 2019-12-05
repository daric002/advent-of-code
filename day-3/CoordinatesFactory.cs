using System;
using System.Collections.Generic;
using System.Text;

namespace day_3
{
    public class CoordinatesFactory
    {
        public static List<Tuple<int,int>> GetCoordinates(List<Tuple<Direction, int>> commands)
        {
            var coordinates = new List<Tuple<int, int>>();
            coordinates.Add(new Tuple<int, int>(0, 0));

            for(int i = 0; i<commands.Count; i++)
            {
                switch (commands[i].Item1)
                {
                    case Direction.UP:
                        coordinates.Add(new Tuple<int, int>(coordinates[i].Item1, coordinates[i].Item2 + commands[i].Item2));
                        break;
                    case Direction.DOWN:
                        coordinates.Add(new Tuple<int, int>(coordinates[i].Item1, coordinates[i].Item2 - commands[i].Item2));
                        break;
                    case Direction.LEFT:
                        coordinates.Add(new Tuple<int, int>(coordinates[i].Item1 - commands[i].Item2, coordinates[i].Item2));
                        break;
                    case Direction.RIGHT:
                        coordinates.Add(new Tuple<int, int>(coordinates[i].Item1 + commands[i].Item2, coordinates[i].Item2));
                        break;
                }
            }
            return coordinates;
        }
    }

    public enum Direction
    {
        UP = 'U',
        DOWN = 'D',
        LEFT = 'L',
        RIGHT = 'R'
    }
}
