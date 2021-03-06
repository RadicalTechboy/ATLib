﻿using System;
using System.Text.RegularExpressions;

namespace HeboTech.ATLib
{
    public class Pin
    {
        private readonly int pinDigit0;
        private readonly int pinDigit1;
        private readonly int pinDigit2;
        private readonly int pinDigit3;

        public Pin(int pinDigit0, int pinDigit1, int pinDigit2, int pinDigit3)
        {
            ThrowIfInvalid(pinDigit0);
            ThrowIfInvalid(pinDigit1);
            ThrowIfInvalid(pinDigit2);
            ThrowIfInvalid(pinDigit3);

            this.pinDigit0 = pinDigit0;
            this.pinDigit1 = pinDigit1;
            this.pinDigit2 = pinDigit2;
            this.pinDigit3 = pinDigit3;
        }

        public Pin(string pin)
        {
            if (!Regex.IsMatch(pin, @"^[0-9]\d{3}$"))
                throw new ArgumentException("Invalid PIN");
            this.pinDigit0 = (int)Char.GetNumericValue(pin[0]);
            this.pinDigit1 = (int)Char.GetNumericValue(pin[1]);
            this.pinDigit2 = (int)Char.GetNumericValue(pin[2]);
            this.pinDigit3 = (int)Char.GetNumericValue(pin[3]);
        }

        private static void ThrowIfInvalid(int pinDigit)
        {
            if (pinDigit < 0 || pinDigit > 9)
                throw new ArgumentOutOfRangeException("PIN digits must be between 0 and 9");
        }

        public override string ToString()
        {
            return $"{pinDigit0}{pinDigit1}{pinDigit2}{pinDigit3}";
        }
    }
}
