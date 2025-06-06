using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components._1_20_6
{
    public record RarityComponent : StructuredComponent
    {
        public ItemRarity Rarity { get; set; }

        public RarityComponent(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry) 
            : base(itemPalette, subComponentRegistry)
        {

        }
        
        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            Rarity = (ItemRarity)DataTypes.ReadNextVarInt(data);
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            data.AddRange(DataTypes.GetVarInt((int)Rarity));
            return new Queue<byte>(data);
        }
        
        public override void ParseFromJson(IMinecraftDataTypes dataTypes, Json.JSONData data)
        {
            Rarity = ItemRarityHelper.GetItemRarity(data.StringValue);
        }
    }
}