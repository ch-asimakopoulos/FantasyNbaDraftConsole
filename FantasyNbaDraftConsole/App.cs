
namespace FantasyNbaDraftConsole
{
    using System;
    using System.Linq;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.VisualBasic;

    /// <summary>
    /// 
    /// </summary>
    public class App 
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IOperationsService operations_;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operations"></param>
        public App(IOperationsService operations)
        {
            operations_ = operations;
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

                configCmd.Command("positions", pCmd =>
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

                configCmd.Command("star_players", starPlayersCmd =>
                {
                    starPlayersCmd.Description = "Set player name you wish to star.";
                    var name = starPlayersCmd.Argument<string>("player", "Player to star").IsRequired();

                    starPlayersCmd.OnExecute(() =>
                    {
                        var more = false;
                        var playerName = name.Value;
                        do
                        {
                            var success = operations_.StarPlayer(playerName);

                            if (success)
                            {
                                Console.WriteLine($"Player Starred: {playerName}");
                            }

                            more = Prompt.GetYesNo("Wanna star more players?", true);

                            if (more)
                            {
                                Console.WriteLine("Type player name you wish to star: ");
                                playerName = Console.ReadLine();
                            }

                        } while (more);

                        return;
                    });
                });

                configCmd.Command("best", starPlayersCmd =>
                {
                    starPlayersCmd.Description = "Number of players you wish to search for: ";
                    var number = starPlayersCmd.Argument<int?>("number of players", "Players to search for");

                    starPlayersCmd.OnExecute(() =>
                    {
                        var playersDto = operations_.GetBestAvailablePlayers(number.ParsedValue);
                       
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
