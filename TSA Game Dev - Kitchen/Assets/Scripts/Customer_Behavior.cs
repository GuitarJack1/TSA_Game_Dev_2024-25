using UnityEngine;

public class Customer_Behavior : MonoBehaviour
{
    public Scriptable_Meal meal;
    public Transform targetPos;

    [SerializeField]
    private Scriptable_Meal[] possibleMeals;
    [SerializeField]
    private float desiredDistFromPos;
    [SerializeField]
    private float customerSpeed;

    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meal = possibleMeals[Random.Range(0, possibleMeals.Length - 1)];
    }

    void Update()
    {
        if (targetPos)
        {
            Vector3 flatVectorToPos = (new Vector3(targetPos.position.x, 0, targetPos.position.z)) - (new Vector3(transform.position.x, 0, transform.position.z));

            if (flatVectorToPos.magnitude > desiredDistFromPos)
            {
                transform.forward = flatVectorToPos;
                rb.linearVelocity = new Vector3(flatVectorToPos.normalized.x, rb.linearVelocity.y, flatVectorToPos.normalized.z) * customerSpeed;
            }
            else
            {
                rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
                transform.forward = new Vector3(0, 0, 1).normalized;
            }
        }
    }
}
