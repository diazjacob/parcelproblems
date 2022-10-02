using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxAcceptor : MonoBehaviour
{

    private float _boxAcceptTimer;
    public float boxAcceptTime;

    private void Update()
    {
        _boxAcceptTimer += Time.deltaTime;
    }

    private void OnTriggerEnter( Collider other )
    {
        BoxController box = other.gameObject.GetComponent<BoxController>();
        if (box == null) box = other.gameObject.GetComponentInParent<BoxController>();

        if(box != null && _boxAcceptTimer > boxAcceptTime)
        {
            print( "BOX SENT" );
            box.BoxSent();
            _boxAcceptTimer = 0;
        }
    }
}
