using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.XR.Interaction;
using UnityEngine.UI;

public class VirtualKeyboard : MonoBehaviour
{
    private const int ARROW_ASCII = 8592;
    private const char ARROW_CHAR = (char) ARROW_ASCII;
    private static string ARROW_STRING = ARROW_CHAR.ToString();
    public TMP_InputField inputField;
    private Camera _camera;
    private PointerEventData _pointerEventData;
    private EventSystem _eventSystem;
    private GraphicRaycaster _raycaster;
    private RaceManager _raceManager;
    [SerializeField] private GameObject _crosshair;


    public string nickName;
    private bool Submit = false;
    void Start()
    {
        inputField.Select();
        _camera = Camera.main;
        _eventSystem = FindObjectOfType<EventSystem>();
        _raycaster = GetComponentInChildren<GraphicRaycaster>();
    }

    private void OnEnable()
    {
        _crosshair.SetActive(true);
    }

    private void OnDisable()
    {
        _crosshair.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _pointerEventData = new PointerEventData(_eventSystem);
        _pointerEventData.position = _camera.WorldToScreenPoint(_crosshair.transform.position);
        List<RaycastResult> results = new List<RaycastResult>();
        if (Debug.isDebugBuild)
        {
            Debug.DrawLine(_camera.transform.position, _camera.transform.position + _camera.transform.forward * 15.0f,
                new Color(1.0f, 0.0f, 0.0f));
            _raycaster.Raycast(_pointerEventData, results);
            Debug.Log(_camera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0.0f)));
        }


        if (results.Count == 0)
        {
            return;
        }
        else
        {
            if (results.Exists(x => x.gameObject.GetComponent<VirtualKey>() != null) 
                && Input.GetButtonDown("MenuButtonAccept"))
            {   
                var key = results.Find(x => x.gameObject.GetComponent<VirtualKey>() != null);
                string keyCode = key.gameObject.GetComponent<VirtualKey>().Click();
                if (keyCode.CompareTo("Ent") == 0)
                {
                    _raceManager.ContinueEnding(inputField.text);
                    return;
                }
                if (keyCode.CompareTo(ARROW_STRING) == 0)
                {
                    if (inputField.text.Length > 0)
                    {
                        inputField.text = inputField.text.Substring(0, inputField.text.Length-1);
                    }
                    return;
                }
                else
                {
                    inputField.text += keyCode;
                    inputField.text = inputField.text.Length <= inputField.characterLimit
                        ? inputField.text
                        : inputField.text.Substring(0, inputField.characterLimit);
                    inputField.caretPosition = inputField.text.Length;
                }

            }
        }
    }

    public void GetNickname(RaceManager raceManager)
    {
        _raceManager = raceManager;
    }
}
