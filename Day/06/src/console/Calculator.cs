using Domain;

static class Calculator
{
    public static long ComputeDistanceForWaitTime(long raceTime, long waitTime) =>
        (raceTime - waitTime) * waitTime;

    public static long ComputeNumberOfWaysToBeatRecord(RaceRecord raceRecord) =>
        RangeOfLong(1, raceRecord.Time - 1)
            .Select(waitTime => ComputeDistanceForWaitTime(raceRecord.Time, waitTime))
            .Where(distance => distance > raceRecord.Distance)
            .Count();

    private static IEnumerable<long> RangeOfLong(long start, long count)
    {
        for (long i = start; i < start + count; i++)
        {
            yield return i;
        }
    }
}
