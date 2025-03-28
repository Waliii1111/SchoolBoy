using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [SerializeField] public GameObject loadingPanel;
    //public SmoothLoading loading;
    [SerializeField] public string sceneToLoad;
   
    public void OnButtonClick()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadSceneWithDelay());

    }

    private IEnumerator LoadSceneWithDelay()
    {
        if (loadingPanel != null)
        {
            loadingPanel.SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneToLoad);
    }
}
