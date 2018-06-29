using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeboTech.ATLib
{
    public class PduEncoder
    {
        private static byte messageReference = 0;
        private readonly Encoding encoding;

        private static class TP_MTI
        {
            public const byte SMS_DELIVER_REPORT = 0b0000_0000;
            public const byte SMS_DELIVER = 0b0000_0000;
            public const byte SMS_SUBMIT = 0b0100_0000;
            public const byte SMS_SUBMIT_REPORT = 0b0100_0000;
            public const byte SMS_COMMAND = 0b1000_0000;
            public const byte SMS_STATUS_REPORT = 0b1000_0000;
            public const byte RESERVED = 0b1100_0000;
        }

        private static class TP_RD
        {
            public const byte TRUE = 0b0010_0000;
            public const byte FALSE = 0b0000_0000;
        }

        private static class TP_VPF
        {
            public const byte NOT_PRESENT = 0b0000_0000;
        }

        private static class TP_SRR
        {
            public const byte TRUE = 0b0000_0100;
            public const byte FALSE = 0b0000_0000;
        }

        private static class TP_UDHI
        {
            public const byte TRUE = 0b0000_0010;
            public const byte FALSE = 0b0000_0000;
        }

        private static class TP_RP
        {
            public const byte TRUE = 0b0000_0001;
            public const byte FALSE = 0b0000_0000;
        }

        private static class TON
        {
            public const byte UNKNOWN = 0b0000_0000;
            public const byte INTERNATIONAL_NUMBER = 0b0001_0000;
            public const byte NATIONAL_NUMBER = 0b0010_0000;
            public const byte NETWORK_SPECIFIC_NUMBER = 0b0011_0000;
            public const byte SUBSCRIBER_NUMBER = 0b0100_0000;
            public const byte ALPHANUMBERIC = 0b0101_0000;
            public const byte ABBREVIATED_NUMBER = 0b0110_0000;
        }

        private static class NPI
        {
            public const byte UNKNOWN = 0b0000_0000;
            public const byte ISDN_TELEPHONE = 0b0000_0001;
            public const byte DATA = 0b0000_0011;
            public const byte TELEX = 0b0000_0100;
            public const byte SERVICE_CENTRE_SPECIFIC_1 = 0b0000_0101;
            public const byte SERVICE_CENTRE_SPECIFIC_2 = 0b0000_0110;
            public const byte NATIONAL = 0b0000_1000;
            public const byte PRIVATE = 0b0000_1001;
            public const byte ERMES = 0b0000_1010;
        }

        public PduEncoder(Encoding encoding)
        {
            this.encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
        }

        public string Encode(PhoneNumber phoneNumber, string message)
        {
            List<byte> tpFields = new List<byte>();

            tpFields.Add(TP_MTI.SMS_SUBMIT | TP_RD.FALSE | TP_VPF.NOT_PRESENT | TP_SRR.FALSE | TP_UDHI.FALSE | TP_RP.FALSE);
            tpFields.Add(messageReference++);

            return ByteArrayToString(tpFields);
        }

        private byte TpDa(string number)
        {
            byte EXT = 0b0001_0000;
            if (number.StartsWith("+"))
                return TON.INTERNATIONAL_NUMBER | EXT | NPI.ISDN_TELEPHONE;
            else
                return TON.NATIONAL_NUMBER | EXT | NPI
        }

        private static string ByteArrayToString(IEnumerable<byte> ba)
        {
            StringBuilder hex = new StringBuilder(ba.Count() * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
