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
        private IConfiguration config_ { get; set; }

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

            config_ = builder.Build();
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

            modelBuilder.Entity<Position>()
                .HasKey(p => p.PositionId);

            modelBuilder.Entity<Position>()
                .Property(p => p.PositionId)
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
                .HasMany<Position>();

            modelBuilder.Entity<Player>()
                .HasMany(p => p.Projections)
                .WithOne(pr => pr.Player)
                .HasForeignKey(pr => pr.PlayerId);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.ProjectionTotals)
                .WithOne(pr => pr.Player)
                .HasForeignKey(pr => pr.PlayerId);

            modelBuilder.Entity<DraftConfig>()
                .HasKey(dc => dc.LeagueId);

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

            modelBuilder.HasPostgresEnum<Constants.ProjectionType>();
            modelBuilder.HasPostgresEnum<Constants.ProjectionTotalsType>();

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        //
        /// </summary>
        /// <param name="opts"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder opts) =>  
            opts.UseNpgsql(config_.GetConnectionString("DefaultConnection"));
    }
}
