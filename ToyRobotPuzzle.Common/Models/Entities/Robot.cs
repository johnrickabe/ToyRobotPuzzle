using ToyRobotPuzzle.Common.Models.Enums;

namespace ToyRobotPuzzle.Common.Models.Entities
{
    public class Robot
    {
        public int? PositionX { get; private set; } = null;
        public int? PositionY { get; private set; } = null;
        public FacingDirection? FacingDirection { get; private set; } = null;

        public TableTop TableTop { get; internal set; } = default!;
        public bool IsPlaced { get; private set; } = false;

        public Robot(TableTop tableTop)
        {
            tableTop.AddRobot(this);
        }

        public void MoveToTable(TableTop tableTop)
        {
            this.TableTop.MoveRobotToAnotherTable(this, tableTop);
        }

        /// <returns>Returns true if the method succeeded. Otherwise, false.</returns> ///
        public bool Place(int x, int y)
        {
            if (IsPlaced)
            {
                return Place(x, y, FacingDirection!.Value);
            }

            return false;
        }

        /// <returns>Returns true if the method succeeded. Otherwise, false.</returns> ///
        public bool Place(int x, int y, FacingDirection facingDirection)
        {
            /* Assumption: Robot can stand at one point on the edges and corners */
            if (x <= TableTop.Width && x >= 0 && y <= TableTop.Height && y >= 0)
            {
                PositionX = x;
                PositionY = y;
                FacingDirection = facingDirection;

                if (!IsPlaced) IsPlaced = true;
                return true;
            }
            return false;
        }

        /// <returns>Returns true if the method succeeded. Otherwise, false.</returns> ///
        public bool MoveForward(int units = 1)
        {
            switch (FacingDirection)
            {
                case Enums.FacingDirection.NORTH:
                    return MoveY(units);
                case Enums.FacingDirection.EAST:
                    return MoveX(units);
                case Enums.FacingDirection.SOUTH:
                    return MoveY(-units);
                case Enums.FacingDirection.WEST:
                    return MoveX(-units);
                default:
                    break;
            }
            return false;
        }

        /// <returns>Returns true if the method succeeded. Otherwise, false.</returns> ///
        public bool MoveX(int units)
        {
            if (IsPlaced)
            {
                int targetX = PositionX!.Value + units;
                if (targetX <= TableTop.Width && targetX >= 0)
                {
                    PositionX = targetX;
                    return true;
                }
            }
            return false;
        }

        /// <returns>Returns true if the method succeeded. Otherwise, false.</returns> ///
        public bool MoveY(int units)
        {
            if (IsPlaced)
            {
                int targetY = PositionY!.Value + units;
                if (targetY <= TableTop.Height && targetY >= 0)
                {
                    PositionY = targetY;
                    return true;
                }
            }

            return false;
        }

        /// <returns>Returns true if the method succeeded. Otherwise, false.</returns> ///
        public bool Rotate(int quarterClockWise)
        {
            if (IsPlaced)
            {
                var targetDirection = (int)FacingDirection!.Value + (quarterClockWise % 4);
                if (targetDirection < 0)
                {
                    targetDirection += 4;
                }
                else if (targetDirection > 3)
                {
                    targetDirection -= 4;
                }

                FacingDirection = (FacingDirection)targetDirection;
                return true;
            }
            return false;
        }
    }
}
