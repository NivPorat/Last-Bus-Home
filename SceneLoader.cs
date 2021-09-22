using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//niv porat and artiom sheremetiev
/// <summary>
/// class handles loding unity scenes using strings and not build order index
/// </summary>
public class SceneLoader : XMLManager 
{
    private int MainMenuBuildIndex = 3;
    private int level1BuildIndex = 4;
    private int level2BuildIndex = 5;
    private int level3BuildIndex = 6;

    //this class is a utility class used for scene management
    public static void LoadLevel(string sceneNameString)
    {
        SceneManager.LoadScene(sceneNameString);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(level1BuildIndex);
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene(level2BuildIndex);
    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene(level3BuildIndex);
    }
    //loads main menu with null player data
    //indicates that player is a guest
    public void LoadGuestMod()
    {
        TempPlayerData = null;
        SceneManager.LoadScene(MainMenuBuildIndex);
    }

}
