using System;
using System.Collections.Generic;
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
        public static readonly ResourceLocation TRIANGLES_BOTTOM_ID            = new("triangles_bottom");
        public static readonly ResourceLocation TRIANGLES_TOP_ID               = new("triangles_top");
        public static readonly ResourceLocation TRIANGLE_BOTTOM_ID             = new("triangle_bottom");
        public static readonly ResourceLocation TRIANGLE_TOP_ID                = new("triangle_top");

        public static readonly BannerPatternType DUMMY_BANNER_PATTERN_TYPE = new(
            ResourceLocation.INVALID, string.Empty);

        public readonly ResourceLocation AssetId;
        public string TranslationKey;

        public BannerPatternType(ResourceLocation assetId, string translationKey)
        {
            AssetId = assetId;
            TranslationKey = translationKey;
        }

        public override string ToString()
        {
            return AssetId.ToString();
        }
    }
}