using UnityEngine;

namespace CraftSharp
{
    public record DustParticleExtraData : ParticleExtraData
    {
        public Color32 Color;
        public float Scale;

        public DustParticleExtraData(Color32 color, float scale)
        {
            Color = color;
            Scale = scale;
        }
    }
}