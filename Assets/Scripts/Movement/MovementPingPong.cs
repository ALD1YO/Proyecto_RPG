using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPingPong : MonoBehaviour
{
	public int distancia = 5;
	public int posicion = 10;
	public int Velocidad = 3;

	void Update()
	{
		transform.position =
		new Vector3(Mathf.PingPong(Time.time * Velocidad, distancia) - posicion, transform.position.y, transform.position.z
		);
	}
}

