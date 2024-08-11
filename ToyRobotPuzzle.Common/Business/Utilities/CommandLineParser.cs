using ToyRobotPuzzle.Common.Models.Enums;

namespace ToyRobotPuzzle.Common.Business.Utilities
{
    public static partial class CommandLineParser
    {
        public static ParserResponse ParseCommand(string readLine)
        {
            var readLineArray = readLine.Split(' ');
            var commandWord = readLineArray[0];
            var isPlace = commandWord == Commands.PLACE.ToString();

            if (isPlace)
            {
                return ParsePlaceCommand(readLineArray);
            }
            else if (Enum.IsDefined(typeof(Commands), commandWord) && !isPlace && commandWord != "NULL")
            {
                return ParseParameterlessCommands(readLineArray);
            }

            ParserResponse response = new()
            {
                Message = $"Invalid command: {commandWord}"
            };
            return response;
        }

       
        private static ParserResponse ParseParameterlessCommands(string[] readLineArray)
        {
            var commandWord = readLineArray[0];

            ParserResponse response = new();

            if (Enum.TryParse<Commands>(commandWord, out var commandEnum))
            {
                if(commandEnum == Commands.PLACE) return response;

                if (readLineArray.Length <= 1)
                {
                    response.Command = commandEnum;
                    response.IsSuccess = true;
                    return response;
                }

                response.Message = $"{commandWord} does not accept any parameter.";
            }

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
