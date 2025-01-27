using System.Collections.Generic;
using System.Numerics;
using Dargon.PlayOn.DataStructures;
using Dargon.PlayOn.Geometry;
#if use_fixed
using cDouble = FixMath.NET.Fix64;
#else
using cDouble = System.Double;
#endif

namespace Dargon.PlayOn.Foundation.Terrain.Declarations {
   public interface IHoleStaticMetadata {
      bool TryProjectOnto(HoleInstanceMetadata instanceMetadata, SectorNodeDescription sectorNodeDescription, out IReadOnlyList<Polygon2> projectedHoleIncludedContours, out IReadOnlyList<Polygon2> projectedHoleExcludedContours);
      AxisAlignedBoundingBox3 ComputeWorldAABB(Matrix4x4 worldTransform);
      bool ContainsPoint(HoleInstanceMetadata instanceMetadata, DoubleVector3 pointWorld, cDouble agentRadius);
   }
}