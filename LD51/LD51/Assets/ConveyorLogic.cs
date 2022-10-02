using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorLogic : MonoBehaviour
{

    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if( _rb != null ) gameObject.tag = "grab";
    }


    void Update()
    {
        
    }

    private void OnCollisionStay( Collision collision )
    {
        GameObject obj = collision.collider.gameObject;

        if ( obj.tag == "conv" )
        {
            if(_rb.velocity.magnitude < GameManager.INSTANCE.maxConveyorSpeed)
                _rb.AddForce( Vector3.right * ( GameManager.INSTANCE.conveyorForce * (GameManager.INSTANCE.currentDifficulty + 1.5f) ));
        }
        if(obj.tag == "del" )
        {
            Destroy(gameObject);
        }


    }
}
