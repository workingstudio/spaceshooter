using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    [SerializeField] private int sceneA;
    [SerializeField] private int sceneB;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Rigidbody playerBody;
    [SerializeField] private CanvasGroup canvasGroup;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private IEnumerator Start()
    {
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.alpha = 1;
        yield return SceneManager.LoadSceneAsync(sceneA,LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneA));
        while (canvasGroup.alpha > 0)
        {
            yield return null;
            canvasGroup.alpha -= 0.3f * Time.deltaTime;
        }
        canvasGroup.gameObject.SetActive(false);
    }

    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            StartCoroutine(SceneTransition());
        }
    }

    private IEnumerator SceneTransition()
    {
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.alpha = 0;
        while (canvasGroup.alpha < 1)
        {
            yield return null;
            canvasGroup.alpha += 0.6f * Time.deltaTime;
            playerCamera.fieldOfView += 10f * Time.deltaTime;
        }

        var scene = SceneManager.GetActiveScene();
        var sceneToLoad = scene.buildIndex != sceneB ? sceneB : sceneA;
        yield return SceneManager.UnloadSceneAsync(scene);
        playerBody.MovePosition(Vector3.zero);
        yield return SceneManager.LoadSceneAsync(sceneToLoad,LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneToLoad));
        while (canvasGroup.alpha > 0)
        {
            yield return null;
            playerCamera.fieldOfView -= 10f * Time.deltaTime;
            canvasGroup.alpha -= 0.6f * Time.deltaTime;
        }
        canvasGroup.gameObject.SetActive(false);

    }
}
