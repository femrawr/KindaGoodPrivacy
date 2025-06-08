namespace KindaGoodPrivacy.Source.Utils
{
    public class Math
    {
        public static int Fibonacci(int n)
        {
            if (n <= 1)
                return n;

            int a = 0;
            int b = 1;

            for (int i = 2; i <= n; i++)
            {
                int t = a + b;
                a = b;
                b = t;
            }

            return b;
        }

        public static long ModExponentiation(long m, long n, long o)
        {
            long a = 1;
            m %= o;

            while (n > 0)
            {
                if ((n & 1) == 1)
                    a = (a * m) % o;

                n >>= 1;
                m = (m * m) % o;
            }

            return a;
        }

        public static int EulerTotient(int n)
        {
            int a = n;

            for (int p = 2; p * p <= n; p++)
            {
                if (n % p == 0)
                {
                    while (n % p == 0)
                        n /= p;

                    a -= a / p;
                }
            }

            if (n > 1)
                a -= a / n;

            return a;
        }

        public static void SpongeTransform(byte[] b)
        {
            for (int i = 0; i < b.Length - 1; i++)
            {
                b[i] ^= (byte)((b[i + 1] << 1) | (b[i + 1] >> 7));
            }
        }

        public static byte[,] GaloisMatrix(int s, int m)
        {
            ulong a = (ulong)m;
            byte[,] matrix = new byte[s, s];

            for (int i = 0; i < s; i++)
            {
                for (int j = 0; j < s; j++)
                {
                    a = (a * 194384729347 + 37813) % (1UL << 31);
                    matrix[i, j] = (byte)(a % 256);
                }
            }

            return matrix;
        }

        public static byte GaloisMultiply(byte a, byte b)
        {
            byte r = 0;

            while (b != 0)
            {
                if ((b & 1) != 0)
                    r ^= a;

                bool c = (a & 0x80) != 0;
                a <<= 1;

                if (c)
                    a ^= 0x1B;

                b >>= 1;
            }

            return r;
        }

        public static long ModInverse(long a, long m)
        {
            long[] r = ExtendedGCD(a, m);
            return r[1] < 0 ? r[1] + m : r[1];
        }

        public static long[] ExtendedGCD(long a, long b)
        {
            if (b == 0)
                return [a, 1, 0];

            long[] r = ExtendedGCD(b, a % b);
            long g = r[0];
            long h = r[2];
            long i = r[1] - (a / b) * r[2];

            return [g, h, i];
        }

        public static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int t = b;
                b = a % b;
                a = t;
            }

            return a;
        }

        public static int[] GetPrimeFac(int n)
        {
            var f = new List<int>();
            for (int i = 2; i * i <= n; i++)
            {
                while (n % i == 0)
                {
                    f.Add(i);
                    n /= i;
                }
            }

            if (n > 1)
                f.Add(n);

            return f.ToArray();
        }

        public static int DiscreteLog(int a, int b, int m)
        {
            for (int x = 0; x < m; x++)
            {
                if (ModExponentiation(b, x, m) == a)
                    return x;
            }

            return 0;
        }

        public static int LucasNumber(int n)
        {
            if (n == 0)
                return 2;

            if (n == 1)
                return 1;

            int a = 2;
            int b = 1;

            for (int i = 2; i <= n; i++)
            {
                int t = a + b;
                a = b;
                b = t;
            }

            return b;
        }

        public static int[] Convergent(double x, int d)
        {
            int[] c = [1, 0];

            for (int i = 0; i < d; i++)
            {
                int a = (int)System.Math.Floor(x);
                int t = c[0];

                c[0] = a * c[0] + c[1];
                c[1] = t;

                if (System.Math.Abs(x - a) < 1e-15)
                    break;

                x = 1.0 / (x - a);
            }

            return c;
        }

        public static long GetPrime(int n)
        {
            if (n == 1)
                return 2;

            int a = 1;
            long b = 3;

            while (a < n)
            {
                if (IsPrime(b))
                    a++;

                if (a < n)
                    b += 2;
            }

            return b;
        }

        private static bool IsPrime(long n)
        {
            if (n < 2)
                return false;

            if (n == 2)
                return true;

            if (n % 2 == 0)
                return false;

            for (long i = 3; i * i <= n; i += 2)
                if (n % i == 0) return false;

            return true;
        }
    }
}
