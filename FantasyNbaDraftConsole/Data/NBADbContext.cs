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
                .HasIndex(p => p.Name);

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

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        //
        /// </summary>
        /// <param name="opts"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder opts) =>  
            opts.UseNpgsql(Config_.GetConnectionString("DefaultConnection"));
    }
}
