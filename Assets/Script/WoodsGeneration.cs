using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodsGenerator : MonoBehaviour
{
	public int woodSize = 25;
	public int spacingSize = 3;
	public Obstacles[] obstacles;
}

[System.Serializable]
public class Obstacles
{
	public string name;
	public GameObject obstacle;
}
