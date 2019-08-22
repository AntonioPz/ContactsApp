using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactsApp
{
    public class Operations
    {
        public static double GenerateRating()
        {
            List<int> numbers = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                numbers.Add(RandomNumber(1, 6));
            }
            return (double)numbers.Sum() / 10;
        }
        static Random random = new Random(Guid.NewGuid().GetHashCode());

        public static int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

    }
}
