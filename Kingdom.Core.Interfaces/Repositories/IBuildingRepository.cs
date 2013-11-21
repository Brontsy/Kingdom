using Kingdom.Core.Enums.Buildings;
using Kingdom.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Core.Interfaces.Repositories
{
    public interface IBuildingRepository
    {
        void Add(int regionId, int x, int y, BuildingType type);
    }
}
