using ProjectManager.Simulator;

var httpClient = new HttpClient();

var client = new ProjectManagerSimulatorClient("http://localhost:5147", httpClient);
var allEstimates = await client.GetAllEstimatesAsync(null);
foreach (var estimate in allEstimates)
{
    Console.WriteLine($"{estimate.EstimatedBy} - {estimate.EstimatedTimespan}");
}
