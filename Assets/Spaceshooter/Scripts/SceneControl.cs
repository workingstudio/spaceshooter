using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Spaceshooter
{
    public class SceneControl : MonoBehaviour
    {
        [SerializeField] private int sceneA;
        [SerializeField] private int sceneB;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private Rigidbody playerBody;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private PlayerCamera playerCameraControl;
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        private IEnumerator Start()
        {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.alpha = 1;
            yield return SceneManager.LoadSceneAsync(sceneA, LoadSceneMode.Additive);
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
            // "loading" ui fade in
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.alpha = 0;
            var targetTime = Time.time + 1.5f;
            while (Time.time < targetTime)
            {
                yield return null;
                canvasGroup.alpha += Time.deltaTime / 1.5f;
                playerCamera.fieldOfView += 60f * Time.deltaTime / 1.5f;
                playerCamera.transform.Translate(Vector3.forward * (12.52f * Time.deltaTime / 1.5f), Space.Self);
            }
            //unload current space scene
            var scene = SceneManager.GetActiveScene();
            var sceneToLoad = scene.buildIndex != sceneB ? sceneB : sceneA;
            yield return SceneManager.UnloadSceneAsync(scene);

            //reset player position for orientation
            playerBody.MovePosition(Vector3.zero);

            //load next space scene
            yield return SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
            // need to set scene activiate so the skybox will take effect
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneToLoad));

            // "loading" ui fade out
            targetTime = Time.time + 1.5f;
            while (Time.time < targetTime)
            {
                yield return null;
                canvasGroup.alpha += Time.deltaTime / -1.5f;
                playerCamera.fieldOfView += 60f * Time.deltaTime / -1.5f;
                playerCamera.transform.Translate(Vector3.forward * (-12.52f * Time.deltaTime / 1.5f), Space.Self);
            }
            canvasGroup.gameObject.SetActive(false);
        }
    }
}