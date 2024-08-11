using ToyRobotPuzzle.Common.Models.Enums;

namespace ToyRobotPuzzle.Common.Business.Utilities
{
    public static partial class CommandLineParser
    {
        public static bool TryParse(string readLine, out ParserResponse response)
        {
            var readLineArray = readLine.Split(' ');
            var commandWord = readLineArray[0];

            if (commandWord == Commands.PLACE.ToString())
            {
                return TryParsePlaceCommand(readLineArray, out response);
            }
            else if (TryParseParameterlessCommands(readLineArray, out response))
            {
                return true;
            }

            response.Message = $"Invalid command: {commandWord}";
            return false;
        }

        public static bool TryParseParameterlessCommands(string[] readLineArray, out ParserResponse response)
        {
            var commandWord = readLineArray[0];
            response = new ParserResponse();

            if (Enum.TryParse<Commands>(commandWord, out var commandEnum))
            {
                if (readLineArray.Length <= 1)
                {
                    response.Command = commandEnum;
                    return true;
                }

                response.Message = $"{commandWord} does not accept any parameter.";
            }

            return false;
        }

        private static bool TryParsePlaceCommand(string[] readLineArray, out ParserResponse response)
        {
            response = new ParserResponse();
            if (readLineArray[0] != Commands.PLACE.ToString()) return false;
            if (readLineArray.Length == 2)
            {
                var parameters = readLineArray[1].Split(',');
                if (parameters.Length == 1)
                {
                    response.Message = "PLACE should have parameters X:int,Y:int,[F:string] ([] = optional after first use)";
                    return false;
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
                            return false;
                        }
                        response.Parameters.Add(stringF);
                    }

                    return true;
                }

                response.Message = $"{stringX} and/or {stringY} is/are not valid integers.";
                return false;
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

            return false;
        }
    }
}
