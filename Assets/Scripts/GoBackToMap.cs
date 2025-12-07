using UnityEngine;

public class GoBackToMap : MonoBehaviour
{
    [SerializeField] private float timeToTransition = 5f;
    [SerializeField] private SceneTransitionManager sceneManager;
    private float timer;
    void Start()
    {
        timer = 0f;        
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeToTransition)
        {
            sceneManager.LoadScene("MapScene");
        }
    }

}
