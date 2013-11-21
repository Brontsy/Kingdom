using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Common.Utils
{
    public static class RandomUtil
    {
        public static int Random(int min, int max)
        {
            var rand = new Random();
            while (true)
            {
                int randomNumber = rand.Next(1, 999999);
                if (randomNumber >= min && randomNumber <= max)
                {
                    return randomNumber;
                }
            }
        }
    }
}
