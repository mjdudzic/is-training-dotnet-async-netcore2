using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using RestSharp;

namespace GoodPracticesDemoApp
{
	public class AsyncGoodPracticesDemoHelper
	{
		public async void WriteTextToFile(string text)
		{
			await Task.Delay(1000);
			await File.WriteAllTextAsync("test.txt", text);
		}

		public async Task<string> GetFirstPlanetFromStarWarsApi()
		{
			var client = new RestClient("http://swapi.dev/api/planets/1/");

			var request = new RestRequest(Method.GET);
			request.AddHeader("Accept", "application/json");
			var response = await client.ExecuteAsync(request);

			return response.Content;
		}

		public async Task<List<string>> GetFirstPlanetsFromStarWarsApi(int planetsCount)
		{
			var client = new RestClient("http://swapi.dev/api/");

			var planetsData = new List<string>();

			for (var i = 1; i <= planetsCount; i++)
			{
				var request = new RestRequest($"planets/{i}/", Method.GET);
				request.AddHeader("Accept", "application/json");

				var planet = (await client.ExecuteAsync(request)).Content;

				planetsData.Add(planet);
			}

			return planetsData;
		}

		public async Task<IRestResponse> GetStarWarsFilmDataResult()
		{
			var client = new RestClient("http://swapi.dev/api/films/1/");

			var request = new RestRequest(Method.GET);
			request.AddHeader("Accept", "application/json");
			return await client.ExecuteAsync(request);
		}

		public async Task MethodThatThrowsException()
		{
			await Task.CompletedTask;

			Console.WriteLine("method 1 runs");
			throw new MyAppException1();
		}

		public async Task OtherMethodThatThrowsException()
		{
			await Task.CompletedTask;

			Console.WriteLine("method 2 runs");
			throw new MyAppException2();
		}

		public Task<string> GetFirstStartShipFromStarWarsApi()
		{
			return Task.Run(() =>
			{
				var client = new RestClient("http://swapi.dev/api/starships/10/");

				var request = new RestRequest(Method.GET);
				request.AddHeader("Accept", "application/json");
				var response = client.Execute(request);

				return response.Content;
			});
		}

		public class MyAppException1 : Exception
		{
		}

		public class MyAppException2 : Exception
		{
		}
	}
}