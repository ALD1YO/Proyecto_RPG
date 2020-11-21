using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Saving;



namespace RPG.SceneManagement
{
    public class MenuManager : MonoBehaviour
    {               
        const string defaultSaveFile = "save";

        public void QuitGame()
        {
            Application.Quit();
        }

        public void DeleteSaveFile()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
        }

        public void ChangeScene(string scenename)
        {
            SceneManager.LoadScene(scenename);
        }
    }
}

