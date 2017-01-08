# RESTful-Unity
RESTful-Unity aims to provides a simple RESTful HTTP server for [Unity3d](https://unity3d.com).<br>
Access and capsule the state of game objects from a Unity scene over http requests.

This project was created during prototyping a 3D simulation where game objects could be accessed and controlled by [openHAB](https://github.com/openhab/openhab).<br> It's a proof of concept side project, therefore the functionality of the server was kept to a minimum.

**GET Example**<br>
for requesting the color of a game object. Path: /color/{game object name}
![GET](https://i.imgur.com/jAz4ah2.png)

**POST Example**<br>
for changing the color of a game object by a POST request. Path: /color/{game object name}
![POST](https://i.imgur.com/XTJav1I.png)

## Features

* Supported HTTP tpyes: GET, POST, PUT and DELETE
* Store and configure the routes within a configuration class
* Create parameterize routing paths
* Access the query string variables from a request
* Support for basic authentication
* Capsule the executed operation within static invoker classes and methods
* Define the response content, HTTP status code and MIME type
* This project contains a demo scene for controlling the color of a cube and two light sources


## Setup

### 1. Init
Attach the _Unity main thread involker_ and the _server init_ script to the main camera.<br>
![Camera](https://i.imgur.com/Eyx27YQ.png)

### 2. The routing table
Define the routing table inside the _server init_ script.
![Routing table](https://i.imgur.com/GkNZAl9.png)

### 3. Invoke
Write a the referenced invoker class and method.<br>
![Invoke](https://i.imgur.com/7vCJHD3.png)

And define the HTTP response the the request<br>
![Response](https://i.imgur.com/wKq96cd.png)


## Licence
Copyright 2016, Tim F. Rieck

Licensed under the GLP, Version 3.0 (the "License"); you may not use this work except in compliance with the License. You may obtain a copy of the License in the LICENSE file, or at:

https://www.gnu.org/licenses/gpl-3.0.en.html

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
