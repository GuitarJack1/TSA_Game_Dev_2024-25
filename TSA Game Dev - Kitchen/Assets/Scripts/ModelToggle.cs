using UnityEngine;
using System.Collections.Generic;

public class ModelToggle : MonoBehaviour
{
    // Assign this to the parent “Characters@Walking” in the Inspector.
    // All the character variations are children of that parent.
    public Transform modelRoot;

    // The name of the child object you ALWAYS want active
    public string hipsObjectName = "mixamorig:Hips";

    // We’ll store all the togglable models (not including hips).
    private List<GameObject> characterModels = new List<GameObject>();

    private int currentIndex = -1;

    void Awake()
    {
        // Gather every child of modelRoot EXCEPT the hips object.
        foreach (Transform child in modelRoot)
        {
            if (child.name != hipsObjectName)
            {
                characterModels.Add(child.gameObject);
            }
        }
    }

    void Start()
    {
        // Optionally, pick one to show at startup (e.g. index 0)
        SwitchCharacter(Random.Range(0, characterModels.Count - 1));
    }

    // Call this method with an index to change which model is visible.
    public void SwitchCharacter(int newIndex)
    {
        if (newIndex < 0 || newIndex >= characterModels.Count) return;

        // Hide all except the one we want
        for (int i = 0; i < characterModels.Count; i++)
        {
            characterModels[i].SetActive(i == newIndex);
        }

        currentIndex = newIndex;
    }
}
