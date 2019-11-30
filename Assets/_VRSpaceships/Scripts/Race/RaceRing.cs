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

    
    void OnTriggerExit(Collider other)
    {
        //Debug.Log("Exit");
        RaycastHit hit;
        if (Debug.isDebugBuild)
        {
            Debug.DrawRay(other.gameObject.transform.position, other.gameObject.transform.forward * 100.0f, Color.red,10.0f);
        }

        int layerMask = LayerMask.GetMask("RingExit");
        if (Physics.Raycast(other.attachedRigidbody.transform.position, other.attachedRigidbody.transform.forward, out hit, 10.0f,
        layerMask))
        {
            Debug.Log(hit.collider.name+ " "+ Exit.transform.name);
            if (hit.collider.name == Exit.transform.name)
            {
                _validEntry = true;
                GetComponent<RingActivator>().DeactivateRing();
            }
        }

    }
}
