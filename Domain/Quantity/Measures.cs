using Abc.Data.Quantity;
using Abc.Domain.Common;

namespace Abc.Domain.Quantity
{
    public sealed class Measures: Entity<MeasureData>
    {
        public Measures(): this(null) { }
        public Measures(MeasureData data) : base(data) { }
    }
}
