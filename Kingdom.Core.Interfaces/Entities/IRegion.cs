﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Core.Interfaces.Entities
{
    public interface IRegion
    {
        int Id { get; }

        IPosition Position { get; }

        ITile[,] Tiles { get; }
    }
}
