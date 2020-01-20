using System.Text;

namespace Business
{
    public class StringOutputWriter : IOutputWriter
    {
        private StringBuilder _builder;

        public StringOutputWriter()
        {
            _builder = new StringBuilder();
        }

        public object Output
        {
            get
            {
                return _builder.ToString();
            }

        }

        public void Write(object value)
        {
            _builder.AppendLine(value.ToString());
        }
    }
}
