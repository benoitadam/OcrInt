using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcrInt
{
    public struct TagValue
    {
        public double Number;
        public string Value;
        public int Score;

        public TagValue(double value, int score)
        {
            Number = value;
            Value = value.ToString(CultureInfo.InvariantCulture);
            Score = score;
        }

        public TagValue(string value, int score)
        {
            Value = value;
            Number = 0.0;
            Score = score;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Value.Equals(obj);
        }

        /// <summary>
        /// Implicit TagValue to string conversion operator
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator string(TagValue value)
        {
            return value.Value;
        }

        /// <summary>
        /// Explicit string to TagValue conversion operator
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator TagValue(string value)
        {
            return new TagValue(value, 100);
        }

        /// <summary>
        /// Implicit TagValue to string conversion operator
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator double(TagValue value)
        {
            return value.Number;
        }

        /// <summary>
        /// Explicit string to TagValue conversion operator
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator TagValue(double value)
        {
            return new TagValue(value, 50);
        }
    }
}
