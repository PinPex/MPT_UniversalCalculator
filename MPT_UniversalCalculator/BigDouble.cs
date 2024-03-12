using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using Random = System.Random;
using System.Numerics;

namespace MPT_UniversalCalculator
{
    class BigDouble
    {
        private BigInteger b_int;
        private double dbl;

        BigDouble(BigInteger b_int, double dbl)
        {
            this.b_int = b_int;
            this.dbl = dbl;
        }
        public BigInteger Integer
        {
            get { return b_int; }
            set { b_int = value; }
        }
        public double Double
        {
            get { return dbl; }
            set { dbl = value; }
        }

        public static BigDouble operator+(BigDouble first, BigDouble second)
        {
            return new BigDouble(first.Integer, second.Double);
        }
    }
}
