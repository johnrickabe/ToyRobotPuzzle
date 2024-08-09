using ToyRobotPuzzle.Common.Business.Utilities;
using ToyRobotPuzzle.Common.Models.Enums;

namespace ToyRobotPuzzle.Tests
{
    public class CommandLineParserTests
    {
        [Test]
        public void ExitTests()
        {
            Assert.That(CommandLineParser.TryParse("EXIT", out var response), Is.True);
            Assert.That(response.Command, Is.EqualTo(Commands.EXIT));

            Assert.That(CommandLineParser.TryParse(" EXIT", out _), Is.False);
            Assert.That(CommandLineParser.TryParse("EXIT 123", out _), Is.False);
            Assert.That(CommandLineParser.TryParse("EXIT 123 123", out _), Is.False);
        }

        [Test]
        public void PlaceTests()
        {
            Assert.That(CommandLineParser.TryParse("PLACE", out _), Is.False);
            Assert.That(CommandLineParser.TryParse("PLACE 123", out _), Is.False);
            Assert.That(CommandLineParser.TryParse("PLACE 123 123", out _), Is.False);
            Assert.That(CommandLineParser.TryParse(" PLACE 123 123", out _), Is.False);

            Assert.That(CommandLineParser.TryParse("PLACE 1,2", out _), Is.True);
            Assert.That(CommandLineParser.TryParse("PLACE -1,2", out _), Is.True);
            Assert.That(CommandLineParser.TryParse("PLACE 4245663,7421331", out _), Is.True);
            Assert.That(CommandLineParser.TryParse("PLACE 651587,-623351", out _), Is.True);

            Assert.That(CommandLineParser.TryParse("PLACE 1,2,NORTH", out _), Is.True);
            Assert.That(CommandLineParser.TryParse("PLACE -1,2,SOUTH", out _), Is.True);
            Assert.That(CommandLineParser.TryParse("PLACE 330,400,EAST", out _), Is.True);
            Assert.That(CommandLineParser.TryParse("PLACE 482,2,WEST", out var response), Is.True);
            Assert.That(response.Command, Is.EqualTo(Commands.PLACE));

            Assert.That(CommandLineParser.TryParse("PLACE 482,2,WEST ", out _), Is.False);
            Assert.That(CommandLineParser.TryParse("PLACE 1,2 ", out _), Is.False);
        }

        [Test]
        public void MoveTests()
        {
            Assert.That(CommandLineParser.TryParse("MOVE", out var response), Is.True);
            Assert.That(response.Command, Is.EqualTo(Commands.MOVE));

            Assert.That(CommandLineParser.TryParse(" MOVE", out _), Is.False);
            Assert.That(CommandLineParser.TryParse("MOVE 123", out _), Is.False);
            Assert.That(CommandLineParser.TryParse("MOVE 123 123", out _), Is.False);
        }

        [Test]
        public void LeftTests()
        {
            Assert.That(CommandLineParser.TryParse("LEFT", out var response), Is.True);
            Assert.That(response.Command, Is.EqualTo(Commands.LEFT));

            Assert.That(CommandLineParser.TryParse(" LEFT", out _), Is.False);
            Assert.That(CommandLineParser.TryParse("LEFT 123", out _), Is.False);
            Assert.That(CommandLineParser.TryParse("LEFT 123 123", out _), Is.False);
        }

        [Test]
        public void RightTests()
        {
            Assert.That(CommandLineParser.TryParse("RIGHT", out var response), Is.True);
            Assert.That(response.Command, Is.EqualTo(Commands.RIGHT));

            Assert.That(CommandLineParser.TryParse(" RIGHT", out _), Is.False);
            Assert.That(CommandLineParser.TryParse("RIGHT 123", out _), Is.False);
            Assert.That(CommandLineParser.TryParse("RIGHT 123 123", out _), Is.False);
        }

        [Test]
        public void ReportTests()
        {
            Assert.That(CommandLineParser.TryParse("REPORT", out var response), Is.True);
            Assert.That(response.Command, Is.EqualTo(Commands.REPORT));

            Assert.That(CommandLineParser.TryParse(" REPORT", out _), Is.False);
            Assert.That(CommandLineParser.TryParse("REPORT 123", out _), Is.False);
            Assert.That(CommandLineParser.TryParse("REPORT 123 123", out _), Is.False);
        }
    }
}
