using Kingdom.Core.Enums.Units;
using Kingdom.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Entities.Units
{
    internal class Archer : IUnit
    {
        public UnitType Type
        {
            get { return UnitType.Archer; }
        }
    }
}
