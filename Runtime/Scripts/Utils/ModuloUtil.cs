namespace CraftSharp
{
    public class ModuloUtil
    {
        public static int Modulo(int a, int b)
        {
            // The result of (a % b) has the same sign as a.
            // Adding b ensures it's at least 0 or greater.
            // The second % b keeps the result within the [0, b) range.
            return ((a % b) + b) % b; 
        }
    }
}