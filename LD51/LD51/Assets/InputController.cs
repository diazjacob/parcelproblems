using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    public float grabForce = 10;
    public float _raiseHeight;
    public float _conveyorLevel;
    public LayerMask ignoreMeLayer;


    private Rigidbody _grabbedRB;
    private Vector3 _initialPos;

    public float _spawnTime = 0.1f;
    private float _spawnTimer;

    void Start()
    {
        
    }

    private void Update()
    {
        _spawnTimer += Time.deltaTime;
    }

    void FixedUpdate()
    {

        if( Input.GetMouseButton( 0 ) && !GameManager.INSTANCE.GAMEOVER )
        {
            //DEBUG
            RaycastHit hit;
            Vector3 dir = Camera.main.ScreenToWorldPoint( new Vector3( Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane ) ) - transform.position;
            Debug.DrawLine( transform.position, 5 * dir + transform.position, Color.red );
            // Does the ray intersect any objects excluding the player layer
            if( Physics.Raycast( transform.position, dir, out hit, Mathf.Infinity ) )
            {
                Debug.DrawLine( transform.position, hit.point, Color.white );


                if( _grabbedRB != null )
                {
                    Debug.DrawLine( hit.point, _grabbedRB.transform.position );
                    RaycastHit hit_occ;
                    Physics.Raycast( transform.position, dir, out hit_occ, Mathf.Infinity, ~ignoreMeLayer );


                    Vector3 hoverPos = new Vector3( hit_occ.point.x, hit_occ.point.y + _raiseHeight, hit_occ.point.z );
                    if( Input.GetMouseButton( 1 ) ) hoverPos = new Vector3( hit_occ.point.x, _conveyorLevel + _raiseHeight, hit_occ.point.z );

                    //if( Input.GetMouseButton( 1 ) ) hoverPos += Vector3.up * _raiseHeight;
                    _grabbedRB.AddForce( ( grabForce * ( hoverPos - _grabbedRB.gameObject.transform.position ) * Time.deltaTime ) * Vector3.Distance( hoverPos, _grabbedRB.gameObject.transform.position ) );
                    //_grabbedRB.AddForceAtPosition( ( grabForce * ( hoverPos - _grabbedRB.gameObject.transform.position ) * Time.deltaTime ), _initialPos + hit.collider.transform.position );// * Vector3.Distance( hoverPos, _grabbedRB.gameObject.transform.position ) );

                }
                else if( hit.collider.gameObject.tag == "grab" || hit.collider.gameObject.tag == "boxtape" || hit.collider.gameObject.tag == "boxlabel" )
                {

                    Debug.DrawRay( transform.position, transform.TransformDirection( Vector3.forward ) * hit.distance, Color.yellow );

                    _grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                    if( _grabbedRB == null ) _grabbedRB = hit.collider.gameObject.GetComponentInParent<Rigidbody>();

                    _initialPos = hit.point - hit.collider.transform.position;
                }
                else
                {
                    _grabbedRB = null;
                    _initialPos = Vector3.zero;
                }


                if(_spawnTimer > _spawnTime )
                {
                    if( hit.collider.gameObject.tag == "button_box" )
                    {
                        GameManager.INSTANCE.audio.PlayClip( 6 );
                        GameManager.INSTANCE.NewBox();
                    }
                    if( hit.collider.gameObject.tag == "button_tape" )
                    {
                        GameManager.INSTANCE.audio.PlayClip( 6 );
                        GameManager.INSTANCE.SpawnTape();
                    }
                    if( hit.collider.gameObject.tag == "button_label" )
                    {
                        GameManager.INSTANCE.audio.PlayClip( 6 );
                        GameManager.INSTANCE.SpawnLabel();
                    }

                    _spawnTimer = 0;
                }

            }


        }
        else
        {
            _grabbedRB = null;
            _initialPos = Vector3.zero;
        }

    }
}
