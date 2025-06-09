using System.IO;

namespace CraftSharp
{
    // https://minecraft.wiki/w/Java_Edition_protocol/Slot_data#Structured_components
    public enum EquipmentSlot
    {
        Any = 0,
        Mainhand,
        Offhand,
        Hand, // Any of Mainhand or Offhand
        Feet,
        Legs,
        Chest,
        Head,
        Armor, // Any of Feet, Legs, Chest or Head
        Body // Used for Llamas, etc.
    }

    public static class EquipmentSlotHelper
    {
        public static EquipmentSlot GetEquipmentSlot(string slotName)
        {
            return slotName switch
            {
                "any"      => EquipmentSlot.Any,
                "mainhand" => EquipmentSlot.Mainhand,
                "offhand"  => EquipmentSlot.Offhand,
                "hand"     => EquipmentSlot.Hand,
                "feet"     => EquipmentSlot.Feet,
                "legs"     => EquipmentSlot.Legs,
                "chest"    => EquipmentSlot.Chest,
                "head"     => EquipmentSlot.Head,
                "armor"    => EquipmentSlot.Armor,
                "body"     => EquipmentSlot.Body,
                _ => throw new InvalidDataException($"Equipment slot {slotName} is not defined!")
            };
        }
        
        public static string GetEquipmentSlotName(this EquipmentSlot slot)
        {
            return slot switch
            {
                EquipmentSlot.Any      => "any",
                EquipmentSlot.Mainhand => "mainhand",
                EquipmentSlot.Offhand  => "offhand",
                EquipmentSlot.Hand     => "hand",
                EquipmentSlot.Feet     => "feet",
                EquipmentSlot.Legs     => "legs",
                EquipmentSlot.Chest    => "chest",
                EquipmentSlot.Head     => "head",
                EquipmentSlot.Armor    => "armor",
                EquipmentSlot.Body     => "body",
                _ => throw new InvalidDataException($"Name for equipment slot {slot} is not defined!")
            };
        }
    }
}