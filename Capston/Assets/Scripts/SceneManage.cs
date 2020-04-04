using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    int scene_idx;

    private void Start()
    {
        scene_idx = SceneManager.GetActiveScene().buildIndex;
        print(scene_idx);
    }
    public void NextScene()
    {
        SceneManager.LoadScene(scene_idx + 1);
    }
}
