﻿using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
	public ui[] u;
	public float def;
	public float morale;
	public float economy;
	public float army;
	public int hep;
	public int activehep;
	public int actlud;
	public float maxacl;
	public GameObject moje;
	public GameObject[] hexas;
	public GameObject[] phexas;
	// Use this for initialization
	public worldcontroller wc;
	public Color myc;
	public string myn;
	public float acl;
	// Use this for initialization
	void Start () {
		this.name = "Player";
		hexas = GameObject.FindGameObjectsWithTag ("hex");
		wc = GameObject.Find ("Main Camera").GetComponent<worldcontroller>();
		for (int i = 0; i < wc.hexys.Length; i++) {
			if (Vector2.Distance (wc.hexys [i].transform.position, this.transform.position) <= 25F && Vector2.Distance (wc.hexys [i].transform.position, this.transform.position) > 5F) {
				wc.hexys [i].gameObject.GetComponent<hexp> ().Przejecie (myn,myc,this.gameObject);
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < u.Length; i++) {
			if (i == 0) {
				u [i] = GameObject.Find ("populacja").GetComponent<ui> ();
				u [i].zmienna = actlud.ToString();
			}else if (i == 1) {
				u [i] = GameObject.Find ("eko").GetComponent<ui> ();
				u [i].zmienna = economy.ToString();
			}else if (i == 2) {
				u [i] = GameObject.Find ("pop").GetComponent<ui> ();
				u [i].zmienna = morale.ToString();
			}
		}
		def = army * (((2 * morale) + economy)/3);
		if (wc.pt && !wc.rt) {
			activehep = 1;
			phexas = new GameObject[0];
			for (int i = 0; i < hexas.Length; i++) {
				if (hexas [i].tag == "hex" && hexas [i].name == "player") {
					GameObject[] tmpgo = new GameObject[1];
					if (phexas.Length != 0) {
						tmpgo = new GameObject[phexas.Length];
						tmpgo = phexas;
						phexas = new GameObject[phexas.Length + 1];
					} else {
						phexas = new GameObject[1];
					}
					if (phexas.Length > 1) {
						for(int x = 0;x<tmpgo.Length;x++){
							phexas[x] = tmpgo[x];
						}
					}
					phexas [phexas.Length - 1] = hexas [i];
					if (hexas [i].GetComponent<hexp> ().lud < hexas [i].GetComponent<hexp> ().maxlud) {
						activehep++;
					}
				}
			}
			hep = phexas.Length + 1;
			economy += (acl / 25) * (economy / 100) * morale / 20;
			if (acl >= maxacl) {
				acl = maxacl;
				if (morale > 0) {
					morale -= acl * (5F / 250F) + acl * (5F / 100F) * (morale / 50F);
				} else {
					morale += -1 * acl * (5F / 250F) + acl * (5F / 100F) * (morale / 50F);
					acl += acl * (5F / 100F) * (morale / 50F);
				}
			} else {
				acl += acl * (5F / 100F) * (morale / 50F);
			}
			if (morale <= 0){
				morale += -1 * acl * (5F / 250F) + acl * (5F / 100F) * (morale / 50F);
				acl += acl * (5F / 100F) * (morale / 10F);
			}
			morale -= army / 100;
			actlud = (int)acl;
			wc.rt = true;
		}
	}
	public void turab (){
		if (wc.pt && wc.rt) {
				wc.pt = false;
		}
	}
	public void add(float[] jej){
		morale += jej[0];
		acl += jej[1];
		maxacl += jej[2];
		economy += jej[3];
	}
}
