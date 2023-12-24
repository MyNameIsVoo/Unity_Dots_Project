using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unit.Component;

namespace Unit
{
    public class UnitTargetPositionAuthoring : MonoBehaviour
    {
        [SerializeField] private float3 targetPosition;

        public float3 TargetPosition
        {
            get => targetPosition;
        }
    }

    public class UnitTargetPositionBaket : Baker<UnitTargetPositionAuthoring>
    {
        public override void Bake(UnitTargetPositionAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new UnitTargetPosition
            {
                value = authoring.TargetPosition
            });
        }
    }
}
