using Kingdom.Common.Constants;
using Kingdom.Common.Utils;
using Kingdom.Core.Enums.Tiles;
using Kingdom.Core.Interfaces.Entities;
using Kingdom.Core.Interfaces.Repositories;
using Kingdom.Entities;
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
    internal class TileRepository : ITileRepository
    {
        private string _connectionString;

        public TileRepository()
        {
            this._connectionString = ConfigurationManager.ConnectionStrings["kingdom"].ToString();
        }

        public ITile GetTile(int regionId, int x, int y)
        {
            string sql = @"Select * From Kingdom.Tiles Where RegionId = @RegionId And TileRow = @X And TileCol = @Y;";

            using (SqlConnection conn = new SqlConnection(this._connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@X", x));
                    cmd.Parameters.Add(new SqlParameter("@Y", y));
                    cmd.Parameters.Add(new SqlParameter("@RegionId", regionId));
                    conn.Open();

                    using (IDataReader reader = cmd.ExecuteReader())
                    {       
                        if (reader.Read())
                        {
                            return this.GetTile(reader);
                        }
                    }
                }
            }

            return null;
        }

        public ITile[,] GetTiles(IRegion region)
        {
            IList<ITile> tiles = new List<ITile>();

            string sql = @"Select * From Kingdom.Tiles Where RegionId = @RegionId;";

            using (SqlConnection conn = new SqlConnection(this._connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@RegionId", region.Id));
                    conn.Open();

                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            tiles.Add(this.GetTile(dr));
                        }
                    }
                }
            }

            int size = (int)Math.Sqrt(tiles.Count);

            ITile[,] tileArray = new ITile[size, size];

            foreach (ITile tile in tiles)
            {
                tileArray[tile.Position.X, tile.Position.Y] = tile;

                int x = (tile.Position.X + region.Position.X * TileConstants.RegionSize);
                int y = (tile.Position.Y + region.Position.Y * TileConstants.RegionSize);

                tile.Position.SetPosition(x, y);
            }

            return tileArray;
        }

        private ITile GetTile(IDataReader reader)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings() { TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All };

            TileType tileType = DbUtil.GetDbEnum<TileType>(reader, "Type", TileType.Grass);
            int id = DbUtil.GetDbValue<int>(reader, "Id");
            int x = DbUtil.GetDbValue<int>(reader, "Row");
            int y = DbUtil.GetDbValue<int>(reader, "Col");
            int regionId = DbUtil.GetDbValue<int>(reader, "RegionId");
            string data = DbUtil.GetDbValue<string>(reader, "Data");

            ITile tile = JsonConvert.DeserializeObject<ITile>(data, settings);
            tile.Id = id;
            tile.Position.SetPosition(x, y);

            return tile;
        }

        public void SaveTile(ITile tile)
        {
            string sql = string.Empty;
            if (tile.Id == 0)
            {
                sql = "Insert Into Kingdom.Tiles (RegionId, Row, Col, Type, Data) Values (@RegionId, @Row, @Col, @Type, @Data)";
            }
            else
            {
                sql = "Update Kingdom.Tiles Set Data = @Data, [Type] = @Type Where Id = @TileId";
            }

            JsonSerializerSettings settings = new JsonSerializerSettings() { TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All };

            using (var conn = new SqlConnection(this._connectionString))
            {
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@Row", tile.Position.X));
                    cmd.Parameters.Add(new SqlParameter("@Col", tile.Position.Y));
                    cmd.Parameters.Add(new SqlParameter("@RegionId", tile.RegionId));
                    cmd.Parameters.Add(new SqlParameter("@Type", tile.Type.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@Data", Newtonsoft.Json.JsonConvert.SerializeObject(tile, settings)));

                    if (tile.Id != 0)
                    {
                        cmd.Parameters.Add(new SqlParameter("@TileId", tile.Id));
                    }

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
