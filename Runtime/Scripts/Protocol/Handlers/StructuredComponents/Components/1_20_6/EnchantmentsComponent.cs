using System.Collections.Generic;
using CraftSharp.Inventory;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components
{
    public record EnchantmentsComponent : StructuredComponent
    {
        public int NumberOfEnchantments { get; set; }
        public List<Enchantment> Enchantments { get; set; } = new();
        public bool ShowTooltip { get; set; }

        public EnchantmentsComponent(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry) 
            : base(itemPalette, subComponentRegistry)
        {

        }

        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            NumberOfEnchantments = DataTypes.ReadNextVarInt(data);

            for (var i = 0; i < NumberOfEnchantments; i++)
            {
                var enchantmentTypeNumId = DataTypes.ReadNextVarInt(data);
                var enchantmentTypeId = EnchantmentTypePalette.INSTANCE.GetIdByNumId(enchantmentTypeNumId);
                Enchantments.Add(new Enchantment(enchantmentTypeId, DataTypes.ReadNextVarInt(data)));
            }

            ShowTooltip = DataTypes.ReadNextBool(data);
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            data.AddRange(DataTypes.GetVarInt(Enchantments.Count));
            foreach (var enchantment in Enchantments)
            {
                var enchantmentTypeNumId = EnchantmentTypePalette.INSTANCE.GetNumIdById(enchantment.EnchantmentId);
                data.AddRange(DataTypes.GetVarInt(enchantmentTypeNumId));
                data.AddRange(DataTypes.GetVarInt(enchantment.Level));
            }
            data.AddRange(DataTypes.GetBool(ShowTooltip));
            return new Queue<byte>(data);
        }
    }
}