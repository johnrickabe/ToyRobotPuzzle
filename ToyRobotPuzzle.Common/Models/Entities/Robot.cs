using ToyRobotPuzzle.Common.Models.Enums;
using Direction = ToyRobotPuzzle.Common.Models.Enums.FacingDirection;

namespace ToyRobotPuzzle.Common.Models.Entities
{
    public class Robot
    {
        private int _positionX;
        private int _positionY;
        private Direction _facingDirection;

        public int? PositionX 
        { 
            get 
            { 
                return IsPlaced ? _positionX : null;
            }
        }

        public int? PositionY
        {
            get
            {
                return IsPlaced ? _positionY : null;
            }
        }

        public FacingDirection? FacingDirection
        {
            get
            {
                return IsPlaced ? _facingDirection : null;
            }
        }

        public TableTop TableTop { get; internal set; } = default!;
        public bool IsPlaced { get; private set; } = false;

        public Robot(TableTop tableTop)
        {
            tableTop.AddRobot(this);
        }

        /// <summary>Returns true if the method succeeded. Otherwise, false.</summary> ///
        public bool Place(int x, int y)
        {
            if (IsPlaced)
            {
                return Place(x, y, _facingDirection);
            }

            return false;
        }

        /// <summary>Returns true if the method succeeded. Otherwise, false.</summary> ///
        public bool Place(int x, int y, FacingDirection facingDirection)
        {
            /* Assumption: Robot can stand at one point on the edges and corners */
            if (x <= TableTop.Width && x >= 0 && y <= TableTop.Height && y >= 0)
            {
                _positionX = x;
                _positionY = y;
                _facingDirection = facingDirection;

                if (!IsPlaced) IsPlaced = true;
                return true;
            }
            return false;
        }

        /// <summary>Returns true if the method succeeded. Otherwise, false.</summary> ///
        public bool MoveForward(int units = 1)
        {
            if (IsPlaced)
            {
                switch (_facingDirection)
                {
                    case Direction.NORTH:
                        return MoveY(units);
                    case Direction.EAST:
                        return MoveX(units);
                    case Direction.SOUTH:
                        return MoveY(-units);
                    case Direction.WEST:
                        return MoveX(-units);
                }
            }
            return false;
        }

        /// <summary>Returns true if the method succeeded. Otherwise, false.</summary> ///
        public bool MoveX(int units)
        {
            if (IsPlaced)
            {
                int targetX = _positionX + units;
                if (targetX <= TableTop.Width && targetX >= 0)
                {
                    _positionX = targetX;
                    return true;
                }
            }
            return false;
        }

        /// <summary>Returns true if the method succeeded. Otherwise, false.</summary> ///
        public bool MoveY(int units)
        {
            if (IsPlaced)
            {
                int targetY = _positionY + units;
                if (targetY <= TableTop.Height && targetY >= 0)
                {
                    _positionY = targetY;
                    return true;
                }
            }

            return false;
        }

        /// <summary>Returns true if the method succeeded. Otherwise, false.</summary> ///
        public bool Rotate(int quarterClockWise)
        {
            if (IsPlaced)
            {
                var targetDirection = (int)_facingDirection + (quarterClockWise % 4);
                if (targetDirection < 0)
                {
                    targetDirection += 4;
                }
                else if (targetDirection > 3)
                {
                    targetDirection -= 4;
                }

                _facingDirection = (FacingDirection)targetDirection;
                return true;
            }
            return false;
        }
    }
}
