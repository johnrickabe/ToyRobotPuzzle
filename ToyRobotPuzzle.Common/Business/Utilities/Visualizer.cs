using ToyRobotPuzzle.Common.Business.Initializer;

namespace ToyRobotPuzzle.Common.Business.Utilities
{
    public static class Visualizer
    {
        public static void Visualize(ToyRobotPuzzleApp launcher)
        {
            if (launcher.IsRobotPlaced())
            {
                var stringRows = Visualizer.CreateStringMatrix(launcher.TableWidth, launcher.TableHeight);

                int x = launcher.TableHeight - launcher.RobotPositionY!.Value;
                int y = launcher.RobotPositionX!.Value;

                if (stringRows.Count - 1 >= x && stringRows[0].Count - 1 >= y)
                {
                    stringRows[x][y] = (new string[] { "▲", "►", "▼", "◄" })[(int)launcher.RobotFacingDirection!.Value];

                    Console.WriteLine(" ");
                    foreach (List<string> row in stringRows)
                    {
                        Console.WriteLine($"{string.Join(" ", row)}");
                    }
                    Console.WriteLine(" ");
                }
            }
        }

        private static List<List<string>> CreateStringMatrix(int width, int height)
        {
            List<List<string>> stringRows = [];
            List<string> stringRow = [];

            for (int i = 0; i <= width; i++)
            {
                stringRow.Add("+");
            }
            for (int i = 0; i <= height; i++)
            {
                stringRows.Add(new List<string>(stringRow));
            }

            return stringRows;
        }
    }
}
