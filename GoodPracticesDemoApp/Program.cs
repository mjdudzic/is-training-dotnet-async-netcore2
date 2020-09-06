using System;
using System.Threading.Tasks;

namespace GoodPracticesDemoApp
{
	class Program
	{
		static async Task Main(string[] args)
		{
			await Task.CompletedTask;

			var demoHelper = new AsyncGoodPracticesDemoHelper();

			// Example 1 - fire and forget task
			demoHelper.WriteTextToFile("some text");

			// Example 2 - blocking async task
			var firstPlanet = demoHelper.GetFirstPlanetFromStarWarsApi().Result;

			// Example 3 - async and loops
			var firstTwoPlanets = await demoHelper.GetFirstPlanetsFromStarWarsApi(2);

			// Example 3 - return await task
			var filmResponseData = await demoHelper.GetStarWarsFilmDataResult();
			var filmData = filmResponseData.Content;

			// Example 4 - exceptions handling in async tasks
			try
			{
				var tasks = new[]
				{
					demoHelper.MethodThatThrowsException(),
					demoHelper.OtherMethodThatThrowsException()
				};

				//Task.WaitAll(tasks);
				await Task.WhenAll(tasks);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.GetType().ToString());
			}

			// Example 5 - faking async 
			var starShip = await demoHelper.GetFirstStartShipFromStarWarsApi();

			Console.WriteLine("The end!");
		}
	}
}
