using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;


public static partial class Utilities 
{
    public static void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    
    public static void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public static void LoadSceneAsync(int index, MonoBehaviour component)
    {
        component.StartCoroutine(LoadSceneAsync(index));
    }

    public static void LoadSceneAsync(string name, MonoBehaviour component)
    {
        component.StartCoroutine(LoadSceneAsync(name));
    }

    private static IEnumerator LoadSceneAsync(int index)
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(index);
        asyncOp.allowSceneActivation = false;
        while (!asyncOp.isDone)
        { 
            if (asyncOp.progress >= 0.9f)
            {
                asyncOp.allowSceneActivation = true;
                yield break;
            }
            yield return null;
        }
    } 

    private static IEnumerator LoadSceneAsync(string name)
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(name);
        asyncOp.allowSceneActivation = false;
        while (!asyncOp.isDone)
        {
            if (asyncOp.progress >= 0.9f)
            {
                asyncOp.allowSceneActivation = true;
                yield break;
            }
            yield return null;
        }
    }
}
