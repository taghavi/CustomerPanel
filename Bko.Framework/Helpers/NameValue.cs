namespace Framework.Helpers
{
    public class NameValue
    {
        private string _name;
        private string _value;
        private bool _Selected;
        public NameValue(string name, string value,bool Selected)
        {
            _name = name;
            _value = value;
            _Selected = Selected;
        }
        public NameValue(string name, string value)
        {
            _name = name;
            _value = value;
            _Selected = false;
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
        public bool Selected
        {
            get { return _Selected; }
            set { _Selected = value; }
        }
    }
}