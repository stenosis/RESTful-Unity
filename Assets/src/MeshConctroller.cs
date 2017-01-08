// RESTful-Unity
// Copyright (C) 2016 - Tim F. Rieck
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
//	You should have received a copy of the GNU General Public License
//	along with this program. If not, see <http://www.gnu.org/licenses/>.
//
// <copyright file="MeshController.cs" company="TRi">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>Tim F. Rieck</author>
// <date>29/11/2016 10:00 AM</date>

using UnityEngine;
using System.Collections;

public class MeshConctroller : MonoBehaviour
{
	private bool _rotate;
	public int speed = 180;
	private Material material;
	private Renderer myrenderer;

	// Use this for initialization
	void Start ()
	{
		_rotate = true;
		myrenderer = GetComponent<Renderer> ();
		material = myrenderer.material;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			Debug.Log("GetMouseButtonDown");
			//RESTfulHTTPServer.src.controller.Logger.Log ("MeshConctroller", "GetMouseButtonDown");

			float red = Random.Range (1, 100) / 100.0f;
			float green = Random.Range (1, 100) / 100.0f;
			float blue = Random.Range (1, 100) / 100.0f;
			float a = Random.Range (1, 100) / 100.0f;

			Color nc = new Color (red, green, blue, a);

			string l = string.Format ("change Color red {0} green {1} blue {2} a {3}", red, green, blue, a);
			Debug.Log(l);

			//material.SetColor ("random", Color.green);
			material.color = nc;
		}

		if (Input.GetKeyUp (KeyCode.R)) {
			_rotate = !_rotate;
		}

		if (_rotate) {
			transform.Rotate (Vector3.up, Time.deltaTime * speed);
		}
	}
}
