public interface IForbiddenWordsService
{
    Task<bool> IsForbiddenAsync(string productName);
    Task<List<string>> GetForbiddenWordsAsync();
    Task AddForbiddenWordAsync(string word);
    Task RemoveForbiddenWordAsync(string word);
}
