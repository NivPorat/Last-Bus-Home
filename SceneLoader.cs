using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : XMLManager 
{
    
    //this class is a utility class used for scene management
    public static string LoadLevel(string sceneNameString)
    {
        SceneManager.LoadScene(sceneNameString);
        return string.Empty;
    }

    public void LoadGuestMod()
    {
        Playerdata = null;
        SceneManager.LoadScene(3);
    }

}
