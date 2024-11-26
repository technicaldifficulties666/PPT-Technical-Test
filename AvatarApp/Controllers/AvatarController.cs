using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AvatarApp.Data;
using AvatarApp.Models;

namespace AvatarApp.Controllers
{
  public class AvatarController : Controller
  {
    private readonly ApplicationDbContext dbContext;

    public AvatarController(ApplicationDbContext dbContext)
    {
      this.dbContext = dbContext;
    }

    [HttpGet]
    [Route("Avatar")]
    public IActionResult Index()
    {
      return View();
    }

    [HttpGet]
    [Route("Avatar/GetAvatar")]
    public async Task<IActionResult> GetAvatar(string userIdentifier)
    {
      if (string.IsNullOrEmpty(userIdentifier))
        return Json(new { url = "https://api.dicebear.com/8.x/pixel-art/png?seed=default&size=150" });

      char lastChar = userIdentifier[^1];
      string? imageUrl = null;

      if (char.IsDigit(lastChar))
      {
        int lastDigit = int.Parse(lastChar.ToString());
        if (lastDigit >= 6 && lastDigit <= 9)
        {
          // Rule 1: Fetch from external API
          using var httpClient = new HttpClient();
          string apiUrl = $"https://my-json-server.typicode.com/ck-pacificdev/tech-test/images/{lastDigit}";
          var response = await httpClient.GetStringAsync(apiUrl);

          if (!string.IsNullOrWhiteSpace(response))
          {
            try
            {
              dynamic jsonResponse = JsonConvert.DeserializeObject(response);
              if (jsonResponse?.url != null)
              {
                imageUrl = jsonResponse.url;
              }
            }
            catch (JsonException ex)
            {
              Console.WriteLine($"JSON Deserialization error: {ex.Message}");
            }
          }
        }
        else if (lastDigit >= 1 && lastDigit <= 5)
        {
          // Rule 2: Fetch from SQLite
          var image = await dbContext.Images.FindAsync(lastDigit);
          if (image != null)
          {
            imageUrl = image.Url;
          }
        }
      }
      else if (userIdentifier.Any(c => "aeiou".Contains(c, StringComparison.OrdinalIgnoreCase)))
      {
        // Rule 3: Contains vowels
        imageUrl = "https://api.dicebear.com/8.x/pixel-art/png?seed=vowel&size=150";
      }
      else if (userIdentifier.Any(c => !char.IsLetterOrDigit(c)))
      {
        // Rule 4: Non-alphanumeric
        int randomSeed = new Random().Next(1, 6);
        imageUrl = $"https://api.dicebear.com/8.x/pixel-art/png?seed={randomSeed}&size=150";
      }

      // Rule 5: if none of the conditions above are met
      if (imageUrl == null)
      {
        imageUrl = "https://api.dicebear.com/8.x/pixel-art/png?seed=default&size=150";
      }

      return Json(new { url = imageUrl });
    }
  }
}
