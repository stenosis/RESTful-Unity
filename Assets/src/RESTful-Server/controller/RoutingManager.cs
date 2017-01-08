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
// <copyright file="RoutingManager.cs" company="TRi">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>Tim F. Rieck</author>
// <date>28/11/2016 22:00 PM</date>

using UnityEngine;
using System;
using System.Net;
using System.Reflection;
using System.Collections.Generic;
using RESTfulHTTPServer.src.models;

namespace RESTfulHTTPServer.src.controller
{
    public class RoutingManager
    {
		private const string TAG = "Routing Manager";
        private List<Route> _getRoutes;             // List of all GET routes
        private List<Route> _postRoutes;            // List of all POST routes
		private List<Route> _putRoutes;				// List of all PUT routes
		private List<Route> _deleteRoutes;			// List of all DELETE routes

        /// <summary>
        /// Default constructor with empty routing lists.
        /// </summary>
        public RoutingManager()
        {
            _getRoutes = new List<Route>();
            _postRoutes = new List<Route>();
			_putRoutes = new List<Route>();
			_deleteRoutes = new List<Route>();
        }

        /// <summary>
        /// Add a route the routing list.
        /// </summary>
        /// <param name="route"></param>
        /// <exception cref="Exception"></exception>
        public void AddRoute(Route route)
        {
			if (route.GetHTTPType().Equals(Route.Type.GET)) 
			{
				_getRoutes.Add(route);
			} else if (route.GetHTTPType().Equals(Route.Type.POST)) 
			{
				_postRoutes.Add(route);
			} else if (route.GetHTTPType().Equals(Route.Type.PUT)) 
			{
				_putRoutes.Add(route);
			} else if (route.GetHTTPType().Equals(Route.Type.DELETE)) 
			{
				_deleteRoutes.Add(route);
			}
            else
            {
                throw new Exception("HTTP Tpye not matching");
            }
        }

        /// <summary>
        /// Adds a GET route to the routing list.
        /// </summary>
        /// <param name="route"></param>
        /// <exception cref="Exception"></exception>
        public void AddGETRoute(Route route)
        {
            if (route.GetHTTPType().Equals(Route.Type.GET))
            {
                _getRoutes.Add(route);
            }
            else
            {
                throw new Exception("HTTP Tpye not matching");
            }
        }

        /// <summary>
        /// Adds a POST route to the routing list.
        /// </summary>
        /// <param name="route"></param>
        /// <exception cref="Exception"></exception>
        public void AddPOSTRoute(Route route)
        {
            if (route.GetHTTPType().Equals(Route.Type.POST))
            {
                _postRoutes.Add(route);
            }
            else
            {
                throw new Exception("HTTP Tpye not matching");
            }
        }

		/// <summary>
		/// Adds the PUT route.
		/// </summary>
		/// <param name="route">Route.</param>
		public void AddPUTRoute(Route route)
		{
			if (route.GetHTTPType().Equals(Route.Type.PUT))
			{
				_putRoutes.Add(route);
			}
			else
			{
				throw new Exception("HTTP Tpye not matching");
			}
		}

		/// <summary>
		/// Adds the DELETE route.
		/// </summary>
		/// <param name="route">Route.</param>
		public void AddDELETERoute(Route route)
		{
			if (route.GetHTTPType().Equals(Route.Type.DELETE))
			{
				_deleteRoutes.Add(route);
			}
			else
			{
				throw new Exception("HTTP Tpye not matching");
			}
		}

        /// <summary>
        /// Determine the GET routings.
        /// </summary>
        /// <returns></returns>
        public List<Route> GetGETRoutes()
        {
            return _getRoutes;
        }

        /// <summary>
        /// Determine the POST routings.
        /// </summary>
        /// <returns></returns>
        public List<Route> GetPOSTRoutes()
        {
            return _postRoutes;
        }

		/// <summary>
		/// Gets the PUT routes.
		/// </summary>
		/// <returns>The PUT routes.</returns>
		public List<Route> GetPUTRoutes()
		{
			return _putRoutes;
		}

		/// <summary>
		/// Gets the DELETE routes.
		/// </summary>
		/// <returns>The DELETE routes.</returns>
		public List<Route> GetDELETERoutes()
		{
			return _deleteRoutes;
		}

        /// <summary>
        /// Checks if a given called url exists in our routing lists.
        /// </summary>
        /// <param name="calledUrl">the called url</param>
        /// <param name="type">http type</param>
        /// <returns></returns>
		public Request DoesRouteExists(string calledUrl, string type)
        {
            Route.Type httpType;
			if (type.Equals("GET")) 
			{
				httpType = Route.Type.GET;
			} else if (type.Equals("POST")) 
			{
				httpType = Route.Type.POST;
			} else if (type.Equals("PUT")) 
			{
				httpType = Route.Type.PUT;
			} else if (type.Equals("DELETE")) 
			{
				httpType = Route.Type.DELETE;
			}
            else
            {
                return null;
            }
            return DoesRouteExists(calledUrl, httpType);
        }

