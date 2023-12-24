using Helpers.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Particle.Components;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Transforms;

namespace Particle.Systems
{
    [BurstCompile]
    public partial struct ParticleSpawnSystem : ISystem
    {
        public EntityQuery particleEntityQuery;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            particleEntityQuery = state.GetEntityQuery(typeof(ParticleTag));
        }

        public void OnDestroy(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) 
        {
            if (SystemAPI.TryGetSingleton(out ParticleBaseComponent particleBaseComponent) &&
                    particleEntityQuery.CalculateEntityCount() < particleBaseComponent.AmountParticles && particleBaseComponent.AmountParticles > 0)
            {
                EntityCommandBuffer.ParallelWriter ecb = GetEntityCommandBuffer(ref state);
                RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();

                for (int i = 0; i < particleBaseComponent.AmountParticles; i++)
                {
                    JobHandle jobHandle = new ProcessSpawnerJob
                    {
                        Ecb = ecb,
                        randomComponent = randomComponent
                    }.ScheduleParallel(state.Dependency);
                    jobHandle.Complete();
                }
            }
        }

        private EntityCommandBuffer.ParallelWriter GetEntityCommandBuffer(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            return ecb.AsParallelWriter();
        }
    }

    [BurstCompile]
    public partial struct ProcessSpawnerJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter Ecb;

        [NativeDisableUnsafePtrRestriction] public RefRW<RandomComponent> randomComponent;

        private void Execute([ChunkIndexInQuery] int chunkIndex, ref Spawner spawner)
        {
            Entity newEntity = Ecb.Instantiate(chunkIndex, spawner.Prefab);

            LocalTransform localTransform = LocalTransform.FromPosition(spawner.SpawnPosition);
            Ecb.SetComponent(chunkIndex, newEntity, new LocalTransform { 
                Position = localTransform.Position,
                Rotation = localTransform.Rotation,
                Scale = 0.05f
            });
            Ecb.SetComponent(chunkIndex, newEntity, new ParticleMovements{
                Speed = randomComponent.ValueRW.random.NextFloat(1f, 4f)
            });
        }
    }
}