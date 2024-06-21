using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawn : MonoBehaviour
{
    public GameObject player;
    public float minx, minz, maxx, maxz;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minx, maxx), 1.5f, Random.Range(minz, maxz));
        PhotonNetwork.Instantiate(player.name, randomPosition, Quaternion.identity);
    }

}
