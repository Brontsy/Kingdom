using Kingdom.Core.Enums.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Entities.Buildings
{
    public class Wall
    {
        public BuildingType Type
        {
            get { return BuildingType.Wall; }
        }
    }
}
