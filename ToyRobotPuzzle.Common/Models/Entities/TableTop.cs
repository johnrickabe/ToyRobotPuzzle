
namespace ToyRobotPuzzle.Common.Models.Entities
{
    public class TableTop
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public List<Robot> Robots { get; private set; } = [];

        #region constructors
        public TableTop()
        {
            Width = 5;
            Height = 5;
        }
        #endregion

        public void AddRobot(Robot robot)
        {
            if (!Robots.Contains(robot))
            {
                this.Robots.Add(robot);
            }
            robot.TableTop = this;
        }
    }
}
