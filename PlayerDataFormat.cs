using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;   //access serializer
using System.Xml;                 //basic XML attributes
using System;
//niv porat and artiom sheremetiev
/// <summary>
/// XML format to create
/// tags for each player object
/// </summary>
[XmlRoot(ElementName = "Players")]
public class PlayerDataFormat
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

    //method override for convenience
        public override string ToString()
        {
            return ("Username = " + UserName +
                     "\n Points = " + points +
                     "\n Max Level = " + MaxLevel +
                     "\n Last Level = " + LastLevelPassed +
                     "\n Time = " + string.Format("{0:0.##}", Time));
        }
}
