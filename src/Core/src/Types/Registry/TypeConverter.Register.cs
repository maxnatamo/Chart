using System.Security.Cryptography;

using static System.Globalization.CultureInfo;
using static System.Globalization.NumberStyles;

namespace Chart.Core
{
    public partial class TypeConverter
    {
        private void RegisterDefaultMappers()
        {
            this.RegisterStringMappers();
            this.RegisterBooleanMappers();

            this.RegisterByteMappers();
            this.RegisterUInt16Mappers();
            this.RegisterUInt32Mappers();
            this.RegisterUInt64Mappers();

            this.RegisterSByteMappers();
            this.RegisterInt16Mappers();
            this.RegisterInt32Mappers();
            this.RegisterInt64Mappers();

            this.RegisterSingleMappers();
            this.RegisterDoubleMappers();
            this.RegisterDecimalMappers();
        }

        private void RegisterStringMappers()
        {
            this.RegisterMapper<String, SByte>(v => SByte.Parse(v, Integer, InvariantCulture));
            this.RegisterMapper<String, Int16>(v => Int16.Parse(v, Integer, InvariantCulture));
            this.RegisterMapper<String, Int32>(v => Int32.Parse(v, Integer, InvariantCulture));
            this.RegisterMapper<String, Int64>(v => Int64.Parse(v, Integer, InvariantCulture));

            this.RegisterMapper<String, Byte>(v => Byte.Parse(v, Integer, InvariantCulture));
            this.RegisterMapper<String, UInt16>(v => UInt16.Parse(v, Integer, InvariantCulture));
            this.RegisterMapper<String, UInt32>(v => UInt32.Parse(v, Integer, InvariantCulture));
            this.RegisterMapper<String, UInt64>(v => UInt64.Parse(v, Integer, InvariantCulture));

            this.RegisterMapper<String, Decimal>(v => Decimal.Parse(v, Float, InvariantCulture));
            this.RegisterMapper<String, Single>(v => Single.Parse(v, Float, InvariantCulture));
            this.RegisterMapper<String, Double>(v => Double.Parse(v, Float, InvariantCulture));
            this.RegisterMapper<String, Boolean>(v => Boolean.Parse(v));
            this.RegisterMapper<String, String>(v => v);
        }

        private void RegisterBooleanMappers()
        {
            this.RegisterMapper<Boolean, SByte>(v => Convert.ToSByte(v));
            this.RegisterMapper<Boolean, Int16>(v => Convert.ToInt16(v));
            this.RegisterMapper<Boolean, Int32>(v => Convert.ToInt32(v));
            this.RegisterMapper<Boolean, Int64>(v => Convert.ToInt64(v));

            this.RegisterMapper<Boolean, Byte>(v => Convert.ToByte(v));
            this.RegisterMapper<Boolean, UInt16>(v => Convert.ToUInt16(v));
            this.RegisterMapper<Boolean, UInt32>(v => Convert.ToUInt32(v));
            this.RegisterMapper<Boolean, UInt64>(v => Convert.ToUInt64(v));

            this.RegisterMapper<Boolean, Decimal>(v => Convert.ToDecimal(v));
            this.RegisterMapper<Boolean, Single>(v => Convert.ToSingle(v));
            this.RegisterMapper<Boolean, Double>(v => Convert.ToDouble(v));
            this.RegisterMapper<Boolean, Boolean>(v => v);
            this.RegisterMapper<Boolean, String>(v => v.ToString(InvariantCulture));
        }

        private void RegisterByteMappers()
        {
            this.RegisterMapper<Byte, SByte>(v => Convert.ToSByte(v));
            this.RegisterMapper<Byte, Int16>(v => Convert.ToInt16(v));
            this.RegisterMapper<Byte, Int32>(v => Convert.ToInt32(v));
            this.RegisterMapper<Byte, Int64>(v => Convert.ToInt64(v));

            this.RegisterMapper<Byte, Byte>(v => v);
            this.RegisterMapper<Byte, UInt16>(v => Convert.ToUInt16(v));
            this.RegisterMapper<Byte, UInt32>(v => Convert.ToUInt32(v));
            this.RegisterMapper<Byte, UInt64>(v => Convert.ToUInt64(v));

            this.RegisterMapper<Byte, Decimal>(v => Convert.ToDecimal(v));
            this.RegisterMapper<Byte, Single>(v => Convert.ToSingle(v));
            this.RegisterMapper<Byte, Double>(v => Convert.ToDouble(v));
            this.RegisterMapper<Byte, Boolean>(v => v != 0);
            this.RegisterMapper<Byte, String>(v => v.ToString(InvariantCulture));
        }

