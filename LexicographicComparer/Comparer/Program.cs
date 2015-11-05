namespace Comparer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var s = new[] { "add", "acc", "cdc" };
            var c = "acd";
            
            foreach(var x in Mysort(s, c))
                Console.WriteLine(x + ",");

            Console.ReadLine();
        }

        private static string[] Mysort(string[] str, string sortBy)
        {
            if (str == null || !str.Any())
                throw new ArgumentNullException("str");

            if (String.IsNullOrEmpty(sortBy))
                throw new ArgumentNullException("sortBy");

            Dictionary<char, int> sortDictionary = new Dictionary<char, int>();

            for (int j = 0; j < sortBy.Length; j++)
            {
                sortDictionary.Add(sortBy[j], j+1);
            }

            var comparer = new LexicographicComparer(sortDictionary);

            //Using Bubble Sort
            for (var i = 0; i < str.Length - 1; i++)
            {
                for (var j = 0; j < str.Length - i - 1; j++)
                {
                    if (comparer.Compare(str[j].ToCharArray(), str[j + 1].ToCharArray()) == 1)
                    {
                        var temp = str[j];
                        str[j] = str[j + 1];
                        str[j + 1] = temp;
                    }
                }
            }

            return str;
        }
    }

    class LexicographicComparer : Comparer<char[]>
    {
        public Dictionary<char, int> SortDictionary { get; set; }

        public LexicographicComparer(Dictionary<char, int> sortDictionary)
        {
            SortDictionary = sortDictionary;
        }

        public override int Compare(char[] x, char[] y)
        {
            if (x == y) 
                return 0; //Saying they are equal, no need to swap

            if (!y.Any())
                return 1;

            if (!x.Any())
                return -1;

            var tempX = "";
            foreach (var c in x)
            {
                tempX += SortDictionary[c];
            }

            var tempY = "";
            foreach (var c in y)
            {
                tempY += SortDictionary[c];
            }
            
            if (int.Parse(tempX) > int.Parse(tempY))
                return 1; // They have to be swapped
            

            return -1; // No need to swap
        }
    }
}