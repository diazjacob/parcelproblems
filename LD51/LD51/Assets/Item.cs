using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;

    public int itemID;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter( Collision collision )
    {
        //GameManager.INSTANCE.audio.PlayClip( 0 );
    }
}
