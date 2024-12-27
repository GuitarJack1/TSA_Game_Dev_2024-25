using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food_Meshes", menuName = "Food_Mesh_Settings", order = 1)]
public class Food_Mesh_Settings : ScriptableObject
{

    [Serializable] // Ensure this is serializable
    public class EnumGameObjectPair
    {
        public Global_Variables.FoodType foodType;
        public GameObject foodMesh;
        public float scale;
    }

    [SerializeField] // Make sure this field is serialized
    private List<EnumGameObjectPair> foodMeshes = new List<EnumGameObjectPair>();

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
                foodMeshes.Add(new EnumGameObjectPair { foodType = type });
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

    public List<EnumGameObjectPair> FoodMeshes => foodMeshes; // Optional accessor for runtime

    public GameObject GetGameObject(Global_Variables.FoodType key)
    {
        foreach (var pair in foodMeshes)
        {
            if (pair.foodType == key)
                return pair.foodMesh;
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
