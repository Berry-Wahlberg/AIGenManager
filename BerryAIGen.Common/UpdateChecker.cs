using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BerryAIGen.Github;

namespace BerryAIGen.Common;

public class UpdateChecker
{
    public GithubClient Client { get; }
    private CancellationTokenSource _cts;

    public void Cancel()
    {
        _cts.Cancel();
    }

    public CancellationToken CancellationToken => _cts.Token;

    public Release LatestRelease { get; private set; }

    public UpdateChecker()
    {
        _cts = new CancellationTokenSource();
        Client = new GithubClient("Berry-Wahlberg", "AIGenManager");
    }

    private async Task<Release> GetLatestRelease(CancellationToken cancellationToken = default)
    {
        var releases = await Client.GetReleases(cancellationToken);
        return releases.OrderByDescending(r => r.published_at).First();
    }

    public async Task<bool> CheckForUpdate(string? path = null, int timeout = 3000, CancellationToken cancellationToken = default)
    {
        // If no cancellation token is provided, use the internal one
        var token = cancellationToken == default ? _cts.Token : cancellationToken;

        try
        {
            LatestRelease = await GetLatestRelease(token);

            var localVersion = SemanticVersionHelper.GetLocalVersion(path);

            if (SemanticVersion.TryParse(LatestRelease.tag_name, out var releaseVersion))
            {
                return releaseVersion > localVersion;
            }
            return false;
        }
        catch (OperationCanceledException)
        {
            // Re-throw cancellation exceptions
            throw;
        }
        catch (HttpRequestException ex)
            {
                Logger.Log($"Network error during update check: {ex.Message}");
                Logger.Log(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Log($"Unexpected error during update check: {ex.Message}");
                Logger.Log(ex);
                throw;
            }
    }
}

