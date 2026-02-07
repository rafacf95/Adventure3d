using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void Load(int i)
    {
        SceneManager.LoadScene(i);
        SaveManager.Instance.Load();
    }

    public void Load(string s)
    {
        SceneManager.LoadScene(s);
        SaveManager.Instance.Load();
    }
}
