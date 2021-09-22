using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
//niv porat and artiom sheremetiev

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
    public List<PlayerDataFormat> Players = new List<PlayerDataFormat>();

}
