using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MPT_UniversalCalculator
{
    public class BigNumber
    {
        private BigInteger integer_part;
        private double fract_part;
        public BigNumber(BigInteger integer_part, double fract_part)
        {
            this.integer_part = integer_part;
            this.fract_part = fract_part;
        }
        public BigInteger Integer
        {
            get
            {
                return integer_part;
            }
            set
            {
                integer_part = value;
            }
        }

        public double Fraction
        {
            get
            {
                return fract_part;
            }
            set
            {
                fract_part = value;
            }
        }

        public override String ToString()
        {
            return integer_part.ToString() + "." + fract_part.ToString().Remove(0, 2);
        }
        public static BigNumber Parse(String bigNumberString)
        {
            string[] parts = bigNumberString.Split('.');
            BigInteger integer_part = BigInteger.Parse(parts[0]);
            double fract_part = Double.Parse("0." + parts[1], NumberStyles.Any, CultureInfo.InvariantCulture);
            return new BigNumber(integer_part, fract_part);
        }
        public static BigNumber operator +(BigNumber first, BigNumber second)
        {
            BigInteger resultInteger = first.Integer + second.Integer;
            double resultFraction = first.Fraction + second.Fraction;

            if (resultFraction >= 1)
            {
                resultInteger++;
                resultFraction = resultFraction - 1;
            }

            return new BigNumber(resultInteger, resultFraction);
        }
        public static BigNumber operator -(BigNumber first, BigNumber second)
        {
            BigInteger resultInteger = first.Integer - second.Integer;
            double resultFraction = first.Fraction - second.Fraction;

            if (resultFraction < 0)
            {
                resultInteger--;
                resultFraction = 1 - Math.Abs(resultFraction);
            }

            return new BigNumber(resultInteger, resultFraction);
        }

        public static BigNumber operator *(BigNumber first, BigNumber second)
        {
            BigInteger resultInteger = first.Integer * second.Integer;
            double resultFraction = first.Fraction * second.Fraction;

            return new BigNumber(resultInteger, resultFraction);
        }

        public static BigNumber operator /(BigNumber first, BigNumber second)
        {
            if (second.Integer == 0 && second.Fraction == 0)
            {
                throw new DivideByZeroException("Деление на ноль недопустимо.");
            }

            BigInteger resultInteger = first.Integer / second.Integer;
            double resultFraction = first.Fraction / second.Fraction;

            return new BigNumber(resultInteger, resultFraction);
        }
    }
}
