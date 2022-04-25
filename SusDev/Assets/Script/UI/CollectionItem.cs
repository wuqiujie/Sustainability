using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionItem : MonoBehaviour
{
    public int collectionNum=0;

    public void setcollectionNum(int a)
    {
        collectionNum = a;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //  Destroy(collision.gameObject);
        collectionNum++;
    }
}
