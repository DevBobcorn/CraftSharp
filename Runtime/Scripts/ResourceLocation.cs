using System;

namespace CraftSharp
{
    public readonly struct ResourceLocation : IEquatable<ResourceLocation>
    {
        private const string DEFAULT_NAMESPACE = "minecraft";
        public static readonly ResourceLocation INVALID = new(DEFAULT_NAMESPACE, "<missingno>");

        public readonly string Namespace;
        public readonly string Path;

        public ResourceLocation(string ns, string path)
        {
            this.Namespace = ns;
            this.Path = path;
        }

        public ResourceLocation(string path)
        {
            this.Namespace = DEFAULT_NAMESPACE;
            this.Path = path;
        }

        public static ResourceLocation FromString(string source)
        {
            if (source.Contains(':'))
            {
                string[] parts = source.Split(':', 2);
                return new ResourceLocation(parts[0], parts[1]);
            }
            return new ResourceLocation(source);
        }

        public static bool operator ==(ResourceLocation a, ResourceLocation b)
        {
            return a.Namespace == b.Namespace && a.Path == b.Path;
        }
        public static bool operator !=(ResourceLocation a, ResourceLocation b)
        {
            return a.Namespace != b.Namespace || a.Path != b.Path;
        }

        public override bool Equals(object obj)
        {
            if (obj is ResourceLocation other)
            {
                return this.Namespace == other.Namespace && this.Path == other.Path;
            }
            return false;
        }

        public bool Equals(ResourceLocation other)
        {
            return Namespace == other.Namespace && Path == other.Path;
        }
        
        public override int GetHashCode()
        {
            return Namespace.GetHashCode() ^ Path.GetHashCode();
        }

        public string GetTranslationKey(string category) => $"{category}.{Namespace}.{Path}";

        public override string ToString() => $"{Namespace}:{Path}";

    }
}