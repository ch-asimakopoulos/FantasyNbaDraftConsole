namespace FantasyNbaDraftConsole
{
    using System;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using McMaster.Extensions.CommandLineUtils;

    /// <summary>
    /// 
    /// </summary>
    public class OperationsService : IOperationsService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void SeedDatabase()
        {
            using (var db = new Data.NBADbContext())
            {
                try
                {
                    // Create and save a new League
                    Console.WriteLine("Enter a name for the league: ");
                    var leagueName = Console.ReadLine();

                    Console.WriteLine("Enter number of draft rounds: ");
                    var draftRounds = Convert.ToByte(Console.ReadLine());

                    var isSnakeDraft = Prompt.GetYesNo("Is the draft a snake draft?", true);

                    var lresult = db.Leagues.Where(l => l.LeagueInitialized && l.LeagueName == leagueName && l.DraftConfig.Rounds == draftRounds);

                    var league = default(Data.Models.League);

                    if (lresult.Any())
                    {
                        league = lresult.Single();
                    }
                    else
                    {
                        league = new Data.Models.League
                        {
                            LeagueName = leagueName,
                            LeagueInitialized = true,
                            DraftConfig = new Data.Models.DraftConfig {
                                Rounds = draftRounds,
                                Snake = isSnakeDraft
                            }
                        };

                        db.Leagues.Add(league);
                        db.SaveChanges();
                    }

                    Console.WriteLine($"League {leagueName} added!");

                    var importPositions = Prompt.GetYesNo("Import default positions for players?", true);

                    if (importPositions)
                    {
                        foreach (var p in Constants.DraftConstants.DefaultPositions)
                        {
                            var position = new Data.Models.Position
                            {
                                PositionName = p
                            };

                            db.Positions.Add(position);
                        }

                        db.SaveChanges();
                        Console.WriteLine("Default Positions PG/SG/SF/PF/C/G/F added!");
                    }

                    Console.WriteLine("Enter number of teams: ");
                    var numberOfTeams = Console.ReadLine();

                    for (byte i = 1; i <= Convert.ToByte(numberOfTeams); i++)
                    {
                        Console.WriteLine("Enter team name: ");
                        var teamName = Console.ReadLine();

                        var team = new Data.Models.Team
                        {
                            League = league,
                            TeamName = teamName,
                            TeamDraftPosition = i
                        };

                        db.Teams.Add(team);
                        db.SaveChanges();

                        Console.WriteLine($"Team {teamName} added! Default draft position is: {i}, but it can be configured later");
                    }

                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void ListDraftPositions()
        {
            using (var db = new Data.NBADbContext())
            {
                try
                {
                    // List draft positions
                    var lresult = db.Leagues.Include(l => l.LeagueTeams).Where(l => l.LeagueInitialized && l.LeagueName == Constants.DraftConstants.LeagueName);

                    var league = default(Data.Models.League);

                    if (lresult.Any())
                    {
                        league = lresult.Single();
                    }
                    else
                    {
                        Console.WriteLine("Enter a name for the league: ");
                        var leagueName = Console.ReadLine();

                        lresult = db.Leagues.Include(l => l.LeagueTeams).Where(l => l.LeagueInitialized && l.LeagueName == leagueName);
                        if (lresult.Any())
                        {
                            league = lresult.Single();
                        }
                        else
                        {
                            return;
                        }
                    }

                    foreach (var team in league.LeagueTeams.OrderBy(t => t.TeamDraftPosition))
                    {
                        Console.WriteLine($"Team's {team.TeamName} draft position: {team.TeamDraftPosition}");
                    }

                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void InitializeDraftPositions()
        {
            using (var db = new Data.NBADbContext())
            {
                try
                {
                    // Initialize draft positions
                    var lresult = db.Leagues.Include(l => l.LeagueTeams).Where(l => l.LeagueInitialized && l.LeagueName == Constants.DraftConstants.LeagueName);

                    var league = default(Data.Models.League);

                    if (lresult.Any())
                    {
                        league = lresult.Single();
                    }
                    else
                    {
                        Console.WriteLine("Enter a name for the league: ");
                        var leagueName = Console.ReadLine();

                        lresult = db.Leagues.Include(l => l.LeagueTeams).Where(l => l.LeagueInitialized && l.LeagueName == leagueName);
                        if (lresult.Any())
                        {
                            league = lresult.Single();
                        }
                        else
                        {
                            return;
                        }
                    }

                    foreach (var team in league.LeagueTeams)
                    {
                        Console.WriteLine($"Enter team's {team.TeamName} draft position: ");
                        var draftPosition = Convert.ToByte(Console.ReadLine());

                        team.TeamDraftPosition = draftPosition;
                        db.SaveChanges();

                        Console.WriteLine($"Team's {team.TeamName} draft position is: {draftPosition}!");
                    }

                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return;
        }
    }
}
