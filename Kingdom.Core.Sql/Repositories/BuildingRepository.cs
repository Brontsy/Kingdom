using Kingdom.Common.Utils;
using Kingdom.Core.Enums.Buildings;
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
    internal class BuildingRepository : IBuildingRepository
    {
        private string _connectionString;

        public BuildingRepository()
        {
            this._connectionString = ConfigurationManager.ConnectionStrings["kingdom"].ToString();
        }

        public void Add(int regionId, int x, int y, BuildingType type)
        {
            string sql = @"Update Where RegionId = @RegionId;";

            using (SqlConnection conn = new SqlConnection(this._connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@RegionId", regionId));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
