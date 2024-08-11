using ToyRobotPuzzle.Common.Models.Enums;

namespace ToyRobotPuzzle.Common.Business.Utilities
{
    public static partial class CommandLineParser
    {
        public static bool TryParse(string readLine, out ParserResponse response)
        {
            var readLineArray = readLine.Split(' ');
            var commandWord = readLineArray[0];

            response = new ParserResponse();
            if (commandWord == Commands.PLACE.ToString())
            {
                return TryParsePlaceCommand(readLineArray, out response);
            }
            // Parameter-less Commands
            else if (Enum.TryParse<Commands>(commandWord, out var commandWordEnum))
            {
                if (readLineArray.Length > 1)
                {
                    response.Message = $"{commandWord} does not accept any parameter.";
                    return false;
                }

                response.Command = commandWordEnum;
                return true;
            }

            response.Message = $"Invalid command: {commandWord}";
            return false;
        }

        private static bool TryParsePlaceCommand(string[] readLineArray, out ParserResponse response)
        {
            response = new ParserResponse();
            if (readLineArray.Length == 2)
            {
                var parameters = readLineArray[1].Split(',');
                if (parameters.Length == 1)
                {
                    response.Message = "PLACE should have parameters X:int,Y:int,[F:string] ([] = optional after first use)";
                    return false;
                }
                else
                {
                    var stringX = parameters[0];
                    var stringY = parameters[1];

                    if(parameters.Length == 2)
                    {
                        if (int.TryParse(stringX, out var _) && int.TryParse(stringY, out var _))
                        {
                            response.Parameters.AddRange([stringX, stringY]);
                            response.Command = Commands.PLACE;
                            return true;
                        }
                        response.Message = "X and Y are not valid integers.";
                        return false;
                    }

                    var stringF = parameters[2];

                    if (int.TryParse(stringX, out var _) && int.TryParse(stringY, out var _) && Enum.IsDefined(typeof(FacingDirection), stringF))
                    {
                        response.Parameters.AddRange([stringX, stringY, stringF]);
                        response.Command = Commands.PLACE;
                        return true;
                    }
                    response.Message = "One or more parameters is in valid.";
                    return false;

                }
            }
            else if (readLineArray.Length > 2)
            {
                response.Message = "PLACE command should only have one space character.";
                return false;
            }
            else if (readLineArray.Length == 1)
            {
                response.Message = "PLACE should have parameters X:int,Y:int,[F:string] ([] = optional after first use)";
                return false;
            }

            response.Message = "An error has occured.";
            return false;
        }
    }
}
