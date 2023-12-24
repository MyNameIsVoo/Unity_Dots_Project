using Unity.Entities;
using Unit.Component;
using Unity.Burst;
using Unit.Aspects;
using Helpers.Components;
using Unity.Jobs;
using Unity.Collections.LowLevel.Unsafe;

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
                RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();

                JobHandle jobHandle = new MoveJob {
                    deltaTime = dt
                }.ScheduleParallel(state.Dependency);
                jobHandle.Complete();

                new CheckReachedTargetDistanceJob { 
                    randomComponent = randomComponent 
                }.Run();
            }
        }

        public partial struct MoveJob : IJobEntity
        {
            public float deltaTime;

            public void Execute(MoveToPositionAspect moveToPositionAspect)
            {
                moveToPositionAspect.Move(deltaTime);
            }
        }

        public partial struct CheckReachedTargetDistanceJob : IJobEntity
        {
            [NativeDisableUnsafePtrRestriction] public RefRW<RandomComponent> randomComponent;

            public void Execute(MoveToPositionAspect moveToPositionAspect)
            {
                moveToPositionAspect.CheckReachedTargetDistance(randomComponent);
            }
        }

    }
}
