using UnityEngine;
using System.Xml.Serialization;   //access serializer
using System.Xml;                 //basic XML attributes
using System.IO;                  //file management
using System.Collections.Generic; //for lists
using System;

public class XMLManager : MonoBehaviour
{

    public static XMLManager XMLmanager;//singleton

    private void Awake()
    {
        XMLmanager = this;
    }
    public PlayerData Playerdata;
    public PlayerDatabase PlayerDB;

 

    public void SavePlayerData()
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PlayerDatabase), new XmlRootAttribute("Players"));
            FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/PlayerList.xml", FileMode.Open);
            serializer.Serialize(stream, PlayerDB);
            //find player data in PlayersDB
            //rewrite in file
            stream.Close();
        }
        catch(Exception e)
        {
            Debug.LogError("Exception importing xml file: " + e);

        }

    }
   public void PlayerAuthentication()
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PlayerData), new XmlRootAttribute("Players"));
            FileStream stream = new FileStream(Application.dataPath + @"\StreamingFiles\XML\PlayerList.xml", FileMode.Open);
            if (stream.Position > 0) stream.Position = 0;
           // Debug.Log(stream.ToString());
            Playerdata = (PlayerData)serializer.Deserialize(stream);

            stream.Close();
        }
        catch (Exception e)
        {
            Debug.LogError("Exception importing xml file: " + e);

        }

        //check if username exists in playersDB
        //if exists alert player to choose another name
        //if not - register user in playerDB file and load Login screen
        Debug.Log(PlayerDB.ToString());
    }

}

/*---------------------------------------------------------------------------------------*/
[Serializable, XmlRoot(Namespace = "PlayerList.xml")]
///
/// this is the class of the root XML element Players 
/// each element in it is player - which contains next class data
/// 
///
public class PlayerDatabase
{
   [XmlArray("Players"), XmlArrayItem(ElementName = "player", Type = typeof(string))]
   [XmlElement("Player")]
    public List <PlayerData> Players = new List <PlayerData>();
   
}


/*---------------------------------------------------------------------------------------*/

  //[XmlRoot(ElementName ="Players")]
  public class PlayerData
    {
    [XmlAttribute("PlayerId")]
    public int id;

    [XmlElement(ElementName = "Username")]
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
}

