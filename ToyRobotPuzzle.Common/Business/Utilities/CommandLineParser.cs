using ToyRobotPuzzle.Common.Models.Enums;

namespace ToyRobotPuzzle.Common.Business.Utilities
{
    public static partial class CommandLineParser
    {
        private static readonly List<string> _parameterLessCommands = [Commands.MOVE.ToString(), Commands.LEFT.ToString(), Commands.RIGHT.ToString(), Commands.REPORT.ToString(), Commands.EXIT.ToString()];

        public static ParserResponse ParseCommand(string readLine)
        {
            var readLineArray = readLine.Split(' ');
            var commandWord = readLineArray[0];
            var response = new ParserResponse();

            if (commandWord == Commands.PLACE.ToString())
            {
                return ParsePlaceCommand(readLineArray);
            }
            else if (_parameterLessCommands.Contains(commandWord) && commandWord != "NULL")
            {
                if (readLineArray.Length <= 1)
                {
                    return ParseParameterlessCommand(commandWord);
                }

                response.Message = $"{commandWord} does not accept any parameter.";
            }
            else
            {
                response.Message = $"Invalid command: {commandWord}";
            }

            return response;
        }


        private static ParserResponse ParseParameterlessCommand(string commandWord)
        {
            ParserResponse response = new();

            if (Enum.TryParse<Commands>(commandWord, out var commandEnum))
            {
                response.Command = commandEnum;
                response.IsSuccess = true;
                return response;
            }

            response.Message = $"Could not parse command '{commandWord}'.";
            return response;
        }

        private static ParserResponse ParsePlaceCommand(string[] readLineArray)
        {
            ParserResponse response = new();

            if (readLineArray.Length == 2)
            {
                var parameters = readLineArray[1].Split(',');
                if (parameters.Length == 1)
                {
                    response.Message = "PLACE should have parameters X:int,Y:int,[F:string] ([] = optional after first use)";
                    return response;
                }

                var stringX = parameters[0];
                var stringY = parameters[1];

                if (int.TryParse(stringX, out var _) && int.TryParse(stringY, out var _))
                {
                    response.Parameters.AddRange([stringX, stringY]);
                    response.Command = Commands.PLACE;

                    if (parameters.Length == 3)
                    {
                        var stringF = parameters[2];
                        if (!Enum.IsDefined(typeof(FacingDirection), stringF))
                        {
                            response.Message = $"{stringF} is not a valid direction.";
                            return response;
                        }
                        response.Parameters.Add(stringF);
                    }

                    response.IsSuccess = true;
                    return response;
                }

                response.Message = $"{stringX} and/or {stringY} is/are not valid integers.";
                return response;
            }
            else if (readLineArray.Length > 2)
            {
                response.Message = "PLACE command should only have one space character.";
            }
            else if (readLineArray.Length == 1)
            {
                response.Message = "PLACE should have parameters X:int,Y:int,[F:string] ([] = optional after first use)";
            }
            else
            {
                response.Message = "An error has occured.";
            }

            return response;
        }
    }
}
