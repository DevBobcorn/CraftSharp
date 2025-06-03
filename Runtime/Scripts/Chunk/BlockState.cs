using System.Collections.Generic;

namespace CraftSharp
{
    public record BlockState
    {
        public static readonly ResourceLocation AIR_ID            = new("air");
        public static readonly ResourceLocation CAVE_AIR_ID       = new("cave_air");
        public static readonly ResourceLocation VOID_AIR_ID       = new("void_air");
        public static readonly ResourceLocation STRUCTURE_VOID_ID = new("structure_void");

        public static readonly ResourceLocation LIGHT_ID         = new("light");
        public static readonly ResourceLocation BARRIER_ID       = new("barrier");

        public static readonly ResourceLocation WATER_ID         = new("water"); // Block state & fluid state
        public static readonly ResourceLocation FLOWING_WATER_ID = new("flowing_water"); // Fluid state only
        public static readonly ResourceLocation LAVA_ID          = new("lava");  // Block state & fluid state
        public static readonly ResourceLocation FLOWING_LAVA_ID  = new("flowing_lava");  // Fluid state only

        public static readonly BlockState AIR_STATE = new(new ResourceLocation("air"), 0F, 0F, true, 0, true, true, 0, 0, null, new());
        public static readonly BlockState UNKNOWN   = new(ResourceLocation.INVALID, 0F, 0F, true, 0, true, true, 0, 0, null, new());

        public static readonly HashSet<ResourceLocation> NO_SOLID_MESH_IDS = new()
        {
            AIR_ID, CAVE_AIR_ID, VOID_AIR_ID, STRUCTURE_VOID_ID,
            LIGHT_ID, BARRIER_ID, WATER_ID, LAVA_ID
        };

        public readonly ResourceLocation BlockId; // Something like 'minecraft:grass_block'
        public readonly Dictionary<string, string> Properties;

        public readonly float BlastResistance;
        public readonly float Hardness;

        public readonly bool NoSolidMesh;
        public readonly int FullFaceMask;
        public readonly bool NoCollision;
        public readonly bool NoOcclusion;

        public BlockShape Shape;

        // A block can have full collider box even if it doesn't collide with player,
        // in this case the collider is used for raycast detection. (e.g. Tall Grass)
        public readonly bool FullShape;
        public bool MeshFaceOcclusionSolid => FullShape && !NoOcclusion;
        public bool AmbientOcclusionSolid => FullShape && !NoCollision;

        public readonly ResourceLocation? FluidStateId;
        public bool InLiquid => FluidStateId is not null;
        public bool InWater => FluidStateId == WATER_ID || FluidStateId == FLOWING_WATER_ID;
        public bool InLava  => FluidStateId == LAVA_ID  || FluidStateId == FLOWING_LAVA_ID;

        public float Friction;
        public float JumpFactor;
        public float SpeedFactor;

        public readonly byte LightBlockageLevel = 0;
        public readonly byte LightEmissionLevel = 0;

        public BlockState(ResourceLocation blockId, float blastResistance, float hardness, bool noSolidMesh,
                int fullFaceMask, bool noCollision, bool noOcclusion, byte lightBlockage, byte lightEmission,
                ResourceLocation? fluidStateId, Dictionary<string, string> props)
        {
            BlockId = blockId;

            BlastResistance = blastResistance;
            Hardness = hardness;
            NoSolidMesh = noSolidMesh;

            FullFaceMask = fullFaceMask;
            NoCollision = noCollision;
            NoOcclusion = noOcclusion;
            FullShape = (FullFaceMask & 0b111111) == 0b111111; // All lowest 6 bits are set

            LightBlockageLevel = lightBlockage;
            LightEmissionLevel = lightEmission;

            FluidStateId = fluidStateId;

            Properties = props;
        }

        public override string ToString()
        {
            if (Properties.Count > 0)
            {
                var prop = Properties.GetEnumerator();
                prop.MoveNext();
                string propsText = $"{prop.Current.Key}={prop.Current.Value}";

                while (prop.MoveNext())
                    propsText += $",{prop.Current.Key}={prop.Current.Value}";

                return $"{BlockId}[{propsText}]";
            }
            return BlockId.ToString();
        }
    }
}

