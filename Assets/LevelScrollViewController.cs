using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScrollViewController : MonoBehaviour
{
    [SerializeField] int numberOfLevels;
    [SerializeField] GameObject levelBtnPref;
    [SerializeField] Transform btnParent;
    // Start is called before the first frame update
    void Start()
    {
        LoadLevelButtons();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void LoadLevelButtons()
    {

        for (int i = 1; i <= numberOfLevels; i++) 
        { 
            GameObject lvlBtnObj = Instantiate(levelBtnPref,btnParent);
            lvlBtnObj.GetComponent<LevelSelectBtnScript>().level = i;
            lvlBtnObj.GetComponent<LevelSelectBtnScript>().levelScrollViewController = this;
        }
    }
}
