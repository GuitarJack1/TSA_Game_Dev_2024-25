using UnityEngine;

public class Fireplace_Behaviour : MonoBehaviour
{
    [SerializeField]
    GameObject fire;

    public void DeleteFire()
    {
        fire.SetActive(false);
    }
    public void ShowFire()
    {
        fire.SetActive(true);
    }
}
