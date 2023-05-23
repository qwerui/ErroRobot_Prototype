using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    static string nextScene;

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        yield return null;

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        operation.allowSceneActivation = false;

        while(!operation.isDone)
        {
            yield return null;

            if(operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
                yield break;
            }
        }
    }

    ///<summary>
    ///로딩 씬을 거친 씬 로드
    ///</summary>
    ///<param name="sceneName"> 불러올 씬 이름 </param>
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    ///<summary>
    ///로딩 씬을 거친 씬 로드
    ///</summary>
    ///<param name="sceneIndex"> 불러올 씬의 인덱스 </param>
    public static void LoadScene(int sceneIndex)
    {
       LoadScene(SceneManager.GetSceneAt(sceneIndex).name);
    }
}
