using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceRing : MonoBehaviour
{
    
    [SerializeField]
    private GameObject Enter;
    [SerializeField]
    private GameObject Exit;
    [SerializeField]
    private bool _validEntry;
    private void OnTriggerEnter(Collider other)
    {
        if (this.name == "Enter")
        {
            _validEntry = true;
        }
        else if (this.name == "Exit")
        {
            if (!_validEntry)
            {
                Enter.SetActive(false);
                Exit.SetActive(false);
            }
            else
            {
                ReportValidRing();
            }
        }

    }

    private void ReportValidRing()
    {

        Debug.Log("Reported");
    }

    // Update is called once per frame
    private void OnTriggerExit(Collider other)
    {
        if (this.name == "Enter")
        {
            Exit.SetActive(true);
        }
        else if (this.name == "Exit")
        {
            Enter.SetActive(true);
        }

    }
}
