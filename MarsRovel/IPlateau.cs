using MarsRover;
using System.Collections.Generic;

namespace MarsRovel
{
    public interface IPlateau
    {
        Position PlateauPosition { get; }
        List<Rover> RoverList { get; set; }
    }
}
