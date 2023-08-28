namespace Chart.Core
{
    public class ScalarTypes
    {
        public static string Byte => nameof(Byte);
        public static string Short => nameof(Short);
        public static string Int => nameof(Int);
        public static string Long => nameof(Long);
        public static string Single => nameof(Single);
        public static string Float => nameof(Float);
        public static string Decimal => nameof(Decimal);
        public static string String => nameof(String);
        public static string Boolean => nameof(Boolean);
        public static string ID => nameof(ID);

        private static readonly List<string> _defaultScalars = new()
        {
            String,
            Boolean,
            Float,
            ID,
            Int
        };

        private static readonly List<string> _nativeScalars = new()
        {
            typeof(Char).Name,
            typeof(String).Name,
            typeof(Boolean).Name,

            typeof(Decimal).Name,
            typeof(Double).Name,
            typeof(Single).Name,

            typeof(Byte).Name,
            typeof(SByte).Name,
            typeof(Int16).Name,
            typeof(UInt16).Name,
            typeof(Int32).Name,
            typeof(UInt32).Name,
            typeof(Int64).Name,
            typeof(UInt64).Name,
            typeof(IntPtr).Name,
            typeof(UIntPtr).Name,
        };

        public static bool IsDefaultScalar(string name)
            => ScalarTypes._defaultScalars.Contains(name);

        public static bool IsNativeScalar(string name)
            => ScalarTypes._nativeScalars.Contains(name);
    }
}