        /// <summary>
        /// Checks if a given called url exists in our routing lists.
        /// </summary>
        /// <param name="calledUrl"></param>
        /// <param name="type"></param>
        /// <returns></returns>
		public Request DoesRouteExists(string calledUrl, Route.Type type)
        {
            List<Route> routes = new List<Route>();
			if (type.Equals (Route.Type.GET)) {
				routes = _getRoutes;

			} else if (type.Equals (Route.Type.POST)) 
			{
				routes = _postRoutes;
			} else if (type.Equals (Route.Type.PUT)) 
			{
				routes = _putRoutes;
			} else if (type.Equals (Route.Type.DELETE)) 
			{
				routes = _deleteRoutes;
			}
            return DoesRouteExists(routes, calledUrl);
        }

        /// <summary>
        /// Starts the delegted function of the route.
        /// </summary>
        /// <param name="route">Called route</param>
        /// <returns></returns>
		public Response CallDelegater(Request request)
        {
			Response response;
            try
            {
				// FOR STATIC METHODS
				Type t = Type.GetType(Configuration.NAMESPACE_INVOKER + request.GetRoute().GetInvokerClass());
				MethodInfo method = t.GetMethod(request.GetRoute().GetInvokerMethod(), BindingFlags.Static | BindingFlags.Public);
				response = (Response) method.Invoke(null, new object[]{request});
            }
            catch (Exception e)
            {
				Logger.Log (TAG, e.ToString());

				response = new Response();
				response.SetContent("Error: Unable to call invoker.");
				response.SetHTTPStatusCode((int)HttpStatusCode.NotFound);
				response.SetMimeType(Response.MIME_CONTENT_TYPE_HTML);
            }
			return response;
        }

        /// <summary>
        /// Checks if the called url exists in our routing lists and returns the called route object.
        /// </summary>
        /// <param name="routes"></param>
        /// <param name="calledUrl"></param>
        /// <returns></returns>
		private static Request DoesRouteExists(List<Route> routes, string calledUrl)
        {
			Request request = new Request();
            Route matchingRoute = null;

            // Check if the called URL exists in the routes
            bool foundRoute = false;
            string[] calledUrlTokens = RemoveStartingSlash(calledUrl).Split('/');

            // Iterate over all stored routes
            for (int i = 0; i < routes.Count && !foundRoute; i++)
            {
                Route route = routes[i];

                // 1. Check if the token count match
                string[] routeTokens = RemoveStartingSlash(route.GetUrl()).Split('/');
                if (calledUrlTokens.Length == routeTokens.Length)
                {
                    bool matching = true;  // Does the current tokens match

                    // 2. Iterate over all url tokens
                    for (int y = 0; y < routeTokens.Length && matching; y++)
                    {
                        string routeToken = routeTokens[y];
                        string calledurlToken = calledUrlTokens[y];

                        if (IsUrlArgumentParameter(routeToken))
                        {
                            // Make sure the URL parameter has content
                            if (calledurlToken.Length == 0)
                            {
                                matching = false;
                            }
                            else
                            {
                                // Add the paramter to the route (and removing the breaces at the key)
								request.AddParameter(routeToken.Substring(1, routeToken.Length - 2), calledurlToken);

                                // Verbose
								Logger.Log(TAG, "Added parameter to route, key: " + 
									routeToken.Substring(1, routeToken.Length - 2) + " value: " + calledurlToken);
                            }
                        // Mismatching tokens
                        } else if (!calledurlToken.Equals(routeToken))
                        {
                            matching = false;
                        }
                    }

                    // We've found our route if the called url tokens matches
                    if (matching)
                    {
                        matchingRoute = routes[i];
						request.SetRoute(matchingRoute);
                        foundRoute = true;
                    }
                }
            }
			return request;
        }

        /// <summary>
        /// Determine if the current url argument is a route parameter.
        /// A RESTful-HTTP-Server parameter starts with a '{' and end with a '}'
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private static bool IsUrlArgumentParameter(string token)
        {
            return token.StartsWith("{") && token.EndsWith("}");
        }

        /// <summary>
        /// Removes a leading slash from a given url.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string RemoveStartingSlash(string url)
        {
            if (url.StartsWith("/")) url = url.Substring(1);
            return url;
        }

		/// <summary>
		/// Determines the URL query.
		/// </summary>
		/// <returns>The URL query.</returns>
		/// <param name="context">Context.</param>
		/// <param name="request">Request.</param>
		public Request DetermineURLQuery(HttpListenerContext context, Request request) 
		{
			for (int i = 0; i < context.Request.QueryString.Count; i++) 
			{
				string key = context.Request.QueryString.GetKey(i);
				string value = context.Request.QueryString[i];
				if (key != null && value != null & key.Length > 0 && value.Length > 0) {
					request.AddQuery(key, value);
					Logger.Log(TAG, "Added query to route, key: " + key + " ,value: " + value);
				} else {
					Logger.Log(TAG, "Invalid URL query");
				}
			}
			return request;
		}
    }
}