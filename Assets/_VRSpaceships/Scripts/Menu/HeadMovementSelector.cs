using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeadMovementSelector : MonoBehaviour
{
    private GraphicRaycaster _raycaster;
    private PointerEventData _pointerEventData;
    private EventSystem _eventSystem;
    private Camera _camera;
    [SerializeField]
    private  GameObject _currentlySelected;
    [SerializeField]
    private Image _fillBar;

    void Start()
    {
        _raycaster = GetComponent<GraphicRaycaster>();
        _eventSystem = FindObjectOfType<EventSystem>();
        _camera = Camera.main;
    }


    void Update()
    {
        _pointerEventData = new PointerEventData(_eventSystem);
        _pointerEventData.position = _camera.WorldToScreenPoint(_fillBar.transform.position);
        List<RaycastResult> results = new List<RaycastResult>();
        _raycaster.Raycast(_pointerEventData, results);


        if (!results.Exists(x=>x.gameObject.GetComponent<Button>() != null))
        {
            if (_currentlySelected != null)
            {
                _eventSystem.SetSelectedGameObject(null);
            }
            _currentlySelected = null;
            _fillBar.fillAmount = 0.0f;
        }
        else if (!Input.GetButton("MenuButtonAccept"))
        {
            Button selectedButton = results.Find(x => x.gameObject.GetComponent<Button>() != null).gameObject.GetComponent<Button>();
            if (selectedButton != _currentlySelected)
            {
                _currentlySelected = selectedButton.gameObject;
                _eventSystem.SetSelectedGameObject(selectedButton.gameObject);
                _fillBar.fillAmount = 0.0f;
            }
        }
        else if(_currentlySelected != null)
        {
            _fillBar.fillAmount += 0.5f * Time.deltaTime;
            if (_fillBar.fillAmount >= 1.0f)
            {
                _currentlySelected.GetComponent<Button>().onClick.Invoke();
            }
        }
    }
}
