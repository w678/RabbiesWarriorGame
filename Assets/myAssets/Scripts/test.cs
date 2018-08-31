using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour {
	
	public Text a;
	int coin = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		a.text = coin.ToString();
	}

	public void testing()
	{
		coin += 10;
		
	}
}
