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
            Assert.That(tableTop.Height, Is.EqualTo(5));
            Assert.That(tableTop.Width, Is.EqualTo(5));
        }
    }
}