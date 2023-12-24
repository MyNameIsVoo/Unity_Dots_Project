using Helpers.Components;
using Particle.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Particle.Aspects
{
    public readonly partial struct ParticleMoveToPositionAspect : IAspect
    {
        private readonly Entity entity;

        private readonly RefRW<LocalTransform> transform;
        private readonly RefRO<ParticleMovements> particleMovements;
        private readonly RefRW<ParticleTargetPosition> targetPosition;

        public void Move(float deltaTime)
        {
            float3 direction = math.normalize(targetPosition.ValueRW.Value - transform.ValueRW.Position);
            transform.ValueRW.Position += direction * deltaTime * particleMovements.ValueRO.Speed;
        }

        public void CheckReachedTargetDistance3D(RefRW<RandomComponent> randomComponent)
        {
            float reachedTargetDistance = 0.5f;
            if (math.distance(transform.ValueRW.Position, targetPosition.ValueRW.Value) <= reachedTargetDistance)
                targetPosition.ValueRW.Value = GenerateNewDirection3D(randomComponent);
        }

        private float3 GenerateNewDirection3D(RefRW<RandomComponent> randomComponent)
        {
            float size = 5;
            return new float3(randomComponent.ValueRW.random.NextFloat(-size, size), randomComponent.ValueRW.random.NextFloat(-size, size), randomComponent.ValueRW.random.NextFloat(-size, size));
        }
    }
}
