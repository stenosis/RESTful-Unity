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
using RESTfulHTTPServer;
using RESTfulHTTPServer.src.controller;
using RESTfulHTTPServer.src.models;

public class ServerInit : MonoBehaviour 
{
	private const string TAG = "Server Init";

	public int port = 8080;
	public string username = "";
	public string password = "";

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start () {

		// Make sure the applications continues to run in the background
		Application.runInBackground = true;

		// ------------------------------
		// Creating a Simple REST server
		// ------------------------------

		// 1. Create the routing table
		// HTTP Type 	 - URL routing path with variables 	- Class and method to be called
		// HTTP Type     - /foo/bar/{variable}   			- DelegetorClass.MethodToBeCalled
		RoutingManager routingManager = new RoutingManager();
		routingManager.AddRoute(new Route(Route.Type.GET, "/color/{objname}", "MaterialInvoke.GetColor"));
		routingManager.AddRoute(new Route(Route.Type.POST, "/color/{objname}", "MaterialInvoke.SetColor"));
		routingManager.AddRoute(new Route(Route.Type.DELETE, "/color/{objname}", "MaterialInvoke.DeleteColor"));

		// Starts the Simple REST Server
		// With or without basic authorisation flag
		if (!username.Equals("") && !password.Equals("")) 
		{
			RESTfulHTTPServer.src.controller.Logger.Log(TAG, "Create basic auth");
			BasicAuth basicAuth = new BasicAuth (username, password);
			new SimpleRESTServer(port, routingManager, basicAuth);
		} 
		else 
		{
			new SimpleRESTServer(port, routingManager);
		}
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () { }
}
