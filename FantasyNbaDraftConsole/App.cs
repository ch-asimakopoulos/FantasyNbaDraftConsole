
namespace FantasyNbaDraftConsole
{
    using System;

    using McMaster.Extensions.CommandLineUtils;
    
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
                    var path = importPlayersCmd.Argument("path", "csv Path").IsRequired();

                    importPlayersCmd.OnExecute(() =>
                    {
                        //operations_.SeedPlayers(path);

                        Console.WriteLine("Players Seeded");

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

                    pCmd.Command("init", ipCmd =>
                    {
                        ipCmd.OnExecute(() =>
                        {
                            Console.WriteLine("Initializing Draft Positions");

                            operations_.InitializeDraftPositions();

                            Console.WriteLine("Draft Positions Initialized");

                            return;
                        });
                    });

                    pCmd.Command("list", lpCmd =>
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
