using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace game_ideas
{
    public class ChangeScene : MonoBehaviour
    {

        public string sceneName;

        public void PlayGame()
        {
            SceneManager.LoadScene(sceneName);
        }

    }
}
