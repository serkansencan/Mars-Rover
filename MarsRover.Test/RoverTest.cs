using Moq;
using Xunit;
using static MarsRover.Constants;

namespace MarsRover.Test
{
    public class RoverTest
    {
        [Theory]
        [InlineData(5, 5)]
        [InlineData(7, 10)]
        [InlineData(20, 10)]
        public void Plateau_CreatePlateu_Success(int x, int y)
        {
            #region Act
            Mock<Plateau> plateau = new Mock<Plateau>(x, y);
            #endregion

            #region Assert
            Assert.Equal(plateau.Object.PlateauPosition.X, x);
            Assert.Equal(plateau.Object.PlateauPosition.Y, y);
            #endregion
        }

        [Theory]
        [InlineData(1, 2, Orientations.N)]
        public void Rover_AddRoverInPlateou_Success(int x, int y, Orientations orientation)
        {
            #region Arrange
            Mock<Plateau> plateauMock = new Mock<Plateau>(10, 10);
            Mock<Rover> roverMock = new Mock<Rover>(x, y, orientation);
            #endregion

            #region Act
            var message = plateauMock.Object.AddRovel(roverMock.Object);
            #endregion

            #region Assert
            Assert.Equal(message, Message.Successful);
            #endregion
        }

        [Theory]
        [InlineData(1, 6, Orientations.N)]
        [InlineData(1, 10, Orientations.N)]
        public void Rover_AddRoverOutsidePlateou_Fail(int x, int y, Orientations orientation)
        {
            #region Arrange
            Mock<Plateau> plateauMock = new Mock<Plateau>(5, 5);
            Mock<Rover> roverMock = new Mock<Rover>(x, y, orientation);
            #endregion

            #region Act
            var message = plateauMock.Object.AddRovel(roverMock.Object);
            #endregion

            #region Assert
            Assert.Equal(message, Message.Fail);
            #endregion
        }

        [Theory]
        [InlineData(2, 2, Orientations.N)]
        public void Rover_AddRoverOnTopOfOtherRover_Fail(int x, int y, Orientations orientation)
        {
            #region Arrange
            Mock<Plateau> plateauMock = new Mock<Plateau>(5, 5);
            Mock<Rover> otherRoverMock = new Mock<Rover>(2, 2, Orientations.N);
            Mock<Rover> roverMock = new Mock<Rover>(x, y, orientation);
            #endregion

            #region Act
            plateauMock.Object.AddRovel(otherRoverMock.Object);
            var message = plateauMock.Object.AddRovel(roverMock.Object);
            #endregion

            #region Assert
            Assert.Equal(message, Message.Fail);
            #endregion
        }

        [Theory]
        [InlineData("MMMMMMMMMMM")]
        [InlineData("RRMMMMMRMMM")]
        public void RoverProcess_RoverMoveCheckInPlateau_Success(string command)
        {
            #region Arrange
            Mock<Plateau> plateauMock = new Mock<Plateau>(5, 5);
            Mock<Rover> roverMock = new Mock<Rover>(4, 4, Orientations.W);
            plateauMock.Object.AddRovel(roverMock.Object);
            #endregion

            #region Act
            plateauMock.Object.Process(roverMock.Object, command);
            #endregion

            #region Assert
            Assert.InRange(roverMock.Object.RoverPosition.X, 0, 5);
            Assert.InRange(roverMock.Object.RoverPosition.Y, 0, 5);
            #endregion
        }

        [Fact]
        public void RoverProcess_RoverMoveCheckOtherRoverInPlateau_Success()
        {
            #region Arrange
            int pateau = 5;
            int roverX = 0;
            int roverY = 0;
            string command = "MMMMMLMMMMMMM";
            Mock<Plateau> plateauMock = new Mock<Plateau>(pateau, pateau);
            Mock<Rover> otherRoverMock = new Mock<Rover>(roverX, roverY, Orientations.N);
            Mock<Rover> roverMock = new Mock<Rover>(3, 3, Orientations.W);
            plateauMock.Object.AddRovel(otherRoverMock.Object);
            #endregion

            #region Act
            plateauMock.Object.AddRovel(roverMock.Object);
            plateauMock.Object.Process(roverMock.Object, command);
            #endregion

            #region Assert
            Assert.Equal(roverMock.Object.RoverPosition.X, roverX);
            Assert.Equal(roverMock.Object.RoverPosition.Y, roverY + 1);
            #endregion
        }

