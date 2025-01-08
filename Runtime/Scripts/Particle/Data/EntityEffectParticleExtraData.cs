using UnityEngine;

namespace CraftSharp
{
    public record EntityEffectParticleExtraData : ParticleExtraData
    {
        public Color32 Color;

        public EntityEffectParticleExtraData(Color32 color)
        {
            Color = color;
        }
    }
}