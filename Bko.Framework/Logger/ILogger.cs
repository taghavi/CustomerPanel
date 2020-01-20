using System.Text;

namespace Framework.Logger
{
    public interface ILogger
    {
        void Log(StringBuilder description);
    }
}
