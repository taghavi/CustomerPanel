
using System;

namespace Framework.Logger
{
    public class LogHelper
    {
        public static string NewLine()
        {
            return Environment.NewLine + "\t";
        }

        public static string NewLine(string name, object val)
        {
            return NewLine() + name + "\t" + val;
        }
        public static string Description(string description)
        {
            return NewLine("Desc", description);
        }
        public static string Error(string error)
        {
            return NewLine("Err", error);
        }
    }

}
