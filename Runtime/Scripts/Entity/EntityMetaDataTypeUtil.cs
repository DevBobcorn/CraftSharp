using System;
using System.Collections.Generic;
using UnityEngine;

namespace CraftSharp
{
    public static class EntityMetaDataTypeUtil
    {
        private static EntityMetaDataType UnexpectedType(string typeName)
        {
            Debug.LogWarning($"Unexpected metadata type: {typeName}");

            return EntityMetaDataType.Byte;
        }

        public static EntityMetaDataType FromSerializedTypeName(string typeName)
        {
            return typeName switch {
                "byte"                  => EntityMetaDataType.Byte,
                "short"                 => EntityMetaDataType.Short,      // 1.8 only
                "_int"                  => EntityMetaDataType.Int,        // 1.8 only
                "vector3int"            => EntityMetaDataType.Vector3Int, // 1.8 only (not used by the game)
                "int"                   => EntityMetaDataType.VarInt,
                "long"                  => EntityMetaDataType.VarLong,
                "float"                 => EntityMetaDataType.Float,
                "string"                => EntityMetaDataType.String,
                "component"             => EntityMetaDataType.Chat,
                "optional_component"    => EntityMetaDataType.OptionalChat,
                "item_stack"            => EntityMetaDataType.Slot,
                "boolean"               => EntityMetaDataType.Boolean,
                "rotations"             => EntityMetaDataType.Rotation,
                "block_pos"             => EntityMetaDataType.Position,
                "optional_block_pos"    => EntityMetaDataType.OptionalPosition,
                "direction"             => EntityMetaDataType.Direction,
                "optional_uuid"         => EntityMetaDataType.OptionalUUID,
                "block_state"           => EntityMetaDataType.BlockId,
                "optional_block_state"  => EntityMetaDataType.OptionalBlockId,
                "compound_tag"          => EntityMetaDataType.Nbt,
                "particle"              => EntityMetaDataType.Particle,
                "villager_data"         => EntityMetaDataType.VillagerData,
                "optional_unsigned_int" => EntityMetaDataType.OptionalVarInt,
                "pose"                  => EntityMetaDataType.Pose,
                "cat_variant"           => EntityMetaDataType.CatVariant,
                "frog_variant"          => EntityMetaDataType.FrogVariant,
                "global_pos"            => EntityMetaDataType.GlobalPosition,
                "optional_global_pos"   => EntityMetaDataType.OptionalGlobalPosition,
                "painting_variant"      => EntityMetaDataType.PaintingVariant,
                "sniffer_state"         => EntityMetaDataType.SnifferState,
                "vector3"               => EntityMetaDataType.Vector3,
                "quaternion"            => EntityMetaDataType.Quaternion,

                _                       => UnexpectedType(typeName)
            };
        }

        public static Type GetType(EntityMetaDataType dataType)
        {
            return dataType switch {
                EntityMetaDataType.Byte                   => typeof (byte),
                EntityMetaDataType.Short                  => typeof (short), // 1.8 only
                EntityMetaDataType.Int                    => typeof (int), // 1.8 only
                EntityMetaDataType.Vector3Int             => typeof (Vector3Int), // 1.8 only (not used by the game)
                EntityMetaDataType.VarInt                 => typeof (int),
                EntityMetaDataType.VarLong                => typeof (long),
                EntityMetaDataType.Float                  => typeof (float),
                EntityMetaDataType.String                 => typeof (string),
                EntityMetaDataType.Chat                   => typeof (string),
                EntityMetaDataType.OptionalChat           => typeof (string),
                EntityMetaDataType.Slot                   => typeof (ItemStack),
                EntityMetaDataType.Boolean                => typeof (bool),
                EntityMetaDataType.Rotation               => typeof (Vector3),
                EntityMetaDataType.Position               => typeof (Location),
                EntityMetaDataType.OptionalPosition       => typeof (Location?),
                EntityMetaDataType.Direction              => typeof (int),
                EntityMetaDataType.OptionalUUID           => typeof (Guid?),
                EntityMetaDataType.BlockId                => typeof (int),
                EntityMetaDataType.OptionalBlockId        => typeof (int),
                EntityMetaDataType.Nbt                    => typeof (Dictionary<string, object>),
                EntityMetaDataType.Particle               => typeof (object), // Not handled, always null
                EntityMetaDataType.VillagerData           => typeof (Vector3Int),
                EntityMetaDataType.OptionalVarInt         => typeof (int?),
                EntityMetaDataType.Pose                   => typeof (int),
                EntityMetaDataType.CatVariant             => typeof (int),
                EntityMetaDataType.FrogVariant            => typeof (int),
                EntityMetaDataType.GlobalPosition         => typeof (Tuple<string, Location>),
                EntityMetaDataType.OptionalGlobalPosition => typeof (Tuple<string, Location>),
                EntityMetaDataType.PaintingVariant        => typeof (int),
                EntityMetaDataType.SnifferState           => typeof (int),
                EntityMetaDataType.Vector3                => typeof (Vector3),
                EntityMetaDataType.Quaternion             => typeof (Quaternion),

                _                                         => typeof (object)
            };
        }
    }
}