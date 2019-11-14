using System;
using System.Linq;
using System.Threading.Tasks;

using Octokit;

namespace OctokitBugRepro
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var token = "the token";
            var productInformation = new ProductHeaderValue("OctokitBugRepro");
            var client = new GitHubClient(productInformation);
            client.Credentials = new Credentials(token);

            Console.WriteLine("Get teams...");
            var allTeams = await client.Organization.Team.GetAll("dotnet");
            var msTeam = allTeams.Single(t => string.Equals(t.Name, "Microsoft", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine("Get members...");
            var members = await client.Organization.Team.GetAllMembers(msTeam.Id, new TeamMembersRequest(TeamRoleFilter.Member));
            Console.WriteLine(members.Count);
        }
    }
}
