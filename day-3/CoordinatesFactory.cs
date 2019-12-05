using System;
using System.Collections.Generic;
using System.Text;

namespace day_3
{
    public class CoordinatesFactory
    {
        public static List<Tuple<decimal,decimal>> GetCoordinates(List<Tuple<Direction, decimal>> commands)
        {
            var coordinates = new List<Tuple<decimal, decimal>>();
            coordinates.Add(new Tuple<decimal, decimal>(0, 0));

            for(int i = 0; i<commands.Count; i++)
            {
                switch (commands[i].Item1)
                {
                    case Direction.UP:
                        coordinates.Add(new Tuple<decimal, decimal>(coordinates[i].Item1, coordinates[i].Item2 + commands[i].Item2));
                        break;
                    case Direction.DOWN:
                        coordinates.Add(new Tuple<decimal, decimal>(coordinates[i].Item1, coordinates[i].Item2 - commands[i].Item2));
                        break;
                    case Direction.LEFT:
                        coordinates.Add(new Tuple<decimal, decimal>(coordinates[i].Item1 - commands[i].Item2, coordinates[i].Item2));
                        break;
                    case Direction.RIGHT:
                        coordinates.Add(new Tuple<decimal, decimal>(coordinates[i].Item1 + commands[i].Item2, coordinates[i].Item2));
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
