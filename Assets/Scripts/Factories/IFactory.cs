namespace TD.Factories
{
    public interface IFactory<T> where T: class
    {
        T Create();
    }
}
