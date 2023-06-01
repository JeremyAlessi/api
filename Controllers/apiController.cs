using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

namespace api.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class apiController : ControllerBase {

		private readonly ILogger<apiController> _logger;

		private API.Robots.BestRobot bestRobot;
		private string bestRobotJson;

		public apiController(ILogger<apiController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		[Route("robots/closest")]
		public async Task<IActionResult> Post([FromBody] JsonElement json )
		{
			IAsyncEnumerable<API.Robots> asyncEnumerable = DoGet( json.ToString() );
			await foreach (API.Robots robot in asyncEnumerable){}
			while( bestRobot == null ){};
			return Ok( bestRobotJson );
		}

		private async IAsyncEnumerable<API.Robots> DoGet( string json )
		{
			HttpClient client = new HttpClient();
			string url = "https://60c8ed887dafc90017ffbd56.mockapi.io/robots";
        
			HttpResponseMessage response = await client.GetAsync(url);
        
			if (response.IsSuccessStatusCode)
			{
				string svtList = await response.Content.ReadAsStringAsync();

				API.Robots robots = new API.Robots();
				bestRobot = robots.Best( svtList, json );
				bestRobotJson = JsonSerializer.Serialize( bestRobot ).ToString();
			}
        
			client.Dispose();

			yield break;
		}
	}
}
