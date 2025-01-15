# HackerNewsExercise

Exercise for obtain hacker news stories through their public API.

## Overview

Taking the considerations in the document for the exercise provided. The client used for retrieve the information through the Hacker News API is configured for resilient conections and retry attempts, to avoid the flood of the request to the HackerNews API. Also a cache in Memory is configured to prevent going to get the information on every request made throug the endpoints.

## How to Run the API

Open your preferred CLI to run the next commands.

Locate in the desired folder to download the project.

Clone the repository using the next command: git clone https://github.com/sombrerudo/HackerNewsExercise

Navigate to project directory: cd HackerNewsExercise/HackerNewsExercise

Build the project: dotnet build

if the build is sucess Run the project using cmd: dotnet run

The API will be accessible at http://localhost:5051/swagger/index.html (The port could change dependig the configurations in the project)

Video Tutorial for running the project: https://youtu.be/bFmWWSonAaI

## Improvements pending

For the lack of time there's some features missing. Like saving in persistance at least the relation of the Id with it´s score to improve the performance in the response.

For this project there's no unit or integration testing. Could be for the next version.
