using Unity.Entities;
using Unity.Mathematics;

namespace Helpers.Components
{
    public struct RandomComponent : IComponentData
    {
        public Random random;
    }
}
