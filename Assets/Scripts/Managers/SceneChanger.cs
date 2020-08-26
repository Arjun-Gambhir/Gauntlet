using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeScene(string sceneToChange)
    {
        SceneManager.LoadScene(sceneToChange);
    }

}
