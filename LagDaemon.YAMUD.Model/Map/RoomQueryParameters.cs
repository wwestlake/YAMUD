using LagDaemon.YAMUD.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.Model.Map
{
    public class RoomQueryParameters
    {
        public int? X { get; set; }
        public int? Y { get; set; }
        public int? Level { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Func<IQueryable<Room>, IOrderedQueryable<Room>> OrderBy { get; set; }

        public Expression<Func<Room, bool>> ToExpression()
        {
            var predicate = PredicateBuilder.New<Room>(true);

            if (X.HasValue)
            {
                predicate = predicate.And(room => room.Address.X == X.Value);
            }
            if (Y.HasValue)
            {
                predicate = predicate.And(room => room.Address.Y == Y.Value);
            }
            if (Level.HasValue)
            {
                predicate = predicate.And(room => room.Address.Level == Level.Value);
            }
            if (!string.IsNullOrWhiteSpace(Name))
            {
                predicate = predicate.And(room => room.Name.Contains(Name));
            }
            if (!string.IsNullOrWhiteSpace(Description))
            {
                predicate = predicate.And(room => room.Description.Contains(Description));
            }

            return predicate;
        }
    }
}
