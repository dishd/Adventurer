
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ns
{
    /// <summary>
    /// 
    /// </summary>
    public class TestScene : MonoBehaviour
    {
        private void Start()
        {
            Global.LoadSceneName = "InitialPage";

            SceneManager.LoadScene("LoadPage");
        }
    }

}
