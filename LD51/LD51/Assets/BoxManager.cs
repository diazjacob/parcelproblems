using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{

    public GameObject _boxPrefab;

    public Vector3 _velocityDir;

    void Start()
    {
        GameManager.INSTANCE.OnPackageSend += NewBox;
    }



    void Update()
    {
        
    }

    public void NewBox()
    {
        var obj = Instantiate( _boxPrefab, transform.position, Quaternion.identity, transform );

        var rb = obj.GetComponent<Rigidbody>();

        if(rb != null )
        {
            rb.AddForce( Vector3.right * GameManager.INSTANCE.spawnVel * GameManager.INSTANCE.GetDifficulty());
        }

    }
}
