using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace UnityTutorials
{
    [BurstCompile] // Работает и без него (я так понял он ускоряет код, путем конвертации его в более производительный - см Jobs/BurstInspector)
    public partial struct SpawnerSystem : ISystem
    {
        public void OnCreate(ref SystemState state) { }

        public void OnDestroy(ref SystemState state) { }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer.ParallelWriter ecb = GetEntityCommandBuffer(ref state);

            new ProcessSpawnerJob
            {
                ElapsedTime = SystemAPI.Time.ElapsedTime,
                Ecb = ecb
            }.ScheduleParallel();
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

        private void Execute([ChunkIndexInQuery] int chunkIndex, ref Spawner spawner)
        {
            if (spawner.NextSpawnTime < ElapsedTime)
            {
                Entity newEntity = Ecb.Instantiate(chunkIndex, spawner.Prefab);
                Ecb.SetComponent(chunkIndex, newEntity, LocalTransform.FromPosition(spawner.SpawnPosition));
                spawner.NextSpawnTime = (float)ElapsedTime + spawner.SpawnRate;
            }
        }
    }
}
