﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kingdom.Web.Models.Position
{
    public class PositionViewModel
    {

        public int X { get; set; }

        public int Y { get; set; }

        public PositionViewModel() { }

        public PositionViewModel(int x, int y)
        {
            this.X = x; 
            this.Y = y;
        }
    }
}