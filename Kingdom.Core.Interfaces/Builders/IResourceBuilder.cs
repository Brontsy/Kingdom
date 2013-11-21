using Kingdom.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Core.Interfaces.Builders
{
    public interface IResourceBuilder
    {
        void BuildResources(IList<IRegion> regions);
    }
}
