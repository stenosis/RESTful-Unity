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
// <copyright file="LightController.cs" company="TRi">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>Tim F. Rieck</author>
// <date>29/11/2016 10:00 AM</date>

using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour 
{
	private Light _light;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start () 
	{
		_light = (Light) gameObject.GetComponent(typeof(Light));
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () 
	{
		if(Input.GetKeyUp(KeyCode.Space)) _light.enabled = !_light.enabled;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <returns><c>true</c>, if status was gotten, <c>false</c> otherwise.</returns>
	public bool GetStatus() 
	{
		return _light.enabled;
	}
}
