using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Cubes : MonoBehaviour
{
    [SerializeField] bool[] cubes;
    public GameObject TargetCubes;
    // Start is called before the first frame update
    void Start()
    {
            cubes = new bool[9];

        if (PhotonNetwork.IsMasterClient)
        {
        for (int i = 0; i < 9; ++i)
        {
          cubes[i] = (Random.Range(1, 2.99f) < 2);
                TargetCubes.transform.Find("Cube (" + (i + 1).ToString() + ")").gameObject.SetActive(cubes[i]);
            }       
            
        }
        else gameObject.GetPhotonView().RPC("NewPlayer", RpcTarget.MasterClient);

    }

    // Update is called once per frame
    void Update()
    {

    }
    [PunRPC]void NewPlayer() { 
    gameObject.GetPhotonView().RPC("SetCubes", RpcTarget.Others, cubes);
    }
    [PunRPC] void SetCubes(bool[]cube){

        for (int i = 0; i < 9; ++i)
        {
            cubes[i] = cube[i];
            TargetCubes.transform.Find("Cube (" + (i + 1).ToString() + ")").gameObject.SetActive(cubes[i]);
        }
    }

}
