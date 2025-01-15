using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Trashcan_Behavior : MonoBehaviour
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
            if (!playerInteracting)
            {
                if (freezePlayer)
                    collider.gameObject.GetComponent<Player_Movement>().Freeze();

                playerInteracting = collider.gameObject;
                collider.gameObject.GetComponentInChildren<Food_Item_Behaviour>().SetFoodType(Global_Variables.FoodType.None);
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
