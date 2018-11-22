using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LazyCache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SKU.Stream.Services.Models.GitHub;
using StreamKontrolUnit.BackEnd.Clients;
using Octokit;
using SKU.Stream.Services;

namespace StreamKontrolUnit.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitHubController : ControllerBase
    {
        public GitHubController(IAppCache cache, GitHubRepository repository, JT7SKUOctoClient octoClient, ILogger<GitHubController> logger, IOptions<GitHubConfiguration> githubConfiguration)
        {
            this.Cache = cache;
            this.Logger = logger;
            this.OctoClient = octoClient;
            _githubRepository = repository;
            _githubConfiguration = githubConfiguration.Value;
        }

        public IAppCache Cache { get; private set; }
        public ILogger<GitHubController> Logger { get; private set; }
        public JT7SKUOctoClient OctoClient { get; private set; }

        private readonly GitHubRepository _githubRepository;
        private readonly GitHubConfiguration _githubConfiguration;

        public async Task<IActionResult> ContributorsInformation(string repo, string userName, int count)
        {
            var outModel = await _githubRepository.GetRecentContributors(_githubConfiguration.RepositoryCsv);

            if (!string.IsNullOrEmpty(repo))
            {
                outModel.First(i => i.Repository.Equals(repo, StringComparison.InvariantCultureIgnoreCase)).TopWeekContributors.Add(new GitHubContributor
                {
                    Author = userName,
                    Commits = count
                });
            }
            //Viewbag.Configuration = _githubConfiguration;
            return Ok($"contributor {_githubConfiguration.DisplayMode} {outModel.ToArray()}");

        }

        public IActionResult Configuration()
        {
            return Ok(_githubConfiguration);
        }
        [HttpGet("api/GitHub/Latest")]
        public async Task<IActionResult> LatestChanges()
        {
            var outModel = await _githubRepository.GetLastCommitTimestamp(_githubConfiguration.RepositoryCsv);
            return Ok(outModel.Item1.ToString("MM/dd/yyyy HH:mm:ss"));
        }

        [HttpGet("api/GitHub/Contributors")]
        public async Task<IActionResult> GetContributors()
        {
            var outModel = await _githubRepository.GetRecentContributors(_githubConfiguration.RepositoryCsv);
            return Ok(outModel);
        }

        public IActionResult Test(int value, string devName, string projectName)
        {
            GitHubService.LastUpdate = DateTime.MinValue;
            return Ok(0);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Configuration(GitHubConfiguration gitHubConfiguration)
        {
            if (ModelState.IsValid)
            {
                _githubConfiguration.RepositoryName = gitHubConfiguration.RepositoryName;
                _githubConfiguration.RepositoryOwner = gitHubConfiguration.RepositoryOwner;
            }
            return Ok(gitHubConfiguration);
        }


    }
}