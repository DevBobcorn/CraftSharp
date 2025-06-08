using System;
using System.Collections.Generic;
using UnityEngine;

namespace CraftSharp
{
    public static class EntityMetadataTypeUtil
    {
        private static EntityMetadataType UnexpectedType(string typeName)
        {
            Debug.LogWarning($"Unexpected metadata type: {typeName}");

            return EntityMetadataType.Byte;
        }

        public static EntityMetadataType FromSerializedTypeName(string typeName)
        {
            return typeName switch {
                "byte"                  => EntityMetadataType.Byte,
                "short"                 => EntityMetadataType.Short,      // 1.8 only
                "_int"                  => EntityMetadataType.Int,        // 1.8 only
                "vector3int"            => EntityMetadataType.Vector3Int, // 1.8 only (not used by the game)
                "int"                   => EntityMetadataType.VarInt,
                "long"                  => EntityMetadataType.VarLong,
                "float"                 => EntityMetadataType.Float,
                "string"                => EntityMetadataType.String,
                "component"             => EntityMetadataType.Chat,
                "optional_component"    => EntityMetadataType.OptionalChat,
                "item_stack"            => EntityMetadataType.Slot,
                "boolean"               => EntityMetadataType.Boolean,
                "rotations"             => EntityMetadataType.Rotation,
                "block_pos"             => EntityMetadataType.Position,
                "optional_block_pos"    => EntityMetadataType.OptionalPosition,
                "direction"             => EntityMetadataType.Direction,
                "optional_uuid"         => EntityMetadataType.OptionalUUID,
                "block_state"           => EntityMetadataType.BlockId,
                "optional_block_state"  => EntityMetadataType.OptionalBlockId,
                "compound_tag"          => EntityMetadataType.Nbt,
                "particle"              => EntityMetadataType.Particle,
                "particles"             => EntityMetadataType.Particles,
                "villager_data"         => EntityMetadataType.VillagerData,
                "optional_unsigned_int" => EntityMetadataType.OptionalVarInt,
                "pose"                  => EntityMetadataType.Pose,
                "cat_variant"           => EntityMetadataType.CatVariant,
                "cow_variant"           => EntityMetadataType.CowVariant,
                "wolf_variant"          => EntityMetadataType.WolfVariant,
                "wolf_sound_variant"    => EntityMetadataType.WolfSoundVariant,
                "frog_variant"          => EntityMetadataType.FrogVariant,
                "pig_variant"           => EntityMetadataType.PigVariant,
                "chicken_variant"       => EntityMetadataType.ChickenVariant,
                "global_pos"            => EntityMetadataType.GlobalPosition,
                "optional_global_pos"   => EntityMetadataType.OptionalGlobalPosition,
                "painting_variant"      => EntityMetadataType.PaintingVariant,
                "sniffer_state"         => EntityMetadataType.SnifferState,
                "armadillo_state"       => EntityMetadataType.ArmadilloState,
                "vector3"               => EntityMetadataType.Vector3,
                "quaternion"            => EntityMetadataType.Quaternion,

                _                       => UnexpectedType(typeName)
            };
        }

        public static Type GetType(EntityMetadataType dataType)
        {
            return dataType switch {
                EntityMetadataType.Byte                   => typeof (byte),
                EntityMetadataType.Short                  => typeof (short), // 1.8 only
                EntityMetadataType.Int                    => typeof (int), // 1.8 only
                EntityMetadataType.Vector3Int             => typeof (Vector3Int), // 1.8 only (not used by the game)
                EntityMetadataType.VarInt                 => typeof (int),
                EntityMetadataType.VarLong                => typeof (long),
                EntityMetadataType.Float                  => typeof (float),
                EntityMetadataType.String                 => typeof (string),
                EntityMetadataType.Chat                   => typeof (string),
                EntityMetadataType.OptionalChat           => typeof (string),
                EntityMetadataType.Slot                   => typeof (ItemStack),
                EntityMetadataType.Boolean                => typeof (bool),
                EntityMetadataType.Rotation               => typeof (Vector3),
                EntityMetadataType.Position               => typeof (Location),
                EntityMetadataType.OptionalPosition       => typeof (Location?),
                EntityMetadataType.Direction              => typeof (int),
                EntityMetadataType.OptionalUUID           => typeof (Guid?),
                EntityMetadataType.BlockId                => typeof (int),
                EntityMetadataType.OptionalBlockId        => typeof (int),
                EntityMetadataType.Nbt                    => typeof (Dictionary<string, object>),
                EntityMetadataType.Particle               => typeof (object), // Not handled, always null
                EntityMetadataType.VillagerData           => typeof (Vector3Int),
                EntityMetadataType.OptionalVarInt         => typeof (int?),
                EntityMetadataType.Pose                   => typeof (int),
                EntityMetadataType.CatVariant             => typeof (int),
                EntityMetadataType.FrogVariant            => typeof (int),
                EntityMetadataType.GlobalPosition         => typeof (Tuple<string, Location>),
                EntityMetadataType.OptionalGlobalPosition => typeof (Tuple<string, Location>),
                EntityMetadataType.PaintingVariant        => typeof (int),
                EntityMetadataType.SnifferState           => typeof (int),
                EntityMetadataType.Vector3                => typeof (Vector3),
                EntityMetadataType.Quaternion             => typeof (Quaternion),

                _                                         => typeof (object)
            };
        }
    }
}