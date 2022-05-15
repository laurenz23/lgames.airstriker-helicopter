using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// usage:      this class is attached to loadSceneManager prefab
/// functions:  call this class to handle the switching of scenes 
/// </summary>
namespace game_ideas
{
    public class LoadSceneManager : MonoBehaviour
    {
        private static LoadSceneManager instance;

        public static LoadSceneManager GetInstance()
        {
            return instance;
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public void LoadScene(string value)
        {

            LoadSceneName.SetLoadScene(value); // reference of what scene will be load once inside the LoadingScene
            SceneManager.LoadScene("LoadingScene"); // load the LoadingScene

        }

    }
}
