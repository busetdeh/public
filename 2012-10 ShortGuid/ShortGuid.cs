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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Obfuscates a globally unique identifier (GUID) to an ASCII armoured base64 encoded string.
    /// </summary>
    public struct ShortGuid : 
        IComparable,
        IComparable<ShortGuid>, IComparable<Guid>,
        IEquatable<Guid>, IEquatable<ShortGuid>
    {
        #region [ Fields ]
        private Guid _guid;
        private string _value;
        private Regex _regex;
        /// <summary>
        /// A read-only instance of the <see cref="System.ShortGuid"/> class whose value is guaranteed to have a globally unique identifier (GUID) of all zeros.
        /// </summary>
        public readonly static ShortGuid Empty = new ShortGuid(Guid.Empty);

        private const string TOKEN_REGEX_EXPRESSION = @"^[a-z0-9\-_]{22}$";
        #endregion

        #region [ Constructors ]
        /// <summary>
        /// Initializes a new <see cref="System.ShortGuid"/> with the specified obfuscated GUID.
        /// </summary>
        /// <param name="rawToken"></param>
        public ShortGuid(string rawToken)
        {
            _regex = new Regex(TOKEN_REGEX_EXPRESSION, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (rawToken == null || !_regex.IsMatch(rawToken))
                throw new FormatException(String.Format("Token must be of the following format: {0}", TOKEN_REGEX_EXPRESSION));

            StringBuilder sb = new StringBuilder(rawToken);
            sb.Replace('_', '/');
            sb.Replace('-', '+');
            sb.Append("=="); // otherwise not accepted as valid base64

            _guid = new Guid(Convert.FromBase64String(sb.ToString()));
            _value = rawToken;
        }
        /// <summary>
        /// Initializes a new Token with the specified globally unique identifier (GUID).
        /// </summary>
        /// <param name="guid">A globally unique identifier (GUID).</param>
        public ShortGuid(Guid guid)
        {
            _regex = new Regex(TOKEN_REGEX_EXPRESSION, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            _guid = guid;

            StringBuilder sb = new StringBuilder(Convert.ToBase64String(guid.ToByteArray()));
            sb.Replace('/', '_');
            sb.Replace('+', '-');
            _value = sb.ToString(0, 22);
        }
        #endregion

        #region [ Properties ]
        /// <summary>
        /// Gets a value indicating the current GUID.
        /// </summary>
        public Guid Guid
        {
            get { return _guid; }
        }
        /// <summary>
        /// Gets a value indicating the ASCII armoured base64 encoded string.
        /// </summary>
        public string Value
        {
            get { return _value; }
        }
        #endregion

        #region [ Methods ]
        /// <summary>
        /// Returns the ASCII armoured base64 encoded string.
        /// </summary>
        /// <returns>ASCII armoured base64 encoded string.</returns>
        public override string ToString()
        {
            return Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ShortGuid)) return false;

            return ((ShortGuid)obj).Guid == Guid;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region [ Statics ]
        #region [ Operators ]
        public static bool operator !=(ShortGuid a, ShortGuid b)
        {
            return !object.Equals(a.Guid, b.Guid);
        }

        public static bool operator ==(ShortGuid a, ShortGuid b)
        {
            return object.Equals(a.Guid, b.Guid);
        }

        public static implicit operator Guid(ShortGuid e)
        {
            return e.Guid;
        }

        public static implicit operator ShortGuid(Guid e)
        {
            return new ShortGuid(e);
        }

        public static implicit operator ShortGuid(string e)
        {
            return new ShortGuid(e);
        }

        public static implicit operator ShortGuid(byte[] e)
        {
            return new ShortGuid(new Guid(e));
        }
        #endregion

        /// <summary>
        ///  Initializes a new instance of the <see cref="System.ShortGuid"/> class.
        /// </summary>
        /// <returns>A new Token object.</returns>
        public static ShortGuid NewGuid()
        {
            return new ShortGuid(Guid.NewGuid());
        }
        /// <summary>
        /// Verifies the obfuscated GUID has a valid format
        /// </summary>
        /// <param name="token">Obfuscated GUID</param>
        /// <returns></returns>
        public static bool IsValid(string token)
        {
            Regex regex = new Regex(TOKEN_REGEX_EXPRESSION, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return token != null && regex.IsMatch(token);
        }
        #endregion

        #region [ IComparable ]
        /// <summary>
        /// Compares this instance to a specified object and returns an indication of their relative values.
        /// </summary>
        /// <param name="value">An object to compare, or null.</param>
        /// <returns>A signed number indicating the relative values of this instance and value.</returns>
        public int CompareTo(object value)
        {
            return _guid.CompareTo(value);
        }
        /// <summary>
        /// Compares this instance to a specified ShortGuid object and returns an indication of their relative values.
        /// </summary>
        /// <param name="other">An object to compare to this instance.</param>
        /// <returns>A signed number indicating the relative values of this instance and value.</returns>
        public int CompareTo(ShortGuid other)
        {
            return _guid.CompareTo(other.Guid);
        }
        /// <summary>
        /// Compares this instance to a specified Guid object and returns an indication of their relative values.
        /// </summary>
        /// <param name="other">An object to compare to this instance.</param>
        /// <returns>A signed number indicating the relative values of this instance and value.</returns>
        public int CompareTo(Guid other)
        {
            return _guid.CompareTo(other);
        }
        #endregion

        #region [ IEquatable ]
        /// <summary>
        /// Returns a value indicating whether this instance and a specified Guid object represent the same value.
        /// </summary>
        /// <param name="other">An object to compare to this instance.</param>
        /// <returns>true if other is equal to this instance; otherwise, false.</returns>
        public bool Equals(Guid other)
        {
            return _guid.Equals(other);
        }
        /// <summary>
        /// Returns a value indicating whether this instance and a specified ShortGuid object represent the same value.
        /// </summary>
        /// <param name="other">An object to compare to this instance.</param>
        /// <returns>true if other is equal to this instance; otherwise, false.</returns>
        public bool Equals(ShortGuid other)
        {
            return _guid.Equals(other.Guid);
        }
        #endregion

        #endregion
        
    }
}
