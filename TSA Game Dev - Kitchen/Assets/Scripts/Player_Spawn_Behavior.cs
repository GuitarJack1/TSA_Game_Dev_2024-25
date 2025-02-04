using UnityEngine;

public class Player_Spawn_Behavior : MonoBehaviour
{
    [SerializeField]
    private GameObject playerWASDObject;
    [SerializeField]
    private GameObject playerTFGHObject;
    [SerializeField]
    private GameObject playerIJKLObject;
    [SerializeField]
    private GameObject playerARROWSObject;

    private Player_Controls playerControls;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControls = new Player_Controls();
        playerControls.Players.Enable();

        playerWASDObject.SetActive(false);
        playerTFGHObject.SetActive(false);
        playerIJKLObject.SetActive(false);
        playerARROWSObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControls.Players.WASD.ReadValue<Vector2>() != Vector2.zero)
        {
            playerWASDObject.SetActive(true);
        }
        else if (playerControls.Players.TFGH.ReadValue<Vector2>() != Vector2.zero)
        {
            playerTFGHObject.SetActive(true);
        }
        else if (playerControls.Players.IJKL.ReadValue<Vector2>() != Vector2.zero)
        {
            playerIJKLObject.SetActive(true);
        }
        else if (playerControls.Players.Arrows.ReadValue<Vector2>() != Vector2.zero)
        {
            playerARROWSObject.SetActive(true);
        }
    }
}
