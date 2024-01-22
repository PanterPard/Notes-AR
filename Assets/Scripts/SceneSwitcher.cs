using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string main_menu;
    public string scanner;

    public void OpenScanner()
    {
        SceneManager.LoadScene(scanner);
    }
    
    public void OpenMainMenu()
    {
        SceneManager.LoadScene(main_menu);
    }
}
