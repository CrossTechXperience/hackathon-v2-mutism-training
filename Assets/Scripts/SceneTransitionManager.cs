using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    [Header("Transition Animator")]
    public Animator transitionAnimator;
    public float transitionDuration = 1f;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    private System.Collections.IEnumerator LoadSceneRoutine(string sceneName)
    {
        // Optional animation trigger
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("Start");
            yield return new WaitForSeconds(transitionDuration);
        }

        SceneManager.LoadScene(sceneName);

        // Optional exit animation after loading scene
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("End");
        }
    }
}
