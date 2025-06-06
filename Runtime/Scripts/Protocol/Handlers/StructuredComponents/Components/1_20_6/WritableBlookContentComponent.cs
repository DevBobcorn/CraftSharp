using System;
using System.Collections.Generic;
using CraftSharp.Inventory;
using CraftSharp.Protocol.Handlers.StructuredComponents.Core;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Components._1_20_6
{
    public record WritableBlookContentComponent : StructuredComponent
    {
        public int NumberOfPages { get; set; }
        public List<BookPage> Pages { get; set; } = new();

        public WritableBlookContentComponent(ItemPalette itemPalette, SubComponentRegistry subComponentRegistry) 
            : base(itemPalette, subComponentRegistry)
        {
            
        }
        
        public override void Parse(IMinecraftDataTypes dataTypes, Queue<byte> data)
        {
            NumberOfPages = DataTypes.ReadNextVarInt(data);

            for (var i = 0; i < NumberOfPages; i++)
            {
                var rawContent = DataTypes.ReadNextString(data);
                var hasFilteredContent = DataTypes.ReadNextBool(data);
                var filteredContent = null as string;
                
                if(hasFilteredContent)
                    filteredContent = DataTypes.ReadNextString(data);
                
                Pages.Add(new BookPage(rawContent, hasFilteredContent, filteredContent));
            }
        }

        public override Queue<byte> Serialize(IMinecraftDataTypes dataTypes)
        {
            var data = new List<byte>();
            
            data.AddRange(DataTypes.GetVarInt(NumberOfPages));

            if (NumberOfPages != Pages.Count)
                throw new InvalidOperationException("Can not serialize WritableBlookContentComponent1206 because NumberOfPages != Pages.Count!");

            foreach (var page in Pages)
            {
                data.AddRange(DataTypes.GetString(page.RawContent));
                data.AddRange(DataTypes.GetBool(page.HasFilteredContent));

                if (page.HasFilteredContent)
                {
                    if(page.FilteredContent is null)
                        throw new InvalidOperationException("Can not serialize WritableBlookContentComponent1206 because page.HasFilteredContent = true, but FilteredContent is null!");
                    
                    data.AddRange(DataTypes.GetString(page.FilteredContent));
                }
            }

            return new Queue<byte>(data);
        }
    }
}