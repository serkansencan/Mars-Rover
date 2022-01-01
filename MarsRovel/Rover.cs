using static MarsRover.Constants;

namespace MarsRover
{
    public class Rover : IRover
    {
        public Position RoverPosition { get; set; }
        public Orientations RoverOrientation { get; set; }

        public Rover(int positionX, int positionY, Orientations orientation)
        {
            RoverPosition = new Position(positionX, positionY);
            RoverOrientation = orientation;
        }
        public Rover(Rover rover) : this(rover.RoverPosition.X, rover.RoverPosition.Y, rover.RoverOrientation)
        {
        }

        public void TurnLeft()
        {
            RoverOrientation = (RoverOrientation - 1) < Orientations.N ? Orientations.W : RoverOrientation - 1;
        }
        public void TurnRight()
        {
            RoverOrientation = (RoverOrientation + 1) > Orientations.W ? Orientations.N : RoverOrientation + 1;
        }
        public void Move()
        {
            if (RoverOrientation == Orientations.N)
            {
                RoverPosition.Y++;
            }
            else if (RoverOrientation == Orientations.E)
            {
                RoverPosition.X++;
            }
            else if (RoverOrientation == Orientations.S)
            {
                RoverPosition.Y--;
            }
            else if (RoverOrientation == Orientations.W)
            {
                RoverPosition.X--;
            }
        }
    }
}
