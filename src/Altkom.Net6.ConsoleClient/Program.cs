using Altkom.Net6.ConsoleClient;

Console.WriteLine("Hello, .NET6!");

Helper.DoWork("Hello", 1);

Helper.DoWork("Hello", "World", id: 1);

// Nazwane parametry (named parameters)
Helper.Search(longitude: 18.01, latitude: 52.01);