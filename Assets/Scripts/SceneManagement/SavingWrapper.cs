using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using UnityEngine.SceneManagement;


namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {

        const string defaultSaveFile = "save";

        [SerializeField] float fadeInTime = 0.2f;

        private void Awake()
        {
            //if(SceneManager.GetActiveScene().name != "Muerte")
            StartCoroutine(LoadLastScene());
        }


        IEnumerator LoadLastScene()
        {
            
            //yield return es esperar a que termine esta corrutina
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return fader.FadeIn(fadeInTime);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                //yield return SceneManager.LoadScene(0);
                Load();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                
                SceneManager.LoadScene("Muerte");
                print(this.gameObject.name);
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Load()
        {
            //Call saving system
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
        }
    }

}
