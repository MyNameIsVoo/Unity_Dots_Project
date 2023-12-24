using Unity.Entities;
using Unity.Burst;
using Helpers.Components;
using Unity.Jobs;
using Unity.Collections.LowLevel.Unsafe;
using Particle.Aspects;
using Particle.Components;

namespace Particle.System
{
    [BurstCompile]
    public partial struct ParticleMovingSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ParticleMovements>();
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

            new CheckReachedTargetDistance3DJob
            {
                randomComponent = randomComponent
            }.Run();
        }
    }

    [BurstCompile]
    public partial struct MoveJob : IJobEntity
    {
        public float deltaTime;

        public void Execute(ParticleMoveToPositionAspect moveToPositionAspect)
        {
            moveToPositionAspect.Move(deltaTime);
        }
    }

    [BurstCompile]
    public partial struct CheckReachedTargetDistance3DJob : IJobEntity
    {
        [NativeDisableUnsafePtrRestriction] public RefRW<RandomComponent> randomComponent;

        public void Execute(ParticleMoveToPositionAspect moveToPositionAspect)
        {
            moveToPositionAspect.CheckReachedTargetDistance3D(randomComponent);
        }
    }
}
