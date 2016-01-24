using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using PrincessAPI.Infrastructure;
using PrincessAPI.Models;
using PrincessAPI.Utility.Hash;

namespace PrincessAPI.Utility.CSVParser
{
    public class CSVHouseParser
    {
        public static void UpdateDatabase(string csv_file_path)
        {
            var data = getCSVContent(csv_file_path);
            var houseModel = GetHouseModel(data);

            using (var db = new SystemDBContext())
            {
                var dbHouses = db.Houses.Select(x => x.Hash).ToList();

                foreach (var house in houseModel.Where(house => !dbHouses.Contains(house.Hash)))
                {
                    db.Houses.Add(house);
                }
                db.SaveChanges();
            }
        }

        private static DataTable getCSVContent(string csv_file_path)
        {
            var csvData = new DataTable();

            try
            {
                using (var csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(",");
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    var colFields = csvReader.ReadFields();

                    if (colFields == null)
                        return null;

                    foreach (var datecolumn in colFields.Select(column => new DataColumn(column)))
                    {
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }

                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();

                        // Continue to next set if null
                        if (fieldData == null) continue;

                        //Making empty value as null
                        for (var i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }

                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return csvData;
        }

        private static List<HouseModel> GetHouseModel(DataTable data)
        {
            var addressIndex = data.Columns.IndexOf("address");
            var latIndex = data.Columns.IndexOf("lat");
            var lonIndex = data.Columns.IndexOf("long");

            var monthSoldIndex = data.Columns.IndexOf("month_sold");
            var yearSoldIndex = data.Columns.IndexOf("year_sold");

            var finalAmountIndex = data.Columns.IndexOf("final_amount");
            var askingAmountIndex = data.Columns.IndexOf("asking_price");

            // list to insert
            var insertList = new List<HouseModel>();

            foreach (DataRow row in data.Rows)
            {
                string address = "", lon = "", lat = "", monthSold = "", yearSold = "", finalAmount = "", askingAmount = "";

                if (addressIndex >= 0)
                    address = row.ItemArray[addressIndex].ToString();
                if (latIndex >= 0)
                    lat = row.ItemArray[latIndex].ToString();
                if (lonIndex >= 0)
                    lon = row.ItemArray[lonIndex].ToString();
                if (monthSoldIndex >= 0)
                    monthSold = row.ItemArray[monthSoldIndex].ToString();
                if (yearSoldIndex >= 0)
                    yearSold = row.ItemArray[yearSoldIndex].ToString();
                if (finalAmountIndex >= 0)
                    finalAmount = row.ItemArray[finalAmountIndex].ToString();
                if (askingAmountIndex >= 0)
                    askingAmount = row.ItemArray[askingAmountIndex].ToString();

                // Ask google map API 
                if ((lat == "" || lon == "")&& address !="")
                {
                    var lonlat = API.Google.GeoCoding.GetAddressLonLat(address);
                }

                var newHouse = new HouseModel()
                {
                    Address = address,
                    Lat = lat,
                    Lon = lon,
                    MonthSold = monthSold,
                    YearSold = yearSold,
                    FinalAmount = finalAmount,
                    AskingAmount = askingAmount,
                    Hash = SHA1Hash.GetSHA1HashData(address + monthSold + yearSold)
                };

                insertList.Add(newHouse);
            }

            return insertList;
        } 
    }
}