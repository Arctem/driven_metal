using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechManager : MonoBehaviour {
    private HashSet<Mech> mechs;

    private void Awake() {
        mechs = new HashSet<Mech>();
    }

    public void AddMech(Mech mech) {
        this.mechs.Add(mech);
        Debug.Log("Registered new mech " + mech);
    }
}
