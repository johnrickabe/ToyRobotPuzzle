using ToyRobotPuzzle.Common.Business.Initializer;
using ToyRobotPuzzle.Common.Models.Enums;

namespace ToyRobotPuzzle.Tests
{
    public class ToyRobotPuzzleLauncherTests
    {
        ToyRobotPuzzleLauncher launcher;

        [SetUp]
        public void Setup()
        {
            launcher = new ToyRobotPuzzleLauncher();
        }

        [Test]
        public void RobotNotPlacedBasicTests()
        {
            Assert.That(launcher.IsRobotPlaced, Is.False);

            Assert.That(launcher.RobotPositionX, Is.Null);
            Assert.That(launcher.RobotPositionY, Is.Null);
            Assert.That(launcher.RobotFacingDirection, Is.Null);

            launcher.ExecuteCommand("MOVE");
            Assert.That(launcher.IsRobotPlaced, Is.False);
            Assert.That(launcher.RobotPositionX, Is.Null);
            Assert.That(launcher.RobotPositionY, Is.Null);
            Assert.That(launcher.RobotFacingDirection, Is.Null);

            launcher.ExecuteCommand("LEFT");
            Assert.That(launcher.IsRobotPlaced, Is.False);
            Assert.That(launcher.RobotPositionX, Is.Null);
            Assert.That(launcher.RobotPositionY, Is.Null);
            Assert.That(launcher.RobotFacingDirection, Is.Null);

            launcher.ExecuteCommand("RIGHT");
            Assert.That(launcher.IsRobotPlaced, Is.False);
            Assert.That(launcher.RobotPositionX, Is.Null);
            Assert.That(launcher.RobotPositionY, Is.Null);
            Assert.That(launcher.RobotFacingDirection, Is.Null);

            launcher.ExecuteCommand("REPORT");
            Assert.That(launcher.IsRobotPlaced, Is.False);
            Assert.That(launcher.RobotPositionX, Is.Null);
            Assert.That(launcher.RobotPositionY, Is.Null);
            Assert.That(launcher.RobotFacingDirection, Is.Null);
        }

        [Test]
        public void RobotPlacedBasicTests()
        {
            launcher.ExecuteCommand("PLACE 1,2,NORTH");
            Assert.That(launcher.IsRobotPlaced, Is.True);
            Assert.That(launcher.RobotPositionX, Is.EqualTo(1));
            Assert.That(launcher.RobotPositionY, Is.EqualTo(2));
            Assert.That(launcher.RobotFacingDirection, Is.EqualTo(FacingDirection.NORTH));

            launcher.ExecuteCommand("MOVE");
            Assert.That(launcher.IsRobotPlaced, Is.True);
            Assert.That(launcher.RobotPositionX, Is.EqualTo(1));
            Assert.That(launcher.RobotPositionY, Is.EqualTo(3));
            Assert.That(launcher.RobotFacingDirection, Is.EqualTo(FacingDirection.NORTH));

            launcher.ExecuteCommand("LEFT");
            Assert.That(launcher.IsRobotPlaced, Is.True);
            Assert.That(launcher.RobotPositionX, Is.EqualTo(1));
            Assert.That(launcher.RobotPositionY, Is.EqualTo(3));
            Assert.That(launcher.RobotFacingDirection, Is.EqualTo(FacingDirection.WEST));

            launcher.ExecuteCommand("MOVE");
            Assert.That(launcher.IsRobotPlaced, Is.True);
            Assert.That(launcher.RobotPositionX, Is.EqualTo(0));
            Assert.That(launcher.RobotPositionY, Is.EqualTo(3));
            Assert.That(launcher.RobotFacingDirection, Is.EqualTo(FacingDirection.WEST));

            launcher.ExecuteCommand("RIGHT");
            Assert.That(launcher.IsRobotPlaced, Is.True);
            Assert.That(launcher.RobotPositionX, Is.EqualTo(0));
            Assert.That(launcher.RobotPositionY, Is.EqualTo(3));
            Assert.That(launcher.RobotFacingDirection, Is.EqualTo(FacingDirection.NORTH));

            launcher.ExecuteCommand("RIGHT");
            Assert.That(launcher.IsRobotPlaced, Is.True);
            Assert.That(launcher.RobotPositionX, Is.EqualTo(0));
            Assert.That(launcher.RobotPositionY, Is.EqualTo(3));
            Assert.That(launcher.RobotFacingDirection, Is.EqualTo(FacingDirection.EAST));
        }

        [Test]
        public void PlaceFacingDirectionParameter()
        {
            Assert.That(launcher.IsRobotPlaced, Is.False);
            Assert.That(launcher.RobotPositionX, Is.Null);
            Assert.That(launcher.RobotPositionY, Is.Null);
            Assert.That(launcher.RobotFacingDirection, Is.Null);

            launcher.ExecuteCommand("PLACE 1,2");
            Assert.That(launcher.RobotPositionX, Is.Null);
            Assert.That(launcher.RobotPositionY, Is.Null);
            Assert.That(launcher.RobotFacingDirection, Is.Null);

            launcher.ExecuteCommand("PLACE 1,2,NORTH");
            Assert.That(launcher.IsRobotPlaced, Is.True);
            Assert.That(launcher.RobotPositionX, Is.EqualTo(1));
            Assert.That(launcher.RobotPositionY, Is.EqualTo(2));
            Assert.That(launcher.RobotFacingDirection, Is.EqualTo(FacingDirection.NORTH));
        }
    }
}
