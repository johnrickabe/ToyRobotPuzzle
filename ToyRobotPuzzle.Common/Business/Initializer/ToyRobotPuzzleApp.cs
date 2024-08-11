using ToyRobotPuzzle.Common.Business.Utilities;
using ToyRobotPuzzle.Common.Models.Entities;
using ToyRobotPuzzle.Common.Models.Enums;

namespace ToyRobotPuzzle.Common.Business.Initializer
{
    public class ToyRobotPuzzleApp
    {
        private readonly string SUCCESS_MESSAGE = "Success!";
        private readonly string FAILURE_MESSAGE = "Failed! (Robot might fall off the edge)";
        private readonly string ROBOT_NEED_PLACEMENT_MESSAGE = "Robot should be placed first!";

        private TableTop TableTop { get; set; }
        private Robot Robot { get; set; }
        private bool IsLaunched { get; set; } = true;

        public bool IsRobotPlaced => this.Robot.IsPlaced;

        public FacingDirection? RobotFacingDirection => this.Robot.FacingDirection;
        public int? RobotPositionX => this.Robot.PositionX;
        public int? RobotPositionY => this.Robot.PositionY;

        public int TableWidth => this.TableTop.Width;
        public int TableHeight => this.TableTop.Height;

        public ToyRobotPuzzleApp()
        {
            TableTop = new TableTop();
            Robot = new Robot(TableTop);
        }

        public void LaunchConsole()
        {
            Console.WriteLine("Toy Robot Puzzle!\n");
            Console.WriteLine("Commands:");
            Console.WriteLine("\"PLACE X,Y,[F]\" Where X and Y is an integer, and F is one of \"NORTH\",\"SOUTH\",\"EAST\", and \"WEST\". ([] = optional except on first use)");
            Console.WriteLine("\"MOVE\" will move the toy robot one unit forward in the direction it is currently facing.");
            Console.WriteLine("\"LEFT\" and \"RIGHT\" rotate the robot 90 degrees in the specified direction without changing the robot's position.");
            Console.WriteLine("\"REPORT\" announces the X,Y and F of the robot. (e.g. output 1, 2 NORTH )");
            Console.WriteLine("\"EXIT\" to close the application.\n");

            while (IsLaunched)
            {
                var readLine = Console.ReadLine();
                Console.Clear();

                if (readLine != null)
                {
                    ExecuteCommand(readLine);
                }

                Visualizer.Visualize(this);
            }
        }

        public void ExecuteCommand(string readLine)
        {
            var response = CommandLineParser.ParseCommand(readLine);
            if (response.IsSuccess)
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
                        ExecuteMoveCommand();
                        break;
                    case Commands.LEFT:
                        ExecuteLeftCommand();
                        break;
                    case Commands.RIGHT:
                        ExecuteRightCommand();
                        break;
                    case Commands.REPORT:
                        ExecuteReportCommand();
                        break;
                    default:
                        Console.WriteLine($"Error: Invalid command: '{readLine}'");
                        break;
                }
            }
            else
            {
                Console.WriteLine($"Error: '{response.Message}'");
            }
        }

        private void ExecutePlaceCommand(CommandLineParser.ParserResponse response)
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

        private void ExecuteMoveCommand()
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

        private void ExecuteLeftCommand()
        {
            if (!this.Robot.IsPlaced)
            {
                Console.WriteLine(ROBOT_NEED_PLACEMENT_MESSAGE);
                return;
            }

            this.Robot.Rotate(-1);
            Console.WriteLine(SUCCESS_MESSAGE);
        }

        private void ExecuteRightCommand()
        {
            if (!this.Robot.IsPlaced)
            {
                Console.WriteLine(ROBOT_NEED_PLACEMENT_MESSAGE);
                return;
            }

            this.Robot.Rotate(1);
            Console.WriteLine(SUCCESS_MESSAGE);
        }

        private void ExecuteReportCommand()
        {
            if (!this.Robot.IsPlaced)
            {
                Console.WriteLine(ROBOT_NEED_PLACEMENT_MESSAGE);
                return;
            }

            Console.WriteLine($"Robot is at ({this.Robot.PositionX},{this.Robot.PositionY}) facing {this.Robot.FacingDirection}");
        }
    }
}
