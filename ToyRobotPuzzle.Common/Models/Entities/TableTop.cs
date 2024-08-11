
namespace ToyRobotPuzzle.Common.Models.Entities
{
    public class TableTop
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public List<Robot> Robots { get; private set; } = new List<Robot>();

        #region constructors
        public TableTop()
        {
            Width = 5;
            Height = 5;
        }

        public TableTop(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public TableTop(int side)
        {
            Width = side;
            Height = side;
        }
        #endregion

        public void AddRobot(Robot robot)
        {
            if (!Robots.Contains(robot))
            {
                this.Robots.Add(robot);
                robot.TableTop = this;
            }
        }

        public void MoveRobotToAnotherTable(Robot robot, TableTop targetTableTop)
        {
            if (!Robots.Contains(robot))
            {
                return;
            }

            this.Robots.Remove(robot);
            targetTableTop.AddRobot(robot);
            robot.TableTop = targetTableTop;
        }
    }
}
