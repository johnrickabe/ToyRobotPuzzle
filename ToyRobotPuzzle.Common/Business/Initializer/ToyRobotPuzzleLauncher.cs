using ToyRobotPuzzle.Common.Business.Utilities;
using ToyRobotPuzzle.Common.Models.Entities;
using ToyRobotPuzzle.Common.Models.Enums;
using static ToyRobotPuzzle.Common.Business.Utilities.CommandLineParser;

namespace ToyRobotPuzzle.Common.Business.Initializer
{
    public class ToyRobotPuzzleLauncher
    {
        private readonly string SUCCESS_MESSAGE = "Success!";
        private readonly string FAILURE_MESSAGE = "Failed! (Robot might fall off the edge)";
        private readonly string ROBOT_NEED_PLACEMENT_MESSAGE = "Robot should be placed first!";

        private TableTop TableTop { get; set; }
        private Robot Robot { get; set; }
        private bool IsLaunched { get; set; } = true;

        public ToyRobotPuzzleLauncher()
        {
            TableTop = new TableTop();
            Robot = new Robot(TableTop);
        }

        public ToyRobotPuzzleLauncher(TableTop tableTop)
        {
            TableTop = tableTop;
            Robot = new Robot(tableTop);
        }

        public void Launch()
        {
            Console.WriteLine("Toy Robot Puzzle!");

            while (IsLaunched)
            {
                var readLine = Console.ReadLine();
                Console.Clear();

                if (readLine != null)
                {
                    ExecuteCommand(readLine);
                }
                else
                {
                    Console.WriteLine($"Error: Invalid command: '{readLine}'");
                }

                Visualizer.Visualize(this);
            }
        }

        public void ExecuteCommand(string readLine)
        {
            var isParsed = CommandLineParser.TryParse(readLine, out var response);
            if (isParsed)
            {
                switch (response.Command)
                {
                    case Commands.EXIT:
                        IsLaunched = false;
                        break;
                    case Commands.PLACE:
                        ExecutePlaceCommand(response);
                        break;
                    case Commands.MOVE:
                        ExecuteMoveCommand(response);
                        break;
                    case Commands.LEFT:
                        ExecuteLeftCommand(response);
                        break;
                    case Commands.RIGHT:
                        ExecuteRightCommand(response);
                        break;
                    case Commands.REPORT:
                        ExecuteReportCommand(response);
                        break;
                }
            }
            else
            {
                Console.WriteLine($"Error: '{response.Message}'");
            }
        }

        public void ExecutePlaceCommand(CommandLineParserResponse response)
        {
            int x = int.Parse(response.Parameters[0]);
            int y = int.Parse(response.Parameters[1]);

            if (response.Parameters.Count == 2)
            {
                if (!this.Robot.IsPlaced)
                {
                    Console.WriteLine("PLACE should have direction parameter on first use.");
                    return;
                }

                if (this.Robot.Place(x, y))
                    Console.WriteLine(SUCCESS_MESSAGE);
                else
                    Console.WriteLine(FAILURE_MESSAGE);
            }
            else if (response.Parameters.Count == 3)
            {
                if (this.Robot.Place(x, y, Enum.Parse<FacingDirection>(response.Parameters[2])))
                    Console.WriteLine(SUCCESS_MESSAGE);
                else
                    Console.WriteLine(FAILURE_MESSAGE);
            }
        }

        public void ExecuteMoveCommand(CommandLineParserResponse response)
        {
            if (!this.Robot.IsPlaced)
            {
                Console.WriteLine(ROBOT_NEED_PLACEMENT_MESSAGE);
                return;
            }

            if (this.Robot.MoveForward())
                Console.WriteLine(SUCCESS_MESSAGE);
            else
                Console.WriteLine(FAILURE_MESSAGE);
        }

        public void ExecuteLeftCommand(CommandLineParserResponse response)
        {
            if (!this.Robot.IsPlaced)
            {
                Console.WriteLine(ROBOT_NEED_PLACEMENT_MESSAGE);
                return;
            }

            if (this.Robot.Rotate(-1))
                Console.WriteLine(SUCCESS_MESSAGE);
            else
                Console.WriteLine(FAILURE_MESSAGE);
        }

        public void ExecuteRightCommand(CommandLineParserResponse response)
        {
            if (!this.Robot.IsPlaced)
            {
                Console.WriteLine(ROBOT_NEED_PLACEMENT_MESSAGE);
                return;
            }

            if (this.Robot.Rotate(1))
                Console.WriteLine(SUCCESS_MESSAGE);
            else
                Console.WriteLine(FAILURE_MESSAGE);
        }

        public void ExecuteReportCommand(CommandLineParserResponse response)
        {
            if (!this.Robot.IsPlaced)
            {
                Console.WriteLine(ROBOT_NEED_PLACEMENT_MESSAGE);
                return;
            }

            Console.WriteLine($"Robot is at {this.Robot.PositionX}, {this.Robot.PositionY}, {this.Robot.FacingDirection}");
        }

        public bool IsRobotPlaced() => this.Robot.IsPlaced;
        public FacingDirection? RobotFacingDirection => this.Robot.FacingDirection;
        public int? RobotPositionX => this.Robot.PositionX;
        public int? RobotPositionY => this.Robot.PositionY;
        public int TableWidth => this.TableTop.Width;
        public int TableHeight => this.TableTop.Height;

    }
}
