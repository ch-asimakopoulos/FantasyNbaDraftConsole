using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FantasyNbaDraftConsole
{
    /// <summary>
    /// 
    /// </summary>
    public class HelperMapper : IHelperMapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        public IEnumerable<Data.Models.Position> MapToPositionModel(string[] positions)
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
        public Data.Models.Player MapRowToPlayerModel(Csv.ImportPlayers.ImportPlayersRow row)
        {
            var player = new Data.Models.Player();

            player.Name = row.Name;
            player.NbaTeam = row.NbaTeam;

            var positions = row.Positions.Split("/");
            player.Positions.AddRange(MapToPositionModel(positions));

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

        /// <summary>
        /// /
        /// </summary>
        /// <param name="projections"></param>
        /// <returns></returns>
        public ICollection<Constants.ProjectionTypeId> MapToProjectionTypeId(string[] projections)
        {
            var projectionTypeIds = new HashSet<Constants.ProjectionTypeId>();

            foreach (var p in projections)
            {
                switch (p)
                {
                    case "TO":
                        projectionTypeIds.Add(Constants.ProjectionTypeId.Turnovers);
                        break;
                    case "FT":
                        projectionTypeIds.Add(Constants.ProjectionTypeId.FreeThrowPercentage);
                        break;
                    case "PTS":
                        projectionTypeIds.Add(Constants.ProjectionTypeId.Points);
                        break;
                    case "3PM":
                        projectionTypeIds.Add(Constants.ProjectionTypeId.ThreePointersMade);
                        break;
                    case "REB":
                        projectionTypeIds.Add(Constants.ProjectionTypeId.Rebounds);
                        break;
                    case "AST":
                        projectionTypeIds.Add(Constants.ProjectionTypeId.Assists);
                        break;
                    case "BLK":
                        projectionTypeIds.Add(Constants.ProjectionTypeId.Blocks);
                        break;
                    case "STL":
                        projectionTypeIds.Add(Constants.ProjectionTypeId.Steals);
                        break;
                    case "FG":
                        projectionTypeIds.Add(Constants.ProjectionTypeId.FieldGoalPercentage);
                        break;
                    default:
                        break;
                }
            }

            return projectionTypeIds;
        }


    }
}
