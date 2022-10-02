using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public float randomProbability = 0.01f;
    public float pkgTime = 2;

    private float _packageTimer;
    

    void Start()
    {
        
    }

    void Update()
    {
        int[] trueNeeds = GameManager.INSTANCE.packageReq.currentReq.requestItems;

        _packageTimer += Time.deltaTime;
        if(Random.value < randomProbability || _packageTimer > pkgTime/(GameManager.INSTANCE.currentDifficulty+1))
        {
            Quaternion rot = Quaternion.Euler( new Vector3( 0, 0, Random.value * 360f ) );

            GameObject packagePrefab = null;

            if(Random.value < 0.59)  packagePrefab = GameManager.INSTANCE.globalItemArray[trueNeeds[Random.Range( 0, trueNeeds.Length )]];
            else  packagePrefab = GameManager.INSTANCE.globalItemArray[Random.Range( 0, GameManager.INSTANCE.globalItemArray.Length )];

            Instantiate( packagePrefab, transform.position + new Vector3(Random.value, 0, Random.value) * 0.25f, rot, transform );

            _packageTimer = 0;
        }
    }
}
