using UnityEngine;
using Unity.Entities;
using Helpers.Components;

namespace Helpers
{
    class SpawnerAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private float spawnRate;

        public GameObject Prefab
        {
            get => prefab;
        }

        public float SpawnRate
        {
            get => spawnRate;
        }
    }

    class SpawnerBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new Spawner
            {
                Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                SpawnPosition = authoring.transform.position,
                NextSpawnTime = 0.0f,
                SpawnRate = authoring.SpawnRate
            });
        }
    }
}
