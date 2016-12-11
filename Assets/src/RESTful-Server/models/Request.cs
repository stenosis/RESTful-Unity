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
// <copyright file="Request.cs" company="TRi">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>Tim F. Rieck</author>
// <date>28/11/2016 22:00 PM</date>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RESTfulHTTPServer.src.controller;

namespace RESTfulHTTPServer.src.models
{
	public class Request {

		private Route _route;								// Stores the called route
		private Dictionary<string, string> _querys;    		// Stores all the URL querys with it's key and values   	-> /route?foo=bar&narf=zort
		private Dictionary<string, string> _parameters;		// Stores all the URL parameters with it's key and values 	-> /route/{paremter}/
		private string _postData;                         	// Stores received POST data

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="route">Route.</param>
		public Request(Route route) 
		{
			_route = route;
			_querys = new Dictionary<string, string>();
			_parameters = new Dictionary<string, string>();
			_postData = "";
		}

		/// <summary>
		/// Default Constuctor.
		/// </summary>
		public Request() 
		{
			_querys = new Dictionary<string, string>();
			_parameters = new Dictionary<string, string>();
			_postData = "";
		}

		/// <summary>
		/// Sets the route.
		/// </summary>
		/// <param name="route">Route.</param>
		public void SetRoute(Route route) 
		{
			_route = route;
		}

		/// <summary>
		/// Gets the route.
		/// </summary>
		/// <returns>The route.</returns>
		public Route GetRoute()
		{
			return _route;
		}

		/// <summary>
		/// Set the received POST data.
		/// </summary>
		/// <param name="postData"></param>
		public void SetPOSTData(string postData)
		{
			_postData = postData;
		}

		/// <summary>
		/// Determine the received POST data.
		/// </summary>
		/// <returns></returns>
		public string GetPOSTData()
		{
			return _postData;
		}

		/// <summary>
		/// Check if some POST data was set.
		/// </summary>
		/// <returns></returns>
		public bool HasPOSTData()
		{
			return _postData.Length > 0;
		}

		/// <summary>
		/// Add a key and value query pair to the route.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public void AddQuery(string key, string value)
		{
			if (!_querys.ContainsKey(key))
			{
				_querys.Add(key, value);
			}
			else
			{
				_querys[key] = value;
			}
		}

		/// <summary>
		/// Adds the parameter.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="value">Value.</param>
		public void AddParameter(string key, string value)
		{
			if (!_parameters.ContainsKey (key)) 
			{
				_parameters.Add(key, value);
			} 
			else 
			{
				_parameters[key] = value;
			}
		}

		/// <summary>
		/// Get the dictionary with all stored key and value querys of the route.
		/// </summary>
		/// <returns></returns>
		public Dictionary<string, string> GetQuerys()
		{
			return _querys;
		}

		/// <summary>
		/// Gets the parameters.
		/// </summary>
		/// <returns>The parameters.</returns>
		public Dictionary<string, string> GetParameters() 
		{
			return _parameters;
		}

		/// <summary>
		/// Determine the query value.
		/// </summary>
		/// <returns>The variable.</returns>
		/// <param name="key">Key.</param>
		public string GetQuery(string key) 
		{
			string value = "";
			if (_querys.ContainsKey(key)) value = _querys[key];
			return value;
		}

		/// <summary>
		/// Gets the parameter.
		/// </summary>
		/// <returns>The parameter.</returns>
		/// <param name="key">Key.</param>
		public string GetParameter(string key)
		{
			string value = "";
			if (_parameters.ContainsKey (key)) value = _parameters[key];
			return value;
		}
	}
}