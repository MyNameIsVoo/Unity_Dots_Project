using Unity.Entities;
using Unity.Mathematics;

namespace Unit
{
    namespace Component
    {
        public struct UnitBaseComponent : IComponentData
        {
            public int AmountUnits;
        }

        public struct UnitTag : IComponentData
        {
            
        }

        public struct UnitMovements : IComponentData
        {
            public float Speed;
        }

        public struct UnitTargetPosition : IComponentData
        {
            public float3 Value;
        }
    }
}