using Unity.Mathematics;

namespace CraftSharp
{
    public record DustColorTransitionParticleExtraData : ParticleExtraData
    {
        public float3 ColorFrom;
        public float3 ColorTo;
        public float Scale;

        public DustColorTransitionParticleExtraData(float3 colorFrom, float3 colorTo, float scale)
        {
            ColorFrom = colorFrom;
            ColorTo = colorTo;
            Scale = scale;
        }
    }
}