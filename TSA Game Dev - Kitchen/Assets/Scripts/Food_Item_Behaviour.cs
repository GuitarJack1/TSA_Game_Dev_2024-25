using UnityEngine;

public class Food_Item_Behaviour : MonoBehaviour
{

    public Global_Variables.FoodType foodType;
    private GameObject currentFoodObject;

    [SerializeField]
    private Food_Mesh_Settings food_Mesh_Settings;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateMesh();
    }

    public void SetFoodType(Global_Variables.FoodType food)
    {
        foodType = food;
        UpdateMesh();
    }

    public void UpdateMesh()
    {
        if (currentFoodObject)
        {
            Destroy(currentFoodObject);
            currentFoodObject = null;
        }

        if (foodType != Global_Variables.FoodType.None)
        {
            GameObject newFoodObject = Instantiate(food_Mesh_Settings.GetGameObject(foodType), transform);
            float scale = food_Mesh_Settings.GetScale(foodType);
            newFoodObject.transform.localScale = new Vector3(scale, scale, scale);

            currentFoodObject = newFoodObject;
        }
    }
}
