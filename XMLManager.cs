using UnityEngine;
using System.Xml.Serialization;   //access serializer
using System.Xml;                 //basic XML attributes
using System.IO;                  //file management
using System.Collections.Generic; //for lists
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
//Niv Porat and Artiom Sheremetiev
/// <summary>
/// control related class
/// manages player data, creation, allocation 
/// and XML serialization and formatting
/// passing user data and perform IO with XML file
/// </summary>
public class XMLManager : MonoBehaviour
{
    //paths
    private string folderPath = @"C:\SavesLOB";
    private string fullPath = @"C:\SavesLOB\PlayerList.xml";
    //references to user name in UI
    public GameObject UsernameInput;
    public GameObject TextToDisplay;
    public InputField PasswordToSave;
    public GameObject TextDeleteProfile;
    public GameObject ButtonEnabler;


    //UI
    private string UserNameEntered = string.Empty;
    private XmlSerializer serializer = new XmlSerializer(typeof(PlayerDatabase));
    public static PlayerDataFormat TempPlayerData;//temp player
    protected PlayerDatabase PlayersDB;//players list 
    public static XMLManager XMLmanager;//singleton
    protected List<PlayerDataFormat> TopPlayers;


    //variables for scenes index - may be more in future
    public int buildIndexDifferential = 3;//build index 0 - 3 are menu and gui scenes
    private static int MaxLevelCup = 3;

