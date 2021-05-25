using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : XMLManager 
{
    
    //this class is a utility class used for scene management
    public static void LoadLevel(string sceneNameString)
    {
        SceneManager.LoadScene(sceneNameString);
    }

    public void LoadGuestMod()
    {
        Playerdata = null;
        SceneManager.LoadScene(3);
    }

}
