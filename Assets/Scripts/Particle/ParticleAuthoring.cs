using Unity.Entities;
using UnityEngine;
using Particle.Components;

namespace Particle
{
    public class ParticleAuthoring : MonoBehaviour
    {
        #region Attributes

        [SerializeField] private float movementSpeed;

        #endregion

        #region FUNCTIONS

        public float MovementSpeed
        {
            get => movementSpeed;
        }

        #endregion
    }

    public class ParticleBaker : Baker<ParticleAuthoring>
    {
        public override void Bake(ParticleAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new ParticleTag());

            AddComponent(entity, new ParticleMovements
            {
                Speed = authoring.MovementSpeed
            });
        }
    }
}
