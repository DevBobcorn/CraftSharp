namespace CraftSharp
{
    public sealed class BannerPatternPalette : DynamicPalette<BannerPatternType>
    {
        public static readonly BannerPatternPalette INSTANCE = new();
        protected override string Name => "BannerPattern Palette";
        protected override BannerPatternType UnknownObject => BannerPatternType.DUMMY_BANNER_PATTERN_TYPE;

        private BannerPatternPalette()
        {
            // Generated using https://gist.github.com/DevBobcorn/5bd88c9ec7576c164ff8a73dc3048631
            AddEntry(BannerPatternType.BASE_ID, new(BannerPatternType.BASE_ID, "block.minecraft.banner.base"));
            AddEntry(BannerPatternType.BORDER_ID, new(BannerPatternType.BORDER_ID, "block.minecraft.banner.border"));
            AddEntry(BannerPatternType.BRICKS_ID, new(BannerPatternType.BRICKS_ID, "block.minecraft.banner.bricks"));
            AddEntry(BannerPatternType.CIRCLE_ID, new(BannerPatternType.CIRCLE_ID, "block.minecraft.banner.circle"));
            AddEntry(BannerPatternType.CREEPER_ID, new(BannerPatternType.CREEPER_ID, "block.minecraft.banner.creeper"));
            AddEntry(BannerPatternType.CROSS_ID, new(BannerPatternType.CROSS_ID, "block.minecraft.banner.cross"));
            AddEntry(BannerPatternType.CURLY_BORDER_ID, new(BannerPatternType.CURLY_BORDER_ID, "block.minecraft.banner.curly_border"));
            AddEntry(BannerPatternType.DIAGONAL_LEFT_ID, new(BannerPatternType.DIAGONAL_LEFT_ID, "block.minecraft.banner.diagonal_left"));
            AddEntry(BannerPatternType.DIAGONAL_RIGHT_ID, new(BannerPatternType.DIAGONAL_RIGHT_ID, "block.minecraft.banner.diagonal_right"));
            AddEntry(BannerPatternType.DIAGONAL_UP_LEFT_ID, new(BannerPatternType.DIAGONAL_UP_LEFT_ID, "block.minecraft.banner.diagonal_up_left"));
            AddEntry(BannerPatternType.DIAGONAL_UP_RIGHT_ID, new(BannerPatternType.DIAGONAL_UP_RIGHT_ID, "block.minecraft.banner.diagonal_up_right"));
            AddEntry(BannerPatternType.FLOW_ID, new(BannerPatternType.FLOW_ID, "block.minecraft.banner.flow"));
            AddEntry(BannerPatternType.FLOWER_ID, new(BannerPatternType.FLOWER_ID, "block.minecraft.banner.flower"));
            AddEntry(BannerPatternType.GLOBE_ID, new(BannerPatternType.GLOBE_ID, "block.minecraft.banner.globe"));
            AddEntry(BannerPatternType.GRADIENT_ID, new(BannerPatternType.GRADIENT_ID, "block.minecraft.banner.gradient"));
            AddEntry(BannerPatternType.GRADIENT_UP_ID, new(BannerPatternType.GRADIENT_UP_ID, "block.minecraft.banner.gradient_up"));
            AddEntry(BannerPatternType.GUSTER_ID, new(BannerPatternType.GUSTER_ID, "block.minecraft.banner.guster"));
            AddEntry(BannerPatternType.HALF_HORIZONTAL_ID, new(BannerPatternType.HALF_HORIZONTAL_ID, "block.minecraft.banner.half_horizontal"));
            AddEntry(BannerPatternType.HALF_HORIZONTAL_BOTTOM_ID, new(BannerPatternType.HALF_HORIZONTAL_BOTTOM_ID, "block.minecraft.banner.half_horizontal_bottom"));
            AddEntry(BannerPatternType.HALF_VERTICAL_ID, new(BannerPatternType.HALF_VERTICAL_ID, "block.minecraft.banner.half_vertical"));
            AddEntry(BannerPatternType.HALF_VERTICAL_RIGHT_ID, new(BannerPatternType.HALF_VERTICAL_RIGHT_ID, "block.minecraft.banner.half_vertical_right"));
            AddEntry(BannerPatternType.MOJANG_ID, new(BannerPatternType.MOJANG_ID, "block.minecraft.banner.mojang"));
            AddEntry(BannerPatternType.PIGLIN_ID, new(BannerPatternType.PIGLIN_ID, "block.minecraft.banner.piglin"));
            AddEntry(BannerPatternType.RHOMBUS_ID, new(BannerPatternType.RHOMBUS_ID, "block.minecraft.banner.rhombus"));
            AddEntry(BannerPatternType.SKULL_ID, new(BannerPatternType.SKULL_ID, "block.minecraft.banner.skull"));
            AddEntry(BannerPatternType.SMALL_STRIPES_ID, new(BannerPatternType.SMALL_STRIPES_ID, "block.minecraft.banner.small_stripes"));
            AddEntry(BannerPatternType.SQUARE_BOTTOM_LEFT_ID, new(BannerPatternType.SQUARE_BOTTOM_LEFT_ID, "block.minecraft.banner.square_bottom_left"));
            AddEntry(BannerPatternType.SQUARE_BOTTOM_RIGHT_ID, new(BannerPatternType.SQUARE_BOTTOM_RIGHT_ID, "block.minecraft.banner.square_bottom_right"));
            AddEntry(BannerPatternType.SQUARE_TOP_LEFT_ID, new(BannerPatternType.SQUARE_TOP_LEFT_ID, "block.minecraft.banner.square_top_left"));
            AddEntry(BannerPatternType.SQUARE_TOP_RIGHT_ID, new(BannerPatternType.SQUARE_TOP_RIGHT_ID, "block.minecraft.banner.square_top_right"));
            AddEntry(BannerPatternType.STRAIGHT_CROSS_ID, new(BannerPatternType.STRAIGHT_CROSS_ID, "block.minecraft.banner.straight_cross"));
            AddEntry(BannerPatternType.STRIPE_BOTTOM_ID, new(BannerPatternType.STRIPE_BOTTOM_ID, "block.minecraft.banner.stripe_bottom"));
            AddEntry(BannerPatternType.STRIPE_CENTER_ID, new(BannerPatternType.STRIPE_CENTER_ID, "block.minecraft.banner.stripe_center"));
            AddEntry(BannerPatternType.STRIPE_DOWNLEFT_ID, new(BannerPatternType.STRIPE_DOWNLEFT_ID, "block.minecraft.banner.stripe_downleft"));
            AddEntry(BannerPatternType.STRIPE_DOWNRIGHT_ID, new(BannerPatternType.STRIPE_DOWNRIGHT_ID, "block.minecraft.banner.stripe_downright"));
            AddEntry(BannerPatternType.STRIPE_LEFT_ID, new(BannerPatternType.STRIPE_LEFT_ID, "block.minecraft.banner.stripe_left"));
            AddEntry(BannerPatternType.STRIPE_MIDDLE_ID, new(BannerPatternType.STRIPE_MIDDLE_ID, "block.minecraft.banner.stripe_middle"));
            AddEntry(BannerPatternType.STRIPE_RIGHT_ID, new(BannerPatternType.STRIPE_RIGHT_ID, "block.minecraft.banner.stripe_right"));
            AddEntry(BannerPatternType.STRIPE_TOP_ID, new(BannerPatternType.STRIPE_TOP_ID, "block.minecraft.banner.stripe_top"));
            AddEntry(BannerPatternType.TRIANGLES_BOTTOM_ID, new(BannerPatternType.TRIANGLES_BOTTOM_ID, "block.minecraft.banner.triangles_bottom"));
            AddEntry(BannerPatternType.TRIANGLES_TOP_ID, new(BannerPatternType.TRIANGLES_TOP_ID, "block.minecraft.banner.triangles_top"));
            AddEntry(BannerPatternType.TRIANGLE_BOTTOM_ID, new(BannerPatternType.TRIANGLE_BOTTOM_ID, "block.minecraft.banner.triangle_bottom"));
            AddEntry(BannerPatternType.TRIANGLE_TOP_ID, new(BannerPatternType.TRIANGLE_TOP_ID, "block.minecraft.banner.triangle_top"));
        }
    }
}
