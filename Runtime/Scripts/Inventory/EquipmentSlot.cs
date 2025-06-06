using System.IO;

namespace CraftSharp
{
    public enum EquipmentSlot
    {
        Head,
        Chest,
        Legs,
        Feet,
        Mainhand,
        Offhand,
        Body // Used for llamas, etc.
    }

    public static class EquipmentSlotHelper
    {
        public static EquipmentSlot GetEquipmentSlot(string slotName)
        {
            return slotName switch
            {
                "head" => EquipmentSlot.Head,
                "chest" => EquipmentSlot.Chest,
                "legs" => EquipmentSlot.Legs,
                "feet" => EquipmentSlot.Feet,
                "mainhand" => EquipmentSlot.Mainhand,
                "offhand" => EquipmentSlot.Offhand,
                "body" => EquipmentSlot.Body,
                _ => throw new InvalidDataException($"Equipment slot {slotName} is not defined!")
            };
        }
    }
}