using UnityEngine;

public class WaterBehavior : MonoBehaviour
{
    private GameObject playerInteracting;

    [SerializeField]
    private bool freezePlayer;

    void Start()
    {
        playerInteracting = null;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Food_Item_Behaviour food_Item_Behaviour = collider.gameObject.GetComponentInChildren<Food_Item_Behaviour>();
            if (!playerInteracting && food_Item_Behaviour.foodType == Global_Variables.FoodType.None)
            {
                if (freezePlayer)
                    collider.gameObject.GetComponent<Player_Movement>().Freeze();

                playerInteracting = collider.gameObject;
                food_Item_Behaviour.SetFoodType(Global_Variables.FoodType.Water);
            }
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
