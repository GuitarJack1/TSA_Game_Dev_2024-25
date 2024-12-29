using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Food_Image_Settings", menuName = "Scriptable Objects/Food_Image_Settings")]
public class Food_Image_Settings : ScriptableObject
{
    [Serializable] // Ensure this is serializable
    public class EnumImagePair
    {
        public Global_Variables.FoodType foodType;
        public Texture foodImage;
        public float scale;
    }

    [SerializeField] // Make sure this field is serialized
    private List<EnumImagePair> foodMeshes = new List<EnumImagePair>();

    // Ensure each FoodType is unique and populate missing ones
    private void OnValidate()
    {
        var enumValues = Enum.GetValues(typeof(Global_Variables.FoodType));
        var existingTypes = new HashSet<Global_Variables.FoodType>();

        // Add missing FoodTypes
        foreach (Global_Variables.FoodType type in enumValues)
        {
            if (!foodMeshes.Exists(pair => pair.foodType == type))
            {
                foodMeshes.Add(new EnumImagePair { foodType = type });
            }
        }

        // Remove duplicates
        for (int i = foodMeshes.Count - 1; i >= 0; i--)
        {
            var foodType = foodMeshes[i].foodType;
            if (existingTypes.Contains(foodType))
            {
                foodMeshes.RemoveAt(i);
            }
            else
            {
                existingTypes.Add(foodType);
            }
        }
    }

    public List<EnumImagePair> FoodMeshes => foodMeshes; // Optional accessor for runtime

    public Texture GetTexture(Global_Variables.FoodType key)
    {
        foreach (var pair in foodMeshes)
        {
            if (pair.foodType == key)
                return pair.foodImage;
        }
        throw new KeyNotFoundException($"FoodType: {key} not found in map.");
    }
    public float GetScale(Global_Variables.FoodType key)
    {
        foreach (var pair in foodMeshes)
        {
            if (pair.foodType == key)
                return pair.scale;
        }
        throw new KeyNotFoundException($"FoodType: {key} not found in map.");
    }
}
