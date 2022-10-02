using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public GameObject _folded;
    public GameObject _unfolded;
    public GameObject _packed;
    public GameObject label;

    private bool unfolded = false;
    private bool packaged = false;
    private bool labeled = false;
    private bool sent = false;

    private List<Item> _currentItems = new List<Item>();
    private int[] _storedItems;

    void Start()
    {
        _folded.SetActive( true );
        _unfolded.SetActive( false );
        _packed.SetActive( false );
        label.SetActive( false );
    }

    void Update()
    {
        
    }

    private void OnCollisionStay( Collision collision )
    {
        GameObject obj = collision.collider.gameObject;

        if( obj.tag == "boxarea" && !unfolded)
        {
            _folded.SetActive( false );
            _unfolded.SetActive( true );

            GameManager.INSTANCE.boxEffects.PlayBoxFold( transform.position );

            unfolded = true;

            GameManager.INSTANCE.audio.PlayClip( 1 );
        }

    }

    private void OnCollisionEnter( Collision collision )
    {

        GameObject obj = collision.collider.gameObject;

        //if( obj.tag == "Untagged" ) GameManager.INSTANCE.audio.PlayClip( 0 );


        if( obj.tag == "boxlabel" && unfolded && packaged && !labeled )
        {
            GameManager.INSTANCE.boxEffects.PlayBoxLabel( transform.position );

            labeled = true;

            Destroy( collision.gameObject );

            label.SetActive( true );

            GameManager.INSTANCE.audio.PlayClip( 3 );

        }
        if( obj.tag == "boxtape" && unfolded && !packaged )
        {
            GameManager.INSTANCE.boxEffects.PlayBoxClose( transform.position );

            packaged = true;

            _storedItems = new int[_currentItems.Count];

            for( int i = 0; i < _currentItems.Count; i++ )
            {
                _storedItems[i] = _currentItems[i].itemID;
                Destroy( _currentItems[i].gameObject );
            }

            Destroy( collision.gameObject );


            _folded.SetActive( false );
            _unfolded.SetActive( false );
            _packed.SetActive( true );

            GameManager.INSTANCE.audio.PlayClip( 2 );
        }
    }

    public void BoxSent()
    {
        sent = true;
        GameManager.INSTANCE.SendPackage(unfolded, packaged, labeled, _storedItems);

        _folded.SetActive( false );
        _unfolded.SetActive( false );

        Destroy( gameObject );
    }

    private void OnTriggerEnter( Collider other )
    {
        var itemScript = other.gameObject.GetComponent<Item>();
        if (itemScript == null) itemScript = other.gameObject.GetComponentInParent<Item>();

        if(itemScript != null ) _currentItems.Add( itemScript );
        
    }

    private void OnTriggerExit( Collider other )
    {
        var itemScript = other.gameObject.GetComponent<Item>();
        if( itemScript == null ) itemScript = other.gameObject.GetComponentInParent<Item>();

        if( itemScript != null ) _currentItems.Remove( itemScript );
    }

}
