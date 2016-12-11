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
// <copyright file="BasicAuth.cs" company="TRi">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>Tim F. Rieck</author>
// <date>29/11/2016 14:00 PM</date>

using System;

namespace RESTfulHTTPServer.src.models
{
	public class BasicAuth
	{
		private string _username;
		private string _password;

		/// <summary>
		/// Initializes a new instance of the <see cref="RESTfulHTTPServer.src.models.BasicAuth"/> class.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="password">Password.</param>
		public BasicAuth (string username, string password)
		{
			_username = username;
			_password = password;
		}

		/// <summary>
		/// Gets the username.
		/// </summary>
		/// <returns>The username.</returns>
		public string GetUsername()
		{
			return _username;
		}

		/// <summary>
		/// Gets the password.
		/// </summary>
		/// <returns>The password.</returns>
		public string GetPassword()
		{
			return _password;
		}
	}
}