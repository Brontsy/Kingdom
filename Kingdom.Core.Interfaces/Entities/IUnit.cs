using Kingdom.Core.Enums.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Core.Interfaces.Entities
{
    public interface IUnit
    {
        UnitType Type { get; }
    }
}
