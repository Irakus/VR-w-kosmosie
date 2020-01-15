using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceRadar : MonoBehaviour
{
    RaceManager _raceManager;
    [SerializeField]
    GameObject arrow;
    bool race = false;
    void Start()
    {
        _raceManager = FindObjectOfType<RaceManager>();
        if (_raceManager != null) race = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (race)
        {
            Transform ringPos = _raceManager.GetCurrentRingPosition();
            if (ringPos == null)
            {

            }
            else
            {
                arrow.transform.LookAt(ringPos);
            }
        }
    }
}
