using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer_Pos_Behavior : MonoBehaviour
{
    [SerializeField]
    private Transform[] positions;
    [SerializeField]
    private GameObject customerPrefab;
    [SerializeField]
    private Transform customerSpawnPt;

    private List<Customer_Behavior> customerBehaviors;

    void Start()
    {
        customerBehaviors = new List<Customer_Behavior>();
        StartCoroutine(InstantiateCustomers());
    }

    IEnumerator InstantiateCustomers()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            customerBehaviors.Add(Instantiate(customerPrefab, customerSpawnPt.position, new Quaternion(0, 0, 0, 0)).GetComponent<Customer_Behavior>());
            customerBehaviors[i].targetPos = positions[i];

            yield return new WaitForSeconds(1);
        }
    }

    private void PopulatePositions()
    {
        for (int i = 0; i < customerBehaviors.Count; i++)
        {
            customerBehaviors[i].targetPos = positions[i];
        }
    }

    public void FirstCustomerDone()
    {
        customerBehaviors[0].rb.linearVelocity = new Vector3(0, 1, 0);
        customerBehaviors.Remove(customerBehaviors[0]);
        PopulatePositions();

        Customer_Behavior newCustomer = Instantiate(customerPrefab, customerSpawnPt.position, new Quaternion(0, 0, 0, 0)).GetComponent<Customer_Behavior>();
        customerBehaviors.Add(newCustomer);
        newCustomer.targetPos = positions[^1];
    }
}
