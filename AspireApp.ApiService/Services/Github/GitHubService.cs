using System.Net.Http.Headers;
using System.Text.Json;

namespace AspireApp.ApiService.Services.Github;

public class GitHubService(HttpClient client, ILogger<GitHubService> logger)
{
    public async Task<GithubProfileDto?> GetUserProfileAsync(string accessToken)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        try
        {
            var uri = new Uri("user", UriKind.Relative);
            var response = await client.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            var rawProfile = await response.Content.ReadAsStringAsync();

            var profileDto = JsonSerializer.Deserialize<GithubProfileDto>(rawProfile);

            return profileDto;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to get GitHub user profile");
            return null;
        }
    }
}