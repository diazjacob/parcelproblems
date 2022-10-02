using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageRequester : MonoBehaviour
{
    public Text _requestText;

    public PackageRequest currentReq;

    void Start()
    {
        GameManager.INSTANCE.OnPackageSend += NewPackageRequest;

        NewPackageRequest();
    }

    void Update()
    {
        
    }

    private void NewPackageRequest()
    {
        currentReq = new PackageRequest( Mathf.Clamp(GameManager.INSTANCE.currentDifficulty,0,2) );

        _requestText.text = currentReq.GetItemString();
    }
}

public class PackageRequest
{
    public int[] requestItems;

    public PackageRequest( int difficulty )
    {
        int numRequestedItems;

        int numTotalItems = GameManager.INSTANCE.globalItemArray.Length;

        numRequestedItems = Random.Range( 1, 3 + difficulty );
        requestItems = new int[numRequestedItems];

        for(int i = 0; i < requestItems.Length; i++ )
        {
            requestItems[i] = Random.Range( 0, numTotalItems);
        }
        
    }

    public string GetItemString()
    {
        string returnString = "";

        for( int i = 0; i < requestItems.Length; i++ )
        {
            var item = GameManager.INSTANCE.globalItemArray[requestItems[i]];

            if(item != null )
            {
                var itemScript = item.GetComponent<Item>();

                if(itemScript != null )
                {
                    returnString += itemScript.itemName + "\n";
                }

                    
            }

        }

        return returnString;
    }


}
