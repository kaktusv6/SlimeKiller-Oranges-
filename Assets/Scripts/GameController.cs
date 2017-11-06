using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public GameObject PanelWin;
	public GameObject PanelLoos;
	
	private GameObject _player;
	private GameObject[] _generatorsSlime;

	public static int CountEnemy = 0;
	
	// Use this for initialization
	void Start () {
		_player = GameObject.FindWithTag("Player");
		_generatorsSlime = GameObject.FindGameObjectsWithTag("GenrateSlime");
		
		foreach(GameObject genrator in _generatorsSlime)
		{
			GeneratorSlime genSlimeComponent = genrator.GetComponent<GeneratorSlime>();
			GameController.CountEnemy += genSlimeComponent.Count;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (_player)
		{
			Player playerComponent = _player.GetComponent<Player>();

			if (playerComponent && playerComponent.Hp <= 0)
			{
				PanelLoos.SetActive(true);
			}
			else if (!playerComponent)
			{
				Debug.LogError("Not fount component Player");
			}
		}

		if (GameController.CountEnemy <= 0)
		{
			PanelWin.SetActive(true);
		}
	}
}
