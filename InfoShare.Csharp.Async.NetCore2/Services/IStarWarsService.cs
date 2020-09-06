using System.Collections.Generic;
using System.Threading.Tasks;
using InfoShare.Csharp.Async.NetCore2.Models;

namespace InfoShare.Csharp.Async.NetCore2.Services
{
	public interface IStarWarsService
	{
		IEnumerable<StarWarsCharacter> SearchCharacters(string filter);
		KeyValuePair<string, string> GetCharacterLogoImgSrc(string key, string imgPath);
		Task<IEnumerable<StarWarsCharacter>> SearchCharactersAsync(string filter);
		Task<KeyValuePair<string, string>> GetCharacterLogoImgSrcAsync(string key, string imgPath);
	}
}