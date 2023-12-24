using Helpers.Components;
using Unit.Component;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Unit.Aspects
{
    public readonly partial struct UnitMoveToPositionAspect : IAspect
    {
        private readonly Entity entity;

        private readonly RefRW<LocalTransform> transform;
        private readonly RefRO<UnitMovements> unitMovements;
        private readonly RefRW<UnitTargetPosition> targetPosition;

        public void Move(float deltaTime)
        {
            float3 direction = math.normalize(targetPosition.ValueRW.Value - transform.ValueRW.Position);
            transform.ValueRW.Position += direction * deltaTime * unitMovements.ValueRO.Speed;
        }

        public void CheckReachedTargetDistance2D(RefRW<RandomComponent> randomComponent)
        {
            float reachedTargetDistance = 0.5f;
            if (math.distance(transform.ValueRW.Position, targetPosition.ValueRW.Value) <= reachedTargetDistance)
                targetPosition.ValueRW.Value = GenerateNewDirection2D(randomComponent);
        }

        private float3 GenerateNewDirection2D(RefRW<RandomComponent> randomComponent)
        {
            float size = 5;
            return new float3(randomComponent.ValueRW.random.NextFloat(-size, size), 0, randomComponent.ValueRW.random.NextFloat(-size, size));
        }
    }
}
