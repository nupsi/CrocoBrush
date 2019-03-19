using UnityEngine;
using UnityEngine.SceneManagement;

namespace CrocoBrush.UI
{
    public class MainMenu : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene(1);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}