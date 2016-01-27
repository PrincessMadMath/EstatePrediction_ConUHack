namespace PrincessAPI.Models
{
    public class FormHouseModel
    {
        public string address { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string numberOfBedrooms { get; set; }
        public string numberOfBathrooms { get; set; }
        public string numberOfRooms { get; set; }
        public string livingSpaceArea { get; set; }
        public string lotArea { get; set; }
        public string buildingDimensions { get; set; }
        public string yearOfConstruction { get; set; }

        public HousePredictionModel ToPredictionModel()
        {
            return new HousePredictionModel()
            {
                number_of_bedrooms = numberOfBedrooms,
                number_of_bathrooms = numberOfBathrooms,
                total_number_of_rooms = numberOfRooms,
                living_space_area_basement_excl = livingSpaceArea,
                lot_dimensions = lotArea,
                building_dimensions = buildingDimensions,
                year_of_construction = yearOfConstruction,
                lat = lat,
                lon = lon
            };
        }
    }

    public class FormCondoModel
    {
        public string address { get; set; }
        public string numberOfBedrooms { get; set; }
        public string numberOfBathrooms { get; set; }
        public string numberOfRooms { get; set; }
        public string numberOfLevels { get; set; }
        public string witchFloor { get; set; }
        public string interiorParking { get; set; }
        public string yearOfConstruction { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }

        public HousePredictionModel ToPredictionModel()
        {
            return new HousePredictionModel()
            {
                number_of_bedrooms = numberOfBedrooms,
                number_of_bathrooms = numberOfBathrooms,
                total_number_of_rooms = numberOfRooms,
                number_of_levels_basement_excl = numberOfLevels,
                located_on_which_floor_if_condo = witchFloor,
                number_of_interior_parking = interiorParking,
                year_of_construction = yearOfConstruction,
                lat = lat,
                lon = lon
            };
        }
    }
}