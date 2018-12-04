using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraControl : MonoBehaviour {

    public float headTime = 1.0f;
    public float headTimer;
    private HydraHead[] hydraHeads;
    private int currentHead;

	void Start () {
        hydraHeads = GetComponentsInChildren<HydraHead>();
        headTimer = headTime;
        switchHeads();
	}
	
	void Update () {
        headTimer -= Time.deltaTime;
        if(headTimer < 0) {
            headTimer = headTime;
            switchHeads();
        }
	}

    private void switchHeads() {
        if(hydraHeads.Length == 1) {
            return;
        }
        int head;
        head = Random.Range(0, hydraHeads.Length);
        if(head == currentHead) {
            head += Random.Range(0, 2) * 2 - 1;
            if(head == -1) {
                head += 2;
            }
            if(head == hydraHeads.Length) {
                head -= 2;
            }
        }
        currentHead = head;
        for(int i = 0; i<hydraHeads.Length; i++) {
            if(i == head) {
                hydraHeads[i].gameObject.SetActive(true);
                continue;
            }
            hydraHeads[i].gameObject.SetActive(false);
        }
    }
}
