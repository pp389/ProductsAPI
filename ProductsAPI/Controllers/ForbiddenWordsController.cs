using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/forbidden-words")]
public class ForbiddenWordsController : ControllerBase
{
    private readonly IForbiddenWordsService _forbiddenWordsService;

    public ForbiddenWordsController(IForbiddenWordsService forbiddenWordsService)
    {
        _forbiddenWordsService = forbiddenWordsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetForbiddenWords()
    {
        var words = await _forbiddenWordsService.GetForbiddenWordsAsync();
        return Ok(words);
    }

    [HttpPost]
    public async Task<IActionResult> AddForbiddenWord([FromBody] string word)
    {
        await _forbiddenWordsService.AddForbiddenWordAsync(word);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveForbiddenWord([FromBody] string word)
    {
        await _forbiddenWordsService.RemoveForbiddenWordAsync(word);
        return NoContent();
    }
}
