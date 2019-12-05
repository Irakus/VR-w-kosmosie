using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragCounter : MonoBehaviour
{
    private TextMesh _textMesh;

    public int Count {
        get => _count;
        set
        {
            if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
            _count = value;
            _textMesh.text = value.ToString();
        }
    }

    private int _count;

    private void Awake()
    {
        _textMesh = GetComponent<TextMesh>();
    }
}
