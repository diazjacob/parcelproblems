using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxEffects : MonoBehaviour
{
    public ParticleSystem boxFold;
    public ParticleSystem boxClose;
    public ParticleSystem boxLabel;

    public float _particlHeight = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBoxFold(Vector3 pos)
    {
        if(boxFold != null )
        {
            boxFold.transform.position = new Vector3( pos.x, _particlHeight, pos.z );

            boxFold.Play();
        }
    }

    public void PlayBoxClose(Vector3 pos )
    {
        if( boxClose != null )
        {
            boxClose.transform.position = new Vector3( pos.x, _particlHeight, pos.z );

            boxClose.Play();
        }
    }

    public void PlayBoxLabel( Vector3 pos )
    {
        if( boxLabel != null )
        {
            boxLabel.transform.position = new Vector3( pos.x, _particlHeight, pos.z );

            boxLabel.Play();
        }
    }
}
