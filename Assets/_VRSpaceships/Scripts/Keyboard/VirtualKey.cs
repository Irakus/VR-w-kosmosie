using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VirtualKey : MonoBehaviour
{
    [SerializeField]
    private string character;
    [SerializeField]
    private TextMeshProUGUI text;
    void Start()
    {
        text.text = character.ToUpper();
    }

    public string Click()
    {
        return character;
    }
}
