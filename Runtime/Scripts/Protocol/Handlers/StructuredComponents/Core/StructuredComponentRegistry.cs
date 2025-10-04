using System;
using System.Collections.Generic;
using CraftSharp.Protocol.Handlers.StructuredComponents.Components;
using UnityEngine;

namespace CraftSharp.Protocol.Handlers.StructuredComponents.Core
{
    public abstract class StructuredComponentRegistry : IdentifierPalette<Type>
    {
        private readonly IMinecraftDataTypes dataTypes;
        private readonly ItemPalette itemPalette;

        public SubComponentRegistry SubComponentRegistry { get; }

        protected override string Name => "StructuredComponent Palette";
        protected override Type UnknownObject => typeof (StructuredComponent);

        protected StructuredComponentRegistry(IMinecraftDataTypes dataTypes, ItemPalette itemPalette, SubComponentRegistry subComponentRegistry)
        {
            this.dataTypes = dataTypes;
            this.itemPalette = itemPalette;
            SubComponentRegistry = subComponentRegistry;
        }
        
        protected void RegisterComponent<T>(int numId, ResourceLocation id)
        {
            AddEntry(id, numId, typeof (T));
        }

        public StructuredComponent ParseComponent(int numId, Queue<byte> data)
        {
            if (TryGetByNumId(numId, out var type))
            {
                var component =
                    Activator.CreateInstance(type, itemPalette, SubComponentRegistry) as StructuredComponent 
                    ?? throw new InvalidOperationException($"Could not instantiate a parser for a structured component type {numId}");
                
                component.Parse(dataTypes, data);
                return component;
            }
            throw new Exception($"No parser found for component with num Id {numId}");
        }
        
        public StructuredComponent ParseComponentFromJson(ResourceLocation id, Json.JSONData data)
        {
            if (TryGetById(id, out var type))
            {
                var component =
                    Activator.CreateInstance(type, itemPalette, SubComponentRegistry) as StructuredComponent 
                    ?? throw new InvalidOperationException($"Could not instantiate a parser for a structured component type {id}");

                try
                {
                    component.ParseFromJson(dataTypes, data);
                }
                catch (Exception)
                {
                    Debug.LogWarning($"Failed to parse data for default component {id}");
                }

                return component;
            }

            // throw new Exception($"No parser found for component with Id {id}");
            Debug.LogWarning($"No parser found for component with Id {id}");
            return new EmptyComponent(itemPalette, SubComponentRegistry);
        }
    }
}