using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;


    class UserHUD : MonoBehaviour
    {
        
        [SerializeField] private TextMeshPro _hpText;

        public void ShowHP(int value)
        {
            if (value < 0) value = 0;
            if (value > 100) value = 100;
            _hpText.text = "" + value;
            _hpText.color = Color32.Lerp( new Color32(255, 0, 0,255), new Color32(16, 221, 0, 255), (float) value / 100.0f);
        }
    }

