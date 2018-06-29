using System.ComponentModel.DataAnnotations;

namespace HeboTech.ATLib
{
    public enum TypeOfNumber
    {
        Unknown,
        International,
        National,
        //NetworkSpecific,
        //Subscriber,
        //Alphanumeric,
        //Abbreviated
    }

    public class PhoneNumber
    {
        private readonly string number;

        public PhoneNumber(string number)
        {
            (new PhoneAttribute()).Validate(number, nameof(number));

            if (number.StartsWith("+"))
                NumberType = TypeOfNumber.International;
            else
                NumberType = TypeOfNumber.National;
            //TODO: Support all types.

            this.number = number;
        }

        public TypeOfNumber NumberType { get; }

        public override string ToString()
        {
            return number;
        }
    }
}
