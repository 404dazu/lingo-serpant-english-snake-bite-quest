using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadToScene : MonoBehaviour
{
    // load to scene management

    public void SceneLoader(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
