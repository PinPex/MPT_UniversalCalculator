using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace CalculatorWPF {
    public sealed class TFrac : ANumber {
        public BigInteger Numerator;
        public BigInteger Denominator;

        #region Current Class Things
        static void Swap<T>(ref T lhs, ref T rhs) {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
        public static BigInteger GCD(BigInteger a, BigInteger b) {
            a = BigInteger.Abs(a);
            b = BigInteger.Abs(b);
            while (b > 0) {
                a %= b;
                Swap(ref a, ref b);
            }
            return a;
        }
        #endregion

        #region Constructor
        public TFrac() {
            Numerator = new BigInteger(0);
            Denominator = new BigInteger(1);
        }
        public TFrac(BigInteger a, BigInteger b) {
            try {
                if (a < 0 && b < 0) {
                    a *= -1;
                    b *= -1;
                }
                else if (b < 0 && a > 0) {
                    b *= -1;
                    a *= -1;
                }
                else if (a == 0 && b == 0 || b == 0 || a == 0 && b == 1) {
                    Numerator = new BigInteger(0);
                    Denominator = new BigInteger(1);
                    return;
                }
                Numerator = a;
                Denominator = b;
                BigInteger gcdResult = GCD(a, b);
                if (gcdResult > 1) {
                    Numerator /= gcdResult;
                    Denominator /= gcdResult;
                }
            } catch {
                throw new OverflowException();
            }
        }
        public TFrac(int a, int b) {
            if (a < 0 && b < 0) {
                a *= -1;
                b *= -1;
            }
            else if (b < 0 && a > 0) {
                b *= -1;
                a *= -1;
            }
            else if (a == 0 && b == 0 || b == 0 || a == 0 && b == 1) {
                Numerator = new BigInteger(0);
                Denominator = new BigInteger(1);
                return;
            }
            Numerator = new BigInteger(a);
            Denominator = new BigInteger(b);
            BigInteger gcdResult = GCD(a, b);
            if (gcdResult > 1) {
                Numerator /= gcdResult;
                Denominator /= gcdResult;
            }
        }
        public TFrac(string fraction) {
            Regex FracRegex = new Regex(@"^-?(\d+)/(\d+)$");
            Regex NumberRegex = new Regex(@"^-?\d+/?$");
            if (FracRegex.IsMatch(fraction)) {
                List<string> FracParts = fraction.Split('/').ToList();
                Numerator = BigInteger.Parse(FracParts[0]);
                Denominator = BigInteger.Parse(FracParts[1]);
                if (Denominator == 0) {
                    Numerator = new BigInteger(0);
                    Denominator = new BigInteger(1);
                    return;
                }
                BigInteger gcdResult = GCD(Numerator, Denominator);
                if (gcdResult > 1) {
                    Numerator /= gcdResult;
                    Denominator /= gcdResult;
                }
                return;
            }
            else if (NumberRegex.IsMatch(fraction)) {
                Numerator = BigInteger.Parse(fraction);
                Denominator = new BigInteger(1);
                return;
            }
            else {
                Numerator = new BigInteger(0);
                Denominator = new BigInteger(1);
                return;
            }
        }
        public TFrac(TFrac anotherFrac) {
            Numerator = anotherFrac.Numerator;
            Denominator = anotherFrac.Denominator;
        }
        #endregion

        #region Override operators
        public static TFrac operator +(TFrac a, TFrac b) {
            TFrac temp;
            temp = new TFrac(a.Numerator * b.Denominator + a.Denominator * b.Numerator, a.Denominator * b.Denominator);
            return temp;
        }
        public static TFrac operator *(TFrac a, TFrac b) {
            TFrac temp;
            temp = new TFrac(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
            return temp;
        }
        public static TFrac operator -(TFrac a, TFrac b) {
            TFrac temp;
            temp = new TFrac(a.Numerator * b.Denominator - a.Denominator * b.Numerator, a.Denominator * b.Denominator);
            return temp;
        }
        public static TFrac operator /(TFrac a, TFrac b) {
            if (b.IsZero())
                throw new Exception();
            TFrac temp;
            temp = new TFrac(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
            return temp;
        }
        public static TFrac operator -(TFrac a) {
            return new TFrac(-a.Numerator, a.Denominator);
        }
        public static bool operator ==(TFrac a, TFrac b) {
            return a.Numerator == b.Numerator && a.Denominator == b.Denominator;
        }
        public static bool operator !=(TFrac a, TFrac b) {
            return a.Numerator != b.Numerator && a.Denominator != b.Denominator;
        }
        public static bool operator >(TFrac a, TFrac b) {
            return (a.Numerator / a.Denominator) > (b.Numerator / b.Denominator);
        }
        public static bool operator <(TFrac a, TFrac b) {
            return (a.Numerator / a.Denominator) < (b.Numerator / b.Denominator);
        }
        #endregion

        #region Abstract Override
        public override ANumber Add(ANumber a) {
            TFrac temp;
            temp = new TFrac(Numerator * (a as TFrac).Denominator + Denominator * (a as TFrac).Numerator, Denominator * (a as TFrac).Denominator);
            return temp;
        }
        public override ANumber Mul(ANumber a) {
            TFrac temp;
            temp = new TFrac((a as TFrac).Numerator * Numerator, (a as TFrac).Denominator * Denominator);
            return temp;
        }
        public override ANumber Div(ANumber a) {
            TFrac temp;
            temp = new TFrac((a as TFrac).Numerator * Denominator, (a as TFrac).Denominator * Numerator);
            return temp;
        }
        public override ANumber Sub(ANumber a) {
            TFrac temp;
            temp = new TFrac((a as TFrac).Numerator * Denominator - (a as TFrac).Denominator * Numerator, (a as TFrac).Denominator * Denominator);
            return temp;
        }
        public override object Square() {
            TFrac temp;
            temp = new TFrac(Numerator * Numerator, Denominator * Denominator);
            return temp;
        }
        public override object Reverse() {
            return new TFrac(Denominator, Numerator);
        }
        public override bool IsZero() {
            return Numerator == 0;
        }
        public override void SetString(string str) {
            TFrac TempFrac = new TFrac(str);
            Numerator = TempFrac.Numerator;
            Denominator = TempFrac.Denominator;
        }
        #endregion

        public override string ToString() {
            return Numerator.ToString() + "/" + Denominator.ToString();
        }
        public override bool Equals(object obj) {
            var frac = obj as TFrac;
            return frac != null &&
                   Numerator == frac.Numerator &&
                   Denominator == frac.Denominator;
        }
        public override int GetHashCode() {
            var hashCode = -1534900553;
            hashCode = hashCode * -1521134295 + Numerator.GetHashCode();
            hashCode = hashCode * -1521134295 + Denominator.GetHashCode();
            return hashCode;
        }
    }
}
