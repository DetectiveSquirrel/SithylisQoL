using System;
using ExileCore.Shared.AtlasHelper;
using SharpDX;

namespace Random_Features.Libs
{
    public class MapIcon
    {
        public AtlasTexture Texture { get; set; }
        public Color Color { get; set; } = Color.White;
        public float Size { get; set; } = 13;

        public MapIcon()
        {
        }

        public MapIcon(AtlasTexture texture, Color color, float size)
        {
            Texture = texture;
            Color = color;
            Size = size;
        }
        public MapIcon(AtlasTexture texture, float size)
        {
            Texture = texture;
            Size = size;
        }

        public static Vector2 DeltaInWorldToMinimapDelta(Vector2 delta, double diag, float scale, float deltaZ = 0)
        {
            const float CAMERA_ANGLE = 38 * MathUtil.Pi / 180;

            // Values according to 40 degree rotation of cartesian coordiantes, still doesn't seem right but closer
            var cos = (float) (diag * Math.Cos(CAMERA_ANGLE) / scale);
            var sin = (float) (diag * Math.Sin(CAMERA_ANGLE) / scale); // possible to use cos so angle = nearly 45 degrees

            // 2D rotation formulas not correct, but it's what appears to work?
            return new Vector2((delta.X - delta.Y) * cos, deltaZ - (delta.X + delta.Y) * sin);
        }
    }
}
