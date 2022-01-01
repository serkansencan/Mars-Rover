using System;
using static MarsRover.Constants;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            var message = string.Empty;

            Plateau plateau = new Plateau(5, 5);

            var rover1 = new Rover(0, 0, Orientations.N);

            var rover2 = new Rover(3, 3, Orientations.E);
            
            plateau.AddRovel(rover1);
            message = plateau.Process(rover1, "LMLMLMLMM");
            Console.WriteLine(message);

            plateau.AddRovel(rover2);
            message = plateau.Process(rover2, "MMRMMRMRRM");
            Console.WriteLine(message);
        }
    }
}
