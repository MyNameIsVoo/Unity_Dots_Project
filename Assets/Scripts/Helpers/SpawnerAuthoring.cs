using UnityEngine;
using Unity.Entities;
using Helpers.Components;
using Unit.Component;

namespace Helpers
{
    class SpawnerAuthoring : MonoBehaviour
    {
        #region Attributes

        [Header("ATTRIBUTES")]
        [SerializeField, Min(0)] private int amountUnits;
        [SerializeField, Min(-1)] private float spawnRate;

        [Header("PREFABS")]
        [SerializeField] private GameObject prefab;

        #endregion

        #region FUNCTIONS

        public GameObject Prefab
        {
            get => prefab;
        }

        public int AmountUnits
        {
            get => amountUnits;
        }

        public float SpawnRate
        {
            get => spawnRate;
        }

        #endregion
    }

    class SpawnerBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new UnitBaseComponent{
                AmountUnits = authoring.AmountUnits,
            });
            AddComponent(entity, new Spawner{
                Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                SpawnPosition = authoring.transform.position,
                NextSpawnTime = 0.0f,
                SpawnRate = authoring.SpawnRate,
            });
        }
    }
}
