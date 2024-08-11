using ToyRobotPuzzle.Common.Models.Enums;

namespace ToyRobotPuzzle.Common.Business.Utilities
{
    public static class CommandLineParser
    {
        public static bool TryParse(string readLine, out CommandLineParserResponse response)
        {
            response = new CommandLineParserResponse();

            var readLineArray = readLine.Split(' ');
            var commandWord = readLineArray[0];

            #region EXIT
            if (commandWord == Commands.EXIT.ToString())
            {
                if (readLineArray.Length > 1)
                {
                    response.Message = "EXIT does not accept any parameter.";
                    return false;
                }

                response.Command = Commands.EXIT;
                return true;
            }
            #endregion

            #region PLACE
            else if (commandWord == Commands.PLACE.ToString())
            {
                if (readLineArray.Length > 2)
                {
                    response.Message = "PLACE command should only have one space character.";
                    return false;
                }
                else if (readLineArray.Length == 1)
                {
                    response.Message = "PLACE should have parameters X:int,Y:int,[F:string] ([] = optional after first use)";
                    return false;
                }
                else if (readLineArray.Length == 2)
                {
                    var parameters = readLineArray[1].Split(',');
                    if (parameters.Length == 1)
                    {
                        response.Message = "PLACE should have parameters X:int,Y:int,[F:string] ([] = optional after first use)";
                        return false;
                    }
                    else if (parameters.Length == 2)
                    {
                        var stringX = parameters[0];
                        var stringY = parameters[1];
                        if (int.TryParse(stringX, out var x) && int.TryParse(stringY, out var y))
                        {
                            response.Parameters.Add(stringX);
                            response.Parameters.Add(stringY);
                            response.Command = Commands.PLACE;
                            return true;
                        }
                        else
                        {
                            response.Message = "X and Y are not valid integers.";
                            return false;
                        }
                    }
                    else if (parameters.Length == 3)
                    {
                        var stringX = parameters[0];
                        var stringY = parameters[1];
                        var stringF = parameters[2];
                        if (int.TryParse(stringX, out var x) && int.TryParse(stringY, out var y) && Enum.IsDefined(typeof(FacingDirection), stringF))
                        {
                            response.Parameters.Add(stringX);
                            response.Parameters.Add(stringY);
                            response.Parameters.Add(stringF);
                            response.Command = Commands.PLACE;
                            return true;
                        }
                        else
                        {
                            response.Message = "One or more parameters is in valid.";
                            return false;
                        }
                    }
                }

                response.Message = "An error has occured.";
                return false;
            }
            #endregion

            #region MOVE
            if (commandWord == Commands.MOVE.ToString())
            {
                if (readLineArray.Length > 1)
                {
                    response.Message = "MOVE does not accept any parameter.";
                    return false;
                }

                response.Command = Commands.MOVE;
                return true;
            }
            #endregion

            #region LEFT
            if (commandWord == Commands.LEFT.ToString())
            {
                if (readLineArray.Length > 1)
                {
                    response.Message = "LEFT does not accept any parameter.";
                    return false;
                }

                response.Command = Commands.LEFT;
                return true;
            }
            #endregion

            #region RIGHT
            if (commandWord == Commands.RIGHT.ToString())
            {
                if (readLineArray.Length > 1)
                {
                    response.Message = "RIGHT does not accept any parameter.";
                    return false;
                }

                response.Command = Commands.RIGHT;
                return true;
            }
            #endregion

            #region REPORT
            if (commandWord == Commands.REPORT.ToString())
            {
                if (readLineArray.Length > 1)
                {
                    response.Message = "REPORT does not accept any parameter.";
                    return false;
                }

                response.Command = Commands.REPORT;
                return true;
            }
            #endregion

            response.Message = $"Invalid command: {commandWord}";
            return false;
        }

        public class CommandLineParserResponse
        {
            public Commands Command { get; set; } = Commands.NULL;
            public List<string> Parameters { get; set; } = [];
            public string Message { get; set; } = string.Empty;
        }
    }
}
