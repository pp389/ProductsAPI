public class ForbiddenWordsService : IForbiddenWordsService
{
    private readonly List<string> _forbiddenWords = new List<string> { };

    public Task<bool> IsForbiddenAsync(string productName)
    {
        return Task.FromResult(_forbiddenWords.Any(word => productName.Contains(word, StringComparison.OrdinalIgnoreCase)));
    }

    public Task<List<string>> GetForbiddenWordsAsync()
    {
        return Task.FromResult(_forbiddenWords.ToList());
    }

    public Task AddForbiddenWordAsync(string word)
    {
        if (!_forbiddenWords.Contains(word, StringComparer.OrdinalIgnoreCase))
        {
            _forbiddenWords.Add(word);
        }
        return Task.CompletedTask;
    }

    public Task RemoveForbiddenWordAsync(string word)
    {
        _forbiddenWords.RemoveAll(w => w.Equals(word, StringComparison.OrdinalIgnoreCase));
        return Task.CompletedTask;
    }
}
