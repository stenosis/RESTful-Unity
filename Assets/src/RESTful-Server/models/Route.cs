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
// <copyright file="Route.cs" company="TRi">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>Tim F. Rieck</author>
// <date>28/11/2016 22:00 PM</date>

using System;
using System.Collections.Generic;
using RESTfulHTTPServer.src.controller;

namespace RESTfulHTTPServer.src.models
{
    public class Route
    {
        // Connection type
		public enum Type {GET, POST, DELETE, PUT};

        private Type _type;                               // Connection type (GET, POST, DELETE or PUT)
        private string _url;                              // To be routed url path
		private string _invokerInfo;                      // Delegeter to be called
        private string _className;                        // The called reflection class name (based on the delegter)
        private string _methodName;                       // The called reflection method name (based on the delegeter)

        /// <summary>
        /// Consteuctor.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="url"></param>
        /// <param name="delegeter"></param>
        public Route(Type type, string url, string invokerInfo)
        {
            _className = "";
            _methodName = "";

            _type = type;
            _url = url;
			_invokerInfo = invokerInfo;

            // Init proceed some data first
            InitInvoker();
        }

        /// <summary>
        /// Determine the HTTP type of the route.
        /// </summary>
        /// <returns></returns>
        public Type GetHTTPType()
        {
            return _type;
        }

        /// <summary>
        /// Determine the preset url of the route.
        /// </summary>
        /// <returns></returns>
        public string GetUrl()
        {
            return _url;
        }

        /// <summary>
        /// Determine the delegeters class name.
        /// </summary>
        /// <returns></returns>
        public string GetInvokerClass()
        {
            return _className;
        }

        /// <summary>
        /// Determine the delegeters method name.
        /// </summary>
        /// <returns></returns>
        public string GetInvokerMethod()
        {
            return _methodName;
        }

        /// <summary>
        /// Creating the invoker information for the relfection class and method call.
        /// </summary>
        private void InitInvoker()
        {
            string[] arg = _invokerInfo.Split('.');
            if (arg.Length == 2)
            {
                _className = arg[0];
                _methodName = arg[1];
            }
            else
            {
                throw new Exception("Invoker parameter missmatch");
            }
        }
    }
}