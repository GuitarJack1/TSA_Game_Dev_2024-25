using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Meal_Assembly_Behavior : MonoBehaviour
{
    public Scriptable_Meal currentMeal;
    [SerializeField]
    private Transform foodNeededCanvasTransform;
    [SerializeField]
    private GameObject foodImage;
    [SerializeField]
    private float foodImageSpacing;

    [SerializeField]
    private Food_Image_Settings foodImageSettings;

    private class FoodTypeBooleanPair
    {
        public Global_Variables.FoodType foodType;
        public bool stillNeeded;

        public FoodTypeBooleanPair(Global_Variables.FoodType foodType, bool stillNeeded)
        {
            this.foodType = foodType;
            this.stillNeeded = stillNeeded;
        }
    }
    private List<FoodTypeBooleanPair> currentMealFoods;

    void Start()
    {
        if (currentMeal)
        {
            UpdateMealFoodList();
            UpdateNeededFoodGraphics();
        }
    }
    public void UpdateMeal(Scriptable_Meal newMeal)
    {
        currentMeal = newMeal;
        UpdateMealFoodList();
        UpdateNeededFoodGraphics();
    }
    private void UpdateMealFoodList()
    {
        currentMealFoods = new List<FoodTypeBooleanPair>();

        foreach (Global_Variables.FoodType foodType in currentMeal.neededFoods)
        {
            FoodTypeBooleanPair newFoodTypeBooleanPair = new FoodTypeBooleanPair(foodType, true);
            currentMealFoods.Add(newFoodTypeBooleanPair);
        }
    }

    private void UpdateNeededFoodGraphics()
    {
        int nbChildren = foodNeededCanvasTransform.childCount;

        for (int i = nbChildren - 1; i >= 0; i--)
        {
            Destroy(foodNeededCanvasTransform.GetChild(i).gameObject);
        }

        int numOfCurrFoods = 0;
        foreach (FoodTypeBooleanPair fBP in currentMealFoods)
        {
            if (fBP.stillNeeded)
                numOfCurrFoods++;
        }

        float currGraphicPos = numOfCurrFoods % 2 == 0 ? (int)((numOfCurrFoods - 1) / 2) * -foodImageSpacing - (foodImageSpacing / 2) : ((numOfCurrFoods - 1) / 2) * -foodImageSpacing;
        foreach (FoodTypeBooleanPair fBP in currentMealFoods)
        {
            if (fBP.stillNeeded)
            {
                GameObject newImage = Instantiate(foodImage, foodNeededCanvasTransform);
                newImage.GetComponent<RectTransform>().localPosition = new Vector3(currGraphicPos, 0, 0);
                newImage.GetComponent<RawImage>().texture = foodImageSettings.GetTexture(fBP.foodType);

                currGraphicPos += foodImageSpacing;
            }

        }
    }

    public bool IsFoodTypeNeeded(Global_Variables.FoodType foodType)
    {
        if (!currentMeal)
        {
            return false;
        }
        foreach (FoodTypeBooleanPair foodTypeBooleanPair in currentMealFoods)
        {
            if (foodTypeBooleanPair.foodType == foodType && foodTypeBooleanPair.stillNeeded)
            {
                return true;
            }
        }
        return false;
    }

    public void CompleteFoodType(Global_Variables.FoodType foodType)
    {
        foreach (FoodTypeBooleanPair foodTypeBooleanPair in currentMealFoods)
        {
            if (foodTypeBooleanPair.foodType == foodType && foodTypeBooleanPair.stillNeeded)
            {
                foodTypeBooleanPair.stillNeeded = false;
                UpdateNeededFoodGraphics();
                return;
            }
        }
    }

    public bool FinishedMeal()
    {
        foreach (FoodTypeBooleanPair foodTypeBooleanPair in currentMealFoods)
        {
            if (foodTypeBooleanPair.stillNeeded)
            {
                return false;
            }
        }
        return true;
    }
}
