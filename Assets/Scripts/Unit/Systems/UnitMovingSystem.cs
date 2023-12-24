using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unit.Component;
using Unity.Burst;

namespace Unit
{
    namespace System
    {
        public partial struct UnitMovingSystem : ISystem
        {
            [BurstCompile]
            public void OnCreate(ref SystemState state)
            {
                state.RequireForUpdate<UnitMovements>();
            }

            [BurstCompile]
            public void OnUpdate(ref SystemState state)
            {
                var dt = SystemAPI.Time.DeltaTime;

                foreach (var (transform, unitMovements, targetPosition) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<UnitMovements>, RefRW<UnitTargetPosition>>())
                {
                    float3 direction = math.normalize(targetPosition.ValueRW.value - transform.ValueRW.Position);
                    transform.ValueRW.Position += direction * dt * unitMovements.ValueRO.Speed;
                }
            }
        }
    }
}
