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
// <date>29/11/2016 10:13 AM</date>

using UnityEngine;
using System;
using System.Net;
using RESTfulHTTPServer.src.models;
using RESTfulHTTPServer.src.controller;

namespace RESTfulHTTPServer.src.invoker
{
	public class MaterialInvoke
	{
		private const string TAG = "Material Invoke";

		/// <summary>
		/// Get the color of an object
		/// </summary>
		/// <returns>The color.</returns>
		/// <param name="request">Request.</param>
		public static Responce GetColor(Request request)
		{
			Responce responce = new Responce();
			string objname = request.GetParameter("objname");
			string responceData = "";

			// Verbose all URL variables
			foreach(string key in request.GetQuerys().Keys) {

				string value = request.GetQuery(key);
				RESTfulHTTPServer.src.controller.Logger.Log(TAG, "key: " + key + " , value: " + value);
			}

			UnityInvoker.ExecuteOnMainThread.Enqueue(() => {  

				// Determine our object in the scene
				GameObject gameObject = GameObject.Find(objname);
				if (gameObject != null)
				{
					try {
						VHSMaterial vhsMaterial = new VHSMaterial();

						// Check if it's our light source
						if (gameObject.GetComponent<Light>() != null)
						{	
							Light light = gameObject.GetComponent<Light>();
							vhsMaterial = new VHSMaterial(light.color);

						// Or a mesh object
						} else 
						{
							Material mat = gameObject.GetComponent<Renderer>().material;
							vhsMaterial = new VHSMaterial(mat.color);
						}
						responceData = JsonUtility.ToJson(vhsMaterial);
						responce.SetHTTPStatusCode((int) HttpStatusCode.OK);

					} catch(Exception e)
					{
						string msg = "Failed to seiralised JSON";
						responceData = msg;

						RESTfulHTTPServer.src.controller.Logger.Log(TAG, msg);
						RESTfulHTTPServer.src.controller.Logger.Log(TAG, e.ToString());
					}	

				} else 
				{
					// 404 - Not found
					responceData = "404";
					responce.SetContent(responceData);
					responce.SetHTTPStatusCode((int) HttpStatusCode.NotFound);
					responce.SetMimeType(Responce.MIME_CONTENT_TYPE_TEXT);
				}
			});

			// Wait for the main thread
			while (responceData.Equals ("")) {}

			// 200 - OK
			// Fillig up the responce with data
			responce.SetContent(responceData);
			responce.SetMimeType(Responce.MIME_CONTENT_TYPE_JSON);

			return responce;
		}

		/// <summary>
		/// Deletes the color.
		/// </summary>
		/// <returns>The color.</returns>
		/// <param name="request">Request.</param>
		public static Responce DeleteColor(Request request)
		{
			Responce responce = new Responce ();
			string responceData = "";
			string objname = request.GetParameter ("objname");

			UnityInvoker.ExecuteOnMainThread.Enqueue (() => { 

				// Determine our object in the scene
				GameObject gameObject = GameObject.Find(objname);
				if (gameObject != null)
				{
					// Check if it's our light source
					if (gameObject.GetComponent<Light>() != null)
					{	
						// Set the color to the object
						Light light = gameObject.GetComponent<Light>();
						light.color = Color.white;

						// Create a returning json object for the result
						VHSMaterial vhsMaterialResult = new VHSMaterial();
						vhsMaterialResult.SetColor(light.color);
						responceData = JsonUtility.ToJson(vhsMaterialResult);
					} 
					// It's our mesh object
					else 
					{
						// Set the color to the object
						Material mat = gameObject.GetComponent<Renderer>().material;
						mat.color = Color.white;

						// Create a returning json object for the result
						VHSMaterial vhsMaterialResult = new VHSMaterial();
						vhsMaterialResult.SetColor(mat.color);
						responceData = JsonUtility.ToJson(vhsMaterialResult);
					}
					responce.SetHTTPStatusCode((int) HttpStatusCode.OK);

				} else {

					// 404 - Not Found
					responceData = "404";
					responce.SetContent(responceData);
					responce.SetHTTPStatusCode((int) HttpStatusCode.NotFound);
					responce.SetMimeType(Responce.MIME_CONTENT_TYPE_HTML);
				}
			});

			// Wait for the main thread
			while (responceData.Equals("")) {}

			responce.SetContent(responceData);
			responce.SetMimeType(Responce.MIME_CONTENT_TYPE_JSON);
			return responce;
		}

		/// <summary>
		/// Sets the color of a game object.
		/// </summary>
		/// <returns>The color.</returns>
		/// <param name="request">Request.</param>
		public static Responce SetColor(Request request)
		{
			Responce responce = new Responce();
			string responceData = "";
			string json = request.GetPOSTData();
			string objname = request.GetParameter("objname");
			bool valid = true;

			UnityInvoker.ExecuteOnMainThread.Enqueue (() => {  

				// Determine our object in the scene
				GameObject gameObject = GameObject.Find(objname);
				if (gameObject != null)
				{
					try {
						// Deserialise the material
						VHSMaterial vhsMaterial = JsonUtility.FromJson<VHSMaterial>(json);
						VHSMaterial vhsMaterialResult = new VHSMaterial();

						// Check if it's our light source
						if (gameObject.GetComponent<Light>() != null)
						{	
							// Set the color to the object
							Light light = gameObject.GetComponent<Light>();
							light.color = vhsMaterial.GetColor();

							// Create a returning json object for the result
							vhsMaterialResult.SetColor(light.color);
							responceData = JsonUtility.ToJson(vhsMaterialResult);
						} 
						// It's our mesh object
						else 
						{
							// Set the color to the object
							Material mat = gameObject.GetComponent<Renderer>().material;
							mat.color = vhsMaterial.GetColor();

							// Create a returning json object for the result
							vhsMaterialResult.SetColor(mat.color);
							responceData = JsonUtility.ToJson(vhsMaterialResult);
						}

					} catch (Exception e)
					{
						valid = false;
						string msg = "Failed to deseiralised JSON";
						responceData = msg;

						RESTfulHTTPServer.src.controller.Logger.Log(TAG, msg);
						RESTfulHTTPServer.src.controller.Logger.Log(TAG, e.ToString());
					}

				} else {

					// 404 - Not Found
					responceData = "404";
					responce.SetContent(responceData);
					responce.SetHTTPStatusCode((int) HttpStatusCode.NotFound);
					responce.SetMimeType(Responce.MIME_CONTENT_TYPE_HTML);
				}
			});

			// Wait for the main thread
			while (responceData.Equals("")) {}

			// Filling up the responce with data
			if (valid) {

				// 200 - OK
				responce.SetContent(responceData);
				responce.SetHTTPStatusCode ((int)HttpStatusCode.OK);
				responce.SetMimeType (Responce.MIME_CONTENT_TYPE_JSON);
			} else {

				// 406 - Not acceptable
				responce.SetContent("Failed to deseiralised JSON");
				responce.SetHTTPStatusCode((int) HttpStatusCode.NotAcceptable);
				responce.SetMimeType(Responce.MIME_CONTENT_TYPE_HTML);
			}

			return responce;
		}
	}
}

