using static MarsRover.Constants;

namespace MarsRover
{
    public interface IRover
    {
        Position RoverPosition { get; set; }
        Orientations RoverOrientation { get; set; }
        void TurnLeft();
        void TurnRight();
        void Move();
    }
}
