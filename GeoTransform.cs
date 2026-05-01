namespace modified_structure_analysis
{
    public class GeoTransform
    {
        public double OriginX { get; set; }
        public double OriginY { get; set; }
        public double PixelSizeX { get; set; }
        public double PixelSizeY { get; set; }
        public double Rotation1 { get; set; }
        public double Rotation2 { get; set; }

        public string? ProjectionWkt { get; set; }
        public string? ProjectionName { get; set; }
        public string? CoordinateSystem { get; set; }
        public string? UnitName { get; set; }

        public GeoTransform()
        {
            PixelSizeX = 1.0;
            PixelSizeY = 1.0;
        }

        public GeoTransform(double originX, double originY, double pixelSizeX, double pixelSizeY)
        {
            OriginX = originX;
            OriginY = originY;
            PixelSizeX = pixelSizeX;
            PixelSizeY = pixelSizeY;
            Rotation1 = 0;
            Rotation2 = 0;
        }

        public double[] ToGdalArray()
        {
            return new double[]
            {
                OriginX,
                PixelSizeX,
                Rotation1,
                OriginY,
                Rotation2,
                PixelSizeY
            };
        }

        public static GeoTransform FromGdalArray(double[] arr)
        {
            if (arr.Length < 6)
                throw new ArgumentException("GeoTransform array must have at least 6 elements");

            return new GeoTransform
            {
                OriginX = arr[0],
                PixelSizeX = arr[1],
                Rotation1 = arr[2],
                OriginY = arr[3],
                Rotation2 = arr[4],
                PixelSizeY = arr[5]
            };
        }

        public (int x, int y) WorldToPixel(double worldX, double worldY)
        {
            int px = (int)((worldX - OriginX) / PixelSizeX);
            int py = (int)((worldY - OriginY) / PixelSizeY);
            return (px, py);
        }

        public (double x, double y) PixelToWorld(int pixelX, int pixelY)
        {
            double wx = OriginX + (pixelX + 0.5) * PixelSizeX;
            double wy = OriginY + (pixelY + 0.5) * PixelSizeY;
            return (wx, wy);
        }

        public override string ToString()
        {
            return $"Origin: ({OriginX}, {OriginY}), PixelSize: ({PixelSizeX}, {PixelSizeY}), Projection: {ProjectionName ?? "None"}";
        }
    }
}