#nullable enable
using System.Collections.Generic;
using CraftSharp.Inventory;

namespace CraftSharp.Protocol
{
    public interface IMinecraftDataTypes
    {
        public bool UseAnonymousNBT { get; }

        #region Complex data readers
        
        public ItemStack? ReadNextItemSlot(Queue<byte> cache, ItemPalette itemPalette);

        public EntityData ReadNextEntity(Queue<byte> cache, EntityTypePalette entityPalette, bool living);

        public Dictionary<int, object?> ReadNextMetadata(Queue<byte> cache, ItemPalette itemPalette, EntityMetadataPalette metadataPalette);

        public ParticleExtraData ReadParticleData(Queue<byte> cache, ItemPalette itemPalette);

        public VillagerTrade ReadNextTrade(Queue<byte> cache, ItemPalette itemPalette);

        public string ReadNextChat(Queue<byte> cache);

        public Json.JSONData ReadNextChatAsJson(Queue<byte> cache);

        #endregion
        
        #region Complex data getters

        public byte[] GetItemSlot(ItemStack? item, ItemPalette itemPalette);

        public byte[] GetSlotsArray(Dictionary<int, ItemStack> items, ItemPalette itemPalette);

        public byte GetBlockFace(Direction direction);

        public byte[] GetLastSeenMessageList(Message.LastSeenMessageList msgList, bool isOnlineMode);

        public byte[] GetAcknowledgment(Message.LastSeenMessageList.Acknowledgment ack, bool isOnlineMode);

        #endregion
    }
}