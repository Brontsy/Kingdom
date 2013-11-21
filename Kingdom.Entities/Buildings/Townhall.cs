using Kingdom.Core.Enums.Buildings;
using Kingdom.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Entities.Buildings
{
    internal class Townhall : IBuilding
    {
        public BuildingType Type
        {
            get { return BuildingType.Townhall; }
        }
    }
}
