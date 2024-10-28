#nullable enable
using System;
using System.Collections.Generic;

namespace CraftSharp
{
    /// <summary>
    /// Represents an entity evolving into a Minecraft world
    /// </summary>
    public class EntityData
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
        public float Yaw = 0;

        /// <summary>
        /// Entity head pitch
        /// </summary>
        public float Pitch = 0;

        /// <summary>
        /// Entity head yaw
        /// </summary>
        public float HeadYaw = 0;

        public static float GetYawFromByte(byte yaw) => (yaw / 256F * 360F) + 90F;
        public static float GetPitchFromByte(byte pitch) => pitch / 256F * 360F;
        public static float GetHeadYawFromByte(byte headYaw) => (headYaw / 256F * 360F) + 90F;

        /// <summary>
        /// Used in Item Frame, Falling Block and Fishing Float.
        /// See https://wiki.vg/Object_Data for details.
        /// </summary>
        /// <remarks>Untested</remarks>
        public int ObjectData = -1;

        /// <summary>
        /// Health of the entity
        /// </summary>
        public float Health;

        /// <summary>
        /// Max health of the entity
        /// </summary>
        public float MaxHealth;
        
        /// <summary>
        /// Entity metadata
        /// </summary>
        public Dictionary<int, object?>? Metadata;

        /// <summary>
        /// Create a new entity based on Entity ID, Entity Type and location
        /// </summary>
        /// <param name="Id">Entity ID</param>
        /// <param name="type">Entity Type Enum</param>
        /// <param name="location">Entity location</param>
        public EntityData(int Id, EntityType type, Location location)
        {
            this.Id = Id;
            this.Type = type;
            this.Location = location;
            this.Health = 1F;
            this.MaxHealth = 1F;
        }

        /// <summary>
        /// Create a new entity based on Entity ID, Entity Type and location
        /// </summary>
        /// <param name="Id">Entity ID</param>
        /// <param name="type">Entity Type Enum</param>
        /// <param name="location">Entity location</param>
        public EntityData(int Id, EntityType type, Location location, byte yaw, byte pitch, byte headYaw, int objectData)
        {
            this.Id = Id;
            this.Type = type;
            this.Location = location;
            this.Health = 1F;
            this.MaxHealth = 1F;
            this.Yaw = GetYawFromByte(yaw);
            this.Pitch = GetPitchFromByte(pitch);
            this.HeadYaw = GetHeadYawFromByte(headYaw);
            this.ObjectData = objectData;
        }

        /// <summary>
        /// Create a new entity based on Entity ID, Entity Type, location, name and UUID
        /// </summary>
        /// <param name="Id">Entity ID</param>
        /// <param name="type">Entity Type Enum</param>
        /// <param name="location">Entity location</param>
        /// <param name="uuid">Player uuid</param>
        /// <param name="name">Player name</param>
        public EntityData(int Id, EntityType type, Location location, Guid uuid, string? name)
        {
            this.Id = Id;
            this.Type = type;
            this.Location = location;
            this.UUID = uuid;
            this.Name = name;
            this.Health = 1F;
            this.MaxHealth = 1F;
        }

        public override string ToString()
        {
            return "Entity " + Id + " (" + Type.ToString() + ")";
        }
    }
}
