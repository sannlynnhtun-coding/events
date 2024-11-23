using System.Xml;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

string jsonString = await File.ReadAllTextAsync(DateTime.Now.Year + ".json");

// Deserialize the JSON string to a list of dictionaries
var list = JsonConvert.DeserializeObject<List<Dictionary<int, string>>>(jsonString);

bool isRunning = true;
while (isRunning)
{
    // Example input date string
    string inputDate = Console.ReadLine()!; // Replace this with your input date

    // Parse the input date
    DateTime date = DateTime.ParseExact(inputDate, "yyyy-MM-dd", null);

    // Calculate the total days from the start of the year
    int dayOfYear = date.DayOfYear;

    // Find the corresponding ID for the input date using LINQ
    var id = list.SelectMany(dict => dict)
        .Where(kv => kv.Key == dayOfYear)
        .Select(kv => kv.Value)
        .FirstOrDefault();

    var result = new
    {
        Date = inputDate,
        TotalDays = dayOfYear,
        GitHubFileId = id
    };

    // Serialize the result to JSON
    string json = JsonConvert.SerializeObject(result, Formatting.Indented);
    Console.WriteLine(json);
}

static void GetYearJson()
{
    int totalDays = DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365;
    var list = new List<Dictionary<int, string>>();
    for (int i = 1; i <= totalDays; i++)
    {
        list.Add(new Dictionary<int, string>
    {
        { i, Ulid.NewUlid().ToString() }
    });
    }
    string json = JsonConvert.SerializeObject(list, Newtonsoft.Json.Formatting.Indented);
    Console.WriteLine(json);
    Console.ReadLine();
}