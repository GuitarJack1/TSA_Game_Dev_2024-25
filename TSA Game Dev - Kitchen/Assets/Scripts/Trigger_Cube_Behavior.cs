using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Trigger_Cube_Behavior : MonoBehaviour
{
    private GameObject playerInteracting;

    // Event delegates triggered onEnterTrigger
    [FormerlySerializedAs("onEnterTrigger")]
    [SerializeField]
    private UnityEvent m_OnEnterTrigger = new UnityEvent();

    // Event delegates triggered onExitTrigger
    [FormerlySerializedAs("onExitTrigger")]
    [SerializeField]
    private UnityEvent m_OnExitTrigger = new UnityEvent();

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
                collider.gameObject.GetComponent<Player_Movement>().Freeze();
                playerInteracting = collider.gameObject;
                m_OnEnterTrigger.Invoke();
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
                m_OnExitTrigger.Invoke();
            }
        }
    }

    public UnityEvent onEnterTrigger
    {
        get { return m_OnEnterTrigger; }
        set { m_OnEnterTrigger = value; }
    }
    public UnityEvent onExitTrigger
    {
        get { return m_OnExitTrigger; }
        set { m_OnExitTrigger = value; }
    }
}
