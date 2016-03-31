Hello! Thank you for your interest in my application.

# MTA-GhostBus-Sample-Codes
This is a repository for the MTA Ghostbus app we developed for a class project.

# Project Description: MTA GhostBus
The MTA GhostBus is a virtual reality application that allows users to track the location of the the B54 bus in a 2D or 3D view. For a video demo of this app, you can visit: https://vimeo.com/130789600

For this project, we used the MapNav Unity plugin in this project - the MapNav plugin enables us to plot objects in Unity using real GPS coordinates. MapNav utilizes Google Maps, so in this project we plotted our bus objects in an actual Google Map.

# SCRIPT SAMPLES

## UpdateBusLocation.cs
This is the script I created to plot the location of the bus objects in Unity. It is based on MapNav's script SetGeolocation.cs.
However, MapNav's existing script is a component that only allows plotting of objects one at a time in an editor. In MapNav's component, the user must manually enter the longitude and latitude of only one object in order to convert it to the equivalent value in Unity's cartesian coordinate system. Once the values are entered, the user must click "Apply" in order to update the location of a the object so it will be visible in the Google Map. But for the project, I wanted to be able to plot several objects dynamically at the same time since I want to display all the running buses in the map. Thus, I call this script in BusUpdater.cs when I want to plot all the buses that are currently running.

## RetrieveAllBus.cs
RetrieveAllBus.cs is the script I use to call the MTA BusTime API in an XML file format. This script also retrieves the specific data I need inside the XML file.

## BusUpdater.cs
BusUpdater.cs is the script that retrieves and reads the XML data acquired the by RetrieveAllBus.cs script. I added several variables to retrieve specific details of the bus such as its longitude, latitude, the next stop, and vehicle ID. Once I retrieved the GPS location of multiple buses, I call the UpdateBusLocation.cs to plot it in Unity coordinates.

## SetGeolocation.cs
I did not create this script. I included it for reference and comparison between the UpdateBusLocation.cs script that I created.

Thank you for taking your time in reading this! Enjoy! :)

