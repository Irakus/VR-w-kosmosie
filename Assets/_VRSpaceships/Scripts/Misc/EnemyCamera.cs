using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        FindObjectOfType<ScreenChecker>().RegisterEnemyCamera(this);
    }

    // Update is called once per frame
    void OnDisable()
    {
        FindObjectOfType<ScreenChecker>().UnregisterEnemyCamera(this);
    }
}
