using Kingdom.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Core.Interfaces
{
    public interface IRegionService
    {
        IRegion GetRegion(int regionId);

        IRegion GetRegion(int x, int y);

        IList<IRegion> GetRegions();

        void CreateRegion(int size);

        IRegion SaveRegion(IRegion region);

        void CreateRegions(int x, int y);

        IList<IRegion> GetRegions(int topLeftX, int topLeftY, int topRightX, int topRightY, int bottomLeftX, int bottomLeftY, int bottomRightX, int bottomRightY, IList<int> exclude);

        IList<IRegion> GetVisibleRegionPositions(int screenWidth, int screenHeight, int x, int y, IList<int> exclude);
        
        IList<int> GetVisibleRegionIds(int screenWidth, int screenHeight, int x, int y);

        
    }
}