        [Fact]
        public void RoverProcess_RoverMove_Success()
        {
            #region Arrange
            int pateau = 5;
            int roverX = 1;
            int roverY = 2;
            string command = "LMLMLMLMM";
            Mock<Plateau> plateauMock = new Mock<Plateau>(pateau, pateau);
            Mock<Rover> roverMock = new Mock<Rover>(roverX, roverY, Orientations.N);
            #endregion

            #region Act
            plateauMock.Object.AddRovel(roverMock.Object);
            plateauMock.Object.Process(roverMock.Object, command);
            #endregion

            #region Assert
            Assert.Equal(roverMock.Object.RoverPosition.X, 1);
            Assert.Equal(roverMock.Object.RoverPosition.Y, 3);
            Assert.Equal(roverMock.Object.RoverOrientation, Orientations.N);
            #endregion
        }

        [Theory]
        [InlineData(2, 2, Orientations.N)]
        [InlineData(3, 1, Orientations.N)]
        [InlineData(5, 0, Orientations.N)]
        public void IsOutsidePlateau_RoverIsInsidePlateau_Success(int x, int y, Orientations orientation)
        {
            #region Arrange
            Mock<Plateau> plateauMock = new Mock<Plateau>(5, 5);
            Rover roverMock = new Rover(x, y, orientation);
            #endregion

            #region Act
            var result = plateauMock.Object.IsOutsidePlateau(roverMock);
            #endregion

            #region Assert
            Assert.False(result);
            #endregion
        }

        [Theory]
        [InlineData(7, 1, Orientations.N)]
        [InlineData(3, 8, Orientations.N)]
        [InlineData(9, 9, Orientations.N)]
        [InlineData(3, -1, Orientations.N)]
        [InlineData(-3, 4, Orientations.N)]
        [InlineData(-3, -1, Orientations.N)]
        public void IsOutsidePlateau_RoverIsInsidePlateau_Fail(int x, int y, Orientations orientation)
        {
            #region Arrange
            Mock<Plateau> plateauMock = new Mock<Plateau>(5, 5);
            Rover roverMock = new Rover(x, y, orientation);
            #endregion

            #region Act
            var result = plateauMock.Object.IsOutsidePlateau(roverMock);
            #endregion

            #region Assert
            Assert.True(result);
            #endregion
        }

        [Theory]
        [InlineData(2, 2, Orientations.N)]
        [InlineData(3, 1, Orientations.N)]
        [InlineData(5, 0, Orientations.N)]
        public void CheckPointPosition_RoverCheckPointPosition_Success(int x, int y, Orientations orientation)
        {
            #region Arrange
            Mock<Plateau> plateauMock = new Mock<Plateau>(5, 5);
            Rover roverMock = new Rover(x, y, orientation);
            #endregion

            #region Act
            var result = plateauMock.Object.CheckPointPosition(roverMock);
            #endregion

            #region Assert
            Assert.False(result);
            #endregion
        }

        [Theory]
        [InlineData(2, 2, Orientations.N)]
        public void CheckPointPosition_RoverCheckPointPosition_Fail(int x, int y, Orientations orientation)
        {
            #region Arrange
            Mock<Plateau> plateauMock = new Mock<Plateau>(5, 5);
            Rover otherRoverMock = new Rover(2, 2, Orientations.N);
            Rover roverMock = new Rover(x, y, orientation);
            #endregion

            #region Act
            plateauMock.Object.AddRovel(otherRoverMock);
            var result = plateauMock.Object.CheckPointPosition(roverMock);
            #endregion

            #region Assert
            Assert.True(result);
            #endregion
        }

        [Theory]
        [InlineData("DMMMSMMMMMM","D")]
        [InlineData("RRM?MMMRMMM","?")]
        public void RoverProcess_InvalidValue_Fail(string command, string invalidValue)
        {
            #region Arrange
            Mock<Plateau> plateauMock = new Mock<Plateau>(5, 5);
            Mock<Rover> roverMock = new Mock<Rover>(4, 4, Orientations.W);
            plateauMock.Object.AddRovel(roverMock.Object);
            #endregion

            #region Act
            var message = plateauMock.Object.Process(roverMock.Object, command);
            #endregion

            #region Assert
            Assert.Contains(message, string.Format("{0}: {1}", Message.Invalid_Value, invalidValue));
            #endregion
        }
    }
}