        private void RegisterSByteMappers()
        {
            this.RegisterMapper<SByte, SByte>(v => v);
            this.RegisterMapper<SByte, Int16>(v => Convert.ToInt16(v));
            this.RegisterMapper<SByte, Int32>(v => Convert.ToInt32(v));
            this.RegisterMapper<SByte, Int64>(v => Convert.ToInt64(v));

            this.RegisterMapper<SByte, Byte>(v => Convert.ToByte(v));
            this.RegisterMapper<SByte, UInt16>(v => Convert.ToUInt16(v));
            this.RegisterMapper<SByte, UInt32>(v => Convert.ToUInt32(v));
            this.RegisterMapper<SByte, UInt64>(v => Convert.ToUInt64(v));

            this.RegisterMapper<SByte, Decimal>(v => Convert.ToDecimal(v));
            this.RegisterMapper<SByte, Single>(v => Convert.ToSingle(v));
            this.RegisterMapper<SByte, Double>(v => Convert.ToDouble(v));
            this.RegisterMapper<SByte, Boolean>(v => v != 0);
            this.RegisterMapper<SByte, String>(v => v.ToString(InvariantCulture));
        }

        private void RegisterInt16Mappers()
        {
            this.RegisterMapper<Int16, SByte>(v => Convert.ToSByte(v));
            this.RegisterMapper<Int16, Int16>(v => v);
            this.RegisterMapper<Int16, Int32>(v => Convert.ToInt32(v));
            this.RegisterMapper<Int16, Int64>(v => Convert.ToInt64(v));

            this.RegisterMapper<Int16, Byte>(v => Convert.ToByte(v));
            this.RegisterMapper<Int16, UInt16>(v => Convert.ToUInt16(v));
            this.RegisterMapper<Int16, UInt32>(v => Convert.ToUInt32(v));
            this.RegisterMapper<Int16, UInt64>(v => Convert.ToUInt64(v));

            this.RegisterMapper<Int16, Decimal>(v => Convert.ToDecimal(v));
            this.RegisterMapper<Int16, Single>(v => Convert.ToSingle(v));
            this.RegisterMapper<Int16, Double>(v => Convert.ToDouble(v));
            this.RegisterMapper<Int16, Boolean>(v => v != 0);
            this.RegisterMapper<Int16, String>(v => v.ToString(InvariantCulture));
        }

        private void RegisterUInt16Mappers()
        {
            this.RegisterMapper<UInt16, SByte>(v => Convert.ToSByte(v));
            this.RegisterMapper<UInt16, Int16>(v => Convert.ToInt16(v));
            this.RegisterMapper<UInt16, Int32>(v => Convert.ToInt32(v));
            this.RegisterMapper<UInt16, Int64>(v => Convert.ToInt64(v));

            this.RegisterMapper<UInt16, Byte>(v => Convert.ToByte(v));
            this.RegisterMapper<UInt16, UInt16>(v => v);
            this.RegisterMapper<UInt16, UInt32>(v => Convert.ToUInt32(v));
            this.RegisterMapper<UInt16, UInt64>(v => Convert.ToUInt64(v));

            this.RegisterMapper<UInt16, Decimal>(v => Convert.ToDecimal(v));
            this.RegisterMapper<UInt16, Single>(v => Convert.ToSingle(v));
            this.RegisterMapper<UInt16, Double>(v => Convert.ToDouble(v));
            this.RegisterMapper<UInt16, Boolean>(v => v != 0);
            this.RegisterMapper<UInt16, String>(v => v.ToString(InvariantCulture));
        }

