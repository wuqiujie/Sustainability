using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianSpawner : MonoBehaviour
{
    public GameObject[] pedestrianPrefab;
    public GameObject citizens;
    public int numOfPedestrian;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        int count = 0;
        while (count < numOfPedestrian)
        {
            GameObject obj = Instantiate(pedestrianPrefab[Random.Range(0, pedestrianPrefab.Length)]);
            obj.transform.SetParent(citizens.transform);
            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            obj.GetComponent<WaypointNavigator>()._currentWP = child.GetComponent<Waypoint>();
            obj.transform.position = child.position;

            yield return new WaitForSeconds(0.1f);

            count++;

        }
    }
}
