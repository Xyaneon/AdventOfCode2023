﻿using Domain;

using Extensions;

if (args.Length != 1)
{
    Console.Error.WriteLine($"Incorrect number of arguments (expected 1, got {args.Length})");
    Console.Error.WriteLine("Usage: dotnet Day5.exe input-file");
    return 1;
}

string[] lines;

try
{
    lines = File.ReadAllLines(args[0]);
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Failed to read input file: {ex.Message}");
    return 2;
}

Almanac almanac = AlmanacParser.Parse(lines);


OutputWriter.PrintAlmanac(almanac);

var lowestLocationNumber = long.MaxValue;
foreach (long seedNumber in almanac.SeedList)
{
    long soilNumber = almanac.Maps[MapKind.SeedToSoil].GetCorrespondingDestinationNumber(seedNumber);
    long fertilizerNumber = almanac.Maps[MapKind.SoilToFertilizer].GetCorrespondingDestinationNumber(soilNumber);
    long waterNumber = almanac.Maps[MapKind.FertilizerToWater].GetCorrespondingDestinationNumber(fertilizerNumber);
    long lightNumber = almanac.Maps[MapKind.WaterToLight].GetCorrespondingDestinationNumber(waterNumber);
    long temperatureNumber = almanac.Maps[MapKind.LightToTemperature].GetCorrespondingDestinationNumber(lightNumber);
    long humidityNumber = almanac.Maps[MapKind.TemperatureToHumidity].GetCorrespondingDestinationNumber(temperatureNumber);
    long locationNumber = almanac.Maps[MapKind.HumidityToLocation].GetCorrespondingDestinationNumber(humidityNumber);

    if (locationNumber < lowestLocationNumber)
    {
        lowestLocationNumber = locationNumber;
    }

    Console.WriteLine($"Seed {seedNumber}, soil {soilNumber}, fertilizer {fertilizerNumber}, water {waterNumber}, light {lightNumber}, temperature {temperatureNumber}, humidity {humidityNumber}, location {locationNumber}");
}

Console.WriteLine("---");
Console.WriteLine($"Lowest location number: {lowestLocationNumber}");

return 0;


static class OutputWriter
{
    public static void PrintAlmanac(Almanac almanac)
    {
        foreach (string line in almanac.ToLines())
        {
            Console.WriteLine(line);
        }
    }
}
