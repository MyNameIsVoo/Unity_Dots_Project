using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Helpers.Components;
using Unit.Component;
using Unity.Collections.LowLevel.Unsafe;

namespace Helpers.Systems
{
    [BurstCompile] // Работает и без него (я так понял он ускоряет код, путем конвертации его в более производительный - см Jobs/BurstInspector)
    public partial struct SpawnerSystem : ISystem
    {
        public EntityQuery unitEntityQuery;

        public void OnCreate(ref SystemState state) 
        {
            unitEntityQuery = state.GetEntityQuery(typeof(UnitTag));
        }

        public void OnDestroy(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            if (SystemAPI.TryGetSingleton(out UnitBaseComponent unitBaseComponent) && 
                unitEntityQuery.CalculateEntityCount() < unitBaseComponent.AmountUnits && unitBaseComponent.AmountUnits > 0)
            {
                EntityCommandBuffer.ParallelWriter ecb = GetEntityCommandBuffer(ref state);
                RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();
                
                new ProcessSpawnerJob
                {
                    ElapsedTime = SystemAPI.Time.ElapsedTime,
                    Ecb = ecb,
                    randomComponent = randomComponent
                }.ScheduleParallel();
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
        public double ElapsedTime;

        [NativeDisableUnsafePtrRestriction] public RefRW<RandomComponent> randomComponent;

        private void Execute([ChunkIndexInQuery] int chunkIndex, ref Spawner spawner)
        {
            if (spawner.NextSpawnTime < ElapsedTime || spawner.SpawnRate <= 0)
            {
                Entity newEntity = Ecb.Instantiate(chunkIndex, spawner.Prefab);
                Ecb.SetComponent(chunkIndex, newEntity, LocalTransform.FromPosition(spawner.SpawnPosition));
                Ecb.SetComponent(chunkIndex, newEntity, new UnitMovements { 
                    Speed = randomComponent.ValueRW.random.NextFloat(1f, 4f)
                });

                spawner.NextSpawnTime = (float)ElapsedTime + spawner.SpawnRate;
            }
        }
    }
}
