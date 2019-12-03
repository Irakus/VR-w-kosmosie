using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceRing : MonoBehaviour
{
    private bool _activatedRing = true;

    private RaceManager _raceManager;

    [SerializeField]
    private GameObject _triggers;

    public void ActivateRing(RaceManager raceManager)
    {
        _activatedRing = true;
        _triggers.SetActive(true);
        _raceManager = raceManager;
    }

    public void DeactivateRing()
    {
        _activatedRing = false;
        _triggers.SetActive(false);
        _raceManager.NextRing();
    }

    public bool IsActivated()
    {
        return _activatedRing;
    }

    IEnumerator CountDown()
    {
        int countdownTime = 5;

    }
}
