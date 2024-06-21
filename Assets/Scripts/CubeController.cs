using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CubeController : MonoBehaviour
{
    // Start is called before the first frame update
    public int state=0;
    Transform target;
    public GameObject CubesPlace;
    public Vector3 Place;
    void Start()
    {
       
    }
    public bool Take(Transform player,int Id) {
        if (state == 0)
       {
            state = Id;
            target = player;
            GetComponent<Rigidbody>().useGravity = false;
          gameObject.GetPhotonView().RPC("SetState", RpcTarget.Others, state);
            return true;
       }
        else return false;
    }
    public bool Break(int Id) {
        if (state == Id) {
            state = 0;
            target = null;
            GetComponent<Rigidbody>().useGravity = true;
            
            gameObject.GetPhotonView().RPC("SetState", RpcTarget.Others, state);
            
            return true;
        }
        else return false; 
    }
    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = target.position + target.transform.forward * 1.5f;
            transform.rotation = target.rotation;
        }
        else
        {
            for (int i = 1; i <= 9; ++i)
            {
                Place = CubesPlace.transform.Find("Cube (" + i.ToString() + ")").position + new Vector3(0, 0.8f, 0);
                if ((Place - transform.position).magnitude <= 0.3f)
                {
                    transform.position = Place;
                    transform.rotation = CubesPlace.transform.Find("Cube (" + i.ToString() + ")").rotation;
                    break;
                }
            }
        }
    }
    [PunRPC] void SetState(int Id) {
        state = Id;
    }
}
