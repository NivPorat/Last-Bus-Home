using UnityEngine;
using System.Xml.Serialization;   //access serializer
using System.Xml;                 //basic XML attributes
using System.IO;                  //file management
using System.Collections.Generic; //for lists
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class XMLManager : MonoBehaviour
{
    public GameObject UsernameInput;
    public GameObject TextToDisplay;
    public GameObject PasswordToSave;
    public static PlayerData Playerdata = new PlayerData();
    public PlayerDatabase PlayerDB;
    private string UserNameEntered = string.Empty;
    public static XMLManager XMLmanager;//singleton

    private void Awake()
    {
        XMLmanager = this;
    }

    //opens XML file and updates it with new data
    public void SavePlayerData()
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PlayerDatabase));
            FileStream stream = new FileStream(Application.dataPath + @"/StreamingFiles/XML/PlayerList.xml", FileMode.Open);
            serializer.Serialize(stream, PlayerDB);
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
        try//
        {
            OpenXmlFileForReadingPlayerDB();
        }
        catch (Exception e)
        {
            Debug.LogError("Exception importing xml file: " + e);
        }

    }

    private bool OpenXmlFileForReadingPlayerDB()
    {
        bool flag = true;
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerDatabase));
        using (FileStream stream = new FileStream(Application.dataPath + @"\StreamingFiles\XML\PlayerList.xml", FileMode.Open))
        {
            InitDataStream(stream);
            PlayerDB = (PlayerDatabase)serializer.Deserialize(stream);
            flag = CheckIfUserNameExistsInDB(flag);
            stream.Close();

            if (flag)
            {
                //enters data from registration menu to playerData variable
                CreatePlayerData();
                //opens stream and updates XML file
                SavePlayerData();
            }
        }
        return flag;
    }

    private bool CheckIfUserNameExistsInDB(bool flag)
    {
        UserNameEntered = UsernameInput.GetComponent<Text>().text;//get user name from input field in scene
                                                                  //checks if username already exists in DB
        for (int i = 0; i < PlayerDB.Players.Count; i++)
        {
            if (PlayerDB.Players[i].UserName.ToString().Equals(UserNameEntered))
            {
                TextToDisplay.GetComponent<Text>().text = "Entered Username is Taken. Please Enter another username";
                flag = false;
                Playerdata = PlayerDB.Players[i];
                break;
            }
        }

        return flag;
    }

    private void CreatePlayerData()
    {
        CreateNewPlayerEntry();//var playerdata initialize
        PlayerDB.Players.Add(Playerdata);//add current var playerdata to DB
        //write to XML FILE
        TextToDisplay.GetComponent<Text>().text = "You have been successfully registered";
    }

    private void CreateNewPlayerEntry()
    {

        //an inclass semi constructor
        Playerdata.id = "P" + PlayerDB.Players.Count;
        Playerdata.UserName = UserNameEntered;
        Playerdata.Password = PasswordToSave.GetComponent<Text>().text;
        Playerdata.points = 0;
        Playerdata.MaxLevel = 1;
        Playerdata.LastLevelPassed = 0;
        Playerdata.Time = 0f;
    }

    public void Login()
    {
        bool flag = false;//assuming user does not exist
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerDatabase));
        using (FileStream stream = new FileStream(Application.dataPath + @"\StreamingFiles\XML\PlayerList.xml", FileMode.Open))
        {
            InitDataStream(stream);
            PlayerDB = (PlayerDatabase)serializer.Deserialize(stream);
            if (!(CheckIfUserNameExistsInDB(flag)))//checks if username exists in XML file
            {
                if (Playerdata != null)//if it exists and playerdata was not null 
                {
                    if (Playerdata.UserName == null || Playerdata.Password == null)
                    {
                        TextToDisplay.GetComponent<Text>().text = "Incorrect Username or Password.\n Try again";
                        return;
                    }
                    UserNameEntered = UsernameInput.GetComponent<Text>().text;
                    if (Playerdata.UserName.Equals(UserNameEntered) && Playerdata.Password.Equals(PasswordToSave.GetComponent<Text>().text))
                    {
                        TextToDisplay.GetComponent<Text>().text = "Entered Username is Verified.\n The bus will arrive shortly..";
                        StartCoroutine(Wait(50.0f));
                        DontDestroyOnLoad(this);
                        SceneManager.LoadScene(3);
                    }
                }
            }
        }
        IEnumerator Wait(float duration)
        {
            yield return new WaitForSeconds(duration);
        }
       
    }

    private static void InitDataStream(FileStream stream)
    {
        if (stream.Position > 0)
        { stream.Position = 0; }
    }

   

    /*---------------------------------------------------------------------------------------*/
    [Serializable, XmlRoot(ElementName = "Players")]
    ///
    /// this is the class of the root XML element Players 
    /// each element in it is player - which contains next class data
    /// 
    ///
    public class PlayerDatabase
    {

        [XmlElement("player")]
        public List<PlayerData> Players = new List<PlayerData>();

    }


    /*---------------------------------------------------------------------------------------*/

    [XmlRoot(ElementName = "Players")]
    public class PlayerData
    {
        [XmlAttribute("PlayerID")]
        public string id;

        [XmlElement(ElementName = "UserName")]
        public string UserName;

        [XmlElement(ElementName = "Password")]
        public string Password;

        [XmlElement(ElementName = "Points")]
        public int points;

        [XmlElement(ElementName = "MaxLevel")]
        public int MaxLevel;

        [XmlElement(ElementName = "LastLevelPassed")]
        public int LastLevelPassed;

        [XmlElement(ElementName = "Time")]
        public double Time;

        public override string ToString()
        {
            return ("Player ID = " + id +
                    "\n Username = " + UserName +
                     "\n Password = " + Password +
                     "\n Points = " + points +
                     "\n MaxLevel = " + MaxLevel +
                     "\n LastLevel = " + LastLevelPassed +
                     "\n Time = " + Time);
        }
    }
}
