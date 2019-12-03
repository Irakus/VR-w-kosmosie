using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [SerializeField] private List<RaceRing> _rings;

    private int _currentRing;
    void Start()
    {
        _currentRing = 0;
        _rings[_currentRing].ActivateRing(this);
    }

    public void NextRing()
    {
        _currentRing += 1;
        if (_rings.Count == _currentRing)
        {
            EndRace();
        }
        else
        {
            _rings[_currentRing].ActivateRing(this);
        }
    }

    private void EndRace()
    {
        return;
    }
}
