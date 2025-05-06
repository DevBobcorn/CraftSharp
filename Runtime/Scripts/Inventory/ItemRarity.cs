using System.IO;

namespace CraftSharp
{
    public enum ItemRarity : int
    {
        Common = 0, // White
        Uncommon,   // Yellow
        Rare,       // Aqua
        Epic        // Light Purple
    }

    public static class ItemRarityHelper
    {
        public static ItemRarity GetItemRarity(string rarityName)
        {
            return rarityName switch
            {
                "common"   => ItemRarity.Common,
                "uncommon" => ItemRarity.Uncommon,
                "rare"     => ItemRarity.Rare,
                "epic"     => ItemRarity.Epic,
                _ => throw new InvalidDataException($"Item rarity {rarityName} is not defined!")
            };
        }
    }
}