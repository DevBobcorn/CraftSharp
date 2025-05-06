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
        Offhand
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
                _ => throw new InvalidDataException($"Equipment slot {slotName} is not defined!")
            };
        }
    }
}