using Kingdom.Core.Enums.Buildings;
using Kingdom.Core.Enums.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Core.Interfaces.Entities
{
    public interface IBuilding
    {
        BuildingType Type { get; }
    }
}
