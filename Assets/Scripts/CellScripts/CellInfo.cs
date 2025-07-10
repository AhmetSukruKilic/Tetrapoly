using System;
using UnityEngine;

[System.Serializable]
public class CellInfo
{
    private const Car noOwner = null;
    internal string name;
    internal int price;
    internal Car owner;
    internal Color color;
    internal DistrictType districtType;

    public CellInfo(string name, int price, Car owner, Color color, DistrictType districtType)
    {
        this.name = name;
        this.price = price;
        this.owner = owner;
        this.color = color;
        this.districtType = districtType;
    }

    public static CellInfo[] InitializeCellInfos()
    {
        CellInfo[] cells = new CellInfo[64]; // 8x8 grid

        cells[0] = new CellInfo("Base", 0, noOwner, Color.white, DistrictType.Base);      // bottom-left
        cells[13] = new CellInfo("Jail", 0, noOwner, Color.gray, DistrictType.Jail);       // bottom-right
        cells[50] = new CellInfo("Jail", 0, noOwner, Color.gray, DistrictType.Jail);      // top-left
        cells[63] = new CellInfo("Base", 0, noOwner, Color.white, DistrictType.Base);     // top-right

        

        var cityGroups = new (string[] cities, Color color)[]
        {
            (new[] { "New York", "Chicago", "Philadelphia" }, new Color(0.3f, 0.3f, 0.9f)), // 3
            (new[] { "Los Angeles", "San Diego", "San Francisco", "Houston" }, new Color(0.9f, 0.3f, 0.3f)), // 4

            (new[] { "London", "Manchester", "Birmingham", "Liverpool" }, new Color(0.1f, 0.6f, 0.1f)),
            (new[] { "Ankara", "Izmir", "Istanbul" }, new Color(0.8f, 0.5f, 0.1f)),
            
            (new[] { "Berlin", "Munich", "Hamburg", "Frankfurt" }, new Color(0.6f, 0.2f, 0.2f)),
            (new[] { "Paris", "Marseille", "Lyon", "Nice" }, new Color(0.5f, 0.2f, 0.7f)),

            (new[] { "Madrid", "Barcelona", "Valencia", "Seville" }, new Color(0.7f, 0.7f, 0.2f)),
            (new[] { "Tokyo", "Osaka", "Nagoya", "Fukuoka" }, new Color(0.3f, 0.8f, 0.8f)),

            (new[] { "Cairo", "Nairobi", "Lagos", "Accra" }, new Color(0.4f, 0.2f, 0.1f)),
            (new[] { "Toronto", "Vancouver", "Calgary", "Montreal" }, new Color(0.6f, 0.6f, 0.6f)),

            (new[] { "Dubai", "Doha", "Riyadh", "Abu Dhabi" }, new Color(0.9f, 0.4f, 0.1f)),
            (new[] { "Buenos Aires", "Santiago", "Lima", "Bogota" }, new Color(0.9f, 0.9f, 0.2f)),

            (new[] { "Beijing", "Shanghai", "Shenzhen" }, new Color(0.1f, 0.8f, 0.3f)),
            (new[] { "Sydney", "Melbourne", "Brisbane", "Perth" }, new Color(0.2f, 0.4f, 0.6f)),

            (new[] { "Moscow", "Norilsk", "Kazan", "Sochi" }, new Color(0.5f, 0.1f, 0.5f)),
            (new[] { "Vienna", "Prague", "Budapest" }, new Color(0.7f, 0.3f, 0.9f))
        };


        int index = 1; // Start after base at index 0
        int price = 100;

        int row = 0;
        int col = 0;
        
        foreach (var group in cityGroups)
        {
            foreach (string city in group.cities)
            {
                if (index >= cells.Length - 1) break;

                if (index == 13 || index == 50) // Skip the jail cells
                    index++;
                

                int cityPrice = price + (row * 15) + (col * 15); // Example price calculation based on row and column

                cells[index] = new CellInfo(city, cityPrice, noOwner, group.color, DistrictType.City);
                index++;
                col++;
            }
            row++;
            col = 0; // Reset column for next group
        }
        

        return cells;
    }




}
