namespace FantasyNbaDraftConsole
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Globalization;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using CsvHelper;
    using McMaster.Extensions.CommandLineUtils;
    using System.Runtime.Intrinsics.Arm;

    /// <summary>
    /// 
    /// </summary>
    public class OperationsService : IOperationsService
    {
        /// <summary>
        /// 
        /// </summary>
        private IHelperMapper helperMapper_ { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helperMapper"></param>
        public OperationsService(IHelperMapper helperMapper)
        {
            helperMapper_ = helperMapper;
        }

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

                    var lresult = db.Leagues.Where(l => l.LeagueInitialized
                        && l.LeagueName == leagueName
                        && l.DraftConfig.Snake == isSnakeDraft
                        && l.DraftConfig.Rounds == draftRounds);

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
                            DraftConfig = new Data.Models.DraftConfig
                            {
                                Snake = isSnakeDraft,
                                Rounds = draftRounds
                            }
                        };

                        db.Leagues.Add(league);
                        db.SaveChanges();
                    }

                    Console.WriteLine($"League {leagueName} added!");

                    Console.WriteLine("Enter number of teams: ");
                    var numberOfTeams = Console.ReadLine();

                    for (byte i = 1; i <= Convert.ToByte(numberOfTeams); i++)
                    {
                        Console.WriteLine("Enter team name: ");
                        var teamName = Console.ReadLine();

                        var team = new Data.Models.Team
                        {
                            League = league,
                            Name = teamName,
                            DraftPosition = i
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
                    var lresult = db.Leagues.AsNoTracking().Include(l => l.LeagueTeams).Where(l => l.LeagueInitialized && l.LeagueName == Constants.DraftConstants.LeagueName);

                    var league = default(Data.Models.League);

                    if (lresult.Any())
                    {
                        league = lresult.Single();
                    }
                    else
                    {
                        Console.WriteLine("Enter a name for the league: ");
                        var leagueName = Console.ReadLine();

                        lresult = db.Leagues.AsNoTracking().Include(l => l.LeagueTeams).Where(l => l.LeagueInitialized && l.LeagueName == leagueName);
                        if (lresult.Any())
                        {
                            league = lresult.Single();
                        }
                        else
                        {
                            return;
                        }
                    }

                    foreach (var team in league.LeagueTeams.OrderBy(t => t.DraftPosition))
                    {
                        Console.WriteLine($"Team's {team.Name} draft position: {team.DraftPosition}");
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
        /// <param name="name"></param>
        /// <returns></returns>
        public bool StarPlayer(string name)
        {
            using (var db = new Data.NBADbContext())
            {
                try
                {
                    // Find Player
                    var presult = db.Players.FirstOrDefault(p => name == p.Name);

                    if (presult == null)
                    {
                        return false;
                    }
                    else
                    {
                        presult.Starred = true;
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return true;
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
                        Console.WriteLine($"Enter team's {team.Name} draft position: ");
                        var draftPosition = Convert.ToByte(Console.ReadLine());

                        team.DraftPosition = draftPosition;
                        db.SaveChanges();

                        Console.WriteLine($"Team's {team.Name} draft position is: {draftPosition}!");
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
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int SeedPlayers(string filePath)
        {
            var addedPlayers = 0;

            using (var db = new Data.NBADbContext())
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var rows = csv.GetRecords<Csv.ImportPlayers.ImportPlayersRow>();

                    foreach (var row in rows)
                    {
                        if (db.Players.Any(p => p.Name == row.Name))
                        {
                            continue;
                        }

                        var player = helperMapper_.MapRowToPlayerModel(row);

                        if (player == null)
                        {
                            Console.WriteLine($"Error mapping {row.Name}");
                            continue;
                        }
                        else
                        {
                            db.Players.Add(player);
                            addedPlayers++;
                        }
                    }
                }
                db.SaveChanges();
            }

            return addedPlayers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="teamId"></param>
        /// <param name="currentPick"></param>
        /// <returns></returns>
        public bool PickPlayer(string name, byte teamId, short? currentPick)
        {
            using (var db = new Data.NBADbContext())
            {
                try
                {
                    // Find Player
                    var presult = db.Players.FirstOrDefault(p => name == p.Name);

                    while (presult == null)
                    {
                        Console.WriteLine("Enter a valid name for the player picked: ");
                        name = Console.ReadLine();

                        presult = db.Players.FirstOrDefault(p => name == p.Name);

                    };

                    var tresult = db.Teams.Include(t => t.DraftedPlayers).FirstOrDefault(t => teamId == t.TeamId);

                    while (tresult == null)
                    {
                        Console.WriteLine("Enter a valid name for the team that picked him: ");
                        var team = Console.ReadLine();

                        tresult = db.Teams.Include(t => t.DraftedPlayers).FirstOrDefault(t => team == t.Name);

                    };

                    presult.PickNumber = currentPick;
                    tresult.DraftedPlayers.Add(presult);

                    db.SaveChanges();

                    Console.WriteLine($"Player {name} picked by {tresult.Name} with {currentPick} pick.");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Data.Dto.DraftInfo GetDraftInfo(string leagueName)
        {
            using (var db = new Data.NBADbContext())
            {
                var league = db.Leagues.Include(l => l.DraftConfig).Include(l => l.LeagueTeams).First(l => l.LeagueName == leagueName && l.LeagueInitialized);

                return new Data.Dto.DraftInfo
                {
                    Snake = league.DraftConfig.Snake,
                    Rounds = league.DraftConfig.Rounds,
                    TeamCount = (byte)league.LeagueTeams.Count(),
                    TeamOrder = league.LeagueTeams.OrderBy(t => t.DraftPosition).Select(t => (t.TeamId, t.Name)).ToArray()
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ICollection<Data.Dto.LiveRankingsInfo> GetLiveRankings()
        {
            var liveRankings = new List<Data.Dto.LiveRankingsInfo>();

            using (var db = new Data.NBADbContext())
            {
                foreach (var t in db.Teams.Include(t => t.DraftedPlayers).ThenInclude(p => p.ProjectionTotals).Include(t => t.DraftedPlayers).ThenInclude(p => p.Projections).Include(t => t.DraftedPlayers).ThenInclude(p => p.ProjectionTotals).AsNoTracking().ToList())
                {
                    liveRankings.Add(new Data.Dto.LiveRankingsInfo
                    {
                        PlayersDrafted = t.DraftedPlayers.Select(dp => new Data.Dto.PlayersInfo
                        {
                            Name = dp.Name,
                            Positions = dp.Positions.Select(p => p.PositionTypeId.ToString()).ToList(),
                            Ranking = dp.ProjectionTotals.First(pjt => pjt.ProjectionTotalsTypeId == Constants.ProjectionTotalsTypeId.TotalRanking).ProjectionValue
                        }).ToList(),
                        TeamName = t.Name,
                        ProjectionTypeRankings = t.DraftedPlayers.SelectMany(dp => dp.Projections).GroupBy(grp => grp.ProjectionTypeId).ToDictionary(kvp => kvp.Key, kvp => kvp.Sum(prj => prj.ProjectionValue)),
                    });
                }
            }

            return liveRankings.OrderByDescending(lr => lr.PlayersDrafted.Sum(dp => dp.Ranking)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="projectionsToPunt"></param>
        /// <returns></returns>
        public ICollection<Data.Dto.PlayersInfo> GetBestAvailablePlayers(int? number, ICollection<Constants.ProjectionTypeId> projectionsToPunt)
        {
            using (var db = new Data.NBADbContext())
            {
                var toReturn = number ?? Constants.DraftConstants.PlayersToReturn;
                if (projectionsToPunt.Any())
                {
                    return db.Players.AsNoTracking().Where(p => p.TeamId == null).OrderByDescending(p => p.ProjectionTotals.First(pjt => pjt.ProjectionTotalsTypeId == Constants.ProjectionTotalsTypeId.TotalRanking).ProjectionValue
                     - p.Projections.Where(pj => projectionsToPunt.Contains(pj.ProjectionTypeId)).Sum(pj => pj.ProjectionValue)).Take(toReturn)
                     .Select(p => new Data.Dto.PlayersInfo
                     {
                         Name = p.Name,
                         Starred = p.Starred,
                         Positions = p.Positions.Select(x => x.PositionTypeId.ToString()).ToList(),
                         Ranking = p.ProjectionTotals.First(pjt => pjt.ProjectionTotalsTypeId == Constants.ProjectionTotalsTypeId.TotalRanking).ProjectionValue
                                - p.Projections.Where(pj => projectionsToPunt.Contains(pj.ProjectionTypeId)).Sum(pj => pj.ProjectionValue)
                     })
                     .ToList();
                }

                return db.Players.AsNoTracking().Where(p => p.TeamId == null)
                    .OrderByDescending(p => p.ProjectionTotals.First(pjt => pjt.ProjectionTotalsTypeId == Constants.ProjectionTotalsTypeId.TotalRanking).ProjectionValue)
                    .Take(toReturn)
                    .Select(p => new Data.Dto.PlayersInfo
                    {
                        Name = p.Name,
                        Starred = p.Starred,
                        Positions = p.Positions.Select(x => x.PositionTypeId.ToString()).ToList(),
                        Ranking = p.ProjectionTotals.First(pjt => pjt.ProjectionTotalsTypeId == Constants.ProjectionTotalsTypeId.TotalRanking).ProjectionValue
                    }
                    ).ToList();
            }
        }
    }
}
