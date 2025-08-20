using UnityEditor;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public SceneLoader sceneLoader;
    
    public void StartGame()
    {
        sceneLoader = SceneLoader.Instance;
        sceneLoader.LoadScene("Level01");
    }

    public void Quit()
    {
        AAAGameManager gameManager = AAAGameManager.Instance;
        gameManager.ReallyQuit();
    }
}
