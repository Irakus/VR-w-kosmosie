using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingEntryDetector : MonoBehaviour
{
    
    [SerializeField]
    private GameObject Enter;
    [SerializeField]
    private GameObject Exit;
    [SerializeField]
    private bool _validEntry;
    private RaceRing _raceRing;
    void Start()
    {
        _raceRing = GetComponent<RaceRing>();
    }
    
    void OnTriggerExit(Collider other)
    {
        if (!_raceRing.IsActivated() || other.attachedRigidbody == null)
        {
            return;
        }

        RaycastHit hit;
        int layerMask = LayerMask.GetMask("RingExit");
        if (Physics.Raycast(other.attachedRigidbody.transform.position,
                            other.attachedRigidbody.transform.forward, out hit, 5.0f,
                            layerMask))
        {
            if (hit.collider.name == Exit.transform.name)
            {
                _validEntry = true;
                GetComponent<RaceRing>().DeactivateRing();
            }
        }

    }
}
