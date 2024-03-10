using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MPT_UniversalCalculator
{
    public class Complex : IComparable, IConvertible
    {
        private BigNumber complex_real;
        private BigNumber complex_imaginary;
        public Complex(String complex)
        {
            complex = complex.Replace(" ", "");
            complex = complex.Replace("i*", "");
            string[] parts = complex.Split('+');
            Console.WriteLine(parts[0]);
            complex_real = BigNumber.Parse(parts[0]);
            complex_imaginary = BigNumber.Parse(parts[1]);
        }
        public override String ToString()
        {
            return complex_real.ToString() + " + i * " + complex_imaginary.ToString();
        }
        public BigNumber ComplexReal
        {
            get
            {
                return complex_real;
            }
            set
            {
                complex_real = value;
            }
        }

        public BigNumber ComplexImaginary
        {
            get
            {
                return complex_imaginary;
            }
            set
            {
                complex_imaginary = value;
            }
        }

        #region interface_realise
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

        public byte ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public double ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public short ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public long ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public float ToSingle(IFormatProvider provider)
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

        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
