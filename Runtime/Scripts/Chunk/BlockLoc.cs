using System;

namespace CraftSharp
{
    /// <summary>
    /// Represents a block location into a Minecraft world
    /// </summary>
    public struct BlockLoc
    {
        /// <summary>
        /// The X Coordinate
        /// </summary>
        public int X;
        
        /// <summary>
        /// The Y Coordinate (vertical)
        /// </summary>
        public int Y;

        /// <summary>
        /// The Z coordinate
        /// </summary>
        public int Z;

        /// <summary>
        /// Get location with zeroed coordinates
        /// </summary>
        public static BlockLoc Zero => new(0, 0, 0);

        /// <summary>
        /// Create a new location
        /// </summary>
        public BlockLoc(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Get a squared distance to the specified location
        /// </summary>
        /// <param name="blockLoc">Other location for computing distance</param>
        /// <returns>Distance to the specified location, without using a square root</returns>
        public readonly double DistanceSquared(BlockLoc blockLoc)
        {
            return ((X - blockLoc.X) * (X - blockLoc.X))
                 + ((Y - blockLoc.Y) * (Y - blockLoc.Y))
                 + ((Z - blockLoc.Z) * (Z - blockLoc.Z));
        }

        /// <summary>
        /// Get exact distance to the specified location
        /// </summary>
        /// <param name="blockLoc">Other location for computing distance</param>
        /// <returns>Distance to the specified location, with square root so lower performances</returns>
        public readonly double Distance(BlockLoc blockLoc)
        {
            return Math.Sqrt(DistanceSquared(blockLoc));
        }

        /// <summary>
        /// Compare two locations. Locations are equals if the integer part of their coordinates are equals.
        /// </summary>
        /// <param name="obj">Object to compare to</param>
        /// <returns>TRUE if the locations are equals</returns>
        public readonly override bool Equals(object obj)
        {
            if (obj is BlockLoc loc)
            {
                return ((int)this.X) == ((int)loc.X)
                    && ((int)this.Y) == ((int)loc.Y)
                    && ((int)this.Z) == ((int)loc.Z);
            }
            return false;
        }

        /// <summary>
        /// Compare two locations. Locations are equals if the integer part of their coordinates are equals.
        /// </summary>
        /// <param name="loc1">First location to compare</param>
        /// <param name="loc2">Second location to compare</param>
        /// <returns>TRUE if the locations are equals</returns>
        public static bool operator ==(BlockLoc loc1, BlockLoc loc2)
        {
            return loc1.Equals(loc2);
        }

        /// <summary>
        /// Compare two locations. Locations are not equals if the integer part of their coordinates are not equals.
        /// </summary>
        /// <param name="loc1">First location to compare</param>
        /// <param name="loc2">Second location to compare</param>
        /// <returns>TRUE if the locations are equals</returns>
        public static bool operator !=(BlockLoc loc1, BlockLoc loc2)
        {
            return !loc1.Equals(loc2);
        }

        /// <summary>
        /// Sums two locations and returns the result.
        /// </summary>
        /// <exception cref="NullReferenceException">
        /// Thrown if one of the provided location is null
        /// </exception>
        /// <param name="loc1">First location to sum</param>
        /// <param name="loc2">Second location to sum</param>
        /// <returns>Sum of the two locations</returns>
        public static BlockLoc operator +(BlockLoc loc1, BlockLoc loc2)
        {
            return new BlockLoc
            (
                loc1.X + loc2.X,
                loc1.Y + loc2.Y,
                loc1.Z + loc2.Z
            );
        }

        /// <summary>
        /// Substract a location to another
        /// </summary>
        /// <exception cref="NullReferenceException">
        /// Thrown if one of the provided location is null
        /// </exception>
        /// <param name="loc1">First location</param>
        /// <param name="loc2">Location to substract to the first one</param>
        /// <returns>Sum of the two locations</returns>
        public static BlockLoc operator -(BlockLoc loc1, BlockLoc loc2)
        {
            return new BlockLoc
            (
                loc1.X - loc2.X,
                loc1.Y - loc2.Y,
                loc1.Z - loc2.Z
            );
        }

        /// <summary>
        /// DO NOT USE. Defined to comply with C# requirements requiring a GetHashCode() when overriding Equals() or ==
        /// </summary>
        /// <remarks>
        /// A modulo will be applied if the location is outside the following ranges:
        /// X: -4096 to +4095
        /// Y: -32 to +31
        /// Z: -4096 to +4095
        /// </remarks>
        /// <returns>A simplified version of the location</returns>
        public readonly override int GetHashCode()
        {
            return (X & ~((~0) << 13)) << 19
                 | (Y & ~((~0) << 13)) << 13
                 | (Z & ~((~0) << 06)) << 00;
        }

        /// <summary>
        /// Convert the location into a string representation
        /// </summary>
        /// <returns>String representation of the location</returns>
        public readonly override string ToString()
        {
            return $"X: {X} Y: {Y} Z: {Z}";
        }

        public readonly Location ToLocation()
        {
            return new Location(this.X, this.Y, this.Z);
        }

        public readonly Location ToCenterLocation()
        {
            return new Location(this.X + 0.5F, this.Y + 0.5F, this.Z + 0.5F);
        }

        public readonly double DistanceTo(BlockLoc blockLoc)
        {
            return Math.Sqrt(Math.Pow(this.X - blockLoc.X, 2) + Math.Pow(this.Y - blockLoc.Y, 2) + Math.Pow(this.Z - blockLoc.Z, 2));
        }

        public readonly double SqrDistanceTo(BlockLoc blockLoc)
        {
            return Math.Pow(this.X - blockLoc.X, 2) + Math.Pow(this.Y - blockLoc.Y, 2) + Math.Pow(this.Z - blockLoc.Z, 2);
        }

        public readonly int ManhattanDistanceTo(BlockLoc blockLoc)
        {
            return Math.Abs(this.X - blockLoc.X) + Math.Abs(this.Y - blockLoc.Y) + Math.Abs(this.Z - blockLoc.Z);
        }

        public readonly BlockLoc Up()
        {
            return new BlockLoc(X, Y + 1, Z);
        }

        public readonly BlockLoc Down()
        {
            return new BlockLoc(X, Y - 1, Z);
        }

        // MC Z Neg
        public readonly BlockLoc North()
        {
            return new BlockLoc(X, Y, Z - 1);
        }

        // MC Z Pos
        public readonly BlockLoc South()
        {
            return new BlockLoc(X, Y, Z + 1);
        }

        // MC X Pos
        public readonly BlockLoc East()
        {
            return new BlockLoc(X + 1, Y, Z);
        }

        // MC X Neg
        public readonly BlockLoc West()
        {
            return new BlockLoc(X - 1, Y, Z);
        }
    }
}
