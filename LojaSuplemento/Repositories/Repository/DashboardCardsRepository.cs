using LojaSuplemento.Models.Dashboard;
using LojaSuplemento.Repositories.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LojaSuplemento.Repositories.Repository
{
    public class DashboardCardsRepository : IDashboardCardsRepository
    {

        private readonly IConfiguration _configuration;

        public DashboardCardsRepository(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public DashboardCardsResponse GetDashboardCards()
        {
            DashboardCardsResponse dashboardCardsResponse = new DashboardCardsResponse();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("stp_DashboardCards_Sel", con)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 60
                };

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    dashboardCardsResponse.Estoque = Convert.ToInt32(rdr["Estoque"]);
                    dashboardCardsResponse.VendasDia = Convert.ToDecimal(rdr["VendasDia"]);
                    dashboardCardsResponse.VendasMes = Convert.ToDecimal(rdr["VendasMes"]);
                    dashboardCardsResponse.VendasAno = Convert.ToDecimal(rdr["VendasAno"]);
                }

                con.Close();
            }
            return dashboardCardsResponse;
        }
    }
}
