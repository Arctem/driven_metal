using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Driven;

public class Mech : WorldObject {
    public List<Module> modules;

    private MechManager manager;

	void Start () {
        manager = FindObjectOfType<MechManager>();
        manager.AddMech(this.GetComponent<Mech>());
        foreach(Module m in modules) {
            if(m == null) {
                continue;
            }
            Debug.Log("Module " + m + " and I am " + this);
            m.SetMech(this);
        }
	}

	// Update is called once per frame
	void Update () {
		
	}

    public List<Module> ModulesForPilot(int pilot) {
        return modules.GetRange(pilot * Driven.Globals.ModulesPerPlayer, Driven.Globals.ModulesPerPlayer);
    }
}
