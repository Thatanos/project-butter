using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //public GameObject LoadingAsset;
    //public Slider LoadingBarSlider;
    public GameObject OptionMenu, AboutMenu;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(LoadSceneAsync());
    }

    public void OpenOption()
    {
        OptionMenu.SetActive(true);AboutMenu.SetActive(false);
    }

    public void CloseOption()
    {
        OptionMenu.SetActive(false);
    }

    public void OpenAbout()
    {
        OptionMenu.SetActive(false); AboutMenu.SetActive(true);
    }

    public void CloseAbout()
    {
        AboutMenu.SetActive(false);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneAsync()
    {
        //LoadingAsset.SetActive(true);
        yield return new WaitForSeconds(3f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        /*while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            LoadingBarSlider.value = progressValue;
            yield return null;
        }*/

    }
}
