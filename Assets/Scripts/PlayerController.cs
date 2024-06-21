using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{


    public float SpeedTrans = 5;
    public float SpeedRot = 30;
    public GameObject head;
    public Camera camera;
    private float _rotationX, _rotationY;
    private float vertical, horizontal;
    PhotonView view;
    public GameObject MyCube=null;
    void Start()
    {
        view = GetComponent<PhotonView>();
        camera.enabled = view.IsMine;
    }
    void Update()
    {

        if (view.IsMine)
        {
            horizontal = Input.GetAxis("Horizontal") * SpeedTrans * Time.deltaTime;
            vertical = Input.GetAxis("Vertical") * SpeedTrans * Time.deltaTime;
            _rotationX -= Input.GetAxis("Mouse Y") * SpeedRot;
            _rotationX = Mathf.Clamp(_rotationX, -80, 40);
            _rotationY += Input.GetAxis("Mouse X") * SpeedRot;
            transform.Rotate(0, horizontal, 0);
            transform.Translate(horizontal, 0, vertical);
            transform.localEulerAngles = new Vector3(0, _rotationY, 0);
            head.transform.localEulerAngles = new Vector3(_rotationX, 0, 0);
            if (Input.GetMouseButtonDown(0))
            {
                if (MyCube == null)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 3.0f))
                    {
                        GameObject HitCube = hit.collider.gameObject;
                        if (HitCube.tag.Equals("Cube"))
                        {
                            if (HitCube.GetComponent<CubeController>().Take(transform, gameObject.GetPhotonView().ViewID)) {
                                MyCube = HitCube;
                            }
                        }
                    }
                }
                else if (MyCube.GetComponent<CubeController>().Break(gameObject.GetPhotonView().ViewID)) MyCube = null;
            }
        }
    }
}
