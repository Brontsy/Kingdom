using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Core.Interfaces.Entities
{
    public interface IPosition
    {
        int X { get; }

        int Y { get; }

        void SetPosition(int x, int y);
    }


    public interface I2dPosition : IPosition
    {
        IIsoCoordinate ToIsoCoordinate(IRegion region);

        I2dCoordinate To2dCoordinate(IRegion region);
    }

    public interface I2dCoordinate : IPosition
    {
        IIsoCoordinate ToIsoCoordinate(IRegion region);

        I2dPosition To2dPosition();
    }

    public interface IIsoCoordinate : IPosition
    {
        I2dCoordinate To2dCoordinate(IRegion region);

        I2dPosition To2dPosition();
    }
}
