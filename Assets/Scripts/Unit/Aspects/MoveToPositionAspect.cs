using Helpers.Components;
using Unit.Component;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Unit.Aspects
{
    public readonly partial struct MoveToPositionAspect : IAspect
    {
        private readonly Entity entity;

        private readonly RefRW<LocalTransform> transform;
        private readonly RefRO<UnitMovements> unitMovements;
        private readonly RefRW<UnitTargetPosition> targetPosition;

        public void Move(float deltaTime)
        {
            float3 direction = math.normalize(targetPosition.ValueRW.value - transform.ValueRW.Position);
            transform.ValueRW.Position += direction * deltaTime * unitMovements.ValueRO.Speed;
        }

        public void CheckReachedTargetDistance(RefRW<RandomComponent> randomComponent)
        {
            float reachedTargetDistance = 0.5f;
            if (math.distance(transform.ValueRW.Position, targetPosition.ValueRW.value) <= reachedTargetDistance)
                targetPosition.ValueRW.value = GenerateNewDirection(randomComponent);
        }

        private float3 GenerateNewDirection(RefRW<RandomComponent> randomComponent)
        {
            return new float3(randomComponent.ValueRW.random.NextFloat(0, 15f), 0, randomComponent.ValueRW.random.NextFloat(0, 15f));
        }
    }
}
