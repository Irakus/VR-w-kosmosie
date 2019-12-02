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

    private  GameObject _currentlySelected;
    [SerializeField]
    private Image _fillBar;

    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        _raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        _eventSystem = GetComponent<EventSystem>();
        _camera = Camera.main;
    }


    void Update()
    {
        //Check if the left Mouse button is clicked
        if (true)
        {
            //Set up the new Pointer Event
            _pointerEventData = new PointerEventData(_eventSystem);
            //Set the Pointer Event Position to that of the mouse position
            _pointerEventData.position = _camera.WorldToScreenPoint(_fillBar.transform.position);

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            _raycaster.Raycast(_pointerEventData, results);
            Debug.DrawRay(_camera.transform.position, _camera.transform.position + _camera.transform.forward*50.0f,new Color(1.0f,0.0f,0.0f));
            Debug.Log(results[0].gameObject);
            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            if (results.Count == 0 || results[0].gameObject != _currentlySelected)
            {
                _currentlySelected = results[0].gameObject;
                _fillBar.fillAmount = 0.0f;
            }
            else
            {
                _fillBar.fillAmount += 0.2f * Time.deltaTime;
                if (_fillBar.fillAmount >= 1.0f)
                {
                    _currentlySelected.GetComponent<Button>().onClick.Invoke();
                }
            }
        }
    }
}
