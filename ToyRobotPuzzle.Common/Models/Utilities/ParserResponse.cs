using ToyRobotPuzzle.Common.Models.Enums;

namespace ToyRobotPuzzle.Common.Business.Utilities
{
    public static partial class CommandLineParser
    {
        public class ParserResponse
        {
            public bool IsSuccess { get; set; } = false;
            public Commands Command { get; set; } = Commands.NULL;
            public List<string> Parameters { get; set; } = [];
            public string Message { get; set; } = string.Empty;
        }
    }
}
