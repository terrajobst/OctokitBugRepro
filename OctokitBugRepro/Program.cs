using System;
using System.Collections.Generic;
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
            var logins = new SortedSet<string>(members.Select(m => m.Login));
            Console.WriteLine(logins.Count);

            var folks = logins.Where(m => m.StartsWith("opb", StringComparison.OrdinalIgnoreCase))
                              .ToArray();

            foreach (var f in folks)
                Console.WriteLine(f);

            // prints:
            // 665
            // // no users
            //
            // Here is what it should display:
            // 1246 // that's the number of members according to the web UI
            // opbld10
            // opbld11
            // opbld12
            // opbld13
            // opbld14
            // opbld15
            // opbld16
            // opbld17
            // opbld18
            // opbld19
            // opbld20
            // opbld21
            // opbld22
            // opbld25
            // opbld26
            // opbld27
            // opbld28
            // opbld29
            // opbld30
            // opbld31
            // opbld32
            // opbld33
            // opbld34
            // opbld35
            // opbld36
            // opbld37
            // opbld38
            // opbld39
            // opbld40
            // opbld41
            // opbld42
            // opbld43
            // opbld44
            // opbld45
            // opbld47
            // opbld48
            // opbld49
            // opbld50
            // opbld51
            // opbld52
            // opbld53
            // opbld54
            // opbld55
            // opbld56
            // opbldsb1
            // opbldsb2
            // opbldsb3
            // opbldsb6
            // opbldsb7
            // opbldsb9
        }
    }
}
