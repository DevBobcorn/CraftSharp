using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components
{
    public record ContainerComponent : StructuredComponent
    {
        public int NumberOfItems { get; set; }
        public List<ItemStack> Items { get; set; } = new();

        public ContainerComponent(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry) 
            : base(itemPalette, subComponentRegistry)
        {

        }
        
        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            NumberOfItems = DataTypes.ReadNextVarInt(data);
            for (var i = 0; i < NumberOfItems; i++)
            {
                var item = dataTypes.ReadNextItemSlot(data, ItemPalette);

                if (item is null)
                    continue;
                
                Items.Add(item);
            }
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            data.AddRange(DataTypes.GetVarInt(NumberOfItems));
            for (var i = 0; i < NumberOfItems; i++)
                data.AddRange(dataTypes.GetItemSlot(Items[i], ItemPalette));
                
            return new Queue<byte>(data);
        }
    }
}