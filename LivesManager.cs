using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesManager : MonoBehaviour
{
    public static LivesManager instance;
    public TextMeshProUGUI text;
    public int Life;
    // Start is called before the first frame update
    void Start()
    {
        
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    public void changeLives(int lives)
    {
        Life-=lives;
        text.text = "x" + Life.ToString();
    }

}
