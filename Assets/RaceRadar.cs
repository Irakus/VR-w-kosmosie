using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceRadar : MonoBehaviour
{
    RaceManager _raceManager;
    [SerializeField]
    GameObject arrow;
    void Start()
    {
        _raceManager = FindObjectOfType<RaceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform ringPos = _raceManager.GetCurrentRingPosition();
        if(ringPos == null)
        {

        }
        else
        {
            arrow.transform.LookAt(ringPos);
        }
    }
}
