using System.Xml;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

//GetYearJson();

string jsonString = await File.ReadAllTextAsync(DateTime.Now.Year + ".json");

// Deserialize the JSON string to a list of dictionaries
var list = JsonConvert.DeserializeObject<List<EventGroup>>(jsonString);

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
    var id = list
        .Where(x => x.Id == dayOfYear)
        .Select(x => x.GitHubFileId)
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

    var list = new List<EventGroup>();
    //var list = new List<Dictionary<int, string>>();
    for (int i = 1; i <= totalDays; i++)
    {
        //    list.Add(new Dictionary<int, string>
        //{
        //    { i, Ulid.NewUlid().ToString() }
        //});

        list.Add(new EventGroup
        {
            Id = i,
            GitHubFileId = Ulid.NewUlid().ToString(),
            Events = new List<Event>()
        });
    }
    string json = JsonConvert.SerializeObject(list, Newtonsoft.Json.Formatting.Indented);
    Console.WriteLine(json);
    Console.ReadLine();
}


public class EventGroup
{
    public int Id { get; set; }
    public string GitHubFileId { get; set; }
    public List<Event> Events { get; set; }
}

public class Event
{
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}