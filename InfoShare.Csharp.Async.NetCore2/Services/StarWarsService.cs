using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using InfoShare.Csharp.Async.NetCore2.Models;
using Newtonsoft.Json;
using RestSharp;

namespace InfoShare.Csharp.Async.NetCore2.Services
{
	public class StarWarsService : IStarWarsService
	{
		private const string ApiUrl = "http://swapi.dev/api/people/";

		public IEnumerable<StarWarsCharacter> SearchCharacters(string filter)
		{
			var url = ApiUrl;
			if (!string.IsNullOrWhiteSpace(filter))
			{
				url = $"{ApiUrl}?search={filter}";
			}

			var client = new RestClient(url);

			var request = new RestRequest(Method.GET);
			request.AddHeader("Accept", "application/json");
			var response = client.Execute(request);

			var data = JsonConvert.DeserializeObject<ApiResult>(response.Content);

			return data.Results;
		}

		public async Task<IEnumerable<StarWarsCharacter>> SearchCharactersAsync(string filter)
		{
			var url = ApiUrl;
			if (!string.IsNullOrWhiteSpace(filter))
			{
				url = $"{ApiUrl}?search={filter}";
			}

			var client = new RestClient(url);

			var request = new RestRequest(Method.GET);
			request.AddHeader("Accept", "application/json");
			var response = await client.ExecuteAsync(request);

			var data = JsonConvert.DeserializeObject<ApiResult>(response.Content);

			return data.Results;
		}

		public KeyValuePair<string, string> GetCharacterLogoImgSrc(string key, string imgPath)
		{
			if (File.Exists(imgPath) == false)
			{
				return new KeyValuePair<string, string>(key, string.Empty);
			}

			var imageBytes = File.ReadAllBytes(imgPath);
			var base64String = Convert.ToBase64String(imageBytes);
			return new KeyValuePair<string, string>(key, base64String);
		}

		public async Task<KeyValuePair<string, string>> GetCharacterLogoImgSrcAsync(string key, string imgPath)
		{
			if (File.Exists(imgPath) == false)
			{
				return new KeyValuePair<string, string>(key, string.Empty);
			}

			var imageBytes = await File.ReadAllBytesAsync(imgPath);
			var base64String = Convert.ToBase64String(imageBytes);
			return new KeyValuePair<string, string>(key, base64String);
		}

		private class ApiResult
		{
			public IEnumerable<StarWarsCharacter> Results { get; set; }
		}
	}
}
