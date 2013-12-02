using Kingdom.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Core.Interfaces.Repositories
{
    public interface IRegionRepository
    {
        IRegion GetRegion(int regionId);

        IRegion GetRegion(int x, int y);

        IList<IRegion> GetRegions(IPosition topLeft, IPosition topRight, IPosition bottomLeft, IPosition bottomRight, IList<int> exclude);

        IList<IRegion> GetRegions(int minX, int maxX, int minY, int maxY, IList<int> exclude);

        IList<IRegion> GetRegions();

        IRegion SaveRegion(IRegion region);

        IList<int> GetRegionIds(int minX, int maxX, int minY, int maxY);
    }
}
