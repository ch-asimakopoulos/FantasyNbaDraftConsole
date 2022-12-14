namespace FantasyNbaDraftConsole.Data
{
    using FantasyNbaDraftConsole.Data.Models;

    using System.IO;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// 
    /// </summary>
    public class NBADbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        public DbSet<Team> Teams { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<League> Leagues { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Player> Players { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private IConfiguration Config_ { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Position> Positions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<ProjectionTotal> ProjectionTotals { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public NBADbContext()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Config_ = builder.Build();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<League>()
                .HasKey(l => l.LeagueId);

            modelBuilder.Entity<League>()
                .Property(l => l.LeagueId)
                .UseIdentityAlwaysColumn();

            modelBuilder.Entity<Player>()
                .HasKey(p => p.PlayerId);

            modelBuilder.Entity<Player>()
                .Property(p => p.PlayerId)
                .UseIdentityAlwaysColumn();

            modelBuilder.Entity<Team>()
                .HasKey(t => t.TeamId);

            modelBuilder.Entity<Team>()
                .Property(t => t.TeamId)
                .UseIdentityAlwaysColumn();

            modelBuilder.Entity<League>()
                .HasMany(l => l.LeagueTeams)
                .WithOne(t => t.League)
                .HasForeignKey(t => t.LeagueId);

            modelBuilder.Entity<League>()
                .HasOne(l => l.DraftConfig)
                .WithOne(dc => dc.League)
                .HasForeignKey<DraftConfig>(dc => dc.LeagueId);

            modelBuilder.Entity<Team>()
                .HasMany(t => t.DraftedPlayers)
                .WithOne(p => p.Team)
                .HasForeignKey(p => p.TeamId);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.Positions);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.Projections);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.ProjectionTotals);

            modelBuilder.Entity<Player>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<Team>()
                .HasIndex(t => t.Name)
                .IsUnique();

            modelBuilder.Entity<Player>()
                .HasIndex(p => p.PickNumber)
                .IsUnique();

            modelBuilder.Entity<Team>()
                .HasIndex(t => t.DraftPosition)
                .IsUnique();

            modelBuilder.Entity<DraftConfig>()
                .HasKey(dc => dc.LeagueId);

            modelBuilder.Entity<Position>()
                .HasKey(pr => new
                {
                    pr.PlayerId,
                    pr.PositionTypeId
                });

            modelBuilder.Entity<Projection>()
                .HasKey(pr => new
                {
                    pr.PlayerId,
                    pr.ProjectionTypeId
                });

            modelBuilder.Entity<ProjectionTotal>()
                .HasKey(pr => new
                {
                    pr.PlayerId,
                    pr.ProjectionTotalsTypeId
                });

            modelBuilder.HasPostgresEnum<Constants.PositionTypeId>();
            modelBuilder.HasPostgresEnum<Constants.ProjectionTypeId>();
            modelBuilder.HasPostgresEnum<Constants.ProjectionTotalsTypeId>();

            modelBuilder.HasDefaultSchema("public");

            modelBuilder.HasCollation("nba_collation", locale: "en-u-ks-primary", provider: "icu", deterministic: false);

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationBuilder"></param>
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().UseCollation("nba_collation");
        }

        /// <summary>
        //
        /// </summary>
        /// <param name="opts"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder opts) =>  
            opts.UseNpgsql(Config_.GetConnectionString("DefaultConnection"));
    }
}
