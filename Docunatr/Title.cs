using Docunatr.Core;

namespace Docunatr
{
    public class Title : ValueObject<string>
    {
        public Title(string value) 
            : base(value)
        {
        }

        public static implicit operator Title(string value)
        {
            return new Title(value);
        }
    }
}