using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpaceSceneLoader : MonoBehaviour
{
    [SerializeField]
    private Image _fillBar;
    [SerializeField]
    private Canvas _fillCanvas;
    public void LoadScene(string sceneName)
    {
        foreach (var canvas in FindObjectsOfType<Canvas>())
        {
            canvas.gameObject.SetActive(false);
        }

        _fillBar.fillAmount = 0.0f;
        _fillCanvas.gameObject.SetActive(true);

        StartCoroutine(LoadYourAsyncScene(sceneName));

    }
    IEnumerator LoadYourAsyncScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            _fillBar.fillAmount = asyncLoad.progress;
            if (asyncLoad.progress == 0.9f)
            {
                _fillBar.fillAmount = 1.0f;
                asyncLoad.allowSceneActivation = true;
            }
            yield return new WaitForSeconds(0.001f);
        }
        //yield return null;
    }
}
