using Domain;

static class Calculator
{
    public static int ComputeDistanceForWaitTime(int raceTime, int waitTime) =>
        (raceTime - waitTime) * waitTime;

    public static int ComputeNumberOfWaysToBeatRecord(RaceRecord raceRecord) =>
        Enumerable.Range(1, raceRecord.Time - 1)
            .Select(waitTime => ComputeDistanceForWaitTime(raceRecord.Time, waitTime))
            .Where(distance => distance > raceRecord.Distance)
            .Count();
}
