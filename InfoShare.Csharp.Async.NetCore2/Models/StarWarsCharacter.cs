using Newtonsoft.Json;

namespace InfoShare.Csharp.Async.NetCore2.Models
{
	public class StarWarsCharacter
	{
		public string Name { get; set; }
		public string Gender { get; set; }
		public string Mass { get; set; }
		public string Height { get; set; }
		[JsonProperty("birth_year")]
		public string BirthYear { get; set; }
		[JsonProperty("hair_color")]
		public string HairColor { get; set; }
		[JsonProperty("skin_color")]
		public string SkinColor { get; set; }
		[JsonProperty("eye_color")]
		public string EyeColor { get; set; }
		public string ImageBase64 { get; set; }
	}
}
