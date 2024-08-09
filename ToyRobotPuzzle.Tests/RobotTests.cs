using ToyRobotPuzzle.Common.Models.Entities;
using ToyRobotPuzzle.Common.Models.Enums;

namespace ToyRobotPuzzle.Tests
{
    public class RobotTests
    {
        TableTop tableTop;
        Robot robot;

        [SetUp]
        public void SetUp()
        {
            tableTop = new TableTop();
            robot = new Robot(tableTop);
        }

        [Test]
        public void RobotNotSetupLogic()
        {
            Assert.That(robot.IsPlaced, Is.False);

            Assert.That(robot.Place(1, 2), Is.False);
            Assert.That(robot.Place(-1, 3), Is.False);

            Assert.That(robot.MoveForward(), Is.False);
            Assert.That(robot.MoveForward(2), Is.False);

            Assert.That(robot.MoveX(1), Is.False);
            Assert.That(robot.MoveX(-1), Is.False);
            Assert.That(robot.MoveY(1), Is.False);
            Assert.That(robot.MoveY(-1), Is.False);

            Assert.That(robot.Rotate(1), Is.False);
            Assert.That(robot.Rotate(4), Is.False);

            robot.Place(1, 2, FacingDirection.NORTH);
            Assert.That(robot.IsPlaced, Is.True);
        }

        [Test]
        public void RobotPlaceTestPositive()
        {
            robot.Place(1, 2, FacingDirection.NORTH);

            Assert.That(robot.PositionX, Is.EqualTo(1));
            Assert.That(robot.PositionY, Is.EqualTo(2));
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.NORTH));

