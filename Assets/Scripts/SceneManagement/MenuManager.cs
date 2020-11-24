using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Saving;



namespace RPG.SceneManagement
{
    public class MenuManager : MonoBehaviour
    {
        SavingSystem savingSystem;
        const string defaultSaveFile = "save";

        private void Start()
        {
            savingSystem = FindObjectOfType<SavingSystem>();
        }

        

        public void QuitGame()
        {
            Application.Quit();
        }

        public void SaveandQuit()
        {
            savingSystem.Save(defaultSaveFile);
            QuitGame();
        }

        public void DeleteSaveFile()
        {
            
            savingSystem.Delete(defaultSaveFile);
            UnPauseGame();
            //GetComponent<SavingSystem>().Delete(defaultSaveFile);
        }

        public void ChangeScene(string scenename)
        {
            SceneManager.LoadScene(scenename);
        }

        public void HideShow_GameObjects(GameObject UIgameObject)
        {                    
            UIgameObject.SetActive(!UIgameObject.activeSelf);
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        public void UnPauseGame()
        {
            Time.timeScale = 1;
        }
    }
}

