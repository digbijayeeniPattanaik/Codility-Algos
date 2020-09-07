using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility
{
    class Tree
    {
        public int x;
        public Tree l;
        public Tree r;

        public Tree(int x, Tree l, Tree r)
        {
            this.x = x;
            this.l = l;
            this.r = r;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            var ou = compress("ABBBCCDDCCC", 3);
        }

        static int get_length(int n, int based = 10)
        {
            if (n < 2) return n;
            int ret = 1;

            while (n > 0)
            {
                ret++;
                n /= based;
            }
            return ret;
        }

        static int compress(string S, int K)
        {

            int L = S.Length;
            if (L <= K) return 0;

            int i, ans = L - K;
            int count_start = 0, cost_start = 0;
            int count_end = 0, cost_end = 0;

            for (i = K; i < L && S[i] == S[K]; i++) count_end++;
            cost_end = get_length(count_end);
            for (; i < L;)
            {
                int cnt = 0;
                char d = S[i];
                for (; i < L && d == S[i]; i++) cnt++;
                cost_end += get_length(cnt);
            }

            ans = Math.Min(ans, cost_end);
            if (K == 0) return ans;

            for (i = 1; i <= L - K; i++)
            {// first delete i, last=i+k-1
                if (i == 1) { count_start = 1; cost_start = 1; }
                else
                {
                    if (S[i - 1] == S[i - 2])
                    {
                        cost_start += get_length(count_start + 1) - get_length(count_start);
                        count_start++;
                    }
                    else
                    {
                        cost_start++;
                        count_start = 1;
                    }
                }

                if (i == L - K)
                {
                    cost_end = 0;
                    count_end = 0;
                }
                else if (S[i + K - 1] == S[i + K])
                {
                    cost_end += get_length(count_end - 1) - get_length(count_end);
                    count_end--;
                }
                else
                {
                    cost_end--;
                    count_end = 0;
                    for (int j = i + K; j < L && S[j] == S[i + K]; j++) count_end++;
                }

                int cost = cost_start + cost_end;
                if (i != L - K && S[i - 1] == S[i + K]) cost += get_length(count_start + count_end) - get_length(count_start) - get_length(count_end);
                ans = Math.Min(ans, cost);
            }

            return ans;
        }
        public static int Toptal2(String S, int K)
        {
            // write your code in Java SE 8
            char[] chars = S.ToCharArray();
            int length = chars.Length;
            string temp = string.Empty;
            char currentChar = new char();
            int charCount = 0;
            string compressedString = string.Empty;
            for (int i = 0; i < length; i++)
            {
                if (string.IsNullOrWhiteSpace(temp))
                {
                    currentChar = chars[i];
                    charCount = 1;
                    temp = Convert.ToString(currentChar);
                }
                else
                {
                    if (currentChar == chars[i])
                    {
                        charCount++;
                        temp = charCount + Convert.ToString(currentChar);
                    }
                    else
                    {
                        compressedString += temp;
                        currentChar = chars[i];
                        charCount = 1;
                        temp = Convert.ToString(currentChar);
                    }
                }
            }
            compressedString += temp;

            if (compressedString.ToCharArray().Where(a => char.IsNumber(a)).Count() == 0) return compressedString.Length - K;
            ////return finalcompressedString.Length;
            Dictionary<int, char> toBeRemoved = new Dictionary<int, char>();
            var compressedChar = compressedString.ToCharArray();
            int len = compressedChar.Length;
            for (int i = 0; i < len; i++)
            {
                if (char.IsNumber(compressedChar[i]))
                {
                    if (Convert.ToInt32(compressedChar[i]) < K)
                        continue;
                }
                else
                {
                    if ((i - 1) >= 0)
                    {
                        if (char.IsNumber(compressedChar[i - 1]))
                        {
                            if (Convert.ToInt32(compressedChar[i - 1]) < K)
                                continue;
                        }
                        else
                        {
                            if (toBeRemoved.Count < K) toBeRemoved.Add(i, compressedChar[i]);
                            else break;
                        }
                    }
                }
            }

            ////string finalcompressedString = string.Empty;
            for (int i = 0; i < toBeRemoved.Count; i++)
            {
                compressedString.Remove(toBeRemoved.ElementAt(i).Key);
            }

            return compressedString.Length;
        }

        public static int Get(String S)
        {
            if (S.Count(a => a == 'a') == 3 && S.Length == 3) return 0;
            var parts = S.Split('a');
            if (parts.Length > 0)
            {

                if ((parts.Length - 1) % 3 != 0)
                    return 0;
                int count = (parts.Length - 1) / 3;
                int partA1 = parts[count].Length + 1;
                int partA2 = parts[count * 2].Length + 1;

                return partA1 * partA2;
            }
            parts = S.Split('b');
            if (parts.Length > 0)
            {

                if ((parts.Length - 1) % 3 != 0)
                    return 0;
                int count = (parts.Length - 1) / 3;
                int partA1 = parts[count].Length + 1;
                int partA2 = parts[count * 2].Length + 1;

                return partA1 * partA2;
            }

            return 0;
        }
        public static int smallestPossible(int[] A)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            //[1, 3, 6, 4, 1, 2],
            // [1, 1, 2, 3, 4, 6]
            int[] B = A.OrderBy(a => a).ToArray();
            int minVal = 1;
            if (!B.Any(a => a != 1)) return minVal;
            minVal = B.Min() < 1 ? 1 : B.Min();
            for (int i = 1; i <= B.Length; i++)
            {
                if (B[i] >= minVal && Math.Abs(B[i] - B[i - 1]) >= minVal)
                {

                }
                else
                {
                    minVal = B[i] + 1;
                    break;
                }
            }

            return minVal;
        }

        ////public int solution(int K, int M, int[] A)
        ////{
        ////    // write your code in Java SE 8
        ////    int maxInt = A.Max();
        ////    int sum = A.Sum();

        ////    int mid = maxInt + sum;
        ////}
        public int MaxProduct(int[] A)
        {
            int[] B = A.OrderByDescending(a => a).ToArray();

            return B[0] * B[1] * B[2];
        }

        public static int LengthOfTree(Tree T)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            int leftlength = 0, rightLength = 0;
            if (T == null) return 0;
            if (T.l == null && T.r == null) return 0;
            if (T.l != null)
                leftlength = LengthOfTree(T.l);
            if (T.r != null)
                rightLength = LengthOfTree(T.r);

            return Math.Max(leftlength, rightLength) + 1;
        }


        public static int MinPerimeter(int N)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            ////List<int> factors = new List<int>();
            int min = 0;
            int peri = 0;
            for (int i = 1; i <= N; i++)
            {
                if (N % i == 0)
                {
                    ////factors.Add(2 * (i + (N / i)));
                    peri = 2 * (i + (N / i));
                    if (min == 0) min = peri;
                    if (peri < min) min = peri;
                }
            }

            return min;
        }

        public static int FindPeak(int[] A)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            int len = A.Length;

            if (len < 3) return 0;
            List<int> peaks = new List<int>();
            for (int i = 1; i < len; i++)
            {
                if (A[i - 1] < A[i] && (i + 1) < len && A[i] > A[i + 1]) peaks.Add(i);
            }
            int peaksLen = peaks.Count;
            if (peaksLen <= 1) return peaksLen;
            int counter = 1;
            while (counter == 1)
            {
                bool removed = false;
                for (int i = 0; i < peaksLen; i++)
                {
                    if ((i + 1) < peaksLen && Math.Abs(peaks[i] - peaks[i + 1]) < peaksLen)
                    {
                        peaksLen--;
                        peaks.RemoveAt(i + 1);
                        removed = true;
                        break;
                    }
                }


                if (removed) counter = 1;
                else counter = 0;
                ////if (peaksLen <= 1)
                ////{
                ////    counter = 0;
                ////}
            }

            if (peaksLen <= 1) return peaksLen;
            return peaksLen;
        }

        public int solution(int N)
        {
            int count = 0;
            for (int i = 1; i <= N; i++)
            {
                if (N % i == 0) count++;
            }
            return count;
        }

        public int PermCheck(int[] A)
        {
            int[] B = A.OrderBy(a => a).ToArray();
            int len = B.Length;
            int output = 1;
            if (len == 1) return 0;
            if (B[0] != 1) return 0;

            for (int i = 0; i < len; i++)
            {
                if ((i + 1) < len && Math.Abs(B[i] - B[i + 1]) != 1)
                {
                    output = 0;
                    break;
                }
                else if ((i + 1) < len && B[i] == B[i + 1])
                {
                    output = 0;
                    break;
                }
            }

            return output;
        }

        public static int dominator(int[] A)
        {
            //var output = A.Select((obj, index) => new { Obj = obj, Index = index });
            int maxValue = A.Length / 2;

            //var groupNumber = output.GroupBy(a => a.Obj).Select(a => new { Num = a.Key, Count = a.Count() });
            //.Max(a => a.Count)
            List<Tuple<int, int, int>> keyValuePairs = new List<Tuple<int, int, int>>();
            for (int i = 0; i < A.Length; i++)
            {
                if (!keyValuePairs.Any(a => a.Item2 == A[i]))
                {
                    keyValuePairs.Add(Tuple.Create(i, A[i], A.Count(a => a == A[i])));
                }
            }

            Console.WriteLine(JsonConvert.SerializeObject(keyValuePairs));

            var output = keyValuePairs.Any(a => a.Item3 > maxValue) ? keyValuePairs.FirstOrDefault(a => a.Item3 > maxValue).Item1 : -1;

            return output;
            // write your code in Java SE 8
        }

        public static int PassingCars(int[] A)
        {
            int length = A.Length;
            int totalCount = 0;
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine(string.Join(",", A.Skip(i).ToList()));
                Console.WriteLine(string.Join(",", A.Skip(i).Take(length - i).ToList()));
                if (A[i] == 0) totalCount += A.Skip(i).Take(length - i).Count(a => a == 1);
            }
            // write your code in Java SE 8

            return totalCount;
        }
        public static int solution(int[] A, int[] B)
        {
            int length = A.Length;
            Stack<int> test = new Stack<int>();
            for (int i = 0; i < length; i++)
            {
                if (!test.Any()) test.Push(i);
                else
                {
                    while (test.Any() && A[test.Peek()] > A[i] && B[test.Peek()] > B[i])
                    {
                        test.Pop();
                    }

                    test.Push(i);
                }
            }

            return test.Count;
            // write your code in Java SE 8
        }
        public static int solution(string S)
        {
            //"(", "{", "[", "]", "}" ")"

            Dictionary<char, char> pairs = new Dictionary<char, char>();
            pairs.Add('(', ')');
            pairs.Add('{', '}');
            pairs.Add('[', ']');
            pairs.Add(')', '(');
            pairs.Add('}', '{');
            pairs.Add(']', '[');
            var scharList = S.ToCharArray();
            int length = scharList.Length;
            int outcome = 1;
            for (int i = 0; i < length; i++)
            {
                var pair = pairs.FirstOrDefault(a => a.Key.Equals(scharList[i]));
                if (!pair.Value.Equals(scharList[length - i - 1]))
                {
                    outcome = 0;
                    break;
                }
            }

            return outcome;
            // write your code in C# 6.0 with .NET 4.5 (Mono)
        }

        public static int solution6(int[] A)
        {
            var length = A.Length;
            List<int> output = new List<int>();
            for (int i = 0; i < length; i++)
            {
                int sumleft = 0, sumright = 0;
                if (i == 0) sumleft = A[i];
                else sumleft = A.Take(i).Sum();
                Console.WriteLine(string.Join(",", A.Take(i).ToList()));

                Console.WriteLine(string.Join(",", A.Skip(i).Take(length - i)));
                sumright = A.Skip(i).Take(length - i).Sum();

                output.Add(Math.Abs(sumright - sumleft));
            }

            return output.Min();
            // write your code in C# 6.0 with .NET 4.5 (Mono)
        }

        public static int solution5(int[] A)
        {
            var test = A.OrderBy(a => a).ToArray();
            var length = A.Length;
            int outcome = 0;
            for (int i = 0; i < length; i++)
            {
                if ((i + 1) < length && Math.Abs(A[i] - A[i + 1]) != 1)
                {
                    outcome = Math.Min(A[i], A[i + 1]) + 1;
                    break;
                }
            }

            return outcome;
            // write your code in C# 6.0 with .NET 4.5 (Mono)
        }
        public static int solution(int X, int Y, int D)
        {
            int maxDistance = Y - X;
            return Convert.ToInt32(Math.Ceiling((double)maxDistance / D));
            // write your code in C# 6.0 with .NET 4.5 (Mono)
        }

        public static int solution3(int[] A)
        {
            var test = A.GroupBy(a => a).Select(b => new { num = b.Key, count = b.Count() });
            return test.Where(a => a.count % 2 != 0).FirstOrDefault().num;
            // write your code in Java SE 8
        }
        public static int[] solution2(int[] A, int K)
        {
            int length = A.Length;
            int[] outcome = new int[length];
            int[] test = A;
            // write your code in Java SE 8
            for (int i = 0; i < K; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (j == 0)
                    {
                        outcome[j] = test[length - 1];
                    }
                    else
                    {
                        outcome[j] = test[j - 1];
                    }
                }

                test = outcome.ToArray();
            }

            return outcome;
        }

        public static int solution1(int N)
        {
            //biggest binary gap
            string binary = Convert.ToString(1041, 2);
            var trim = binary.Trim('0');//// trimming the leading 0's
            var split = trim.Split('1');
            var max = split.Max(a => a.Length);
            return max;
        }

        public static int solution(int[] A)
        {
            var constantMinValue = 1;
            ////var minValue = A.Min(a => a);

            ////if (minValue > 0)
            ////    return minValue + 1;
            ////else if (!A.Any(a => a == constantMinValue)) return constantMinValue;
            ////else return constantMinValue;
            A = A.OrderBy(x => x).ToArray();
            int length = A.Length;
            for (int i = 0; i < length; i++)
            {
                if (A[i] <= 0) continue;
                else if (A[i] == constantMinValue)
                    constantMinValue++;
            }

            return constantMinValue;
        }
    }
}
