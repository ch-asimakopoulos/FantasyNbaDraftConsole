
namespace FantasyNbaDraftConsole
{
    using System;
    using System.Text.Json;
    using System.Collections.Generic;

    using McMaster.Extensions.CommandLineUtils;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public class App 
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IHelperMapper helperMapper_;

        /// <summary>
        /// 
        /// </summary>
        private readonly IOperationsService operations_;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helperMapper"></param>
        /// <param name="operations"></param>
        public App(IHelperMapper helperMapper,
            IOperationsService operations)
        {
            operations_ = operations;
            helperMapper_ = helperMapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public int Run(string[] args)
        {

            var app = new CommandLineApplication
            {
                Name = "fantasy-nba-draft",
                Description = "Welcome to your fantasy league's 2k23 Nba Draft",
            };

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            app.HelpOption(inherited: true);
            app.Command("database", configCmd =>
            {
                configCmd.OnExecute(() =>
                {
                    Console.WriteLine("Specify a subcommand");
                    configCmd.ShowHelp();
                    return 1;
                });

                configCmd.Command("init", setCmd =>
                {
                    setCmd.OnExecute(() =>
                    {
                        Console.WriteLine("Initializing Database");

                        operations_.SeedDatabase();

                        Console.WriteLine("DB Seeded");

                        return;
                    });
                });

                configCmd.Command("import_players", importPlayersCmd =>
                {
                    importPlayersCmd.Description = "Set import csv file.";
                    var path = importPlayersCmd.Argument<string>("path", "csv Path").IsRequired();

                    importPlayersCmd.OnExecute(() =>
                    {
                        var successNumber = operations_.SeedPlayers(path.Value);

                        Console.WriteLine($"Players Seeded: {successNumber}");

                        return;
                    });
                });

                configCmd.Command("star_players", starPlayersCmd =>
                {
                    starPlayersCmd.OnExecute(() =>
                    {
                        var more = false;
                        do
                        {
                            Console.WriteLine("Type player name you wish to star: ");
                            var playerName = Console.ReadLine();

                            var success = operations_.StarPlayer(playerName);

                            if (success)
                            {
                                Console.WriteLine($"Player Starred: {playerName}");
                            }

                            more = Prompt.GetYesNo("Wanna star more players?", true);
                        } while (more);

                        return;
                    });
                });

            });

            app.HelpOption(inherited: true);
            app.Command("draft", configCmd =>
            {
                configCmd.OnExecute(() =>
                {
                    Console.WriteLine("Specify a subcommand");
                    configCmd.ShowHelp();
                    return 1;
                });

                configCmd.Command("teams", pCmd =>
                {
                    pCmd.OnExecute(() =>
                    {
                        Console.WriteLine("Specify a subcommand");
                        pCmd.ShowHelp();
                        return;
                    });

                    pCmd.Command("init_positions", ipCmd =>
                    {
                        ipCmd.OnExecute(() =>
                        {
                            Console.WriteLine("Initializing Draft Positions");

                            operations_.InitializeDraftPositions();

                            Console.WriteLine("Draft Positions Initialized");

                            return;
                        });
                    });

                    pCmd.Command("list_positions", lpCmd =>
                    {
                        lpCmd.OnExecute(() =>
                        {
                            Console.WriteLine("Listing Draft Positions");

                            operations_.ListDraftPositions();

                            Console.WriteLine("Draft Positions Listed");

                            return;
                        });
                    });
                });

                configCmd.Command("pick", pickPlayerCmd =>
                {
                    pickPlayerCmd.OnExecute(() =>
                    {
                        var draftOver = true;
                        short currentPick = 1;
                        var teamPicking = 0;
                        var currentRound = 1;
                        var currentRoundPicks = 0;

                        Console.WriteLine("Specify your league: ");
                        var leagueName = Console.ReadLine();

                        var draftInfo = operations_.GetDraftInfo(leagueName);

                        Console.WriteLine($"Welcome to {leagueName}'s annual draft. Let us begin: ");

                        do
                        {
                            Console.WriteLine($"Round {currentRound}, Team picking {draftInfo.TeamOrder[teamPicking].Item2}, Current Pick Overall {currentPick}, Enter Pick Name: ");
                            var playerName = Console.ReadLine();

                            var success = true;

                            do
                            {
                                success = operations_.PickPlayer(playerName, draftInfo.TeamOrder[teamPicking].Item1, currentPick);

                                if (success)
                                {
                                    currentPick++;
                                    teamPicking = draftInfo.Snake && (currentRound % 2 == 0) ? teamPicking - 1 : teamPicking + 1;

                                    if (++currentRoundPicks == draftInfo.TeamCount)
                                    {
                                        currentRound++;
                                        currentRoundPicks = 0;
                                        teamPicking = draftInfo.Snake && (currentRound % 2 == 0) ? draftInfo.TeamCount - 1 : 0;
                                    }
                                }
                            } while (!success);

                            var getBestInfo = Prompt.GetYesNo("Wanna get best available info?", true);

                            while(getBestInfo)
                            {
                                Console.WriteLine($"Limit number of results or press enter for default {Constants.DraftConstants.PlayersToReturn}");

                                var line = Console.ReadLine();
                                var toReturn = default(int?);

                                if(!int.TryParse(line, out var n))
                                {
                                    toReturn = Constants.DraftConstants.PlayersToReturn;
                                } else
                                {
                                    toReturn = n;
                                }

                                Console.WriteLine("Select Categories To Punt (comma seperated) or press enter");

                                var categoriesToPunt = Console.ReadLine().Split(",");

                                var playersDto = operations_.GetBestAvailablePlayers(toReturn, helperMapper_.MapToProjectionTypeId(categoriesToPunt));
                                
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;

                                foreach (var p in playersDto)
                                {
                                    Console.WriteLine($"{p.Name}, {(p.Starred ? '*' : ' ')} {string.Join("/", p.Positions)}, {p.Ranking}");
                                }

                                getBestInfo = Prompt.GetYesNo("Do you need any more best info?", true);

                            };

                            Console.ForegroundColor = ConsoleColor.DarkYellow;

                            var liveRankings = Prompt.GetYesNo("Wanna get live rankings?", true);

                            if (liveRankings)
                            {
                                var liveRankingList = operations_.GetLiveRankings();

                                Console.WriteLine(JsonSerializer.Serialize(liveRankingList));

                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine(JsonSerializer.Serialize(liveRankingList.Select(lr => new
                                {
                                    lr.TeamName,
                                    TeamTotals  = lr.ProjectionTypeRankings.Values.Sum()
                                }).ToList()));
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                            }
                            
                            if (currentRound < draftInfo.Rounds)
                            {
                                draftOver = false;
                            } else
                            {
                                draftOver = Prompt.GetYesNo("Is draft over?", true);
                            }

                        } while (!draftOver);

                        return;
                    });
                });

                configCmd.Command("best", bestPlayersCmd =>
                {
                    bestPlayersCmd.Description = "Number of players you wish to search for: ";
                    var number = bestPlayersCmd.Argument<int?>("number of players", "Players to search for");

                    bestPlayersCmd.OnExecute(() =>
                    {
                        var playersDto = operations_.GetBestAvailablePlayers(number.ParsedValue, new List<Constants.ProjectionTypeId>());
                       
                        foreach(var p in playersDto)
                        {
                            Console.WriteLine($"{p.Name}, {string.Join("/", p.Positions)}, {p.Ranking}");
                        }

                        return;
                    });
                });
            });

            app.OnExecute(() =>
            {
                Console.WriteLine("Specify a subcommand");
                app.ShowHelp();
                return 1;
            });

            return app.Execute(args);
        }
    }
}
