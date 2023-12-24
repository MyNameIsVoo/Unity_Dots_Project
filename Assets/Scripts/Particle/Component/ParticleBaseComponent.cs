using Unity.Entities;
using Unity.Mathematics;

namespace Particle.Components
{
    public struct ParticleBaseComponent : IComponentData
    {
        public int AmountParticles;
    }

    public struct ParticleTag : IComponentData { }

    public struct ParticleMovements : IComponentData
    {
        public float Speed;
    }

    public struct ParticleTargetPosition : IComponentData
    {
        public float3 Value;
    }
}