    private void Start()
    {
        //creates a game directory to store player data and saves
        if(!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);//creating folder of saves
            FileStream stream = new FileStream(fullPath, FileMode.OpenOrCreate);//creating file of saves
            PlayersDB = new PlayerDatabase();//creating example of XML empty file
            serializer.Serialize(stream, PlayersDB);//write base to xml file
            stream.Close();//close xml file
        }

    }

    private void Awake()
    {
        XMLmanager = this;
    }

    //opens XML file and updates it with new data
    public void SavePlayerData()
    {
        //fullPath = folderPath + "\\" + fileName;
        try
        {
            FileStream stream = new FileStream(fullPath, FileMode.Open);
            serializer.Serialize(stream, PlayersDB);
            //find player data in PlayersDB
            //rewrite in file
            stream.Close();
        }
        catch (Exception e)
        {
            Debug.LogError("Exception importing xml file: " + e);

        }

    }

    //on click of register - checks if data exists in XML file.
    // if true, alerts player that he can't proceed and must pick another
    //if it doesnt exist, saves it to DB and XML file
    public void PlayerRegistrationAuthentication()
    {
        try
        {
            OpenXmlFileForReadingPlayerDB();
            if (!CheckIfUserNameExistsInDB())
            {
                //enters data from registration menu to playerData variable
                CreatePlayerData();
                //opens stream and updates XML file
                SavePlayerData();
                TextToDisplay.GetComponent<Text>().text = "You have been registered successfully\n" +
                    "You can now log in and play";
            }
            else if(UsernameInput.GetComponent<Text>().text.Equals(string.Empty) || PasswordToSave.text.ToString().Equals(string.Empty) )//!!!!!!!!!!!!!!!!!!!!!!!
            {
                TextToDisplay.GetComponent<Text>().text = "Entered Username or Password is empty. Please Enter again";
                Debug.Log(PasswordToSave.text.ToString());
                //Debug.Log(TempPlayerData.Password.ToString());
            }
            else TextToDisplay.GetComponent<Text>().text = "Entered Username is taken. Please Enter another username";
        }
        catch (Exception e)
        {
            Debug.LogError("Exception importing xml file: " + e);
        }
    }

    //opens XML file, serializes it to PlayersDB list
    protected void OpenXmlFileForReadingPlayerDB()
    {
        using (FileStream stream = new FileStream(fullPath, FileMode.Open))
        {
            InitDataStream(stream);
            PlayersDB = (PlayerDatabase)serializer.Deserialize(stream);
            stream.Close();
        }
    }

    //check user name validation in list of players
    private bool CheckIfUserNameExistsInDB()
    {
        UserNameEntered = UsernameInput.GetComponent<Text>().text;//get user name from input field in scene
                                                                  //checks if username already exists in DB
        for (int i = 0; i < PlayersDB.Players.Count; i++)
        {
            if (PlayersDB.Players[i].UserName.ToString().Equals(UserNameEntered))
            {
                TempPlayerData = PlayersDB.Players[i];
                return true;
            }
        }
        return false;
    }

    //create a new player entry in list
    private void CreatePlayerData()
    {
        CreateNewPlayerEntry();//var playerdata initialize
        PlayersDB.Players.Add(TempPlayerData);//add current var playerdata to DB
        //write to XML FILE
        TextToDisplay.GetComponent<Text>().text = "You have been successfully registered";
    }

    //an inclass semi constructor
    //no need for constructor because of XML formatting
    private void CreateNewPlayerEntry()
    {
        TempPlayerData = new PlayerDataFormat();
        TempPlayerData.id = "P" + PlayersDB.Players.Count;
        TempPlayerData.UserName = UserNameEntered;
        TempPlayerData.Password = PasswordToSave.GetComponent<InputField>().text;
        TempPlayerData.points = 0;
        TempPlayerData.MaxLevel = 1;
        TempPlayerData.LastLevelPassed = 0;
        TempPlayerData.Time = 0f;
    }

    //Update XML when level finished
    public void UpdatePlayerDataOnNewLevel()
    {      
        updatePlayerDataInternal();
        if (!MainMenu.isGuest)
        {
            using (FileStream stream = new FileStream(fullPath, FileMode.Open))
            {
                InitDataStream(stream);
                PlayersDB = (PlayerDatabase)serializer.Deserialize(stream);
                stream.Close();
            }
            for (int i = 0; i < PlayersDB.Players.Count; i++)
            {
                if (PlayersDB.Players[i].id == DataPlayer.Player.id)
                {
                    PlayersDB.Players[i] = DataPlayer.Player;
                    SavePlayerData();
                }
            }
        }  
    }


    //updates current playerdata variable, and not the XML file
    private static void updatePlayerDataInternal()
    {
        DataPlayer.Player.points += scoreManager.score_manager.score;//!!!!!Guest mod issue
        DataPlayer.Player.Time += Timer.instance.LevelTime - Timer.instance.CurrentTime;
        DataPlayer.Player.LastLevelPassed = SceneManager.GetActiveScene().buildIndex - XMLmanager.buildIndexDifferential;
        if (DataPlayer.Player.MaxLevel < DataPlayer.Player.LastLevelPassed + 1)
            DataPlayer.Player.MaxLevel = DataPlayer.Player.LastLevelPassed + 1;
        if(DataPlayer.Player.MaxLevel > MaxLevelCup)
        {
            DataPlayer.Player.MaxLevel = MaxLevelCup;
        }
    }
    //return file reader to start
    public void InitDataStream(FileStream stream)
    {
        if (stream.Position > 0)
        { stream.Position = 0; }
    }

    //check if name and passowrd match XML file
    private bool CheckNameAndPass()
    {
        UserNameEntered = UsernameInput.GetComponent<Text>().text;//gets text from username input field
        for (int i = 0;i<PlayersDB.Players.Count;i++)
        {
            if (PlayersDB.Players[i].UserName.Equals(UserNameEntered) && PlayersDB.Players[i].Password.Equals(PasswordToSave.text))
            //checks that entered username and pw matches the data in XML file
            {
                DataPlayer.Player = PlayersDB.Players[i];
                TextToDisplay.GetComponent<Text>().text = "Entered Username is Verified.\n The bus will arrive shortly..";
                return true;
            }
        }
        return false;
    }
    //log in to game using listed player data
    public void Login()
    {
        
            OpenXmlFileForReadingPlayerDB();
            if (CheckNameAndPass())//checks if username and pass exists in XML file
            {
                SceneManager.LoadScene(3);
            }
            else
            {
                TextToDisplay.GetComponent<Text>().text = "Incorrect Username or Password.\n Try again";
                return;
            }
    }
    //log out of user
    public void LogOut()
    {
        DataPlayer.Player = null;
        SceneManager.LoadScene(1);
        TextToDisplay.GetComponent<Text>().text = "You are logged out";
    }
    //delete user profile from XML document

    public void IsInGuestMod()
    {
        if(DataPlayer.Player == null)
        {
            TextDeleteProfile.GetComponent<Text>().text = "You are in guest mod and cant delete profile";
            ButtonEnabler.SetActive(false);
        }    
    }
    public void DeleteProfile()
    {
        if (DataPlayer.Player != null)
        {
            if (File.Exists(fullPath))
            {
               // ButtonEnabler.SetActive(true);
                XmlDocument xml = new XmlDocument();//temp internal xml file object
                xml.Load(fullPath);//copy external to internal xml
                XmlElement element = (XmlElement)xml.SelectSingleNode("/Players/player[UserName="+DataPlayer.Player.UserName.ToString()+"]");//find node in xml by username
                if (element != null)//if element not null that means user is found
                {
                    element.ParentNode.RemoveChild(element);//delete all node info of user
                    xml.Save(fullPath);//save internal to external xml
                }
                else
                {
                    TextDeleteProfile.GetComponent<Text>().text = "File does not exist";
                }
            }
        }
    }
}
