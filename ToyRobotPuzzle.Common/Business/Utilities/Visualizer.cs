using ToyRobotPuzzle.Common.Business.Initializer;
using ToyRobotPuzzle.Common.Models.Enums;

namespace ToyRobotPuzzle.Common.Business.Utilities
{
    public static class Visualizer
    {
        public static void Visualize(ToyRobotPuzzleLauncher launcher)
        {
            if (launcher.IsRobotPlaced())
            {
                int tableWidth = launcher.TableWidth;
                int tableHeight = launcher.TableHeight;

                int robotPositionX = launcher.RobotPositionX!.Value;
                int robotPositionY = launcher.RobotPositionY!.Value;
                FacingDirection facingDirection = launcher.RobotFacingDirection!.Value;

                List<List<string>> stringRows = new List<List<string>>();

                List<string> stringRow = new List<string>();
                for (int i = 0; i <= tableWidth; i++)
                {
                    stringRow.Add(" ");
                }
                for (int i = 0; i <= tableHeight; i++)
                {
                    stringRows.Add(new List<string>(stringRow));
                }

                string robotIcon = "o";
                switch (facingDirection)
                {
                    case FacingDirection.NORTH:
                        robotIcon = "^";
                        break;
                    case FacingDirection.SOUTH:
                        robotIcon = "v";
                        break;
                    case FacingDirection.WEST:
                        robotIcon = "<";
                        break;
                    case FacingDirection.EAST:
                        robotIcon = ">";
                        break;
                }

                int x = tableHeight - robotPositionY;
                int y = robotPositionX;

                if (stringRows.Count - 1 >= x && stringRows[0].Count - 1 >= y)
                {
                    stringRows[x][y] = robotIcon;

                    Console.WriteLine(" ");
                    foreach (List<string> row in stringRows)
                    {
                        Console.WriteLine($"|{string.Join(" ", row)}|");
                    }
                    Console.WriteLine(" ");
                }
            }
        }
    }
}
