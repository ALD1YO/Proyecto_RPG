using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Saving;



namespace RPG.SceneManagement
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject persistentObjects;
        private void Start()
        {
            persistentObjects = GameObject.Find("PersistentObjects(Clone)");
        }

        const string defaultSaveFile = "save";

        [SerializeField] GameObject persistentObjectSpawner;
        public void ActivateLoadFileGameObject()
        {
            Destroy(persistentObjects);
            persistentObjectSpawner.SetActive(true);
            persistentObjectSpawner.SetActive(false);
            persistentObjectSpawner.SetActive(true);


        }

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

