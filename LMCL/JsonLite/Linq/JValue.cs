#region License
// Copyright (c) 2007 James Newton-King
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using Magentaize.Net.LMCL.JsonLite.Utilities;
using System.Globalization;
using System.Numerics;

namespace Magentaize.Net.LMCL.JsonLite.Linq
{
    /// <summary>
    /// Represents a value in JSON (string, integer, date, etc).
    /// </summary>
    public class JValue : JToken, IFormattable, IConvertible
    {
        private JTokenType _valueType;
        private object _value;

        internal JValue(object value, JTokenType type)
        {
            _value = value;
            _valueType = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JValue"/> class from another <see cref="JValue"/> object.
        /// </summary>
        /// <param name="other">A <see cref="JValue"/> object to copy from.</param>
        public JValue(JValue other)
            : this(other.Value, other.Type)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JValue"/> class with the given value.
        /// </summary>
        /// <param name="value">The value.</param>
        public JValue(long value)
            : this(value, JTokenType.Integer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JValue"/> class with the given value.
        /// </summary>
        /// <param name="value">The value.</param>
        public JValue(decimal value)
            : this(value, JTokenType.Float)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JValue"/> class with the given value.
        /// </summary>
        /// <param name="value">The value.</param>
        public JValue(char value)
            : this(value, JTokenType.String)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JValue"/> class with the given value.
        /// </summary>
        /// <param name="value">The value.</param>
        [CLSCompliant(false)]
        public JValue(ulong value)
            : this(value, JTokenType.Integer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JValue"/> class with the given value.
        /// </summary>
        /// <param name="value">The value.</param>
        public JValue(double value)
            : this(value, JTokenType.Float)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JValue"/> class with the given value.
        /// </summary>
        /// <param name="value">The value.</param>
        public JValue(float value)
            : this(value, JTokenType.Float)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JValue"/> class with the given value.
        /// </summary>
        /// <param name="value">The value.</param>
        public JValue(bool value)
            : this(value, JTokenType.Boolean)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JValue"/> class with the given value.
        /// </summary>
        /// <param name="value">The value.</param>
        public JValue(string value)
            : this(value, JTokenType.String)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JValue"/> class with the given value.
        /// </summary>
        /// <param name="value">The value.</param>
        public JValue(Guid value)
            : this(value, JTokenType.Guid)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JValue"/> class with the given value.
        /// </summary>
        /// <param name="value">The value.</param>
        public JValue(Uri value)
            : this(value, (value != null) ? JTokenType.Uri : JTokenType.Null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JValue"/> class with the given value.
        /// </summary>
        /// <param name="value">The value.</param>
        public JValue(object value)
            : this(value, GetValueType(null, value))
        {
        }

        /// <summary>
        /// Gets a value indicating whether this token has child tokens.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this token has child values; otherwise, <c>false</c>.
        /// </value>
        public override bool HasValues
        {
            get { return false; }
        }

        /// <summary>
        /// Creates a <see cref="JValue"/> comment with the given value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="JValue"/> comment with the given value.</returns>
        public static JValue CreateComment(string value)
        {
            return new JValue(value, JTokenType.Comment);
        }

        /// <summary>
        /// Creates a <see cref="JValue"/> string with the given value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="JValue"/> string with the given value.</returns>
        public static JValue CreateString(string value)
        {
            return new JValue(value, JTokenType.String);
        }

        /// <summary>
        /// Creates a <see cref="JValue"/> null value.
        /// </summary>
        /// <returns>A <see cref="JValue"/> null value.</returns>
        public static JValue CreateNull()
        {
            return new JValue(null, JTokenType.Null);
        }

        /// <summary>
        /// Creates a <see cref="JValue"/> undefined value.
        /// </summary>
        /// <returns>A <see cref="JValue"/> undefined value.</returns>
        public static JValue CreateUndefined()
        {
            return new JValue(null, JTokenType.Undefined);
        }

        private static JTokenType GetValueType(JTokenType? current, object value)
        {
            if (value == null)
            {
                return JTokenType.Null;
            }
            else if (value is string)
            {
                return GetStringValueType(current);
            }
            else if (value is long || value is int || value is short || value is sbyte
                     || value is ulong || value is uint || value is ushort || value is byte)
            {
                return JTokenType.Integer;
            }
            else if (value is Enum)
            {
                return JTokenType.Integer;
            }
#if !(NET20 || NET35 || PORTABLE40 || PORTABLE)
            else if (value is BigInteger)
            {
                return JTokenType.Integer;
            }
#endif
            else if (value is double || value is float || value is decimal)
            {
                return JTokenType.Float;
            }
            else if (value is byte[])
            {
                return JTokenType.Bytes;
            }
            else if (value is bool)
            {
                return JTokenType.Boolean;
            }
            else if (value is Guid)
            {
                return JTokenType.Guid;
            }
            else if (value is Uri)
            {
                return JTokenType.Uri;
            }

            throw new ArgumentException("Could not determine JSON object type for type {0}.".FormatWith(CultureInfo.InvariantCulture, value.GetType()));
        }

        private static JTokenType GetStringValueType(JTokenType? current)
        {
            if (current == null)
            {
                return JTokenType.String;
            }

            switch (current.GetValueOrDefault())
            {
                case JTokenType.Comment:
                case JTokenType.String:
                    return current.GetValueOrDefault();
                default:
                    return JTokenType.String;
            }
        }

        /// <summary>
        /// Gets the node type for this <see cref="JToken"/>.
        /// </summary>
        /// <value>The type.</value>
        public override JTokenType Type
        {
            get { return _valueType; }
        }

        /// <summary>
        /// Gets or sets the underlying token value.
        /// </summary>
        /// <value>The underlying token value.</value>
        public object Value
        {
            get { return _value; }
            set
            {
                Type currentType = (_value != null) ? _value.GetType() : null;
                Type newType = (value != null) ? value.GetType() : null;

                if (currentType != newType)
                {
                    _valueType = GetValueType(_valueType, value);
                }

                _value = value;
            }
        }

        /// <summary>
        /// Writes this token to a <see cref="JsonWriter"/>.
        /// </summary>
        /// <param name="writer">A <see cref="JsonWriter"/> into which this method will write.</param>
        /// <param name="converters">A collection of <see cref="JsonConverter"/> which will be used when writing the token.</param>
        public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
        {
            if (converters != null && converters.Length > 0 && _value != null)
            {
                JsonConverter matchingConverter = JsonSerializer.GetMatchingConverter(converters, _value.GetType());
                if (matchingConverter != null && matchingConverter.CanWrite)
                {
                    matchingConverter.WriteJson(writer, _value, JsonSerializer.CreateDefault());
                    return;
                }
            }

            switch (_valueType)
            {
                case JTokenType.Comment:
                    writer.WriteComment((_value != null) ? _value.ToString() : null);
                    return;
                case JTokenType.Null:
                    writer.WriteNull();
                    return;
                case JTokenType.Undefined:
                    writer.WriteUndefined();
                    return;
                case JTokenType.Integer:
                    if (_value is int)
                    {
                        writer.WriteValue((int)_value);
                    }
                    else if (_value is long)
                    {
                        writer.WriteValue((long)_value);
                    }
                    else if (_value is ulong)
                    {
                        writer.WriteValue((ulong)_value);
                    }
#if !(NET20 || NET35 || PORTABLE40 || PORTABLE)
                    else if (_value is BigInteger)
                    {
                        writer.WriteValue((BigInteger)_value);
                    }
#endif
                    else
                    {
                        writer.WriteValue(Convert.ToInt64(_value, CultureInfo.InvariantCulture));
                    }
                    return;
                case JTokenType.Float:
                    if (_value is decimal)
                    {
                        writer.WriteValue((decimal)_value);
                    }
                    else if (_value is double)
                    {
                        writer.WriteValue((double)_value);
                    }
                    else if (_value is float)
                    {
                        writer.WriteValue((float)_value);
                    }
                    else
                    {
                        writer.WriteValue(Convert.ToDouble(_value, CultureInfo.InvariantCulture));
                    }
                    return;
                case JTokenType.String:
                    writer.WriteValue((_value != null) ? _value.ToString() : null);
                    return;
                case JTokenType.Boolean:
                    writer.WriteValue(Convert.ToBoolean(_value, CultureInfo.InvariantCulture));
                    return;
                case JTokenType.Bytes:
                    writer.WriteValue((byte[])_value);
                    return;
                case JTokenType.Guid:
                case JTokenType.Uri:
                    writer.WriteValue((_value != null) ? ((Uri)_value).OriginalString : null);
                    return;
            }

            throw MiscellaneousUtils.CreateArgumentOutOfRangeException("TokenType", _valueType, "Unexpected token type.");
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        /// </exception>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            JValue otherValue = obj as JValue;
            if (otherValue != null)
            {
                return Equals(otherValue);
            }

            return base.Equals(obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            if (_value == null)
            {
                return 0;
            }

            return _value.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (_value == null)
            {
                return string.Empty;
            }

            return _value.ToString();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public string ToString(IFormatProvider formatProvider)
        {
            return ToString(null, formatProvider);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (_value == null)
            {
                return string.Empty;
            }

            IFormattable formattable = _value as IFormattable;
            if (formattable != null)
            {
                return formattable.ToString(format, formatProvider);
            }
            else
            {
                return _value.ToString();
            }
        }

#if !PORTABLE
        TypeCode IConvertible.GetTypeCode()
        {
            if (_value == null)
            {
                return TypeCode.Empty;
            }

            IConvertible convertable = _value as IConvertible;

            if (convertable == null)
            {
                return TypeCode.Object;
            }

            return convertable.GetTypeCode();
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return (bool)this;
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            return (char)this;
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return (sbyte)this;
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return (byte)this;
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return (short)this;
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return (ushort)this;
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return (int)this;
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return (uint)this;
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return (long)this;
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return (ulong)this;
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return (float)this;
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return (double)this;
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return (decimal)this;
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            return ToObject(conversionType);
        }

        /// <summary>
        /// null
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
#endif
    }
}
