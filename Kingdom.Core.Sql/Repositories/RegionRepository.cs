using Kingdom.Common.Utils;
using Kingdom.Core.Enums.Tiles;
using Kingdom.Core.Interfaces.Entities;
using Kingdom.Core.Interfaces.Repositories;
using Kingdom.Entities;
using Kingdom.Entities.Tiles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdom.Core.Sql.Repositories
{
    internal class RegionRepository : IRegionRepository
    {
        private ITileRepository _tileRepository;

        private string _connectionString;

        public RegionRepository(ITileRepository tileRepository)
        {
            this._tileRepository = tileRepository;
            this._connectionString = ConfigurationManager.ConnectionStrings["kingdom"].ToString();
        }

        public IRegion GetRegion(int regionId)
        {
            Region region = null;

            string sql = @"Select * From Regions Where Id = @RegionId;";

            using (SqlConnection conn = new SqlConnection(this._connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@RegionId", regionId));
                    conn.Open();

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int x = DbUtil.GetDbValue<int>(reader, "Row");
                            int y = DbUtil.GetDbValue<int>(reader, "Col");

                            region = new Region(x, y);
                            region.Id = DbUtil.GetDbValue<int>(reader, "Id");
                        }
                    }
                }
            }

            region.Tiles = this._tileRepository.GetTiles(region);

            return region;
        }

        public IRegion GetRegion(int x, int y)
        {
            Region region = null;

            string sql = @"Select * From Regions Where Row = @X And Col = @Y;";

            using (SqlConnection conn = new SqlConnection(this._connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@X", x));
                    cmd.Parameters.Add(new SqlParameter("@Y", y));
                    conn.Open();

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            region = new Region(x, y);
                            region.Id = DbUtil.GetDbValue<int>(reader, "Id");
                        }
                    }
                }
            }

            region.Tiles = this._tileRepository.GetTiles(region);

            return region;
        }

        public IList<IRegion> GetRegions()
        {
            IList<Region> regions = new List<Region>();

            string sql = @"Select * From Kingdom.Regions";

            using (SqlConnection conn = new SqlConnection(this._connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int x = DbUtil.GetDbValue<int>(reader, "Row");
                            int y = DbUtil.GetDbValue<int>(reader, "Col");

                            Region region = new Region(x, y);
                            region.Id = DbUtil.GetDbValue<int>(reader, "Id");

                            regions.Add(region);
                        }
                    }
                }
            }

            foreach (Region region in regions)
            {
                region.Tiles = this._tileRepository.GetTiles(region);
            }

            return regions.Select(o => o as IRegion).ToList();
        }

        public IRegion SaveRegion(IRegion region)
        {
            string sql = string.Empty;
            if (region.Id == 0)
            {
                sql = "Insert Into Kingdom.Regions ([Row], [Col]) Values (@Row, @Col); Select SCOPE_IDENTITY() as RegionId";
            }
            else
            {
                sql = "Update Kingdom.Regions Set Row = @Row, Col = @Col Where Id = @RegionId ";
            }

            using (var conn = new SqlConnection(this._connectionString))
            {
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@Row", region.Position.X));
                    cmd.Parameters.Add(new SqlParameter("@Col", region.Position.Y));

                    if (region.Id != 0)
                    {
                        cmd.Parameters.Add(new SqlParameter("@RegionId", region.Id));
                    }

                    conn.Open();

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            (region as Region).Id = DbUtil.GetDbValue<int>(reader, "RegionId");
                        }
                    }
                }
            }

            return region;
        }


        public IList<IRegion> GetRegions(IPosition topLeft, IPosition topRight, IPosition bottomLeft, IPosition bottomRight, IList<int> exclude)
        {
            IList<Region> regions = new List<Region>();

            string sql = string.Format(@"Select * From Kingdom.Regions Where 
                            (Row >= @TopLeftX And Col >= @TopLeftY And
                            Row <= @BottomRightX And Col <= @BottomRightY)
                            
Or

                            (Row <= @TopRightX And Col >= @TopRightY And
                            Row >= @BottomLeftX And Col <= @BottomLeftY)
And Id Not In ({0})

", string.Join(",", exclude));


            using (SqlConnection conn = new SqlConnection(this._connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@TopLeftX", topLeft.X));
                    cmd.Parameters.Add(new SqlParameter("@TopLeftY", topLeft.Y));
                    cmd.Parameters.Add(new SqlParameter("@TopRightX", topRight.X));
                    cmd.Parameters.Add(new SqlParameter("@TopRightY", topRight.Y));
                    cmd.Parameters.Add(new SqlParameter("@BottomLeftX", bottomLeft.X));
                    cmd.Parameters.Add(new SqlParameter("@BottomLeftY", bottomLeft.Y));
                    cmd.Parameters.Add(new SqlParameter("@BottomRightX", bottomRight.X));
                    cmd.Parameters.Add(new SqlParameter("@BottomRightY", bottomRight.Y));

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int x = DbUtil.GetDbValue<int>(reader, "Row");
                            int y = DbUtil.GetDbValue<int>(reader, "Col");

                            Region region = new Region(x, y);
                            region.Id = DbUtil.GetDbValue<int>(reader, "Id");

                            regions.Add(region);
                        }
                    }
                }
            }

            foreach (Region region in regions)
            {
                region.Tiles = this._tileRepository.GetTiles(region);
            }

            return regions.Select(o => o as IRegion).ToList();
        }


        public IList<IRegion> GetRegions(int minX, int maxX, int minY, int maxY, IList<int> exclude)
        {
            IList<Region> regions = new List<Region>();

            string sql = string.Format(@"Select * From Kingdom.Regions Where Row >= @MinX And Row <= @MaxX And Col >= @MinY And Col <= @MaxY And Id Not In ({0})", string.Join(",", exclude));


            using (SqlConnection conn = new SqlConnection(this._connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@MinX", minX));
                    cmd.Parameters.Add(new SqlParameter("@MaxX", maxX));
                    cmd.Parameters.Add(new SqlParameter("@MinY", minY));
                    cmd.Parameters.Add(new SqlParameter("@MaxY", maxY));

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int x = DbUtil.GetDbValue<int>(reader, "Row");
                            int y = DbUtil.GetDbValue<int>(reader, "Col");

                            Region region = new Region(x, y);
                            region.Id = DbUtil.GetDbValue<int>(reader, "Id");

                            regions.Add(region);
                        }
                    }
                }
            }

            foreach (Region region in regions)
            {
                region.Tiles = this._tileRepository.GetTiles(region);
            }

            return regions.Select(o => o as IRegion).ToList();
        }
    }
}
