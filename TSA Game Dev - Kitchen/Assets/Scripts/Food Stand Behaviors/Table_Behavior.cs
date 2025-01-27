using UnityEngine;

public class Table_Behavior : MonoBehaviour
{
    private GameObject playerInteracting;

    [SerializeField]
    private bool freezePlayer;

    [SerializeField]
    private Meal_Assembly_Behavior mealAssemblyBehavior;
    [SerializeField]
    private Customer_Pos_Behavior customerPosBehavior;

    void Start()
    {
        playerInteracting = null;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Food_Item_Behaviour food_Item_Behaviour = collider.gameObject.GetComponentInChildren<Food_Item_Behaviour>();
            if (!playerInteracting && mealAssemblyBehavior.IsFoodTypeNeeded(food_Item_Behaviour.foodType))
            {
                if (freezePlayer)
                    collider.gameObject.GetComponent<Player_Movement>().Freeze();

                playerInteracting = collider.gameObject;
                mealAssemblyBehavior.CompleteFoodType(food_Item_Behaviour.foodType);
                food_Item_Behaviour.SetFoodType(Global_Variables.FoodType.None);

                if (mealAssemblyBehavior.FinishedMeal())
                {
                    customerPosBehavior.FirstCustomerDone();
                }
            }
        }
        else if (collider.CompareTag("Customer"))
        {
            mealAssemblyBehavior.UpdateMeal(collider.gameObject.GetComponent<Customer_Behavior>().meal);
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (collider.gameObject == playerInteracting)
            {
                playerInteracting = null;
                // m_OnExitTrigger.Invoke();
            }
        }
    }
}
