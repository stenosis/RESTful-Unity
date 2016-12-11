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
// <copyright file="ServerInit.cs" company="TRi">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>Tim F. Rieck</author>
// <date>28/11/2016 22:00 PM</date>

using UnityEngine;
using System.Collections;

public class VSHLight 
{

	private Light _light;
	public bool _isEnabled;
	public float _intensity;
	public Color _color;

	/// <summary>
	/// Initializes a new instance of the <see cref="VSHLight"/> class.
	/// </summary>
	/// <param name="light">Light.</param>
	public VSHLight(Light light) 
	{
	
		_light = light;
		_isEnabled = light.isActiveAndEnabled;
		_color = light.color;
		_intensity = light.intensity;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="VSHLight"/> class.
	/// </summary>
	public VSHLight()
	{
		_light = null;
		_isEnabled = false;
		_color = Color.clear;
		_intensity = 0;
	}

	/// <summary>
	/// Sets the light.
	/// </summary>
	/// <param name="light">Light.</param>
	public void SetLight(Light light)
	{
		_light = light;
		_isEnabled = light.isActiveAndEnabled;
		_color = light.color;
		_intensity = light.intensity;
	}

	/// <summary>
	/// Gets the light.
	/// </summary>
	/// <returns>The light.</returns>
	public Light GetLight()
	{
		return _light;
	}

	/// <summary>
	/// Determines whether this instance is enabled.
	/// </summary>
	/// <returns><c>true</c> if this instance is enabled; otherwise, <c>false</c>.</returns>
	public bool IsEnabled()
	{
		return _isEnabled;
	}

	/// <summary>
	/// Gets the color.
	/// </summary>
	/// <returns>The color.</returns>
	public Color GetColor()
	{
		return _color;
	}

	/// <summary>
	/// Sets the color.
	/// </summary>
	/// <param name="color">Color.</param>
	public void SetColor(Color color)
	{
		_light.color = color;
		_color = color;
	}

	/// <summary>
	/// Sets the intensity.
	/// </summary>
	/// <param name="intensity">Intensity.</param>
	public void SetIntensity(float intensity)
	{
		_light.intensity = intensity;
		_intensity = intensity;
	}

	/// <summary>
	/// Gets the intensity.
	/// </summary>
	/// <returns>The intensity.</returns>
	public float GetIntensity()
	{
		return _intensity;
	}
}
