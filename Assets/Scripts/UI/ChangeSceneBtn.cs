using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneBtn : MonoBehaviour
{
    [SerializeField] private string targetScene;

    public void OnClick()
    {
        SceneManager.LoadScene(targetScene);
    }
}
