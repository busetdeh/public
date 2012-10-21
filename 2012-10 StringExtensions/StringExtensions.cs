/**
 * Copyright (c) 2012, Thomas Charrière
 * http://www.codepanda.ch/
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software 
 * and associated documentation files (the "Software"), to deal in the Software without restriction,
 * including without limitation the rights to use, copy, modify, merge, publish, distribute,
 * sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is 
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or
 * substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE
 * AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
namespace System
{
    using System;
    using System.Text;

    public static class StringExtensions
    {
        /// <summary>
        /// Replaces the format item in a specified string with the string representation of a corresponding object in a specified array.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns>A copy of format in which the format items have been replaced by the string representation of the corresponding objects in args.</returns>
        public static string FormatWith(this String format, params object[] args)
        {
            return String.Format(format, args);
        }
        /// <summary>
        /// Reverses characters in a specified string.
        /// </summary>
        /// <param name="input">A string to format.</param>
        /// <returns>A copy of input which characters have been reversed.</returns>
        public static string Reverse(this String input)
        {
            char[] arr = input.ToCharArray();
            Array.Reverse(arr);
            return new String(arr);
        }
        /// <summary>
        /// Converts an entire string from one encoding to another.
        /// </summary>
        /// <param name="input">The string to convert.</param>
        /// <param name="srcEncoding">The encoding format of string.</param>
        /// <param name="dstEncoding">The target encoding format.</param>
        /// <returns>An System.String containing the results of converting the input string from srcEncoding to dstEncoding.</returns>
        public static string Encode(this String input, Encoding srcEncoding, Encoding dstEncoding)
        {
            return ToStringEncoded(Encoding.Convert(srcEncoding, dstEncoding, srcEncoding.GetBytes(input)), dstEncoding);
        }
        /// <summary>
        /// Encodes all the characters in the specified string into a sequence of bytes with the default Encoder (ANSI).
        /// </summary>
        /// <param name="input">The System.String containing the characters to encode.</param>
        /// <returns>A byte array containing the results of encoding the specified set of characters.</returns>
        public static byte[] ToByteArray(this String input)
        {
            return ToByteArray(input, Encoding.Default);
        }
        /// <summary>
        /// Encodes all the characters in the specified string into a sequence of bytes.
        /// </summary>
        /// <param name="input">The System.String containing the characters to encode.</param>
        /// <param name="encoding">The System.Text.Encoding to use for encoding.</param>
        /// <returns>A byte array containing the results of encoding the specified set of characters.</returns>
        public static byte[] ToByteArray(this String input, Encoding encoding)
        {
            return encoding.GetBytes(input);
        }
        /// <summary>
        /// Decodes all the bytes in the specified byte array to a System.String with the default Encoder (ANSI).
        /// </summary>
        /// <param name="input">The byte array containing the sequence of bytes to decode.</param>
        /// <returns>A System.String containing the results of decoding the specified sequence of bytes.</returns>
        public static string ToStringEncoded(this byte[] input)
        {
            return ToStringEncoded(input, Encoding.Default);
        }
        /// <summary>
        /// Decodes all the bytes in the specified byte array to a System.String with the Encoder provided.
        /// </summary>
        /// <param name="input">The byte array containing the sequence of bytes to decode.</param>
        /// <param name="encoding">The System.Text.Encoding to use for decoding.</param>
        /// <returns>A System.String containing the results of decoding the specified sequence of bytes.</returns>
        public static string ToStringEncoded(this byte[] input, Encoding encoding)
        {
            return encoding.GetString(input);
        }
    }
}
