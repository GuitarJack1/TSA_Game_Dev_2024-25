using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable_Meal", menuName = "Scriptable Objects/Scriptable_Meal_Settings", order = 1)]
public class Scriptable_Meal : ScriptableObject
{
    public string mealName;
    public List<Global_Variables.FoodType> neededFoods = new List<Global_Variables.FoodType>();
}
