using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Acheronte : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform [] objectsToSave;
    void Start()
    {
        // TODO DESTROY ALL NON PUZZLE OBJECTS AND VAFFANCULO
        Debug.Log("E tu che se' costì, anima viva, pàrtiti da cotesti che son morti.");
        foreach (Transform child in transform) {
            if (!objectsToSave.Contains(child))
            {
                Debug.Log("Guai a te anima prava!");
                GameObject.Destroy(child.gameObject);
            }
            else Debug.Log("Vuolsi così cola dove si puote ciò che si vuole e di più non dimandare.");
        }
    }
}
