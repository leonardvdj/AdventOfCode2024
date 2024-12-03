var readings = File.ReadAllLines("input.txt")
    .Select(l => l.Split(' ').Select(int.Parse).ToList()).ToList();

var safeReadings = 0;

foreach (var reading in readings)
    if (TestReading(reading))
    {
        safeReadings++;
    }
    else
    {
        if (reading.Select((_, i) =>
            {
                var newList = new List<int>(reading);
                newList.RemoveAt(i);
                return TestReading(newList);
            }).Any(s => s)) safeReadings++;
    }

Console.WriteLine(safeReadings);

return;

bool TestReading(List<int> reading)
{
    var increasing = false;
    for (var i = 0; i < reading.Count - 1; i++)
    {
        int curr = reading[i], next = reading[i + 1];
        var nextInc = next > curr;
        var diff = Math.Abs(next - curr);
        if (i == 0) increasing = nextInc;
        if (nextInc != increasing || diff == 0 || diff > 3) return false;
    }

    return true;
}