        private void RegisterInt32Mappers()
        {
            this.RegisterMapper<Int32, SByte>(v => Convert.ToSByte(v));
            this.RegisterMapper<Int32, Int16>(v => Convert.ToInt16(v));
            this.RegisterMapper<Int32, Int32>(v => v);
            this.RegisterMapper<Int32, Int64>(v => Convert.ToInt64(v));

            this.RegisterMapper<Int32, Byte>(v => Convert.ToByte(v));
            this.RegisterMapper<Int32, UInt16>(v => Convert.ToUInt16(v));
            this.RegisterMapper<Int32, UInt32>(v => Convert.ToUInt32(v));
            this.RegisterMapper<Int32, UInt64>(v => Convert.ToUInt64(v));

            this.RegisterMapper<Int32, Decimal>(v => Convert.ToDecimal(v));
            this.RegisterMapper<Int32, Single>(v => Convert.ToSingle(v));
            this.RegisterMapper<Int32, Double>(v => Convert.ToDouble(v));
            this.RegisterMapper<Int32, Boolean>(v => v != 0);
            this.RegisterMapper<Int32, String>(v => v.ToString(InvariantCulture));
        }

        private void RegisterUInt32Mappers()
        {
            this.RegisterMapper<UInt32, SByte>(v => Convert.ToSByte(v));
            this.RegisterMapper<UInt32, Int16>(v => Convert.ToInt16(v));
            this.RegisterMapper<UInt32, Int32>(v => Convert.ToInt32(v));
            this.RegisterMapper<UInt32, Int64>(v => Convert.ToInt64(v));

            this.RegisterMapper<UInt32, Byte>(v => Convert.ToByte(v));
            this.RegisterMapper<UInt32, UInt16>(v => Convert.ToUInt16(v));
            this.RegisterMapper<UInt32, UInt32>(v => v);
            this.RegisterMapper<UInt32, UInt64>(v => Convert.ToUInt64(v));

            this.RegisterMapper<UInt32, Decimal>(v => Convert.ToDecimal(v));
            this.RegisterMapper<UInt32, Single>(v => Convert.ToSingle(v));
            this.RegisterMapper<UInt32, Double>(v => Convert.ToDouble(v));
            this.RegisterMapper<UInt32, Boolean>(v => v != 0);
            this.RegisterMapper<UInt32, String>(v => v.ToString(InvariantCulture));
        }

        private void RegisterInt64Mappers()
        {
            this.RegisterMapper<Int64, SByte>(v => Convert.ToSByte(v));
            this.RegisterMapper<Int64, Int16>(v => Convert.ToInt16(v));
            this.RegisterMapper<Int64, Int32>(v => Convert.ToInt32(v));
            this.RegisterMapper<Int64, Int64>(v => v);

            this.RegisterMapper<Int64, Byte>(v => Convert.ToByte(v));
            this.RegisterMapper<Int64, UInt16>(v => Convert.ToUInt16(v));
            this.RegisterMapper<Int64, UInt32>(v => Convert.ToUInt32(v));
            this.RegisterMapper<Int64, UInt64>(v => Convert.ToUInt64(v));

            this.RegisterMapper<Int64, Decimal>(v => Convert.ToDecimal(v));
            this.RegisterMapper<Int64, Single>(v => Convert.ToSingle(v));
            this.RegisterMapper<Int64, Double>(v => Convert.ToDouble(v));
            this.RegisterMapper<Int64, Boolean>(v => v != 0);
            this.RegisterMapper<Int64, String>(v => v.ToString(InvariantCulture));
        }

