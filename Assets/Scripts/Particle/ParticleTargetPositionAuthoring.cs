using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Particle.Components;

namespace Particle
{
    public class ParticleTargetPositionAuthoring : MonoBehaviour
    {
        [SerializeField] private float3 targetPosition;

        public float3 TargetPosition
        {
            get => targetPosition;
        }
    }

    public class ParticleTargetPositionBaket : Baker<ParticleTargetPositionAuthoring>
    {
        public override void Bake(ParticleTargetPositionAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new ParticleTargetPosition
            {
                Value = authoring.TargetPosition
            });
        }
    }
}
