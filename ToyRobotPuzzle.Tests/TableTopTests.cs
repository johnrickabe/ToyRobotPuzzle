using ToyRobotPuzzle.Common.Models.Entities;

namespace ToyRobotPuzzle.Tests
{
    public class TableTopTests
    {
        TableTop tableTop;
        [SetUp]
        public void Setup()
        {
            tableTop = new TableTop();
        }

        [Test]
        public void TableTopDefault5()
        {
            Assert.Multiple(() =>
            {
                Assert.That(tableTop.Height, Is.EqualTo(5));
                Assert.That(tableTop.Width, Is.EqualTo(5));
            });
        }

        [Test]
        public void MoveRobotToAnotherTable()
        {
            var anotherTableTop = new TableTop();
            var robot = new Robot(tableTop);

            Assert.Multiple(() =>
            {
                Assert.That(robot.TableTop, Is.EqualTo(tableTop));
                Assert.That(tableTop.Robots, Does.Contain(robot));
                Assert.That(anotherTableTop.Robots, Does.Not.Contain(robot));
            });

            tableTop.MoveRobotToAnotherTable(robot, anotherTableTop);
            Assert.Multiple(() =>
            {
                Assert.That(robot.TableTop, Is.EqualTo(anotherTableTop));
                Assert.That(anotherTableTop.Robots, Does.Contain(robot));
                Assert.That(tableTop.Robots, Does.Not.Contain(robot));
            });
        }

        [Test]
        public void MoveRobotToAnotherTableFromRobot()
        {
            var anotherTableTop = new TableTop();
            var robot = new Robot(tableTop);

            Assert.Multiple(() =>
            {
                Assert.That(robot.TableTop, Is.EqualTo(tableTop));
                Assert.That(tableTop.Robots, Does.Contain(robot));
                Assert.That(anotherTableTop.Robots, Does.Not.Contain(robot));
            });

            robot.MoveToTable(anotherTableTop);
            Assert.Multiple(() =>
            {
                Assert.That(robot.TableTop, Is.EqualTo(anotherTableTop));
                Assert.That(anotherTableTop.Robots, Does.Contain(robot));
                Assert.That(tableTop.Robots, Does.Not.Contain(robot));
            });
        }
    }
}