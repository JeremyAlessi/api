using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API {

	public class Robots {

		public class BestRobot {
			public string robotId{ get; set; }
			public float distanceToGoal{ get; set; }
			public int batteryLevel{ get; set; }
		}

		public class Load {
			public string loadId;
			public float x;
			public float y;
		}

		public class Robot {
			public string robotId{ get; set; }
			public int batteryLevel{ get; set; }
			public float y{ get; set; }
			public float x{ get; set; }
		}

		public class RobotList {
			public List<Robot> robots {get; set;}
		}

		public BestRobot Best( string list, string json ) {

			var options = new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			};

			List<Robot> robotList = JsonSerializer.Deserialize<List<Robot>>( list, options );

			//test load in Swagger
			//{"loadId": "231", "x": 5, "y": 3}

			Load load = JsonSerializer.Deserialize<Load>( json );

			//Which needle in the haystack of 100 is best?
			Robot bestRobotNeedle = null;

			//Find close robots
			List<Robot> closeRobots = new List<Robot>();
			foreach( Robot robot in robotList) {
				float currDist = (float)Math.Sqrt( Math.Pow(robot.x - load.x, 2) + Math.Pow(robot.y - load.y, 2 ) );
				if( currDist <= 10 ) {
					closeRobots.Add( robot );
				}
			}

			//Find the strongest battery of the close robots
			int strongBattery = 0;
			foreach( Robot robot in closeRobots ) {
				if( robot.batteryLevel > strongBattery ) {
					bestRobotNeedle = robot;
					strongBattery = robot.batteryLevel;
				}
			}

			//Create an instance of BestRobot from bestRobot's properties
			BestRobot bestRobot = new BestRobot();
			bestRobot.robotId = bestRobotNeedle.robotId;
			bestRobot.distanceToGoal = (float)Math.Sqrt( Math.Pow(bestRobotNeedle.x - load.x, 2) + Math.Pow( bestRobotNeedle.y - load.y, 2 ) );
			bestRobot.batteryLevel = bestRobotNeedle.batteryLevel;

			//Return it for API response
			return bestRobot;
		}
	}
}
