using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

/// <summary>
/// usage:      this class is attached to loadingSceneHandle object
/// functions:  handles the asynchronization between scenes before displaying the selected scene
///             to avoid hiccups for selected scene
///             it is okay to have hiccups from LoadingScene since that's it's purpose
/// </summary>
namespace game_ideas
{
    // global access for changing the scene
    // usually this class is only using by LoadSceneManager
    public static class LoadSceneName
    {
        private static string sceneName;

        // requires to provide a scene name, if doesn't it will load to main menu as default
        public static void SetLoadScene(string value) 
        {
            sceneName = value;
        }

        public static string GetLoadedScene()
        {
            return sceneName;
        }
    }

    public class LoadingSceneHandler : MonoBehaviour
    {
        [Header("UI Reference")]
        [SerializeField] private Image loadingFill_img;
        [SerializeField] private TextMeshProUGUI loading_text;

        private void Start()
        {
            LoadScene(); // once the scene is open start loading the called scene
        }

        public async void LoadScene()
        {
            string sceneName = LoadSceneName.GetLoadedScene();

            if (sceneName == null) // if didn't provide a scene name as default it will load to main menu
            {
                sceneName = "MainMenu";
            }

            await Task.Delay(2000); // JUST TEMPORARY DELAY TO DISPLAY THE ANIMATION OF THE SCENE --------------------------
            
            var scene = SceneManager.LoadSceneAsync(sceneName); 
            scene.allowSceneActivation = false; // disallow switching scene while it is loading 

            do
            {
                loadingFill_img.fillAmount = scene.progress; // loading progress
            } while (scene.progress < 0.9f);

            await Task.Delay(1000); // delay before switching scene
            scene.allowSceneActivation = true;
        }

    }
}
