using UnityEngine;

namespace CraftSharp
{
    public static class EntityCategoryTypeUtil
    {
        private static EntityCategory Unexpected(string typeName)
        {
            Debug.LogWarning($"Unexpected category: {typeName}");

            return EntityCategory.Misc;
        }

        public static EntityCategory FromSerializedName(string typeName)
        {
            return typeName switch {
                "creature"                   => EntityCategory.Creature,
                "monster"                    => EntityCategory.Monster,
                "ambient"                    => EntityCategory.Ambient,
                "axolotls"                   => EntityCategory.Axolotls,
                "underground_water_creature" => EntityCategory.UndergroundWaterCreature,
                "water_creature"             => EntityCategory.WaterCreature,
                "water_ambient"              => EntityCategory.WaterAmbient,

                _                            => Unexpected(typeName)
            };
        }
    }
}