//using Kingdom.Core.Enums.Buildings;
//using Kingdom.Core.Interfaces.Entities;
//using Kingdom.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Kingdom.Resolvers.Buildings
//{
//    internal class BuildingResolver : IAggregateBuildingResolver
//    {
//        private Dictionary<BuildingType, IBuildingResolver> _resolvers;

//        public BuildingResolver(IList<IBuildingResolver> resolvers)
//        {
//            this._resolvers = new Dictionary<BuildingType, IBuildingResolver>();

//            foreach (IBuildingResolver resolver in resolvers)
//            //{
//                this._resolvers.Add(resolver.Type, resolver);
//            }
//        }

//        public IBuilding Resolve(Building building)
//        {
//            if (building != null)
//            {
//                return this._resolvers[building.Type].Resolve(building);
//            }

//            return null;
//        }

//        public IBuilding Resolve(BuildingType type)
//        {
//            return this._resolvers[type].Resolve();
//        }
//    }
//}
