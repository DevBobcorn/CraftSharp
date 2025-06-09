using CraftSharp.Protocol.Handlers.StructuredComponents.Components;
using CraftSharp.Protocol.Handlers.StructuredComponents.Components.Subcomponents;

namespace CraftSharp
{
    public record MobEffectInstance(
        ResourceLocation EffectId,
        int Amplifier,
        int Duration,
        bool Ambient,
        bool ShowParticles,
        bool ShowIcon)
    {
        public ResourceLocation EffectId { get; } = EffectId;
        public int Amplifier { get; } = Amplifier;
        public int Duration { get; } = Duration;
        public bool Ambient { get; } = Ambient;
        public bool ShowParticles { get; } = ShowParticles;
        public bool ShowIcon { get; } = ShowIcon;

        public static MobEffectInstance FromJson(Json.JSONData data)
        {
            var typeId = data.Properties.TryGetValue("id", out var val) ?
                ResourceLocation.FromString(val.StringValue) : ResourceLocation.INVALID;
            
            var amplifier = data.Properties.TryGetValue("amplifier", out val) ? int.Parse(val.StringValue) : 0;
            var duration = data.Properties.TryGetValue("duration", out val) ? int.Parse(val.StringValue) : 0;
            var ambient = data.Properties.TryGetValue("ambient", out val) && bool.Parse(val.StringValue); // False by default
            var showParticles = !data.Properties.TryGetValue("show_particles", out val) || bool.Parse(val.StringValue); // True by default
            var showIcon = !data.Properties.TryGetValue("show_icon", out val) || bool.Parse(val.StringValue); // True by default
            
            return new(typeId, amplifier, duration, ambient, showParticles, showIcon);
        }

        public static MobEffectInstance FromComponent(PotionEffectSubComponent component)
        {
            return new(component.EffectId, component.Details.Amplifier, component.Details.Duration,
                component.Details.Ambient, component.Details.ShowParticles, component.Details.ShowIcon);
        }
    }
}