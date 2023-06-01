using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    
    private int NumberOfCrates;
    public int NumberOfCratesOnCorrectPlace = 0;
    public bool IsLevelCompletedBool = false;
    [SerializeField] GameObject LevelCompleteCanvas;
    [SerializeField] AudioClip Applause;
    [SerializeField] AudioClip SokobanBgMusic;
    private AudioSource audioSource;
    void Start()
    {
        GameObject[] crates = GameObject.FindGameObjectsWithTag("Crate");

        
        NumberOfCrates = crates.Length;

        Debug.Log(NumberOfCrates);
        LevelCompleteCanvas.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = SokobanBgMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void AddToCrateInCorrectPlace() 
    { 
        NumberOfCratesOnCorrectPlace++;
    }

    public void SubstractFromCrateInCorrectPlace()
    {
        NumberOfCratesOnCorrectPlace--;
    }

    public void IsLevelCompleted() 
    {
        if (NumberOfCratesOnCorrectPlace == NumberOfCrates)
        {
            IsLevelCompletedBool = true;
            LevelCompleteCanvas.SetActive(true);
            audioSource.Stop();
            audioSource.PlayOneShot(Applause);
            Debug.Log("Bölümü tamamladýn");
        }
    }

    public void LoadMainMenu() 
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadLevelSelection()
    {
        SceneManager.LoadScene("LevelSelection");
    }


}
