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
// <copyright file="Logger.cs" company="TRi">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>Tim F. Rieck</author>
// <date>05/12/2016 13:00 PM</date>

using UnityEngine;
using System;

namespace RESTfulHTTPServer.src.controller
{
	public static class Logger
	{
		/// <summary>
		/// Log the specified tag and msg.
		/// </summary>
		/// <param name="tag">Tag.</param>
		/// <param name="msg">Message.</param>
		public static void Log(string tag, string msg) 
		{
			if (Configuration.VERBOSE) 
			{
				string log = tag + " - " + msg;
				if (Configuration.UNITY) 
				{
					Debug.Log(log);
				} 
				else 
				{
					Console.WriteLine(log);
				}
			}
		}
	}
}
