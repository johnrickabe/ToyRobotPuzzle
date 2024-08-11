using ToyRobotPuzzle.Common.Business.Utilities;
using ToyRobotPuzzle.Common.Models.Enums;

namespace ToyRobotPuzzle.Tests
{
    public class CommandLineParserTests
    {
        [Test]
        public void ExitTests()
        {
            Assert.Multiple(() =>
            {
                Assert.That(CommandLineParser.TryParse("EXIT", out var response), Is.True);
                Assert.That(response.Command, Is.EqualTo(Commands.EXIT));

                Assert.That(CommandLineParser.TryParse(" EXIT", out _), Is.False);
                Assert.That(CommandLineParser.TryParse("EXIT 123", out _), Is.False);
                Assert.That(CommandLineParser.TryParse("EXIT 123 123", out _), Is.False);
            });
        }

        [Test]
        public void PlaceTests()
        {
            Assert.Multiple(() =>
            {
                Assert.That(CommandLineParser.TryParse("PLACE", out _), Is.False);
                Assert.That(CommandLineParser.TryParse("PLACE 123", out _), Is.False);
                Assert.That(CommandLineParser.TryParse("PLACE 123 123", out _), Is.False);
                Assert.That(CommandLineParser.TryParse(" PLACE 123 123", out _), Is.False);

                Assert.That(CommandLineParser.TryParse("PLACE 1,2", out _), Is.True);
                Assert.That(CommandLineParser.TryParse("PLACE -1,2", out _), Is.True);

                Assert.Multiple(() =>
                {
                    Assert.That(CommandLineParser.TryParse("PLACE 4245663,7421331", out var response), Is.True);
                    Assert.That(response.Parameters[0], Is.EqualTo("4245663"));
                    Assert.That(response.Parameters[1], Is.EqualTo("7421331"));
                });

                Assert.Multiple(() =>
                {
                    Assert.That(CommandLineParser.TryParse("PLACE 651587,-623351", out var response), Is.True);
                    Assert.That(response.Parameters[0], Is.EqualTo("651587"));
                    Assert.That(response.Parameters[1], Is.EqualTo("-623351"));
                });

                Assert.Multiple(() =>
                {
                    Assert.That(CommandLineParser.TryParse("PLACE 1,2,NORTH", out var response), Is.True);
                    Assert.That(response.Parameters[0], Is.EqualTo("1"));
                    Assert.That(response.Parameters[1], Is.EqualTo("2"));
                    Assert.That(response.Parameters[2], Is.EqualTo("NORTH"));
                });

                Assert.Multiple(() =>
                {
                    Assert.That(CommandLineParser.TryParse("PLACE -1,2,SOUTH", out var response), Is.True);
                    Assert.That(response.Command, Is.EqualTo(Commands.PLACE));
                    Assert.That(response.Parameters[0], Is.EqualTo("-1"));
                    Assert.That(response.Parameters[1], Is.EqualTo("2"));
                    Assert.That(response.Parameters[2], Is.EqualTo("SOUTH"));
                });

                Assert.Multiple(() =>
                {
                    Assert.That(CommandLineParser.TryParse("PLACE 330,400,EAST", out var response), Is.True);
                    Assert.That(response.Command, Is.EqualTo(Commands.PLACE));
                    Assert.That(response.Parameters[0], Is.EqualTo("330"));
                    Assert.That(response.Parameters[1], Is.EqualTo("400"));
                    Assert.That(response.Parameters[2], Is.EqualTo("EAST"));
                });

                Assert.Multiple(() =>
                {
                    Assert.That(CommandLineParser.TryParse("PLACE 482,2,WEST", out var response), Is.True);
                    Assert.That(response.Command, Is.EqualTo(Commands.PLACE));
                    Assert.That(response.Parameters[0], Is.EqualTo("482"));
                    Assert.That(response.Parameters[1], Is.EqualTo("2"));
                    Assert.That(response.Parameters[2], Is.EqualTo("WEST"));
                });

                Assert.That(CommandLineParser.TryParse("PLACE 482,2,WEST ", out _), Is.False);
                Assert.That(CommandLineParser.TryParse("PLACE 482,2,WHEAT", out _), Is.False);
                Assert.That(CommandLineParser.TryParse("PLACE WEST,1,2", out _), Is.False);
                Assert.That(CommandLineParser.TryParse("PLACE 1.0,2,WEST", out _), Is.False);
                Assert.That(CommandLineParser.TryParse("PLACE 1,-2.0,WEST", out _), Is.False);
                Assert.That(CommandLineParser.TryParse("PLACE -1,2.0,WEST", out _), Is.False);
                Assert.That(CommandLineParser.TryParse("PLACE -1,-2.0,WEST", out _), Is.False);
                Assert.That(CommandLineParser.TryParse("PLACE 1,2 ", out _), Is.False);
            });
        }

        [Test]
        public void MoveTests()
        {
            Assert.Multiple(() =>
            {
                Assert.That(CommandLineParser.TryParse("MOVE", out var response), Is.True);
                Assert.That(response.Command, Is.EqualTo(Commands.MOVE));

                Assert.That(CommandLineParser.TryParse(" MOVE", out _), Is.False);
                Assert.That(CommandLineParser.TryParse("MOVE 123", out _), Is.False);
                Assert.That(CommandLineParser.TryParse("MOVE 123 123", out _), Is.False);
            });
        }

        [Test]
        public void LeftTests()
        {
            Assert.Multiple(() =>
            {
                Assert.That(CommandLineParser.TryParse("LEFT", out var response), Is.True);
                Assert.That(response.Command, Is.EqualTo(Commands.LEFT));

                Assert.That(CommandLineParser.TryParse(" LEFT", out _), Is.False);
                Assert.That(CommandLineParser.TryParse("LEFT 123", out _), Is.False);
                Assert.That(CommandLineParser.TryParse("LEFT 123 123", out _), Is.False);
            });
        }

        [Test]
        public void RightTests()
        {
            Assert.Multiple(() =>
            {
                Assert.That(CommandLineParser.TryParse("RIGHT", out var response), Is.True);
                Assert.That(response.Command, Is.EqualTo(Commands.RIGHT));

                Assert.That(CommandLineParser.TryParse(" RIGHT", out _), Is.False);
                Assert.That(CommandLineParser.TryParse("RIGHT 123", out _), Is.False);
                Assert.That(CommandLineParser.TryParse("RIGHT 123 123", out _), Is.False);
            });
        }

        [Test]
        public void ReportTests()
        {
            Assert.Multiple(() =>
            {
                Assert.That(CommandLineParser.TryParse("REPORT", out var response), Is.True);
                Assert.That(response.Command, Is.EqualTo(Commands.REPORT));

                Assert.That(CommandLineParser.TryParse(" REPORT", out _), Is.False);
                Assert.That(CommandLineParser.TryParse("REPORT 123", out _), Is.False);
                Assert.That(CommandLineParser.TryParse("REPORT 123 123", out _), Is.False);
            });
        }
    }
}
