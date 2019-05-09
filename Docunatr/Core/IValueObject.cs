namespace Docunatr.Core
{
    public interface IValueObject<out T>
    {
        T Value { get; }
    }
}