using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextLevel : MonoBehaviour
{
    //uses other managers to allow player to go from one level to another once they hit the exit

    public int levelNumber;
    public string sceneName;

    private void Start()
    {
        UIManager.instance.levelText.text = levelNumber.ToString();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneChanger.instance.ChangeScene(sceneName);
    }


}
