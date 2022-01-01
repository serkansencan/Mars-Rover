using System.ComponentModel;

namespace MarsRover
{
    public class Constants
    {
        public enum Orientations : int
        {
            [Description("N")]
            N = 1,
            [Description("E")]
            E = 2,
            [Description("S")]
            S = 3,
            [Description("W")]
            W = 4
        }

        public struct Command
        {
            public static string L = "L";
            public static string R = "R";
            public static string M = "M";
        }

        public struct Message
        {
            public static string Successful = "İşlem başarılı";
            public static string Fail = "İşlem hatalı";
            public static string Invalid_Value = "Geçersiz değer";
        }
    }
}
