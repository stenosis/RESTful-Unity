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
// <copyright file="VHSMaterial.cs" company="TRi">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>Tim F. Rieck</author>
// <date>29/11/2016 10:00 AM</date>

using System;
using UnityEngine;

public class VHSMaterial
{
	public Color _color;

	/// <summary>
	/// Initializes a new instance of the <see cref="VHSMaterial"/> class.
	/// </summary>
	/// <param name="color">Color.</param>
	public VHSMaterial(Color color)
	{
		_color = color;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="VHSMaterial"/> class.
	/// </summary>
	public VHSMaterial ()
	{
		_color = Color.clear;
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
		_color = color;
	}
}

