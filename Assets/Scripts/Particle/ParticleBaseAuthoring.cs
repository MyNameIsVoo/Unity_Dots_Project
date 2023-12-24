using Unity.Entities;
using UnityEngine;
using Particle.Components;

namespace Particle
{
    public class ParticleBaseAuthoring : MonoBehaviour
    {
        #region Attributes

        [Header("ATTRIBUTES")]
        [SerializeField, Min(0)] private int amountParticles;

        #endregion

        #region FUNCTIONS

        public int AmountParticles
        {
            get => amountParticles;
        }

        #endregion
    }

    public class ParticleBaseBaker : Baker<ParticleBaseAuthoring>
    {
        public override void Bake(ParticleBaseAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new ParticleBaseComponent { 
                AmountParticles = authoring.AmountParticles,
            });
        }
    }
}