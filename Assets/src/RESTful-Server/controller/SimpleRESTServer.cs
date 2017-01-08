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
// <copyright file="SimpleRESTServer.cs" company="TRi">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>Tim F. Rieck</author>
// <date>28/11/2016 22:00 PM</date>

using UnityEngine;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using RESTfulHTTPServer.src.models;

namespace RESTfulHTTPServer.src.controller
{
    class SimpleRESTServer
    {
		private const string TAG = "Simple REST Server";

		private int _port;
        private Thread _serverThread;
        private HttpListener _listener;
        private RoutingManager _routingManager;
		private BasicAuth _basicAuth;

        /// <summary>
        /// Construct server with given port.
        /// </summary>
        /// <param name="path">Directory path to serve.</param>
        /// <param name="port">Port of the server.</param>
		public SimpleRESTServer(int port, RoutingManager routingManager)
        {
            _routingManager = routingManager;
			_basicAuth = null;

            Initialize(port);
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="RESTfulHTTPServer.src.controller.SimpleHTTPServer"/> class.
		/// </summary>
		/// <param name="port">Port.</param>
		/// <param name="routingManager">Routing manager.</param>
		/// <param name="basicAuth">Basic auth.</param>
		public SimpleRESTServer(int port, RoutingManager routingManager, BasicAuth basicAuth)
		{
			_routingManager = routingManager;
			_basicAuth = basicAuth;

			Initialize (port);
		}

        /// <summary>
        /// Construct server with suitable port.
        /// </summary>
        /// <param name="path">Directory path to serve.</param>
		public SimpleRESTServer(RoutingManager routingManager)
        {
			_routingManager = routingManager;
			_basicAuth = null;

            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();

            Initialize(port);
        }

        /// <summary>
        /// Init the server.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="port"></param>
        private void Initialize(int port)
        {
            _port = port;
            _serverThread = new Thread(Listen);
            _serverThread.Start();
        }

        /// <summary>
        /// Get and sets the server port.
        /// </summary>
        public int Port
        {
            get { return _port; }
            private set { }
        }

        /// <summary>
        /// Stop server and dispose all functions.
        /// </summary>
        public void Stop()
        {
            _serverThread.Abort();
            _listener.Stop();
        }

        /// <summary>
        /// Starts the HTTP listener.
        /// </summary>
        private void Listen()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://*:" + _port.ToString() + "/");
			if (_basicAuth != null) _listener.AuthenticationSchemes = AuthenticationSchemes.Basic;
            _listener.Start();

			// Verbose
			Logger.Log(TAG, "Server is up and running on port " + _port);

            while (true)
            {
                try
                {
                    HttpListenerContext context = _listener.GetContext();
                    Process(context);
                } catch (Exception e)
                {
					Logger.Log(TAG, e.ToString());
                }
            }
        }

		/// <summary>
		/// Generates the stream from string.
		/// </summary>
		/// <returns>The stream from string.</returns>
		/// <param name="s">S.</param>
		public static Stream GenerateStreamFromString(string s)
		{
			Stream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(s);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}

		/// <summary>
		/// Receives the post data.
		/// </summary>
		/// <returns>The post data.</returns>
		/// <param name="request">Request.</param>
		/// <param name="httpRequest">Http request.</param>
		private Request ReceivePostData(Request request, HttpListenerRequest httpRequest)
        {
            string postData;
			using (Stream body = httpRequest.InputStream) // here we have some POST data
            {
				using (StreamReader reader = new StreamReader(body, httpRequest.ContentEncoding))
                {
                    postData = reader.ReadToEnd();
                }

                request.SetPOSTData(postData);
				return request;
            }
        }

        /// <summary>
        /// Process a HTTP request
        /// </summary>
        /// <param name="context"></param>
        private void Process(HttpListenerContext context)
        {
            string httpResult;
			Response response = new Response();
            HttpListenerRequest httpRequest = context.Request;
			string calledURL = context.Request.Url.AbsolutePath;

			// Verbose information
			Logger.Log(TAG, "Rquest Type: " + httpRequest.HttpMethod);
			Logger.Log(TAG, "Requested URL: " + calledURL);

			// Let's check the basic auth first
			bool hasAccess = false;
			if (_basicAuth == null) 
			{
				// Basic auth was diabled.
				hasAccess = true;

			} 
			else 
			{
				HttpListenerBasicIdentity identity = (HttpListenerBasicIdentity) context.User.Identity;
				if (_basicAuth.GetUsername().Equals(identity.Name) && _basicAuth.GetPassword().Equals(identity.Password)) 
				{
					hasAccess = true;

					// Verbose information
					Logger.Log(TAG, "Username: " + identity.Name);
					Logger.Log(TAG, "Password: " + identity.Password);
				}
			}

			if (hasAccess) 
			{
				// Check if the requested url exists in our routing lists
				Request request = _routingManager.DoesRouteExists(calledURL, httpRequest.HttpMethod);
				Route matchedRoute = request.GetRoute();
				bool foundRoute = matchedRoute != null;

				if (foundRoute) 
				{
					// Let's check for some POST and PUT data first
					if ((httpRequest.HttpMethod.Equals("POST")) || (httpRequest.HttpMethod.Equals("PUT"))
						&& httpRequest.HasEntityBody) 
					{
						request = ReceivePostData(request, httpRequest);
					}

					// Let's check for and store URL querys
					request = _routingManager.DetermineURLQuery(context, request);

					// Let's call the delegeters method for the response
					response = _routingManager.CallDelegater(request);
					context.Response.StatusCode = response.GetHTTPStatusCode();
					httpResult = response.GetContent();
				} 
				else 
				{
					// 404 Page - Not found
					context.Response.StatusCode = (int) HttpStatusCode.NotFound;
					httpResult = "<html><head><title>Simple REST Server</title></head><body>404</body></html>";
				}
			} 
			else 
			{
				// 401 Page - Unauthorized
				context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
				httpResult = "<html><head><title>Simple REST Server</title></head><body>401</body></html>";
			}

			// Output the generated data
			try
			{
				Stream input = GenerateStreamFromString(httpResult);

				// Seting up the context flags
				context.Response.ContentType = response.GetMIMEType();
				context.Response.ContentLength64 = input.Length;
		        context.Response.AddHeader("Date", DateTime.Now.ToString("r"));
		        context.Response.AddHeader("Last-Modified", File.GetLastWriteTime(calledURL).ToString("r"));

		        byte[] buffer = new byte[1024 * 16];
		        int nbytes;
				while ((nbytes = input.Read(buffer, 0, buffer.Length)) > 0) 
				{
					context.Response.OutputStream.Write(buffer, 0, nbytes);
				}
		        input.Close();
		        context.Response.OutputStream.Flush();
			}
			catch (Exception e)
			{
				Logger.Log(TAG, e.ToString());
				context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
			}
			context.Response.OutputStream.Close();
        }
	}
}