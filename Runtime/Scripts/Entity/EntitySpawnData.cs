#nullable enable
using System;

namespace CraftSharp
{
    /// <summary>
    /// Represents an entity spawn in a Minecraft world
    /// </summary>
    public class EntitySpawnData
    {
        /// <summary>
        /// Id of the entity on the Minecraft server
        /// </summary>
        public int Id;

        /// <summary>
        /// UUID of the entity if it is a player.
        /// </summary>
        public Guid UUID;

        /// <summary>
        /// Nickname of the entity if it is a player.
        /// </summary>
        public string? Name;

        /// <summary>
        /// Entity type
        /// </summary>
        public EntityType Type;

        /// <summary>
        /// Entity location in the Minecraft world
        /// </summary>
        public Location Location;

        /// <summary>
        /// Entity yaw
        /// </summary>
        public readonly float Yaw;

        /// <summary>
        /// Entity head pitch
        /// </summary>
        public readonly float Pitch;

        /// <summary>
        /// Entity head yaw
        /// </summary>
        public readonly float HeadYaw;

        public static float GetYawFromByte(byte yaw) => (yaw / 256F * 360F) + 90F;
        public static float GetPitchFromByte(byte pitch) => pitch / 256F * 360F;
        public static float GetHeadYawFromByte(byte headYaw) => (headYaw / 256F * 360F) + 90F;

        /// <summary>
        /// Used in Item Frame, Falling Block and Fishing Float.
        /// See https://wiki.vg/Object_Data for details.
        /// </summary>
        /// <remarks>Untested</remarks>
        public readonly int ObjectData = -1;

        /// <summary>
        /// Create a new entity based on Entity Id, Entity Type and location
        /// </summary>
        /// <param name="Id">Entity Id</param>
        /// <param name="type">Entity Type Enum</param>
        /// <param name="location">Entity location</param>
        public EntitySpawnData(int Id, EntityType type, Location location)
        {
            this.Id = Id;
            Type = type;
            Location = location;
        }

        /// <summary>
        /// Create a new entity based on Entity Id, Entity Type and location
        /// </summary>
        /// <param name="Id">Entity Id</param>
        /// <param name="type">Entity Type Enum</param>
        /// <param name="location">Entity location</param>
        /// <param name="yaw">Entity yaw</param>
        /// <param name="pitch">Head pitch</param>
        /// <param name="headYaw">Head yaw</param>
        /// <param name="objectData">Object data</param>
        public EntitySpawnData(int Id, EntityType type, Location location, byte yaw, byte pitch, byte headYaw, int objectData)
        {
            this.Id = Id;
            Type = type;
            Location = location;
            Yaw = GetYawFromByte(yaw);
            Pitch = GetPitchFromByte(pitch);
            HeadYaw = GetHeadYawFromByte(headYaw);
            ObjectData = objectData;
        }

        /// <summary>
        /// Create a new entity based on Entity Id, Entity Type, location, name and UUID
        /// </summary>
        /// <param name="Id">Entity Id</param>
        /// <param name="type">Entity Type Enum</param>
        /// <param name="location">Entity location</param>
        /// <param name="uuid">Player uuid</param>
        /// <param name="name">Player name</param>
        public EntitySpawnData(int Id, EntityType type, Location location, Guid uuid, string? name)
        {
            this.Id = Id;
            Type = type;
            Location = location;
            UUID = uuid;
            Name = name;
        }

        public override string ToString()
        {
            return "EntitySpawnData " + Id + " (" + Type + ")";
        }
    }
}
