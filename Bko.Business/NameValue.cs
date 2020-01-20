namespace Business
{
    public class NameValue
    {
        private string _name;
        private string _value;

        public NameValue(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public NameValue(string name, int value)
        {
            _name = name;
            _value = value.ToString();
        }

        public NameValue(int name, int value)
        {
            _name = name.ToString();
            _value = value.ToString();
        }
        public NameValue(int name, string value)
        {
            _name = name.ToString();
            _value = value;
        }
        public NameValue(int name, int? value)
        {
            _name = name.ToString();
            _value = value.ToString();
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}