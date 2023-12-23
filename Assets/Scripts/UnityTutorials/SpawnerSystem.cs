﻿using Unity.Burst;
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
            foreach (RefRW<Spawner> spawner in SystemAPI.Query<RefRW<Spawner>>())
                ProcessSpawner(ref state, spawner);
        }

        private void ProcessSpawner(ref SystemState state, RefRW<Spawner> spawner)
        {
            if (spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
            {
                Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
                state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(spawner.ValueRO.SpawnPosition));
                spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;
            }
        }
    }
}