            robot.Place(0, 3);
            Assert.That(robot.PositionX, Is.EqualTo(0));
            Assert.That(robot.PositionY, Is.EqualTo(3));
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.NORTH));
        }

        [Test]
        public void RobotPlaceTestNegative()
        {
            robot.Place(1, 2, FacingDirection.NORTH);

            Assert.That(robot.Place(-2, 4, FacingDirection.SOUTH), Is.False);
            Assert.That(robot.PositionX, Is.EqualTo(1));
            Assert.That(robot.PositionY, Is.EqualTo(2));
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.NORTH));

            Assert.That(robot.Place(-3, 2, FacingDirection.SOUTH), Is.False);
            Assert.That(robot.PositionX, Is.EqualTo(1));
            Assert.That(robot.PositionY, Is.EqualTo(2));
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.NORTH));
        }

        [Test]
        public void RobotPlaceTestExceedTableDimensions()
        {
            robot.Place(1, 2, FacingDirection.NORTH);

            Assert.That(robot.Place(2, robot.TableTop.Height + 1, FacingDirection.SOUTH), Is.False);
            Assert.That(robot.PositionX, Is.EqualTo(1));
            Assert.That(robot.PositionY, Is.EqualTo(2));
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.NORTH));

            Assert.That(robot.Place(robot.TableTop.Height + 2, 0, FacingDirection.SOUTH), Is.False);
            Assert.That(robot.PositionX, Is.EqualTo(1));
            Assert.That(robot.PositionY, Is.EqualTo(2));
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.NORTH));
        }

        [Test]
        public void RobotRotateTest()
        {
            robot.Place(1, 2, FacingDirection.NORTH);

            Assert.That(robot.Rotate(1), Is.True);
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.EAST));

            Assert.That(robot.Rotate(-2), Is.True);
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.WEST));

            Assert.That(robot.Rotate(-2), Is.True);
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.EAST));

            Assert.That(robot.Rotate(44), Is.True);
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.EAST));

            Assert.That(robot.Rotate(45), Is.True);
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.SOUTH));

            Assert.That(robot.Rotate(88), Is.True);
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.SOUTH));

            Assert.That(robot.Rotate(89), Is.True);
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.WEST));
        }

        [Test]
        public void RobotMoveForwardTest()
        {
            robot.Place(1, 2, FacingDirection.NORTH);

            Assert.That(robot.MoveForward(), Is.True);
            Assert.That(robot.PositionX, Is.EqualTo(1));
            Assert.That(robot.PositionY, Is.EqualTo(3));
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.NORTH));

            Assert.That(robot.MoveForward(2), Is.True);
            Assert.That(robot.PositionX, Is.EqualTo(1));
            Assert.That(robot.PositionY, Is.EqualTo(5));
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.NORTH));

            Assert.That(robot.MoveForward(-3), Is.True);
            Assert.That(robot.PositionX, Is.EqualTo(1));
            Assert.That(robot.PositionY, Is.EqualTo(2));
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.NORTH));
        }

        [Test]
        public void RobotRotateMoveForwardTest()
        {
            robot.Place(1, 2, FacingDirection.NORTH);

            robot.Rotate(2);
            robot.MoveForward();
            robot.MoveForward();
            robot.Rotate(1);
            robot.MoveForward();
            robot.Rotate(1);
            Assert.That(robot.PositionX, Is.EqualTo(0));
            Assert.That(robot.PositionY, Is.EqualTo(0));
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.NORTH));

            robot.MoveForward();
            robot.MoveForward();
            robot.MoveForward();
            robot.MoveForward();
            Assert.That(robot.MoveForward(), Is.True);
            Assert.That(robot.PositionX, Is.EqualTo(0));
            Assert.That(robot.PositionY, Is.EqualTo(5));
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.NORTH));

            robot.Rotate(1);
            robot.MoveForward();
            robot.Rotate(1);
            robot.MoveForward();
            robot.MoveForward();
            robot.MoveForward();
            robot.MoveForward();
            robot.MoveForward();
            Assert.That(robot.PositionX, Is.EqualTo(1));
            Assert.That(robot.PositionY, Is.EqualTo(0));
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.SOUTH));


            robot.Rotate(-1);
            robot.MoveForward();
            robot.MoveForward();
            robot.MoveForward();
            robot.MoveForward();
            Assert.That(robot.PositionX, Is.EqualTo(5));
            Assert.That(robot.PositionY, Is.EqualTo(0));
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.EAST));

            robot.Rotate(-1);
            robot.MoveForward();
            robot.MoveForward();
            robot.MoveForward();
            robot.MoveForward();
            robot.MoveForward();
            Assert.That(robot.PositionX, Is.EqualTo(5));
            Assert.That(robot.PositionY, Is.EqualTo(5));
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.NORTH));

            robot.Rotate(-1);
            robot.MoveForward();
            robot.MoveForward();
            robot.MoveForward();
            Assert.That(robot.PositionX, Is.EqualTo(2));
            Assert.That(robot.PositionY, Is.EqualTo(5));
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.WEST));

            robot.Rotate(-1);
            robot.MoveForward();
            robot.MoveForward();
            robot.MoveForward();
            Assert.That(robot.PositionX, Is.EqualTo(2));
            Assert.That(robot.PositionY, Is.EqualTo(2));
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.SOUTH));

            robot.Rotate(-1);
            robot.MoveForward();
            robot.MoveForward();
            Assert.That(robot.PositionX, Is.EqualTo(4));
            Assert.That(robot.PositionY, Is.EqualTo(2));
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.EAST));

            robot.Rotate(-1);
            robot.MoveForward();
            robot.MoveForward();
            Assert.That(robot.PositionX, Is.EqualTo(4));
            Assert.That(robot.PositionY, Is.EqualTo(4));
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.NORTH));
        }

        [Test]
        public void RobotMoveForwardTestExceedTable()
        {
            robot.Place(1, 2, FacingDirection.NORTH);
            Assert.That(robot.MoveForward(), Is.True);
            Assert.That(robot.MoveForward(), Is.True);
            Assert.That(robot.MoveForward(), Is.True);
            Assert.That(robot.MoveForward(), Is.False);

            robot.Rotate(-1);
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.WEST));
            Assert.That(robot.MoveForward(), Is.True);
            Assert.That(robot.PositionX, Is.EqualTo(0));
            Assert.That(robot.MoveForward(), Is.False);

            robot.Rotate(2);
            Assert.That(robot.FacingDirection, Is.EqualTo(FacingDirection.EAST));
            Assert.That(robot.MoveForward(), Is.True);
            Assert.That(robot.MoveForward(), Is.True);
            Assert.That(robot.MoveForward(), Is.True);
            Assert.That(robot.MoveForward(), Is.True);
            Assert.That(robot.MoveForward(), Is.True);
            Assert.That(robot.MoveForward(), Is.False);

            Assert.That(robot.PositionX, Is.EqualTo(5));
            Assert.That(robot.PositionY, Is.EqualTo(5));
        }
    }
}
