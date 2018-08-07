using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
    public Mech mech;
    public int crewPosition;

    void Awake() {
        mech = FindObjectOfType<Mech>();
        crewPosition = 0;
    }

    // Update is called once per frame
    void Update() {
        if (!isLocalPlayer) {
            return;
        }
        Driven.PanelInputs[] inputs = new Driven.PanelInputs[] {
            new Driven.PanelInputs(Input.GetAxis("1 Top Left"), Input.GetAxis("1 Top"), Input.GetAxis("1 Top Right"),
                Input.GetAxis("1 Left"), Input.GetAxis("1 Middle"), Input.GetAxis("1 Right"),
                Input.GetAxis("1 Bottom Left"),Input.GetAxis("1 Bottom"), Input.GetAxis("1 Bottom Right"))
        };
        List<Module> modules = mech.ModulesForPilot(crewPosition);
        for (int i = 0; i <  modules.Count; i++) {
            if (modules[i] && modules[i].WorthProcessing(inputs[i])) {
                CmdSendInputs(inputs);
            }
        }
    }
    
    [Command]
    private void CmdSendInputs(Driven.PanelInputs[] inputs) {
        List<Module> modules = mech.ModulesForPilot(crewPosition);
        for (int i = 0; i < modules.Count; i++) {
            if (modules[i]) {
                modules[i].ProcessInputs(inputs[i]);
            }
        }
    }
}
