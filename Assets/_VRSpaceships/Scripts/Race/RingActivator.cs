using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingActivator : MonoBehaviour
{
    private bool _activatedRing = true;

    [SerializeField]
    private GameObject _ringLights;
    public void ActivateRing()
    {
        _activatedRing = true;
        _ringLights.SetActive(true);
    }

    public void DeactivateRing()
    {
        _activatedRing = false;
        _ringLights.SetActive(false);
    }
}
