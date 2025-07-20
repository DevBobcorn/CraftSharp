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
            AddOrUpdateEntry(BannerPatternType.BASE_ID, new(BannerPatternType.BASE_ID, "block.minecraft.banner.base"));
            AddOrUpdateEntry(BannerPatternType.BORDER_ID, new(BannerPatternType.BORDER_ID, "block.minecraft.banner.border"));
            AddOrUpdateEntry(BannerPatternType.BRICKS_ID, new(BannerPatternType.BRICKS_ID, "block.minecraft.banner.bricks"));
            AddOrUpdateEntry(BannerPatternType.CIRCLE_ID, new(BannerPatternType.CIRCLE_ID, "block.minecraft.banner.circle"));
            AddOrUpdateEntry(BannerPatternType.CREEPER_ID, new(BannerPatternType.CREEPER_ID, "block.minecraft.banner.creeper"));
            AddOrUpdateEntry(BannerPatternType.CROSS_ID, new(BannerPatternType.CROSS_ID, "block.minecraft.banner.cross"));
            AddOrUpdateEntry(BannerPatternType.CURLY_BORDER_ID, new(BannerPatternType.CURLY_BORDER_ID, "block.minecraft.banner.curly_border"));
            AddOrUpdateEntry(BannerPatternType.DIAGONAL_LEFT_ID, new(BannerPatternType.DIAGONAL_LEFT_ID, "block.minecraft.banner.diagonal_left"));
            AddOrUpdateEntry(BannerPatternType.DIAGONAL_RIGHT_ID, new(BannerPatternType.DIAGONAL_RIGHT_ID, "block.minecraft.banner.diagonal_right"));
            AddOrUpdateEntry(BannerPatternType.DIAGONAL_UP_LEFT_ID, new(BannerPatternType.DIAGONAL_UP_LEFT_ID, "block.minecraft.banner.diagonal_up_left"));
            AddOrUpdateEntry(BannerPatternType.DIAGONAL_UP_RIGHT_ID, new(BannerPatternType.DIAGONAL_UP_RIGHT_ID, "block.minecraft.banner.diagonal_up_right"));
            AddOrUpdateEntry(BannerPatternType.FLOW_ID, new(BannerPatternType.FLOW_ID, "block.minecraft.banner.flow"));
            AddOrUpdateEntry(BannerPatternType.FLOWER_ID, new(BannerPatternType.FLOWER_ID, "block.minecraft.banner.flower"));
            AddOrUpdateEntry(BannerPatternType.GLOBE_ID, new(BannerPatternType.GLOBE_ID, "block.minecraft.banner.globe"));
            AddOrUpdateEntry(BannerPatternType.GRADIENT_ID, new(BannerPatternType.GRADIENT_ID, "block.minecraft.banner.gradient"));
            AddOrUpdateEntry(BannerPatternType.GRADIENT_UP_ID, new(BannerPatternType.GRADIENT_UP_ID, "block.minecraft.banner.gradient_up"));
            AddOrUpdateEntry(BannerPatternType.GUSTER_ID, new(BannerPatternType.GUSTER_ID, "block.minecraft.banner.guster"));
            AddOrUpdateEntry(BannerPatternType.HALF_HORIZONTAL_ID, new(BannerPatternType.HALF_HORIZONTAL_ID, "block.minecraft.banner.half_horizontal"));
            AddOrUpdateEntry(BannerPatternType.HALF_HORIZONTAL_BOTTOM_ID, new(BannerPatternType.HALF_HORIZONTAL_BOTTOM_ID, "block.minecraft.banner.half_horizontal_bottom"));
            AddOrUpdateEntry(BannerPatternType.HALF_VERTICAL_ID, new(BannerPatternType.HALF_VERTICAL_ID, "block.minecraft.banner.half_vertical"));
            AddOrUpdateEntry(BannerPatternType.HALF_VERTICAL_RIGHT_ID, new(BannerPatternType.HALF_VERTICAL_RIGHT_ID, "block.minecraft.banner.half_vertical_right"));
            AddOrUpdateEntry(BannerPatternType.MOJANG_ID, new(BannerPatternType.MOJANG_ID, "block.minecraft.banner.mojang"));
            AddOrUpdateEntry(BannerPatternType.PIGLIN_ID, new(BannerPatternType.PIGLIN_ID, "block.minecraft.banner.piglin"));
            AddOrUpdateEntry(BannerPatternType.RHOMBUS_ID, new(BannerPatternType.RHOMBUS_ID, "block.minecraft.banner.rhombus"));
            AddOrUpdateEntry(BannerPatternType.SKULL_ID, new(BannerPatternType.SKULL_ID, "block.minecraft.banner.skull"));
            AddOrUpdateEntry(BannerPatternType.SMALL_STRIPES_ID, new(BannerPatternType.SMALL_STRIPES_ID, "block.minecraft.banner.small_stripes"));
            AddOrUpdateEntry(BannerPatternType.SQUARE_BOTTOM_LEFT_ID, new(BannerPatternType.SQUARE_BOTTOM_LEFT_ID, "block.minecraft.banner.square_bottom_left"));
            AddOrUpdateEntry(BannerPatternType.SQUARE_BOTTOM_RIGHT_ID, new(BannerPatternType.SQUARE_BOTTOM_RIGHT_ID, "block.minecraft.banner.square_bottom_right"));
            AddOrUpdateEntry(BannerPatternType.SQUARE_TOP_LEFT_ID, new(BannerPatternType.SQUARE_TOP_LEFT_ID, "block.minecraft.banner.square_top_left"));
            AddOrUpdateEntry(BannerPatternType.SQUARE_TOP_RIGHT_ID, new(BannerPatternType.SQUARE_TOP_RIGHT_ID, "block.minecraft.banner.square_top_right"));
            AddOrUpdateEntry(BannerPatternType.STRAIGHT_CROSS_ID, new(BannerPatternType.STRAIGHT_CROSS_ID, "block.minecraft.banner.straight_cross"));
            AddOrUpdateEntry(BannerPatternType.STRIPE_BOTTOM_ID, new(BannerPatternType.STRIPE_BOTTOM_ID, "block.minecraft.banner.stripe_bottom"));
            AddOrUpdateEntry(BannerPatternType.STRIPE_CENTER_ID, new(BannerPatternType.STRIPE_CENTER_ID, "block.minecraft.banner.stripe_center"));
            AddOrUpdateEntry(BannerPatternType.STRIPE_DOWNLEFT_ID, new(BannerPatternType.STRIPE_DOWNLEFT_ID, "block.minecraft.banner.stripe_downleft"));
            AddOrUpdateEntry(BannerPatternType.STRIPE_DOWNRIGHT_ID, new(BannerPatternType.STRIPE_DOWNRIGHT_ID, "block.minecraft.banner.stripe_downright"));
            AddOrUpdateEntry(BannerPatternType.STRIPE_LEFT_ID, new(BannerPatternType.STRIPE_LEFT_ID, "block.minecraft.banner.stripe_left"));
            AddOrUpdateEntry(BannerPatternType.STRIPE_MIDDLE_ID, new(BannerPatternType.STRIPE_MIDDLE_ID, "block.minecraft.banner.stripe_middle"));
            AddOrUpdateEntry(BannerPatternType.STRIPE_RIGHT_ID, new(BannerPatternType.STRIPE_RIGHT_ID, "block.minecraft.banner.stripe_right"));
            AddOrUpdateEntry(BannerPatternType.STRIPE_TOP_ID, new(BannerPatternType.STRIPE_TOP_ID, "block.minecraft.banner.stripe_top"));
            AddOrUpdateEntry(BannerPatternType.TRIANGLES_BOTTOM_ID, new(BannerPatternType.TRIANGLES_BOTTOM_ID, "block.minecraft.banner.triangles_bottom"));
            AddOrUpdateEntry(BannerPatternType.TRIANGLES_TOP_ID, new(BannerPatternType.TRIANGLES_TOP_ID, "block.minecraft.banner.triangles_top"));
            AddOrUpdateEntry(BannerPatternType.TRIANGLE_BOTTOM_ID, new(BannerPatternType.TRIANGLE_BOTTOM_ID, "block.minecraft.banner.triangle_bottom"));
            AddOrUpdateEntry(BannerPatternType.TRIANGLE_TOP_ID, new(BannerPatternType.TRIANGLE_TOP_ID, "block.minecraft.banner.triangle_top"));
        }
    }
}
