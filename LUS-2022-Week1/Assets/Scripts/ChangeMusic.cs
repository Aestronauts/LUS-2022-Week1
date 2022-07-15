using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    public AK.Wwise.Event auctionEvent;
    public AK.Wwise.Event dramaticEvent;
    public AK.Wwise.Event synthEvent;
    public AK.Wwise.Event stopAllMusic;

    [SerializeField]
    public TMPro.TMP_Dropdown expDropdown;

    private int currentExp;
    private int previousExp;

    // Start is called before the first frame update
    void Start()
    {
        previousExp = -1;
    }

    // Update is called once per frame
    void Update()
    {
        currentExp = expDropdown.value;

        if (expDropdown.value == 0)
        {
            stopAllMusic.Post(gameObject);
        }

        if (expDropdown.value == 1 && previousExp != 1)
        {
            if (previousExp != -1)
            {
                stopAllMusic.Post(gameObject);
            }
            stopAllMusic.Post(gameObject);
            auctionEvent.Post(gameObject);
            previousExp = 1;
            //isNewSelection = false;
            
        }

        if (expDropdown.value == 2 && previousExp != 2)
        {
            if (previousExp != -1)
            {
                stopAllMusic.Post(gameObject);
            }
            stopAllMusic.Post(gameObject);
            dramaticEvent.Post(gameObject);
            previousExp = 2;
        }

        if (expDropdown.value == 3 && previousExp != 3)
        {
            if (previousExp != -1)
            {
                stopAllMusic.Post(gameObject);
            }
            stopAllMusic.Post(gameObject);
            synthEvent.Post(gameObject);
            previousExp = 3;
        }
    }
}
