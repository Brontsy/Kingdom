using Kingdom.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Core.Interfaces.Regions
{
    public interface IRegionResolver
    {
        IRegion CreateRegion(int x, int y, int size);
    }
}
