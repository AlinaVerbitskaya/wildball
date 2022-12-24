using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private Canvas mainMenu, levelsMenu;

    private void Start()
    {
        GetReady();
    }

    private void GetReady()
    {
        mainMenu.gameObject.SetActive(true);
        levelsMenu.gameObject.SetActive(true);
        mainMenu.enabled = true;
        levelsMenu.enabled = false;
    }

    public void LoadLevel(int levelIndex)
    {
        GameManager.LoadScene(levelIndex);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
