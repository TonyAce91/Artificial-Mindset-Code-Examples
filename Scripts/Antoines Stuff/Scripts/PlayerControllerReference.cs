using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerReference : MonoBehaviour {

    //[Header("Controllers")]
    [SerializeField]
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;

	// Use this for initialization
	void Start () {
        trackedObject = GetComponentInParent<SteamVR_TrackedObject>();
        device = SteamVR_Controller.Input((int)trackedObject.index);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
