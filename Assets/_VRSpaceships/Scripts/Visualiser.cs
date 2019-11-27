using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Visualiser : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract void ChangeHandlesPosition();

    public abstract void ChangeYokePosition();
    public abstract void ChangeCameraPosition();
    public abstract void ChangeButtonPosition();

    //[SerializeField] protected GameObject Yoke;
    //[SerializeField] protected GameObject Pad;

    public abstract void AdjustControls();
}
