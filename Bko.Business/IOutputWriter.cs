namespace Business
{
    public interface IOutputWriter
    {
        object Output{ get; }
        void Write(object o);
    }
}
