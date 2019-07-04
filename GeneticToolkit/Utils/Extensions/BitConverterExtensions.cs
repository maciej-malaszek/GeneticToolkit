using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Utils.Extensions
{
    [PublicAPI]
    public static class BitConverterX
    {
        //
        // Summary:
        //     Indicates the byte order (&quot;endianness&quot;) in which data is stored in
        //     this computer architecture.
        public static bool IsLittleEndian => BitConverter.IsLittleEndian;

        //
        // Summary:
        //     Converts the specified double-precision floating point number to a 64-bit signed
        //     integer.
        //
        // Parameters:
        //   value:
        //     The number to convert.
        //
        // Returns:
        //     A 64-bit signed integer whose value is equivalent to value.
        public static long DoubleToInt64Bits(double value)
        {
            return BitConverter.DoubleToInt64Bits(value);
        }

        //
        // Summary:
        //     Returns the specified 32-bit unsigned integer value as an array of bytes.
        //
        // Parameters:
        //   value:
        //     The number to convert.
        //
        // Returns:
        //     An array of bytes with length 4.
        public static byte[] GetBytes(uint value)
        {
            return BitConverter.GetBytes(value);
        }

        //
        // Summary:
        //     Returns the specified 16-bit unsigned integer value as an array of bytes.
        //
        // Parameters:
        //   value:
        //     The number to convert.
        //
        // Returns:
        //     An array of bytes with length 2.
        public static byte[] GetBytes(ushort value)
        {
            return BitConverter.GetBytes(value);
        }

        //
        // Summary:
        //     Returns the specified single-precision floating point value as an array of bytes.
        //
        // Parameters:
        //   value:
        //     The number to convert.
        //
        // Returns:
        //     An array of bytes with length 4.
        public static byte[] GetBytes(float value)
        {
            return BitConverter.GetBytes(value);
        }

        //
        // Summary:
        //     Returns the specified 64-bit signed integer value as an array of bytes.
        //
        // Parameters:
        //   value:
        //     The number to convert.
        //
        // Returns:
        //     An array of bytes with length 8.
        public static byte[] GetBytes(long value)
        {
            return BitConverter.GetBytes(value);
        }

        //
        // Summary:
        //     Returns the specified 64-bit unsigned integer value as an array of bytes.
        //
        // Parameters:
        //   value:
        //     The number to convert.
        //
        // Returns:
        //     An array of bytes with length 8.
        public static byte[] GetBytes(ulong value)
        {
            return BitConverter.GetBytes(value);
        }

        //
        // Summary:
        //     Returns the specified 16-bit signed integer value as an array of bytes.
        //
        // Parameters:
        //   value:
        //     The number to convert.
        //
        // Returns:
        //     An array of bytes with length 2.
        public static byte[] GetBytes(short value)
        {
            return BitConverter.GetBytes(value);
        }

        //
        // Summary:
        //     Returns the specified double-precision floating point value as an array of bytes.
        //
        // Parameters:
        //   value:
        //     The number to convert.
        //
        // Returns:
        //     An array of bytes with length 8.
        public static byte[] GetBytes(double value)
        {
            return BitConverter.GetBytes(value);
        }

        //
        // Summary:
        //     Returns the specified Unicode character value as an array of bytes.
        //
        // Parameters:
        //   value:
        //     A character to convert.
        //
        // Returns:
        //     An array of bytes with length 2.
        public static byte[] GetBytes(char value)
        {
            return BitConverter.GetBytes(value);
        }

        //
        // Summary:
        //     Returns the specified Boolean value as a byte array.
        //
        // Parameters:
        //   value:
        //     A Boolean value.
        //
        // Returns:
        //     A byte array with length 1.
        public static byte[] GetBytes(bool value)
        {
            return BitConverter.GetBytes(value);
        }

        //
        // Summary:
        //     Returns the specified 32-bit signed integer value as an array of bytes.
        //
        // Parameters:
        //   value:
        //     The number to convert.
        //
        // Returns:
        //     An array of bytes with length 4.
        public static byte[] GetBytes(int value)
        {
            return BitConverter.GetBytes(value);
        }

        //
        // Parameters:
        //   value:
        public static float Int32BitsToSingle(int value)
        {
            return BitConverter.Int32BitsToSingle(value);
        }

        //
        // Summary:
        //     Converts the specified 64-bit signed integer to a double-precision floating point
        //     number.
        //
        // Parameters:
        //   value:
        //     The number to convert.
        //
        // Returns:
        //     A double-precision floating point number whose value is equivalent to value.
        public static double Int64BitsToDouble(long value)
        {
            return BitConverter.Int64BitsToDouble(value);
        }

        //
        // Parameters:
        //   value:
        public static int SingleToInt32Bits(float value)
        {
            return BitConverter.SingleToInt32Bits(value);
        }

        public static bool ToBoolean(ReadOnlySpan<byte> value)
        {
            return BitConverter.ToBoolean(value);
        }

        //
        // Summary:
        //     Returns a Boolean value converted from the byte at a specified position in a
        //     byte array.
        //
        // Parameters:
        //   value:
        //     A byte array.
        //
        //   startIndex:
        //     The index of the byte within value.
        //
        // Returns:
        //     true if the byte at startIndex in value is nonzero; otherwise, false.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     value is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     startIndex is less than zero or greater than the length of value minus 1.
        public static bool ToBoolean(byte[] value, int startIndex)
        {
            return BitConverter.ToBoolean(value, startIndex);
        }

        //
        // Summary:
        //     Returns a Unicode character converted from two bytes at a specified position
        //     in a byte array.
        //
        // Parameters:
        //   value:
        //     An array.
        //
        //   startIndex:
        //     The starting position within value.
        //
        // Returns:
        //     A character formed by two bytes beginning at startIndex.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     startIndex equals the length of value minus 1.
        //
        //   T:System.ArgumentNullException:
        //     value is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     startIndex is less than zero or greater than the length of value minus 1.
        public static char ToChar(byte[] value, int startIndex)
        {
            return BitConverter.ToChar(value, startIndex);
        }

        public static char ToChar(ReadOnlySpan<byte> value)
        {
            return BitConverter.ToChar(value);
        }

        //
        // Summary:
        //     Returns a double-precision floating point number converted from eight bytes at
        //     a specified position in a byte array.
        //
        // Parameters:
        //   value:
        //     An array of bytes.
        //
        //   startIndex:
        //     The starting position within value.
        //
        // Returns:
        //     A double precision floating point number formed by eight bytes beginning at startIndex.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     startIndex is greater than or equal to the length of value minus 7, and is less
        //     than or equal to the length of value minus 1.
        //
        //   T:System.ArgumentNullException:
        //     value is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     startIndex is less than zero or greater than the length of value minus 1.
        public static double ToDouble(byte[] value, int startIndex)
        {
            return BitConverter.ToDouble(value, startIndex);
        }

        public static double ToDouble(ReadOnlySpan<byte> value)
        {
            return BitConverter.ToDouble(value);
        }

        //
        // Summary:
        //     Returns a 16-bit signed integer converted from two bytes at a specified position
        //     in a byte array.
        //
        // Parameters:
        //   value:
        //     An array of bytes.
        //
        //   startIndex:
        //     The starting position within value.
        //
        // Returns:
        //     A 16-bit signed integer formed by two bytes beginning at startIndex.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     startIndex equals the length of value minus 1.
        //
        //   T:System.ArgumentNullException:
        //     value is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     startIndex is less than zero or greater than the length of value minus 1.
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

        //
        // Summary:
        //     Returns a 32-bit signed integer converted from four bytes at a specified position
        //     in a byte array.
        //
        // Parameters:
        //   value:
        //     An array of bytes.
        //
        //   startIndex:
        //     The starting position within value.
        //
        // Returns:
        //     A 32-bit signed integer formed by four bytes beginning at startIndex.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     startIndex is greater than or equal to the length of value minus 3, and is less
        //     than or equal to the length of value minus 1.
        //
        //   T:System.ArgumentNullException:
        //     value is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     startIndex is less than zero or greater than the length of value minus 1.
        public static int ToInt32(byte[] value, int startIndex)
        {
            return BitConverter.ToInt32(value, startIndex);
        }

        //
        // Summary:
        //     Returns a 64-bit signed integer converted from eight bytes at a specified position
        //     in a byte array.
        //
        // Parameters:
        //   value:
        //     An array of bytes.
        //
        //   startIndex:
        //     The starting position within value.
        //
        // Returns:
        //     A 64-bit signed integer formed by eight bytes beginning at startIndex.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     startIndex is greater than or equal to the length of value minus 7, and is less
        //     than or equal to the length of value minus 1.
        //
        //   T:System.ArgumentNullException:
        //     value is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     startIndex is less than zero or greater than the length of value minus 1.
        public static long ToInt64(byte[] value, int startIndex)
        {
            return BitConverter.ToInt64(value, startIndex);
        }

        public static long ToInt64(ReadOnlySpan<byte> value)
        {
            return BitConverter.ToInt64(value);
        }

        //
        // Summary:
        //     Returns a single-precision floating point number converted from four bytes at
        //     a specified position in a byte array.
        //
        // Parameters:
        //   value:
        //     An array of bytes.
        //
        //   startIndex:
        //     The starting position within value.
        //
        // Returns:
        //     A single-precision floating point number formed by four bytes beginning at startIndex.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     startIndex is greater than or equal to the length of value minus 3, and is less
        //     than or equal to the length of value minus 1.
        //
        //   T:System.ArgumentNullException:
        //     value is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     startIndex is less than zero or greater than the length of value minus 1.
        public static float ToSingle(byte[] value, int startIndex)
        {
            return BitConverter.ToSingle(value, startIndex);
        }

        public static float ToSingle(ReadOnlySpan<byte> value)
        {
            return BitConverter.ToSingle(value);
        }

        //
        // Summary:
        //     Converts the numeric value of each element of a specified array of bytes to its
        //     equivalent hexadecimal string representation.
        //
        // Parameters:
        //   value:
        //     An array of bytes.
        //
        // Returns:
        //     A string of hexadecimal pairs separated by hyphens, where each pair represents
        //     the corresponding element in value; for example, &quot;7F-2C-4A-00&quot;.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     value is null.
        public static string ToString(byte[] value)
        {
            return BitConverter.ToString(value);
        }

        //
        // Summary:
        //     Converts the numeric value of each element of a specified subarray of bytes to
        //     its equivalent hexadecimal string representation.
        //
        // Parameters:
        //   value:
        //     An array of bytes.
        //
        //   startIndex:
        //     The starting position within value.
        //
        // Returns:
        //     A string of hexadecimal pairs separated by hyphens, where each pair represents
        //     the corresponding element in a subarray of value; for example, &quot;7F-2C-4A-00&quot;.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     value is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     startIndex is less than zero or greater than the length of value minus 1.
        public static string ToString(byte[] value, int startIndex)
        {
            return BitConverter.ToString(value, startIndex);
        }

        //
        // Summary:
        //     Converts the numeric value of each element of a specified subarray of bytes to
        //     its equivalent hexadecimal string representation.
        //
        // Parameters:
        //   value:
        //     An array of bytes.
        //
        //   startIndex:
        //     The starting position within value.
        //
        //   length:
        //     The number of array elements in value to convert.
        //
        // Returns:
        //     A string of hexadecimal pairs separated by hyphens, where each pair represents
        //     the corresponding element in a subarray of value; for example, &quot;7F-2C-4A-00&quot;.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     value is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     startIndex or length is less than zero. -or- startIndex is greater than zero
        //     and is greater than or equal to the length of value.
        //
        //   T:System.ArgumentException:
        //     The combination of startIndex and length does not specify a position within value;
        //     that is, the startIndex parameter is greater than the length of value minus the
        //     length parameter.
        public static string ToString(byte[] value, int startIndex, int length)
        {
            return BitConverter.ToString(value, startIndex, length);
        }

        //
        // Summary:
        //     Returns a 16-bit unsigned integer converted from two bytes at a specified position
        //     in a byte array.
        //
        // Parameters:
        //   value:
        //     The array of bytes.
        //
        //   startIndex:
        //     The starting position within value.
        //
        // Returns:
        //     A 16-bit unsigned integer formed by two bytes beginning at startIndex.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     startIndex equals the length of value minus 1.
        //
        //   T:System.ArgumentNullException:
        //     value is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     startIndex is less than zero or greater than the length of value minus 1.
        public static ushort ToUInt16(byte[] value, int startIndex)
        {
            return BitConverter.ToUInt16(value, startIndex);
        }

        public static ushort ToUInt16(ReadOnlySpan<byte> value)
        {
            return BitConverter.ToUInt16(value);
        }

        //
        // Summary:
        //     Returns a 32-bit unsigned integer converted from four bytes at a specified position
        //     in a byte array.
        //
        // Parameters:
        //   value:
        //     An array of bytes.
        //
        //   startIndex:
        //     The starting position within value.
        //
        // Returns:
        //     A 32-bit unsigned integer formed by four bytes beginning at startIndex.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     startIndex is greater than or equal to the length of value minus 3, and is less
        //     than or equal to the length of value minus 1.
        //
        //   T:System.ArgumentNullException:
        //     value is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     startIndex is less than zero or greater than the length of value minus 1.
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

        //
        // Summary:
        //     Returns a 64-bit unsigned integer converted from eight bytes at a specified position
        //     in a byte array.
        //
        // Parameters:
        //   value:
        //     An array of bytes.
        //
        //   startIndex:
        //     The starting position within value.
        //
        // Returns:
        //     A 64-bit unsigned integer formed by the eight bytes beginning at startIndex.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     startIndex is greater than or equal to the length of value minus 7, and is less
        //     than or equal to the length of value minus 1.
        //
        //   T:System.ArgumentNullException:
        //     value is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     startIndex is less than zero or greater than the length of value minus 1.
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