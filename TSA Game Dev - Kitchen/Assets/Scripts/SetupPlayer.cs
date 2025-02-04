using UnityEngine;

public class SetupPlayer : MonoBehaviour
{
    [SerializeField]
    private ModelToggle modelToggleBehaviour;

    public void SetupThePlayer(Vector3 startPos, int modelIndex)
    {
        transform.position = startPos;
        modelToggleBehaviour.SwitchCharacter(modelIndex);
    }
}
