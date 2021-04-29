# Flight Simulator

## Table of contents :smile_cat:
* [General info](#general-info)
* [File Organiztion](#file-organiztion)
* [Setup](#setup)
* [Run](#run)
* [UML](#uml)
* [Youtube Video](#youtube-video)




## General info
This project is a Flight Simulator Research Tool.
Part of Advenced Programing 2 Course at Bar-Ilan University.
This project was built with MVVM Methodology on .NET Framework.


![](https://i2.paste.pics/C77JW.png?trs=475c231022680624d5590487b5db54382c3c1bd4cf6636753bc4d2d0f400a67e)
*
*
*
*
*

### Main Features:
* Server-Client connection with FlightGear.
* UI for Copy Playback_small.Xml File and Connect to FlightGear with csv file.
![](https://i2.paste.pics/C6W96.png?trs=475c231022680624d5590487b5db54382c3c1bd4cf6636753bc4d2d0f400a67e)
* Scrolling through time with the time scroller - you can jump whenever you want and you can change the speed ratio, see the current Time, play and pause the Simulator.
![](https://i2.paste.pics/C727K.png?trs=475c231022680624d5590487b5db54382c3c1bd4cf6636753bc4d2d0f400a67e)
* You can see the joystick of the pilot, also you can see the throttle and rudder position in every second.
* You can see an updating Heading Deg compass.
* Visualing data of the plane and Stick:
![](https://i2.paste.pics/C6WBC.png?trs=475c231022680624d5590487b5db54382c3c1bd4cf6636753bc4d2d0f400a67e)
![](https://i2.paste.pics/C72BV.png?trs=475c231022680624d5590487b5db54382c3c1bd4cf6636753bc4d2d0f400a67e)
* Visual Graphs: you can see 3 Graphs: Feature Graph, Correlated Feature Graph and Regeression Graph.
* The graphs are dynamically updating while flying. The left graph shows in X axis the number of current line from the CSV file, and in Y axis
* the value of the Feature which chosen in the list, in the right graph the Y axis is the value of the most correlated feature. In the list there are all the features from the CSV file and you can chose one of them to show.
![](https://i2.paste.pics/C782W.png)
* Upload Dll for Research your Flight and find some Errors. You can chose any Anomaly Detection Algorithn you want.
* for more info check the DLL info and the API.
![](https://i2.paste.pics/C783D.png)
	
  
  
  
## File Organiztion
Project is created with:
* View Folder - Contains all the Views and Imges.
* ViewModel Folder - Contains all the ViewModels.
* Model Folder - Contains all the Models.
* Plugin - Contains all the Plugin and the Api for more Plugins.



	
## Setup
To install this project you need to install FlightGear 2020.3.6 or older.
in FlightGear -> settings -> Additional Settngs
```
--generic=socket,in,10,127.0.0.1,5400,tcp,playback_small
--fdm=null
--httpd=8080
```
Download playback_small.xml from the Model.




## Run
* run FG and press "Fly!" wait until running
* run this project
* Copy Playback_small - put the folder source of the file and the folder destention and press "copy XML" 
* example XML: 
* folder Source : C:\Downloads
* folder Destenation : C:\Program Files\FlightGear 2020.3.6\data\Protocol
* example CSV:
* folder Source : C:\Users\Omer\source\repos\FlightSimulator2\model
* make sure yout IP and port are set to 127.0.0.1:5400
* press "Connect to FG"
* ENJOY

## UML
https://drive.google.com/file/d/10Hk8dMoRpZMNY2AmbwXpWI7bn-607tdX/view?usp=sharing




## Youtube Video
https://youtu.be/_prmWUhJu5k
