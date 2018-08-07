using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Module : NetworkBehaviour {
    public string moduleName;
    protected Mech mech;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public abstract bool WorthProcessing(Driven.PanelInputs inputs);
    public abstract void ProcessInputs(Driven.PanelInputs inputs);

    public void SetMech(Mech mech) {
        this.mech = mech;
    }
}
