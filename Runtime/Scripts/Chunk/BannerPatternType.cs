using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CraftSharp
{
    /// <summary>
    /// Represents a Minecraft BannerPattern Type
    /// </summary>
    public record BannerPatternType
    {
        public static readonly ResourceLocation BASE_ID                        = new("base");
        public static readonly ResourceLocation BORDER_ID                      = new("border");
        public static readonly ResourceLocation BRICKS_ID                      = new("bricks");
        public static readonly ResourceLocation CIRCLE_ID                      = new("circle");
        public static readonly ResourceLocation CREEPER_ID                     = new("creeper");
        public static readonly ResourceLocation CROSS_ID                       = new("cross");
        public static readonly ResourceLocation CURLY_BORDER_ID                = new("curly_border");
        public static readonly ResourceLocation DIAGONAL_LEFT_ID               = new("diagonal_left");
        public static readonly ResourceLocation DIAGONAL_RIGHT_ID              = new("diagonal_right");
        public static readonly ResourceLocation DIAGONAL_UP_LEFT_ID            = new("diagonal_up_left");
        public static readonly ResourceLocation DIAGONAL_UP_RIGHT_ID           = new("diagonal_up_right");
        public static readonly ResourceLocation FLOW_ID                        = new("flow");
        public static readonly ResourceLocation FLOWER_ID                      = new("flower");
        public static readonly ResourceLocation GLOBE_ID                       = new("globe");
        public static readonly ResourceLocation GRADIENT_ID                    = new("gradient");
        public static readonly ResourceLocation GRADIENT_UP_ID                 = new("gradient_up");
        public static readonly ResourceLocation GUSTER_ID                      = new("guster");
        public static readonly ResourceLocation HALF_HORIZONTAL_ID             = new("half_horizontal");
        public static readonly ResourceLocation HALF_HORIZONTAL_BOTTOM_ID      = new("half_horizontal_bottom");
        public static readonly ResourceLocation HALF_VERTICAL_ID               = new("half_vertical");
        public static readonly ResourceLocation HALF_VERTICAL_RIGHT_ID         = new("half_vertical_right");
        public static readonly ResourceLocation MOJANG_ID                      = new("mojang");
        public static readonly ResourceLocation PIGLIN_ID                      = new("piglin");
        public static readonly ResourceLocation RHOMBUS_ID                     = new("rhombus");
        public static readonly ResourceLocation SKULL_ID                       = new("skull");
        public static readonly ResourceLocation SMALL_STRIPES_ID               = new("small_stripes");
        public static readonly ResourceLocation SQUARE_BOTTOM_LEFT_ID          = new("square_bottom_left");
        public static readonly ResourceLocation SQUARE_BOTTOM_RIGHT_ID         = new("square_bottom_right");
        public static readonly ResourceLocation SQUARE_TOP_LEFT_ID             = new("square_top_left");
        public static readonly ResourceLocation SQUARE_TOP_RIGHT_ID            = new("square_top_right");
        public static readonly ResourceLocation STRAIGHT_CROSS_ID              = new("straight_cross");
        public static readonly ResourceLocation STRIPE_BOTTOM_ID               = new("stripe_bottom");
        public static readonly ResourceLocation STRIPE_CENTER_ID               = new("stripe_center");
        public static readonly ResourceLocation STRIPE_DOWNLEFT_ID             = new("stripe_downleft");
        public static readonly ResourceLocation STRIPE_DOWNRIGHT_ID            = new("stripe_downright");
        public static readonly ResourceLocation STRIPE_LEFT_ID                 = new("stripe_left");
        public static readonly ResourceLocation STRIPE_MIDDLE_ID               = new("stripe_middle");
        public static readonly ResourceLocation STRIPE_RIGHT_ID                = new("stripe_right");
        public static readonly ResourceLocation STRIPE_TOP_ID                  = new("stripe_top");
        public static readonly ResourceLocation TRIANGLE_BOTTOM_ID             = new("triangle_bottom");
        public static readonly ResourceLocation TRIANGLE_TOP_ID                = new("triangle_top");
        public static readonly ResourceLocation TRIANGLES_BOTTOM_ID            = new("triangles_bottom");
        public static readonly ResourceLocation TRIANGLES_TOP_ID               = new("triangles_top");
        
        public const string BASE_CODE                        = "b";
        public const string BORDER_CODE                      = "bo";
        public const string BRICKS_CODE                      = "bri";
        public const string CIRCLE_CODE                      = "mc";
        public const string CREEPER_CODE                     = "cre";
        public const string CROSS_CODE                       = "cr";
        public const string CURLY_BORDER_CODE                = "cbo";
        public const string DIAGONAL_LEFT_CODE               = "ld";
        public const string DIAGONAL_RIGHT_CODE              = "rud";
        public const string DIAGONAL_UP_LEFT_CODE            = "lud";
        public const string DIAGONAL_UP_RIGHT_CODE           = "rd";
        public const string FLOW_CODE                        = "flw";
        public const string FLOWER_CODE                      = "flo";
        public const string GLOBE_CODE                       = "glb";
        public const string GRADIENT_CODE                    = "gra";
        public const string GRADIENT_UP_CODE                 = "gru";
        public const string GUSTER_CODE                      = "gus";
        public const string HALF_HORIZONTAL_CODE             = "hh";
        public const string HALF_HORIZONTAL_BOTTOM_CODE      = "hhb";
        public const string HALF_VERTICAL_CODE               = "vh";
        public const string HALF_VERTICAL_RIGHT_CODE         = "vhr";
        public const string MOJANG_CODE                      = "moj";
        public const string PIGLIN_CODE                      = "pig";
        public const string RHOMBUS_CODE                     = "mr";
        public const string SKULL_CODE                       = "sku";
        public const string SMALL_STRIPES_CODE               = "ss";
        public const string SQUARE_BOTTOM_LEFT_CODE          = "bl";
        public const string SQUARE_BOTTOM_RIGHT_CODE         = "br";
        public const string SQUARE_TOP_LEFT_CODE             = "tl";
        public const string SQUARE_TOP_RIGHT_CODE            = "tr";
        public const string STRAIGHT_CROSS_CODE              = "sc";
        public const string STRIPE_BOTTOM_CODE               = "bs";
        public const string STRIPE_CENTER_CODE               = "cs";
        public const string STRIPE_DOWNLEFT_CODE             = "dls";
        public const string STRIPE_DOWNRIGHT_CODE            = "drs";
        public const string STRIPE_LEFT_CODE                 = "ls";
        public const string STRIPE_MIDDLE_CODE               = "ms";
        public const string STRIPE_RIGHT_CODE                = "rs";
        public const string STRIPE_TOP_CODE                  = "ts";
        public const string TRIANGLE_BOTTOM_CODE             = "bt";
        public const string TRIANGLE_TOP_CODE                = "tt";
        public const string TRIANGLES_BOTTOM_CODE            = "bts";
        public const string TRIANGLES_TOP_CODE               = "tts";

        public static readonly BannerPatternType DUMMY_BANNER_PATTERN_TYPE = new(
            ResourceLocation.INVALID, string.Empty);

        public readonly ResourceLocation AssetId;
        public string TranslationKey;

        public BannerPatternType(ResourceLocation assetId, string translationKey)
        {
            AssetId = assetId;
            TranslationKey = translationKey;
        }

        public static ResourceLocation GetIdFromCode(string code)
        {
            return code switch
            {
                BASE_CODE => BASE_ID,
                BORDER_CODE => BORDER_ID,
                BRICKS_CODE => BRICKS_ID,
                CIRCLE_CODE => CIRCLE_ID,
                CREEPER_CODE => CREEPER_ID,
                CROSS_CODE => CROSS_ID,
                CURLY_BORDER_CODE => CURLY_BORDER_ID,
                DIAGONAL_LEFT_CODE => DIAGONAL_LEFT_ID,
                DIAGONAL_RIGHT_CODE => DIAGONAL_RIGHT_ID,
                DIAGONAL_UP_LEFT_CODE => DIAGONAL_UP_LEFT_ID,
                DIAGONAL_UP_RIGHT_CODE => DIAGONAL_UP_RIGHT_ID,
                FLOW_CODE => FLOW_ID,
                FLOWER_CODE => FLOWER_ID,
                GLOBE_CODE => GLOBE_ID,
                GRADIENT_CODE => GRADIENT_ID,
                GRADIENT_UP_CODE => GRADIENT_UP_ID,
                GUSTER_CODE => GUSTER_ID,
                HALF_HORIZONTAL_CODE => HALF_HORIZONTAL_ID,
                HALF_HORIZONTAL_BOTTOM_CODE => HALF_HORIZONTAL_BOTTOM_ID,
                HALF_VERTICAL_CODE => HALF_VERTICAL_ID,
                HALF_VERTICAL_RIGHT_CODE => HALF_VERTICAL_RIGHT_ID,
                MOJANG_CODE => MOJANG_ID,
                PIGLIN_CODE => PIGLIN_ID,
                RHOMBUS_CODE => RHOMBUS_ID,
                SKULL_CODE => SKULL_ID,
                SMALL_STRIPES_CODE => SMALL_STRIPES_ID,
                SQUARE_BOTTOM_LEFT_CODE => SQUARE_BOTTOM_LEFT_ID,
                SQUARE_BOTTOM_RIGHT_CODE => SQUARE_BOTTOM_RIGHT_ID,
                SQUARE_TOP_LEFT_CODE => SQUARE_TOP_LEFT_ID,
                SQUARE_TOP_RIGHT_CODE => SQUARE_TOP_RIGHT_ID,
                STRAIGHT_CROSS_CODE => STRAIGHT_CROSS_ID,
                STRIPE_BOTTOM_CODE => STRIPE_BOTTOM_ID,
                STRIPE_CENTER_CODE => STRIPE_CENTER_ID,
                STRIPE_DOWNLEFT_CODE => STRIPE_DOWNLEFT_ID,
                STRIPE_DOWNRIGHT_CODE => STRIPE_DOWNRIGHT_ID,
                STRIPE_LEFT_CODE => STRIPE_LEFT_ID,
                STRIPE_MIDDLE_CODE => STRIPE_MIDDLE_ID,
                STRIPE_RIGHT_CODE => STRIPE_RIGHT_ID,
                STRIPE_TOP_CODE => STRIPE_TOP_ID,
                TRIANGLE_BOTTOM_CODE => TRIANGLE_BOTTOM_ID,
                TRIANGLE_TOP_CODE => TRIANGLE_TOP_ID,
                TRIANGLES_BOTTOM_CODE => TRIANGLES_BOTTOM_ID,
                TRIANGLES_TOP_CODE => TRIANGLES_TOP_ID,
                
                _ => throw new InvalidDataException($"{code} is not a valid banner pattern code!")
            };
        }
        
        public static ResourceLocation GetIdFromIndex(int index)
        {
            return index switch
            {
                0 => throw new InvalidDataException("0 is used as index for inline-defined patterns!"),
                1 => SQUARE_BOTTOM_LEFT_ID,
                2 => SQUARE_BOTTOM_RIGHT_ID,
                3 => SQUARE_TOP_LEFT_ID,
                4 => SQUARE_TOP_RIGHT_ID,
                5 => STRIPE_BOTTOM_ID,
                6 => STRIPE_TOP_ID,
                7 => STRIPE_LEFT_ID,
                8 => STRIPE_RIGHT_ID,
                9 => STRIPE_CENTER_ID,
                10 => STRIPE_MIDDLE_ID,
                11 => STRIPE_DOWNLEFT_ID,
                12 => STRIPE_DOWNRIGHT_ID,
                13 => SMALL_STRIPES_ID,
                14 => CROSS_ID,
                15 => STRAIGHT_CROSS_ID,
                16 => TRIANGLE_BOTTOM_ID,
                17 => TRIANGLE_TOP_ID,
                18 => TRIANGLES_BOTTOM_ID,
                19 => TRIANGLES_TOP_ID,
                20 => DIAGONAL_LEFT_ID,
                21 => DIAGONAL_UP_RIGHT_ID,
                22 => DIAGONAL_UP_LEFT_ID,
                23 => DIAGONAL_RIGHT_ID,
                24 => CIRCLE_ID,
                25 => RHOMBUS_ID,
                26 => HALF_VERTICAL_ID,
                27 => HALF_HORIZONTAL_ID,
                28 => HALF_VERTICAL_RIGHT_ID,
                29 => HALF_HORIZONTAL_BOTTOM_ID,
                30 => BORDER_ID,
                31 => CURLY_BORDER_ID,
                32 => GRADIENT_ID,
                33 => GRADIENT_UP_ID,
                34 => BRICKS_ID,
                35 => GLOBE_ID,
                36 => CREEPER_ID,
                37 => SKULL_ID,
                38 => FLOWER_ID,
                39 => MOJANG_ID,
                40 => PIGLIN_ID,
                41 => FLOW_ID,
                42 => GUSTER_ID,
                
                _ => throw new InvalidDataException($"{index} is not a valid banner pattern index!")
            };
        }
        
        private static readonly Dictionary<ResourceLocation, int> id2index = new()
        {
            //[BASE_ID] = 0, // 0 is used for inline patterns
            [SQUARE_BOTTOM_LEFT_ID] = 1,
            [SQUARE_BOTTOM_RIGHT_ID] = 2,
            [SQUARE_TOP_LEFT_ID] = 3,
            [SQUARE_TOP_RIGHT_ID] = 4,
            [STRIPE_BOTTOM_ID] = 5,
            [STRIPE_TOP_ID] = 6,
            [STRIPE_LEFT_ID] = 7,
            [STRIPE_RIGHT_ID] = 8,
            [STRIPE_CENTER_ID] = 9,
            [STRIPE_MIDDLE_ID] = 10,
            [STRIPE_DOWNLEFT_ID] = 11,
            [STRIPE_DOWNRIGHT_ID] = 12,
            [SMALL_STRIPES_ID] = 13,
            [CROSS_ID] = 14,
            [STRAIGHT_CROSS_ID] = 15,
            [TRIANGLE_BOTTOM_ID] = 16,
            [TRIANGLE_TOP_ID] = 17,
            [TRIANGLES_BOTTOM_ID] = 18,
            [TRIANGLES_TOP_ID] = 19,
            [DIAGONAL_LEFT_ID] = 20,
            [DIAGONAL_UP_RIGHT_ID] = 21,
            [DIAGONAL_UP_LEFT_ID] = 22,
            [DIAGONAL_RIGHT_ID] = 23,
            [CIRCLE_ID] = 24,
            [RHOMBUS_ID] = 25,
            [HALF_VERTICAL_ID] = 26,
            [HALF_HORIZONTAL_ID] = 27,
            [HALF_VERTICAL_RIGHT_ID] = 28,
            [HALF_HORIZONTAL_BOTTOM_ID] = 29,
            [BORDER_ID] = 30,
            [CURLY_BORDER_ID] = 31,
            [GRADIENT_ID] = 32,
            [GRADIENT_UP_ID] = 33,
            [BRICKS_ID] = 34,
            [GLOBE_ID] = 35,
            [CREEPER_ID] = 36,
            [SKULL_ID] = 37,
            [FLOWER_ID] = 38,
            [MOJANG_ID] = 39,
            [PIGLIN_ID] = 40,
            [FLOW_ID] = 41,
            [GUSTER_ID] = 42,
        };

        /// <summary>
        /// 0 should be used for inline patterns, remember to attach definition if using 0
        /// </summary>
        public static int GetIndexFromId(ResourceLocation id)
        {
            return id2index.GetValueOrDefault(id, 0);
        }
        
        public override string ToString()
        {
            return AssetId.ToString();
        }
    }
}