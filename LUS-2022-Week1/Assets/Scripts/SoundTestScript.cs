using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTestScript : MonoBehaviour
{
    public AK.Wwise.Event auctionMusicEvent;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Function...");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space down");
            auctionMusicEvent.Post(this.gameObject);
        }
    }
}
