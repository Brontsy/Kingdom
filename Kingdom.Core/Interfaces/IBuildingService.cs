using Kingdom.Core.Enums.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Core.Interfaces
{
    public interface IBuildingService
    {
        void Add(int regionId, int x, int y, BuildingType type);
    }
}