        private void RegisterUInt64Mappers()
        {
            this.RegisterMapper<UInt64, SByte>(v => Convert.ToSByte(v));
            this.RegisterMapper<UInt64, Int16>(v => Convert.ToInt16(v));
            this.RegisterMapper<UInt64, Int32>(v => Convert.ToInt32(v));
            this.RegisterMapper<UInt64, Int64>(v => Convert.ToInt64(v));

            this.RegisterMapper<UInt64, Byte>(v => Convert.ToByte(v));
            this.RegisterMapper<UInt64, UInt16>(v => Convert.ToUInt16(v));
            this.RegisterMapper<UInt64, UInt32>(v => Convert.ToUInt32(v));
            this.RegisterMapper<UInt64, UInt64>(v => v);

            this.RegisterMapper<UInt64, Decimal>(v => Convert.ToDecimal(v));
            this.RegisterMapper<UInt64, Single>(v => Convert.ToSingle(v));
            this.RegisterMapper<UInt64, Double>(v => Convert.ToDouble(v));
            this.RegisterMapper<UInt64, Boolean>(v => v != 0);
            this.RegisterMapper<UInt64, String>(v => v.ToString(InvariantCulture));
        }

        private void RegisterSingleMappers()
        {
            this.RegisterMapper<Single, SByte>(v => Convert.ToSByte(v));
            this.RegisterMapper<Single, Int16>(v => Convert.ToInt16(v));
            this.RegisterMapper<Single, Int32>(v => Convert.ToInt32(v));
            this.RegisterMapper<Single, Int64>(v => Convert.ToInt64(v));

            this.RegisterMapper<Single, Byte>(v => Convert.ToByte(v));
            this.RegisterMapper<Single, UInt16>(v => Convert.ToUInt16(v));
            this.RegisterMapper<Single, UInt32>(v => Convert.ToUInt32(v));
            this.RegisterMapper<Single, UInt64>(v => Convert.ToUInt64(v));

            this.RegisterMapper<Single, Decimal>(v => Convert.ToDecimal(v));
            this.RegisterMapper<Single, Single>(v => v);
            this.RegisterMapper<Single, Double>(v => Convert.ToDouble(v));
            this.RegisterMapper<Single, String>(v => v.ToString(InvariantCulture));
        }

        private void RegisterDoubleMappers()
        {
            this.RegisterMapper<Double, SByte>(v => Convert.ToSByte(v));
            this.RegisterMapper<Double, Int16>(v => Convert.ToInt16(v));
            this.RegisterMapper<Double, Int32>(v => Convert.ToInt32(v));
            this.RegisterMapper<Double, Int64>(v => Convert.ToInt64(v));

            this.RegisterMapper<Double, Byte>(v => Convert.ToByte(v));
            this.RegisterMapper<Double, UInt16>(v => Convert.ToUInt16(v));
            this.RegisterMapper<Double, UInt32>(v => Convert.ToUInt32(v));
            this.RegisterMapper<Double, UInt64>(v => Convert.ToUInt64(v));

            this.RegisterMapper<Double, Decimal>(v => Convert.ToDecimal(v));
            this.RegisterMapper<Double, Single>(v => Convert.ToSingle(v));
            this.RegisterMapper<Double, Double>(v => v);
            this.RegisterMapper<Double, String>(v => v.ToString(InvariantCulture));
        }

        private void RegisterDecimalMappers()
        {
            this.RegisterMapper<Decimal, SByte>(v => Convert.ToSByte(v));
            this.RegisterMapper<Decimal, Int16>(v => Convert.ToInt16(v));
            this.RegisterMapper<Decimal, Int32>(v => Convert.ToInt32(v));
            this.RegisterMapper<Decimal, Int64>(v => Convert.ToInt64(v));

            this.RegisterMapper<Decimal, Byte>(v => Convert.ToByte(v));
            this.RegisterMapper<Decimal, UInt16>(v => Convert.ToUInt16(v));
            this.RegisterMapper<Decimal, UInt32>(v => Convert.ToUInt32(v));
            this.RegisterMapper<Decimal, UInt64>(v => Convert.ToUInt64(v));

            this.RegisterMapper<Decimal, Decimal>(v => v);
            this.RegisterMapper<Decimal, Single>(v => Convert.ToSingle(v));
            this.RegisterMapper<Decimal, Double>(v => Convert.ToDouble(v));
            this.RegisterMapper<Decimal, String>(v => v.ToString(InvariantCulture));
        }
    }
}