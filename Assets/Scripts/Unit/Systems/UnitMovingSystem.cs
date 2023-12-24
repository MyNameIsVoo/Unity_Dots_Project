using Unity.Entities;
using Unity.Burst;
using Unit.Aspects;
using Helpers.Components;
using Unity.Jobs;
using Unity.Collections.LowLevel.Unsafe;
using Unit.Component;

namespace Unit
{
    namespace System
    {
        [BurstCompile]
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
                RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();
                
                JobHandle jobHandle = new MoveJob 
                {
                    deltaTime = dt
                }.ScheduleParallel(state.Dependency);
                jobHandle.Complete();
                
                new CheckReachedTargetDistance2DJob
                { 
                    randomComponent = randomComponent 
                }.Run();
            }
        }

        [BurstCompile]
        public partial struct MoveJob : IJobEntity
        {
            public float deltaTime;

            public void Execute(UnitMoveToPositionAspect moveToPositionAspect)
            {
                moveToPositionAspect.Move(deltaTime);
            }
        }

        [BurstCompile]
        public partial struct CheckReachedTargetDistance2DJob : IJobEntity
        {
            [NativeDisableUnsafePtrRestriction] public RefRW<RandomComponent> randomComponent;

            public void Execute(UnitMoveToPositionAspect moveToPositionAspect)
            {
                moveToPositionAspect.CheckReachedTargetDistance2D(randomComponent);
            }
        }
    }
}
