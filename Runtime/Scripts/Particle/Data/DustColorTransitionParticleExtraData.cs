using UnityEngine;

namespace CraftSharp
{
    public record DustColorTransitionParticleExtraData : ParticleExtraData
    {
        public Color32 ColorFrom;
        public Color32 ColorTo;
        public float Scale;

        public DustColorTransitionParticleExtraData(Color32 colorFrom, Color32 colorTo, float scale)
        {
            ColorFrom = colorFrom;
            ColorTo = colorTo;
            Scale = scale;
        }
    }
}