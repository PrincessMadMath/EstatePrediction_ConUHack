using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using PrincessAPI.Models;

namespace PrincessAPI.API.AzureEstatePrediction
{
    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; }
    }

    public class EstatePrediction
    {

        public static async Task<string> PredictWithHouseStats(HousePredictionModel model)
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>
                    {
                    {
                        "input1",
                        new StringTable
                        {
                            ColumnNames = new[]
                            {
                                "number_of_levels_basement_excl",
                                "living_space_area_basement_excl",
                                "sold_in",
                                "building_dimensions",
                                "number_of_bathrooms",
                                "total_number_of_rooms",
                                "number_of_bedrooms",
                                "lot_dimensions",
                                "year_of_construction",
                                "year_sold",
                                "sold_in (2)",
                                "asking_price",
                                "final_amount"
                            },
                            Values = new[,] {
                                {
                                    model.number_of_levels_basement_excl,
                                    model.living_space_area_basement_excl,
                                    model.sold_in,
                                    model.building_dimensions,
                                    model.number_of_bathrooms,
                                    model.total_number_of_rooms,
                                    model.number_of_bedrooms,
                                    model.lot_dimensions,
                                    model.year_of_construction,
                                    model.year_sold,
                                    model.sold_in,
                                    model.asking_price,
                                    model.final_amount
                                },  {"0","0","0","0","value","value","0","0","0","0","0","0","0" }  }
                        }
                    }
                },
                    GlobalParameters = new Dictionary<string, string>()
                };
                const string apiKey = "LnPVhn68tUpmnWSZxcKRPWWI3jwadmcZmchKMSFMMTdCor6fNWFbZOk9U3uGjphpOpURoR5zKayncy2G6apzpg==";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/b73bf374bbba41219bb7fb317bfd4048/services/01e28cb97ae0407e8db4560284a95837/execute?api-version=2.0&details=true");

                var response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false); ;

                if (response.IsSuccessStatusCode)
                {
                    // Sucess
                    return await response.Content.ReadAsStringAsync();
                }

                // Fail
                return await response.Content.ReadAsStringAsync();
            }
        }

        public static async Task<string> PredictWithHouseTags(HousePredictionTags model)
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[]
                                {
                                    "classic",
                                    "architecture",
                                    "comfort",
                                    "interior design",
                                    "modern",
                                    "barn",
                                    "parquet",
                                    "garage",
                                    "yard",
                                    "landscape",
                                    "fireplace",
                                    "patio",
                                    "villa",
                                    "scenic",
                                    "minimalist",
                                    "number_of_bathrooms",
                                    "hotel",
                                    "suburb",
                                    "porch",
                                    "environment",
                                    "swimming pool",
                                    "travel",
                                    "gazebo",
                                    "pool",
                                    "flora",
                                    "luxury",
                                    "contemporary",
                                    "mansion",
                                    "pavement",
                                    "old",
                                    "final_amount"
                                },
                                Values = new string[,]
                                {
                                    {
                                        Convert.ToInt32(model.classic).ToString(),
                                        Convert.ToInt32(model.architecture).ToString(),
                                        Convert.ToInt32(model.comfort).ToString(),
                                        Convert.ToInt32(model.interior_design).ToString(),
                                        Convert.ToInt32(model.modern).ToString(),
                                        Convert.ToInt32(model.barn).ToString(),
                                        Convert.ToInt32(model.parquet).ToString(),
                                        Convert.ToInt32(model.garage).ToString(),
                                        Convert.ToInt32(model.yard).ToString(),
                                        Convert.ToInt32(model.landscape).ToString(),
                                        Convert.ToInt32(model.fireplace).ToString(),
                                        Convert.ToInt32(model.patio).ToString(),
                                        Convert.ToInt32(model.villa).ToString(),
                                        Convert.ToInt32(model.scenic).ToString(),
                                        Convert.ToInt32(model.minimalist).ToString(),
                                        Convert.ToInt32(model.number_of_bathrooms).ToString(),
                                        Convert.ToInt32(model.hotel).ToString(),
                                        Convert.ToInt32(model.suburb).ToString(),
                                        Convert.ToInt32(model.porch).ToString(),
                                        Convert.ToInt32(model.environment).ToString(),
                                        Convert.ToInt32(model.swimming_pool).ToString(),
                                        Convert.ToInt32(model.travel).ToString(),
                                        Convert.ToInt32(model.gazebo).ToString(),
                                        Convert.ToInt32(model.pool).ToString(),
                                        Convert.ToInt32(model.flora).ToString(),
                                        Convert.ToInt32(model.luxury).ToString(),
                                        Convert.ToInt32(model.contemporary).ToString(),
                                        Convert.ToInt32(model.mansion).ToString(),
                                        Convert.ToInt32(model.pavement).ToString(),
                                        Convert.ToInt32(model.old).ToString(),
                                        Convert.ToInt32(model.final_amount).ToString()
                                    },  { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },
                                }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                const string apiKey = "trj+qROzUANxmGQcGOsT7Gzkre6WyQW4bvWHLai532x3uo0gkw42wO/Y6NxiI0/QBjmdmpclbty33KR0fKiu4A=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/b73bf374bbba41219bb7fb317bfd4048/services/93bad5a3a06c45e5beb4ccecd12fa98c/execute?api-version=2.0&details=true");

                var response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false); ;

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}