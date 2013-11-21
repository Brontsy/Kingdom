//using Kingdom.Core.Enums.Units;
//using Kingdom.Core.Interfaces.Entities;
//using Kingdom.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Kingdom.Resolvers.Units
//{
//    internal class UnitResolver : IAggregateUnitResolver
//    {
//        private Dictionary<UnitType, IUnitResolver> _resolvers;

//        public UnitResolver(IList<IUnitResolver> resolvers)
//        {
//            this._resolvers = new Dictionary<UnitType, IUnitResolver>();

//            foreach (IUnitResolver resolver in resolvers)
//            {
//                this._resolvers.Add(resolver.Type, resolver);
//            }
//        }

//        public IList<IUnit> Resolve(IList<Unit> dbUnits)
//        {
//            IList<IUnit> units = new List<IUnit>();

//            foreach (Unit unit in dbUnits)
//            {
//                units.Add(this._resolvers[unit.Type].Resolve(unit));
//            }

//            return units;
//        }
//    }
//}
