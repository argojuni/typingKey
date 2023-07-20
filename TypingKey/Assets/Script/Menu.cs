using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public KeyCode quitKey = KeyCode.Escape;

    private void Update()
    {
        if (Input.GetKeyDown(quitKey))
        {
            Quit();
            Debug.Log("Game Quit");
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void btnStart(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
        AudioManager.Instance.PlaySFX("btnclick");
    }
}
