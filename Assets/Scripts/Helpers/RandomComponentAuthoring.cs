using Unity.Entities;
using UnityEngine;
using Helpers.Components;

namespace Helpers
{
    public class RandomComponentAuthoring : MonoBehaviour { }

    public class RandomComponentBaker : Baker<RandomComponentAuthoring>
    {
        public override void Bake(RandomComponentAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new RandomComponent
            {
                random = new Unity.Mathematics.Random(1)
            }); ;
        }
    }
}
