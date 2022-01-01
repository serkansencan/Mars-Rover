using MarsRovel;
using System;
using System.Collections.Generic;
using System.Linq;
using static MarsRover.Constants;

namespace MarsRover
{
    public class Plateau : IPlateau
    {
        public Position PlateauPosition { get; }
        public List<Rover> RoverList { get; set; }

        public Plateau(int x, int y)
        {
            PlateauPosition = new Position(x, y);
            RoverList = new List<Rover>();
        }

        public string AddRovel(Rover marsRovel)
        {
            if (!CheckPointPosition(marsRovel))
            {
                RoverList.Add(marsRovel);
                return Message.Successful;
            }

            return Message.Fail;
        }

        public string Process(Rover marsRovel, string commands)
        {
            foreach (char command in commands.Trim().ToUpper())
            {
                switch (command)
                {
                    case ('L'):
                        marsRovel.TurnLeft();
                        break;
                    case ('R'):
                        marsRovel.TurnRight();
                        break;
                    case ('M'):
                        if (!CheckRoverMovedPosition(marsRovel))
                            marsRovel.Move();
                        break;
                    default:
                        return (string.Format("{0}: {1}", Message.Invalid_Value, command));
                }
            }

            string printedRoverPosition = string.Format("{0} {1} {2}", marsRovel.RoverPosition.X, marsRovel.RoverPosition.Y, marsRovel.RoverOrientation.ToString());

            return printedRoverPosition;
        }

        private bool CheckRoverMovedPosition(Rover marsRovel)
        {
            var rover = new Rover(marsRovel);

            rover.Move();

            var isOutsidePlateau = IsOutsidePlateau(rover);

            var isHaveRoverInWillPosition = isOutsidePlateau ||
                                            RoverList.Any(x => x.RoverPosition.X.Equals(rover.RoverPosition.X)
                                             && x.RoverPosition.Y.Equals(rover.RoverPosition.Y));

            return isHaveRoverInWillPosition;
        }
        public bool CheckPointPosition(Rover marsRovel)
        {
            var isOutsidePlateau = IsOutsidePlateau(marsRovel);

            var isHaveRoverInPosition = isOutsidePlateau ||
                                        RoverList.Any(x => x.RoverPosition.X.Equals(marsRovel.RoverPosition.X)
                                                                     && x.RoverPosition.Y.Equals(marsRovel.RoverPosition.Y));

            return isHaveRoverInPosition;
        }

        public bool IsOutsidePlateau(Rover marsRovel)
        {
            bool isInsideBoundaries = false;

            if (marsRovel.RoverPosition.X > PlateauPosition.X 
                || marsRovel.RoverPosition.Y > PlateauPosition.Y
                || marsRovel.RoverPosition.X < 0
                || marsRovel.RoverPosition.Y < 0)

                isInsideBoundaries = true;

            return isInsideBoundaries;
        }
    }
    public class Position
    {
        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
