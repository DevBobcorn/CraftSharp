using Unity.Mathematics;

namespace CraftSharp
{
    public record DustParticleExtraData : ParticleExtraData
    {
        public float3 Color;
        public float Scale;

        public DustParticleExtraData(float3 color, float scale)
        {
            Color = color;
            Scale = scale;
        }
    }
}