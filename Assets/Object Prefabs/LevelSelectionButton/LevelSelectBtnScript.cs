using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectBtnScript : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public int level;
    [HideInInspector] public LevelScrollViewController levelScrollViewController;
    [SerializeField] Text buttonText;
    private void Start()
    {
        buttonText.text = level.ToString();
    }


    public void OnLevelButtonClick()
    {
        Debug.Log("Basýldý");
       
        SceneManager.LoadScene("Level" + level.ToString());
        
        
    }

    //bool SceneExists(string sceneName)
    //{
    //    foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
    //    {
    //        if (scene.path.Contains(sceneName))
    //        {
    //            return true;
    //        }
    //    }

    //    return false;
    //}
}
