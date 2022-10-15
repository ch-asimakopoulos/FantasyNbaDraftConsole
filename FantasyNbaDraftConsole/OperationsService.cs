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
                            DraftConfig = new Data.Models.DraftConfig {
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
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
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

                        var player = MapRowToPlayer(row);

                        if (player == null)
                        {
                            Console.WriteLine($"Error mapping {row.Name}");
                            continue;
                        } else
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
        /// <param name="number"></param>
        /// <returns></returns>
        public List<Data.Dto.BestPlayersInfo> GetBestAvailablePlayers(int? number)
        {
            using (var db = new Data.NBADbContext())
            {
                var toReturn = number ?? Constants.DraftConstants.PlayersToReturn;

                return db.ProjectionTotals.Where(pjt => pjt.ProjectionTotalsTypeId == Constants.ProjectionTotalsTypeId.TotalRanking)
                    .OrderByDescending(pjt => pjt.ProjectionValue).Take(toReturn)
                    .Select(p => new Data.Dto.BestPlayersInfo
                    {
                        Name = p.Player.Name,
                        Ranking = p.ProjectionValue,
                        Positions = p.Player.Positions.Select(x => x.PositionTypeId.ToString()).ToList()
                    }
                    ).ToList();
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        private IEnumerable<Data.Models.Position> MapToPositions(string[] positions)
        {
            var positionIds = new HashSet<Constants.PositionTypeId>();

            foreach (var p in positions)
            {
                switch (p)
                {
                    case "PG":
                        positionIds.Add(Constants.PositionTypeId.Guard);
                        positionIds.Add(Constants.PositionTypeId.PointGuard);
                        break;
                    case "SG":
                        positionIds.Add(Constants.PositionTypeId.Guard);
                        positionIds.Add(Constants.PositionTypeId.ShootingGuard);
                        break;
                    case "SF":
                        positionIds.Add(Constants.PositionTypeId.Forward);
                        positionIds.Add(Constants.PositionTypeId.SmallForward);
                        break;
                    case "PF":
                        positionIds.Add(Constants.PositionTypeId.Forward);
                        positionIds.Add(Constants.PositionTypeId.PowerForward);
                        break;
                    case "C":
                        positionIds.Add(Constants.PositionTypeId.Center);
                        break;
                    default:
                        break;
                }
            }

            return positionIds.Select(p => new Data.Models.Position
            {
                PositionTypeId = p
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private Data.Models.Player MapRowToPlayer(Csv.ImportPlayers.ImportPlayersRow row)
        {
            var player = new Data.Models.Player();

            player.Name = row.Name;
            player.NbaTeam = row.NbaTeam;

            var positions = row.Positions.Split("/");
            player.Positions.AddRange(MapToPositions(positions));

            player.ProjectionTotals.Add(new Data.Models.ProjectionTotal
            {
                ProjectionTotalsTypeId = Constants.ProjectionTotalsTypeId.TotalRanking,
                ProjectionValue = Convert.ToDecimal(row.TotalRanking, CultureInfo.InvariantCulture)
            });

            player.ProjectionTotals.Add(new Data.Models.ProjectionTotal
            {
                ProjectionTotalsTypeId = Constants.ProjectionTotalsTypeId.GamesPlayed,
                ProjectionValue = Convert.ToDecimal(row.Games, CultureInfo.InvariantCulture)
            });

            player.ProjectionTotals.Add(new Data.Models.ProjectionTotal
            {
                ProjectionTotalsTypeId = Constants.ProjectionTotalsTypeId.MinutesPerGame,
                ProjectionValue = Convert.ToDecimal(row.MinutesPerGame, CultureInfo.InvariantCulture)
            });

            var turnoverInfo = row.Turnovers.Split(" ");

            player.ProjectionTotals.Add(new Data.Models.ProjectionTotal
            {
                ProjectionTotalsTypeId = Constants.ProjectionTotalsTypeId.Turnovers,
                ProjectionValue = Convert.ToDecimal(turnoverInfo[0], CultureInfo.InvariantCulture)
            });

            player.Projections.Add(new Data.Models.Projection
            {
                ProjectionTypeId = Constants.ProjectionTypeId.Turnovers,
                ProjectionValue = Convert.ToDecimal(turnoverInfo[1], CultureInfo.InvariantCulture)
            });

            var stealsInfo = row.Steals.Split(" ");

            player.ProjectionTotals.Add(new Data.Models.ProjectionTotal
            {
                ProjectionTotalsTypeId = Constants.ProjectionTotalsTypeId.Steals,
                ProjectionValue = Convert.ToDecimal(stealsInfo[0], CultureInfo.InvariantCulture)
            });

            player.Projections.Add(new Data.Models.Projection
            {
                ProjectionTypeId = Constants.ProjectionTypeId.Steals,
                ProjectionValue = Convert.ToDecimal(stealsInfo[1], CultureInfo.InvariantCulture)
            });

            var blocksInfo = row.Blocks.Split(" ");

            player.ProjectionTotals.Add(new Data.Models.ProjectionTotal
            {
                ProjectionTotalsTypeId = Constants.ProjectionTotalsTypeId.Blocks,
                ProjectionValue = Convert.ToDecimal(blocksInfo[0], CultureInfo.InvariantCulture)
            });

            player.Projections.Add(new Data.Models.Projection
            {
                ProjectionTypeId = Constants.ProjectionTypeId.Blocks,
                ProjectionValue = Convert.ToDecimal(blocksInfo[1], CultureInfo.InvariantCulture)
            });

            var assistsInfo = row.Assists.Split(" ");

            player.ProjectionTotals.Add(new Data.Models.ProjectionTotal
            {
                ProjectionTotalsTypeId = Constants.ProjectionTotalsTypeId.Assists,
                ProjectionValue = Convert.ToDecimal(assistsInfo[0], CultureInfo.InvariantCulture)
            });

            player.Projections.Add(new Data.Models.Projection
            {
                ProjectionTypeId = Constants.ProjectionTypeId.Assists,
                ProjectionValue = Convert.ToDecimal(assistsInfo[1], CultureInfo.InvariantCulture)
            });

            var reboundsInfo = row.TotalRebounds.Split(" ");

            player.ProjectionTotals.Add(new Data.Models.ProjectionTotal
            {
                ProjectionTotalsTypeId = Constants.ProjectionTotalsTypeId.Rebounds,
                ProjectionValue = Convert.ToDecimal(reboundsInfo[0], CultureInfo.InvariantCulture)
            });

            player.Projections.Add(new Data.Models.Projection
            {
                ProjectionTypeId = Constants.ProjectionTypeId.Rebounds,
                ProjectionValue = Convert.ToDecimal(reboundsInfo[1], CultureInfo.InvariantCulture)
            });

            var pointsInfo = row.Points.Split(" ");

            player.ProjectionTotals.Add(new Data.Models.ProjectionTotal
            {
                ProjectionTotalsTypeId = Constants.ProjectionTotalsTypeId.Points,
                ProjectionValue = Convert.ToDecimal(pointsInfo[0], CultureInfo.InvariantCulture)
            });

            player.Projections.Add(new Data.Models.Projection
            {
                ProjectionTypeId = Constants.ProjectionTypeId.Points,
                ProjectionValue = Convert.ToDecimal(pointsInfo[1], CultureInfo.InvariantCulture)
            });

            var threePointersInfo = row.ThreePointersMade.Split(" ");

            player.ProjectionTotals.Add(new Data.Models.ProjectionTotal
            {
                ProjectionTotalsTypeId = Constants.ProjectionTotalsTypeId.ThreePointersMade,
                ProjectionValue = Convert.ToDecimal(threePointersInfo[0], CultureInfo.InvariantCulture)
            });

            player.Projections.Add(new Data.Models.Projection
            {
                ProjectionTypeId = Constants.ProjectionTypeId.ThreePointersMade,
                ProjectionValue = Convert.ToDecimal(threePointersInfo[1], CultureInfo.InvariantCulture)
            });

            var freeThrowsInfo = row.FreeThrowPercentage.Split(" ");

            var freeThrowAttemptsInfo = freeThrowsInfo[1].Split("/");
            var freeThrowsMade = freeThrowAttemptsInfo[0].Split("(")[1];
            var freeThrowsAttempted = freeThrowAttemptsInfo[1].Split(")")[0];

            player.ProjectionTotals.Add(new Data.Models.ProjectionTotal
            {
                ProjectionTotalsTypeId = Constants.ProjectionTotalsTypeId.FreeThrowsAttempted,
                ProjectionValue = Convert.ToDecimal(freeThrowsAttempted, CultureInfo.InvariantCulture)
            });

            player.ProjectionTotals.Add(new Data.Models.ProjectionTotal
            {
                ProjectionTotalsTypeId = Constants.ProjectionTotalsTypeId.FreeThrowsMade,
                ProjectionValue = Convert.ToDecimal(freeThrowsMade, CultureInfo.InvariantCulture)
            });

            player.Projections.Add(new Data.Models.Projection
            {
                ProjectionTypeId = Constants.ProjectionTypeId.FreeThrowPercentage,
                ProjectionValue = Convert.ToDecimal(freeThrowsInfo[2], CultureInfo.InvariantCulture)
            });

            var fieldgoalsInfo = row.FieldGoalPercentage.Split(" ");

            var fieldgoalAttemptsInfo = fieldgoalsInfo[1].Split("/");
            var fieldgoalsMade = fieldgoalAttemptsInfo[0].Split("(")[1];
            var fieldgoalsAttempted = fieldgoalAttemptsInfo[1].Split(")")[0];

            player.ProjectionTotals.Add(new Data.Models.ProjectionTotal
            {
                ProjectionTotalsTypeId = Constants.ProjectionTotalsTypeId.FieldGoalsAttempted,
                ProjectionValue = Convert.ToDecimal(fieldgoalsAttempted, CultureInfo.InvariantCulture)
            });

            player.ProjectionTotals.Add(new Data.Models.ProjectionTotal
            {
                ProjectionTotalsTypeId = Constants.ProjectionTotalsTypeId.FieldGoalsMade,
                ProjectionValue = Convert.ToDecimal(fieldgoalsMade, CultureInfo.InvariantCulture)
            });

            player.Projections.Add(new Data.Models.Projection
            {
                ProjectionTypeId = Constants.ProjectionTypeId.FieldGoalPercentage,
                ProjectionValue = Convert.ToDecimal(fieldgoalsInfo[2], CultureInfo.InvariantCulture)
            });

            return player;
        }
    }
}
