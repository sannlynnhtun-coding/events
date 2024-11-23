using System.Xml;
using Newtonsoft.Json;

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