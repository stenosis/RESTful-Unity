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
// <copyright file="Response.cs" company="TRi">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>Tim F. Rieck</author>
// <date>01/12/2016 16:00 PM</date>

using System;
using System.Net;

namespace RESTfulHTTPServer.src.models
{
	public class Response
	{
		// Supported MIME content types
		public const string MIME_CONTENT_TYPE_JSON = "application/json";
		public const string MIME_CONTENT_TYPE_XML = "application/xml";
		public const string MIME_CONTENT_TYPE_HTML = "text/html";
		public const string MIME_CONTENT_TYPE_TEXT = "text/plain";

		private string _mimeType;
		private string _content;
		private int _httpStatus;

		/// <summary>
		/// Initializes a new instance of the <see cref="RESTfulHTTPServer.src.models.Response"/> class.
		/// </summary>
		public Response() 
		{
			_mimeType = MIME_CONTENT_TYPE_HTML;
			_content = "";
			_httpStatus = (int) HttpStatusCode.NoContent;
		}	

		/// <summary>
		/// Initializes a new instance of the <see cref="AssemblyCSharp.Response"/> class.
		/// </summary>
		/// <param name="mimeType">MIME type.</param>
		/// <param name="content">Content.</param>
		public Response(string mimeType, string content, int httpStatus)
		{
			_mimeType = mimeType;
			_content = content;
			_httpStatus = httpStatus;
		}

		/// <summary>
		/// Gets the type of the MIME.
		/// </summary>
		/// <returns>The MIME type.</returns>
		public string GetMIMEType() 
		{
			return _mimeType;
		}

		/// <summary>
		/// Sets the type of the MIME.
		/// </summary>
		/// <param name="mimeType">MIME type.</param>
		public void SetMimeType(string mimeType) 
		{
			_mimeType = mimeType;
		}

		/// <summary>
		/// Sets the content.
		/// </summary>
		/// <param name="content">Content.</param>
		public void SetContent(string content) 
		{
			_content = content;
		}

		/// <summary>
		/// Gets the content.
		/// </summary>
		/// <returns>The content.</returns>
		public string GetContent() {
			return _content;
		}

		/// <summary>
		/// Gets the HTTP status code.
		/// </summary>
		/// <returns>The HTTP status code.</returns>
		public int GetHTTPStatusCode() 
		{
			return _httpStatus;
		}

		/// <summary>
		/// Sets the HTTP status code.
		/// </summary>
		/// <param name="httpStatus">Http status.</param>
		public void SetHTTPStatusCode(int httpStatus) 
		{
			_httpStatus = httpStatus;
		}
	}
}
