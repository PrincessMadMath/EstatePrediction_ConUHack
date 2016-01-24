using System;
using System.Collections.Generic;
using System.Linq;
using PrincessAPI.Infrastructure;
using PrincessAPI.Models;

namespace PrincessAPI.Utility.HouseModelQuery
{
    public class HouseModelQuery
    {
        public static List<HouseModel> GetHouseNear(HouseLocalizationRequest request)
        {
            using (var db = new SystemDBContext())
            {
                var dbHouses = db.Houses.ToList();
                var result = new List<HouseModel>();

                foreach (var house in dbHouses)
                {
                    if (house.Lat != "" && house.Lon != "")
                    {
                        var deltaLon = double.Parse(house.Lon) - request.lonStart;
                        var deltaLat = double.Parse(house.Lat) - request.latStart;

                        if(Math.Sqrt(Math.Pow(deltaLat,2) + Math.Pow(deltaLon, 2)) <= request.radius)
                            result.Add(house);
                    }
                }
                return result;
            }
        }

        public static List<HouseModel> GetAllHouses()
        {
            using (var db = new SystemDBContext())
            {
                return db.Houses.ToList();
            }
        }

        public static void InsertHouseModel(HouseModel model)
        {
            using (var db = new SystemDBContext())
            {
                db.Houses.Add(model);
                db.SaveChanges();
            }
        } 
    }
}