using UnityEngine;
using UnityEngine.SceneManagement;
public class Launcher : MonoBehaviour
{
    [SerializeField]
    private string mainScene;

    public void StartGame()
    {
        SceneManager.LoadScene(mainScene);
    }
}
