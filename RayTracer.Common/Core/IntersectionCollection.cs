using System.Collections.Generic;

namespace RayTracer.Common.Core
{
    public class IntersectionCollection : List<Intersection>
    {
        public IntersectionCollection() { }
        
        public IntersectionCollection(IEnumerable<Intersection> intersections) : base(intersections) { }
        
        public IntersectionCollection(params Intersection[] intersections) : base(intersections) { }
        
        public Intersection? GetHit()
        {
            var result = (Intersection?) null;
            foreach (var item in this)
            {
                if (item.Time >= 0 && (result == null || result.Value.Time > item.Time))
                {
                    result = item;
                }
            }

            return result;
        }
    }
}