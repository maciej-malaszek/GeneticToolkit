using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Utils.Extensions
{
    [PublicAPI]
    public static class BitConverterX
    {
        public static bool IsLittleEndian => BitConverter.IsLittleEndian;

        public static long DoubleToInt64Bits(double value)
        {
            return BitConverter.DoubleToInt64Bits(value);
        }
        
        public static byte[] GetBytes(uint value)
        {
            return BitConverter.GetBytes(value);
        }
        
        public static byte[] GetBytes(ushort value)
        {
            return BitConverter.GetBytes(value);
        }
        
        public static byte[] GetBytes(float value)
        {
            return BitConverter.GetBytes(value);
        }
        
        public static byte[] GetBytes(long value)
        {
            return BitConverter.GetBytes(value);
        }
        
        public static byte[] GetBytes(ulong value)
        {
            return BitConverter.GetBytes(value);
        }
        
        public static byte[] GetBytes(short value)
        {
            return BitConverter.GetBytes(value);
        }
        
        public static byte[] GetBytes(double value)
        {
            return BitConverter.GetBytes(value);
        }
        
        public static byte[] GetBytes(char value)
        {
            return BitConverter.GetBytes(value);
        }
        
        public static byte[] GetBytes(bool value)
        {
            return BitConverter.GetBytes(value);
        }
        
        public static byte[] GetBytes(int value)
        {
            return BitConverter.GetBytes(value);
        }
        
        public static float Int32BitsToSingle(int value)
        {
            return BitConverter.Int32BitsToSingle(value);
        }
        
        public static double Int64BitsToDouble(long value)
        {
            return BitConverter.Int64BitsToDouble(value);
        }
        
        public static int SingleToInt32Bits(float value)
        {
            return BitConverter.SingleToInt32Bits(value);
        }

        public static bool ToBoolean(ReadOnlySpan<byte> value)
        {
            return BitConverter.ToBoolean(value);
        }
        
        public static bool ToBoolean(byte[] value, int startIndex)
        {
            return BitConverter.ToBoolean(value, startIndex);
        }
        
        public static char ToChar(byte[] value, int startIndex)
        {
            return BitConverter.ToChar(value, startIndex);
        }

        public static char ToChar(ReadOnlySpan<byte> value)
        {
            return BitConverter.ToChar(value);
        }
        
        public static double ToDouble(byte[] value, int startIndex)
        {
            return BitConverter.ToDouble(value, startIndex);
        }

        public static double ToDouble(ReadOnlySpan<byte> value)
        {
            return BitConverter.ToDouble(value);
        }
        
        public static short ToInt16(byte[] value, int startIndex)
        {
            return BitConverter.ToInt16(value, startIndex);
        }

        public static short ToInt16(ReadOnlySpan<byte> value)
        {
            return BitConverter.ToInt16(value);
        }

        public static int ToInt32(ReadOnlySpan<byte> value)
        {
            return BitConverter.ToInt32(value);
        }
        
        public static int ToInt32(byte[] value, int startIndex)
        {
            return BitConverter.ToInt32(value, startIndex);
        }
        
        public static long ToInt64(byte[] value, int startIndex)
        {
            return BitConverter.ToInt64(value, startIndex);
        }

        public static long ToInt64(ReadOnlySpan<byte> value)
        {
            return BitConverter.ToInt64(value);
        }
        
        public static float ToSingle(byte[] value, int startIndex)
        {
            return BitConverter.ToSingle(value, startIndex);
        }

        public static float ToSingle(ReadOnlySpan<byte> value)
        {
            return BitConverter.ToSingle(value);
        }
        
        public static string ToString(byte[] value)
        {
            return BitConverter.ToString(value);
        }
        
        public static string ToString(byte[] value, int startIndex)
        {
            return BitConverter.ToString(value, startIndex);
        }
        
        public static string ToString(byte[] value, int startIndex, int length)
        {
            return BitConverter.ToString(value, startIndex, length);
        }
        
        public static ushort ToUInt16(byte[] value, int startIndex)
        {
            return BitConverter.ToUInt16(value, startIndex);
        }

        public static ushort ToUInt16(ReadOnlySpan<byte> value)
        {
            return BitConverter.ToUInt16(value);
        }
        
        public static uint ToUInt32(byte[] value, int startIndex)
        {
            return BitConverter.ToUInt32(value, startIndex);
        }

        public static uint ToUInt32(ReadOnlySpan<byte> value)
        {
            return BitConverter.ToUInt32(value);
        }

        public static ulong ToUInt64(ReadOnlySpan<byte> value)
        {
            return BitConverter.ToUInt64(value);
        }
        
        public static ulong ToUInt64(byte[] value, int startIndex)
        {
            return BitConverter.ToUInt64(value, startIndex);
        }

        public static bool TryWriteBytes(Span<byte> destination, bool value)
        {
            return BitConverter.TryWriteBytes(destination, value);
        }

        public static bool TryWriteBytes(Span<byte> destination, char value)
        {
            return BitConverter.TryWriteBytes(destination, value);
        }

        public static bool TryWriteBytes(Span<byte> destination, double value)
        {
            return BitConverter.TryWriteBytes(destination, value);
        }

        public static bool TryWriteBytes(Span<byte> destination, short value)
        {
            return BitConverter.TryWriteBytes(destination, value);
        }

        public static bool TryWriteBytes(Span<byte> destination, int value)
        {
            return BitConverter.TryWriteBytes(destination, value);
        }

        public static bool TryWriteBytes(Span<byte> destination, long value)
        {
            return BitConverter.TryWriteBytes(destination, value);
        }

        public static bool TryWriteBytes(Span<byte> destination, float value)
        {
            return BitConverter.TryWriteBytes(destination, value);
        }

        public static bool TryWriteBytes(Span<byte> destination, ushort value)
        {
            return BitConverter.TryWriteBytes(destination, value);
        }

        public static bool TryWriteBytes(Span<byte> destination, uint value)
        {
            return BitConverter.TryWriteBytes(destination, value);
        }

        public static bool TryWriteBytes(Span<byte> destination, ulong value)
        {
            return BitConverter.TryWriteBytes(destination, value);
        }

        public static byte[] GetBytes<T>(T val)
        {
            switch (val)
            {
                case bool b:
                    return GetBytes(b);
                case char c:
                    return GetBytes(c);
                case short s:
                    return GetBytes(s);
                case int i:
                    return GetBytes(i);
                case long l:
                    return GetBytes(l);
                case float f:
                    return GetBytes(f);
                case double d:
                    return GetBytes(d);
                case ushort us:
                    return GetBytes(us);
                case uint ui:
                    return GetBytes(ui);
                case ulong ul:
                    return GetBytes(ul);
                default:
                    return new byte[0];
            }
        }

        public static T ToValue<T>(byte[] value, int startIndex)
        {
            object returnValue = null;
            var tType = typeof(T);

            if (tType == typeof(bool))
                returnValue = ToBoolean(value, startIndex);

            if (tType == typeof(char))
                returnValue = ToChar(value, startIndex);

            if (tType == typeof(short))
                returnValue = ToInt16(value, startIndex);

            if (tType == typeof(int))
                returnValue = ToInt32(value, startIndex);

            if (tType == typeof(long))
                returnValue = ToInt64(value, startIndex);

            if (tType == typeof(float))
                returnValue = ToSingle(value, startIndex);

            if (tType == typeof(double))
                returnValue = ToDouble(value, startIndex);

            if (tType == typeof(ushort))
                returnValue = ToUInt16(value, startIndex);

            if (tType == typeof(uint))
                returnValue = ToUInt32(value, startIndex);

            if (tType == typeof(ulong))
                returnValue = ToUInt64(value, startIndex);

            return (T) (returnValue == null ? default(T) : Convert.ChangeType(returnValue, tType));
        }

        public static T ToValue<T>(ReadOnlySpan<byte> value)
        {
            object returnValue = null;
            var tType = typeof(T);

            if (tType == typeof(bool))
                returnValue = ToBoolean(value);

            if (tType == typeof(char))
                returnValue = ToChar(value);

            if (tType == typeof(short))
                returnValue = ToInt16(value);

            if (tType == typeof(int))
                returnValue = ToInt32(value);

            if (tType == typeof(long))
                returnValue = ToInt64(value);

            if (tType == typeof(float))
                returnValue = ToSingle(value);

            if (tType == typeof(double))
                returnValue = ToDouble(value);

            if (tType == typeof(ushort))
                returnValue = ToUInt16(value);

            if (tType == typeof(uint))
                returnValue = ToUInt32(value);

            if (tType == typeof(ulong))
                returnValue = ToUInt64(value);

            return (T) (returnValue == null ? default(T) : Convert.ChangeType(returnValue, tType));
        }
    }
}