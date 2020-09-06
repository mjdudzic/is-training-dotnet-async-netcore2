using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InfoShare.Csharp.Async.NetCore2.Models;
using InfoShare.Csharp.Async.NetCore2.Services;

namespace InfoShare.Csharp.Async.NetCore2.Controllers
{
	public class HomeController : Controller
	{
		private readonly IStarWarsService _starWarsService;

		public HomeController(IStarWarsService starWarsService)
		{
			_starWarsService = starWarsService;
		}

		public IActionResult Index()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult StarWarsCharacters(string searchString)
		{
			var w = new Stopwatch();
			w.Start();

			if (string.IsNullOrWhiteSpace(searchString))
			{
				return View(new StarWarsCharacter[0]);
			}

			var result = _starWarsService.SearchCharacters(searchString);

			LoadLogos(result.ToList());

			w.Stop();
			Console.WriteLine($"******Execution time {w.ElapsedMilliseconds}");

			return View(result);
		}

		private void LoadLogos(List<StarWarsCharacter> result)
		{
			var logos = new List<KeyValuePair<string, string>>();

			foreach (var character in result)
			{
				if (_logoPaths.ContainsKey(character.Name))
				{
					var imgFullPath = Path.Combine(
						Directory.GetCurrentDirectory(),
						"wwwroot",
						"images",
						_logoPaths[character.Name]);

					logos.Add(
						_starWarsService.GetCharacterLogoImgSrc(character.Name, imgFullPath));
				}
			}

			foreach (var imgResult in logos)
			{
				result.First(i => i.Name == imgResult.Key).ImageBase64 = imgResult.Value;
			}
		}

		private readonly Dictionary<string, string> _logoPaths = new Dictionary<string, string>
		{
			{"Luke Skywalker", "luke_skywalker.png"},
			{"BB8", "bb8.png"},
			{"Han Solo", "han_solo.png"},
			{"Darth Vader", "darth_vader.png"},
			{"R2-D2", "r2d2.png"}
		};
	}
}
