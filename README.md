# api
Grab a haystack of robots in json format from a url, find all the robots within 10 units of a load, find the robot with the best battery level of those, accept json as post data for the load, return a json response for the closest (best?) robot. Add a readme, describe how to test, and describe what comes next.

Open the api.sln file with Visual Studio 2019

 1) Copy the test load from here or from in the source (Robots.cs line 44) - {"loadId": "231", "x": 5, "y": 3}
 2) Run the program in Visual Studio 2019 (look for the green arrow, choose project "api")
 3) A web browser should open to Swagger and a Post should be accessible for specified route /api/robots/closest
 4) Click "Post"
 5) Click "Try it out"
 6) Paste the example json load into the text field
 7) Click "Execute"
 8) Observe response - {"robotId":"4","distanceToGoal":7.28011,"batteryLevel":37}

In the future or simply in an alternate reality, this app would be great with visuals and simulations. Knowing the best robot from a distance and battery level vantage point is great as is simply querying the avilable data based on other parameters but there are other ways to assess the situation. Having visuals to go with the data could help identify "unseen" potential. The application of machine learning to this data may also be useful, including in-conjunction with object recognition. A robot may be close and have the best battery level, but what if it has to navigate around obstacles that aren't represented in the expected data? The possibilities are endless.
