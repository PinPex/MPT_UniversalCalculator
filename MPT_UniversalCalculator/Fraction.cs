using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MPT_UniversalCalculator
{
    
    public class Fract : IComparable, IConvertible
    {
        private bool sign = false;
        private BigInteger num;
        private BigInteger denom = 1;
        public Fract(String fract)
        {
            string[] num_denom = fract.Split('/');



            if (num_denom.Length == 2)
            {
                Num = BigInteger.Parse(num_denom[0]);
                denom = BigInteger.Parse(num_denom[1]);
            }
            else
            {
                Num = BigInteger.Parse(fract);
            }

            if (Num < 0)
            {
                sign = true;
                Num = BigInteger.Abs(Num);
            }

            BigInteger divider = GCD(num, denom);
            Num = num / divider;
            Denom = denom / divider;
        }

        public Fract(BigInteger num, BigInteger denom)
        {
            BigInteger divider = GCD(num, denom);
            Num = num / divider;
            Denom = denom / divider;
        }
        public BigInteger Num
        {
            get
            {
                return num;
            }
            set
            {
                num = value;
            }
        }

        public BigInteger Denom
        {
            get
            {
                return denom;
            }
            set
            {
                denom = value;
            }
        }

        private BigInteger GCD(BigInteger first, BigInteger second)
        {
            while (first != 0 && second != 0)
            {
                if (first > second)
                    first %= second;
                else
                    second %= first;
            }
            return first | second;
        }


        public static Fract operator +(Fract first, Fract second)
        {
            return new Fract(first.Num * first.Denom + second.Num * first.Denom, first.Denom * first.Denom);
        }

        public static Fract operator -(Fract first, Fract second)
        {
            return new Fract(first.Num * first.Denom - second.Num * first.Denom, first.Denom * first.Denom);
        }

        public static Fract operator *(Fract first, Fract second)
        {
            return new Fract(first.Num * second.Num, first.Denom * second.Denom);
        }

        public static Fract operator /(Fract first, Fract second)
        {
            return new Fract(first.Num * second.Denom, first.Denom * second.Num);
        }

        public static bool operator ==(Fract first, Fract second)
        {
            return (first.Num == second.Num) && (first.Denom == second.Denom);
        }

        public static bool operator !=(Fract first, Fract second)
        {
            return !(first == second);
        }

        public static bool operator >(Fract first, Fract second)
        {
            return ((double)first.Num / (double)first.Denom) > ((double)second.Num / (double)second.Denom);
        }
        public static bool operator <(Fract first, Fract second)
        {
            return ((double)first.Num / (double)first.Denom) < ((double)second.Num / (double)second.Denom);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Fract otherFraction = (Fract)obj;
            return num == otherFraction.num && denom == otherFraction.denom;
        }

        public override int GetHashCode()
        {
            return num.GetHashCode() ^ denom.GetHashCode();
        }

        public Fract Reverse()
        {
            BigInteger temp = num;
            num = denom;
            denom = temp;
            return this;
        }
        public override String ToString()
        {
            if (denom != 1)
            {
                return (sign ? "-" : "") + Num.ToString() + "/" + Denom.ToString();
            }
            else
            {
                return (sign ? "-" : "") + Num.ToString();
            }

        }

        #region IComparable_IConvertible_methods
        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public TypeCode GetTypeCode()
        {
            throw new NotImplementedException();
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public byte ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public short ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public long ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public float ToSingle(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public double ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
