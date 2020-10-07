using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Movement
{
	public class MovementPingPong : MonoBehaviour
	{
		public int distancia = 5;
		public int posicion = 10;
		public int Velocidad = 3;

		void LateUpdate()
		{
			transform.position =
			new Vector3(Mathf.PingPong(Time.time * Velocidad, distancia) - posicion, transform.position.y, transform.position.z
			);
		}
	}
}


