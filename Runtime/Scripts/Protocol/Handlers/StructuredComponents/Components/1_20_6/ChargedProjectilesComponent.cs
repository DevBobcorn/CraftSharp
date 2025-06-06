#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using CraftSharp.Inventory;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components._1_20_6
{
    public record ChargedProjectilesComponent : StructuredComponent
    {
        public int NumberOfItems { get; set; }
        public List<ItemStack?> Items { get; set; } = new();

        public ChargedProjectilesComponent(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry) 
            : base(itemPalette, subComponentRegistry)
        {

        }

        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            NumberOfItems = DataTypes.ReadNextVarInt(data);

            for (var i = 0; i < NumberOfItems; i++)
                Items.Add(dataTypes.ReadNextItemSlot(data, ItemPalette));
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            data.AddRange(DataTypes.GetVarInt(NumberOfItems));

            if (NumberOfItems != Items.Count)
                throw new ArgumentNullException($"Cannot serialize ChargedProjectilesComponent1206 because NumberOfItems != Items.Count!");
                
            foreach (var item in Items.OfType<ItemStack>())
                data.AddRange(dataTypes.GetItemSlot(item, ItemPalette));

            return new Queue<byte>(data);
        }
    }
}