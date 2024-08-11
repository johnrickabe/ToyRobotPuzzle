using ToyRobotPuzzle.Common.Business.Utilities;
using ToyRobotPuzzle.Common.Models.Enums;

namespace ToyRobotPuzzle.Tests
{
    public class CommandLineParserTests
    {
        [Test]
        public void PlaceTests()
        {
            Assert.Multiple(() =>
            {
                Assert.That(CommandLineParser.ParseCommand("PLACE").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("PLACE 123").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("PLACE 123 123").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand(" PLACE 123 123").IsSuccess, Is.False);

                Assert.That(CommandLineParser.ParseCommand("PLACE 1,2").IsSuccess, Is.True);
                Assert.That(CommandLineParser.ParseCommand("PLACE -1,2").IsSuccess, Is.True);

                Assert.Multiple(() =>
                {
                    var response = CommandLineParser.ParseCommand("PLACE 4245663,7421331");
                    Assert.That(response.IsSuccess, Is.True);
                    Assert.That(response.Parameters[0], Is.EqualTo("4245663"));
                    Assert.That(response.Parameters[1], Is.EqualTo("7421331"));
                });

                Assert.Multiple(() =>
                {
                    var response = CommandLineParser.ParseCommand("PLACE 651587,-623351");
                    Assert.That(response.IsSuccess, Is.True);
                    Assert.That(response.Parameters[0], Is.EqualTo("651587"));
                    Assert.That(response.Parameters[1], Is.EqualTo("-623351"));
                });

                Assert.Multiple(() =>
                {
                    var response = CommandLineParser.ParseCommand("PLACE 1,2,NORTH");
                    Assert.That(response.IsSuccess, Is.True);
                    Assert.That(response.Parameters[0], Is.EqualTo("1"));
                    Assert.That(response.Parameters[1], Is.EqualTo("2"));
                    Assert.That(response.Parameters[2], Is.EqualTo("NORTH"));
                });

                Assert.Multiple(() =>
                {
                    var response = CommandLineParser.ParseCommand("PLACE -1,2,SOUTH");
                    Assert.That(response.IsSuccess, Is.True);
                    Assert.That(response.Command, Is.EqualTo(Commands.PLACE));
                    Assert.That(response.Parameters[0], Is.EqualTo("-1"));
                    Assert.That(response.Parameters[1], Is.EqualTo("2"));
                    Assert.That(response.Parameters[2], Is.EqualTo("SOUTH"));
                });

                Assert.Multiple(() =>
                {
                    var response = CommandLineParser.ParseCommand("PLACE 330,400,EAST");
                    Assert.That(response.IsSuccess, Is.True);
                    Assert.That(response.Command, Is.EqualTo(Commands.PLACE));
                    Assert.That(response.Parameters[0], Is.EqualTo("330"));
                    Assert.That(response.Parameters[1], Is.EqualTo("400"));
                    Assert.That(response.Parameters[2], Is.EqualTo("EAST"));
                });

                Assert.Multiple(() =>
                {
                    var response = CommandLineParser.ParseCommand("PLACE 482,2,WEST");
                    Assert.That(response.IsSuccess, Is.True);
                    Assert.That(response.Command, Is.EqualTo(Commands.PLACE));
                    Assert.That(response.Parameters[0], Is.EqualTo("482"));
                    Assert.That(response.Parameters[1], Is.EqualTo("2"));
                    Assert.That(response.Parameters[2], Is.EqualTo("WEST"));
                });

                Assert.That(CommandLineParser.ParseCommand("PLACE 482,2,WEST ").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("PLACE 482,2,WHEAT").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("PLACE WEST,1,2").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("PLACE 1.0,2,WEST").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("PLACE 1,-2.0,WEST").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("PLACE -1,2.0,WEST").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("PLACE -1,-2.0,WEST").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("PLACE 1,2 ").IsSuccess, Is.False);
            });
        }

        [Test]
        public void MoveTests()
        {
            Assert.Multiple(() =>
            {
                var response = CommandLineParser.ParseCommand("MOVE");
                Assert.That(response.IsSuccess, Is.True);
                Assert.That(response.Command, Is.EqualTo(Commands.MOVE));
            });

            Assert.That(CommandLineParser.ParseCommand(" MOVE").IsSuccess, Is.False);
            Assert.That(CommandLineParser.ParseCommand("MOVE 123").IsSuccess, Is.False);
            Assert.That(CommandLineParser.ParseCommand("MOVE 123 123").IsSuccess, Is.False);
        }

        [Test]
        public void LeftTests()
        {
            Assert.Multiple(() =>
            {
                var response = CommandLineParser.ParseCommand("LEFT");
                Assert.That(response.IsSuccess, Is.True);
                Assert.That(response.Command, Is.EqualTo(Commands.LEFT));

                Assert.That(CommandLineParser.ParseCommand(" LEFT").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("LEFT 123").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("LEFT 123 123").IsSuccess, Is.False);
            });
        }

        [Test]
        public void RightTests()
        {
            Assert.Multiple(() =>
            {
                var response = CommandLineParser.ParseCommand("RIGHT");
                Assert.That(response.IsSuccess, Is.True);
                Assert.That(response.Command, Is.EqualTo(Commands.RIGHT));

                Assert.That(CommandLineParser.ParseCommand(" RIGHT").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("RIGHT 123").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("RIGHT 123 123").IsSuccess, Is.False);
            });
        }

        [Test]
        public void ReportTests()
        {
            Assert.Multiple(() =>
            {
                var response = CommandLineParser.ParseCommand("REPORT");
                Assert.That(response.IsSuccess, Is.True);
                Assert.That(response.Command, Is.EqualTo(Commands.REPORT));

                Assert.That(CommandLineParser.ParseCommand(" REPORT").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("REPORT 123").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("REPORT 123 123").IsSuccess, Is.False);
            });
        }

        [Test]
        public void ExitTests()
        {
            Assert.Multiple(() =>
            {
                var response = CommandLineParser.ParseCommand("EXIT");
                Assert.That(response.IsSuccess, Is.True);
                Assert.That(response.Command, Is.EqualTo(Commands.EXIT));

                Assert.That(CommandLineParser.ParseCommand(" EXIT").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("EXIT 123").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("EXIT 123 123").IsSuccess, Is.False);
            });
        }

        [Test]
        public void RandomStringTests()
        {
            Assert.Multiple(() =>
            {
                Assert.That(CommandLineParser.ParseCommand("place 1,2,NORTH").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("PLACe 1,2,NORTH").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("Place 1,2,NORTH").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("PlAcE 1,2,NORTH").IsSuccess, Is.False);
            });

            Assert.Multiple(() =>
            {
                Assert.That(CommandLineParser.ParseCommand("move").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("Move").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("MoVe").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("MOve").IsSuccess, Is.False);
            });

            Assert.Multiple(() =>
            {
                Assert.That(CommandLineParser.ParseCommand("left").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("Left").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("LeFt").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("LEft").IsSuccess, Is.False);
            });

            Assert.Multiple(() =>
            {
                Assert.That(CommandLineParser.ParseCommand("right").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("Right").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("RiGhT").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("RIGht").IsSuccess, Is.False);
            });

            Assert.Multiple(() =>
            {
                Assert.That(CommandLineParser.ParseCommand("report").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("Report").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("RePoRt").IsSuccess, Is.False);
                Assert.That(CommandLineParser.ParseCommand("REPort").IsSuccess, Is.False);
            });

            Assert.That(CommandLineParser.ParseCommand(" ").IsSuccess, Is.False);
            Assert.That(CommandLineParser.ParseCommand("").IsSuccess, Is.False);
        }
    }
}
