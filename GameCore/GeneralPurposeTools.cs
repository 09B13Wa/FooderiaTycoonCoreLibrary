using System;
using System.Xml.Xsl;

namespace GameCore
{
    public static class GeneralPurposeTools
    {
        static public string SqueezeString(string input)
        {
            int firstIndex = 0;
            int lastIndex = 0;
            int numberOfChars = input.Length;
            int offSet;
            while (firstIndex < numberOfChars && input[firstIndex] == ' ' || input[firstIndex] == '\n' || input[firstIndex] == '\t')
                firstIndex += 1;
            while (lastIndex > firstIndex && input[lastIndex] == ' ' || input[lastIndex] == '\n' || input[lastIndex] == '\t')
                lastIndex -= 1;
            lastIndex += 1;
            string shortenedString = "";
            for (int i = firstIndex; i < lastIndex; i++)
                shortenedString += input[i];
            return shortenedString;
        }
        
        static public int GreatestCommonDvididor(int a, int b)
        {
            int result = a;
            int d = b;
            while (d != 0)
            {
                int temp = d;
                d = result % d;
                result = temp;
            }

            return result;
        }

        static public int LeastCommonMultiple(int a, int b)
        {
            int gcd = GreatestCommonDvididor(a, b);
            int lcm = a * b;
            if (gcd != 0)
                lcm /= gcd;
            else
                throw new DivideByZeroException();
            return lcm;
        }
        public static long TimeToGameTicks(UnitOfMeasurement unit, int value)
        {
            int multiplier;
            switch (unit)
            {
                case UnitOfMeasurement.GameTick:
                    multiplier = 1;
                    break;
                case UnitOfMeasurement.Second:
                    multiplier = 60;
                    break;
                case UnitOfMeasurement.Minute:
                    multiplier = 3600;
                    break;
                case UnitOfMeasurement.Hour:
                    multiplier = 216000;
                    break;
                case UnitOfMeasurement.Day:
                    multiplier = 5184000;
                    break;
                case UnitOfMeasurement.Month:
                    multiplier = 158112000;
                    break;
                case UnitOfMeasurement.Year:
                    multiplier = 1893456000;
                    break;
                default:
                    throw new ArgumentException($"Error: {unit} is not a valid time measurement");
                    break;
            }

            return value * multiplier;
        }
    }

    public class Fraction
    {
        public int Numerator { get; private set; }
        public int Denominator { get; private set; }

        public Fraction(int top, int bottom)
        {
            Numerator = top;
            if (bottom == 0)
                throw new DivideByZeroException();
            else
                Denominator = bottom;
        }

        public void Simplify()
        {
            int gcd = GeneralPurposeTools.GreatestCommonDvididor(Numerator, Denominator);
            Numerator /= gcd;
            Denominator /= gcd;
        }
        public static Fraction operator -(Fraction left)
        {
            return new Fraction(-left.Numerator, left.Denominator);
        }

        public static Fraction operator +(Fraction left, Fraction right)
        {
            int lcm = GeneralPurposeTools.LeastCommonMultiple(left.Denominator, right.Denominator);
            int leftMuliplication = lcm / left.Denominator;
            int rightMultiplication = lcm / right.Denominator;
            int leftNumerator = leftMuliplication * left.Numerator;
            int rightNumerator = rightMultiplication * right.Numerator;
            return new Fraction(leftNumerator + rightNumerator, lcm);
        }
        
        public static Fraction operator -(Fraction left, Fraction right)
        {
            int lcm = GeneralPurposeTools.LeastCommonMultiple(left.Denominator, right.Denominator);
            int leftMuliplication = lcm / left.Denominator;
            int rightMultiplication = lcm / right.Denominator;
            int leftNumerator = leftMuliplication * left.Numerator;
            int rightNumerator = rightMultiplication * right.Numerator;
            return new Fraction(leftNumerator - rightNumerator, lcm);
        }

        public static Fraction operator !(Fraction fraction)
        {
            Fraction invertedFraction;
            if (fraction.Numerator != 0)
                invertedFraction = new Fraction(fraction.Denominator, fraction.Numerator);
            else
                throw new DivideByZeroException();
            return invertedFraction;
        }
        
        public static Fraction operator /(Fraction left, Fraction right)
        {
            return left * !right;
        }
        
        public static Fraction operator *(Fraction left, Fraction right)
        {
            Fraction multipliedResult =
                new Fraction(left.Numerator * right.Numerator, right.Numerator * right.Numerator);
            multipliedResult.Simplify();
            return multipliedResult;
        }
    }
}