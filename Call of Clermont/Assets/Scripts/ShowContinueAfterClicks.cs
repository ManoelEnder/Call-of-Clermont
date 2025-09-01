using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowContinueAfterClicks : MonoBehaviour
{
    public GameObject continueButton;   
    public string sceneToLoad = "NomeDaCena"; 
    private int clickCount = 0;

    void Start()
    {
    
        if (continueButton != null)
            continueButton.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickCount++;

            if (clickCount == 1 && continueButton != null)
            {
                continueButton.SetActive(true);
            }
        }
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
