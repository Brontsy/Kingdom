using Kingdom.Core.Interfaces.Entities;
using Kingdom.Core.Interfaces.Regions;
using Kingdom.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Core.Sql.Resolvers
{
    internal class RegionResolver : IRegionResolver
    {
        public IRegion CreateRegion(int x, int y, int size)
        {
            int arraySize = (int)Math.Sqrt(size);

            return new Region(x, y);
        }
    }
}
