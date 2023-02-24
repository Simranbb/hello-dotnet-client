using LaunchDarkly.Sdk;
using LaunchDarkly.Sdk.Client;


do
{
    Console.WriteLine("\nDo you want to start/continue Y/N ?");
    string? answer = Console.ReadLine();
    if (answer == "N")
        break;
    if (answer == "Y")
    {

        Console.WriteLine("\nEnter User Role - OPG or TMA");
        string? role = Console.ReadLine();


        Console.WriteLine("\nEnter User Location - Banchory, Aberdeen, Belgrave");
        string? location = Console.ReadLine();

        var arrayValue = LdValue.BuildArray();
        arrayValue.Add(location);

        var context = Context.Builder("example-sim-key")
                       .Name("SimTest")
                       .Set("Locations", arrayValue.Build())
                       .Set("Role", role)
                       .Build();

        var timeSpan = TimeSpan.FromSeconds(10);
        LdClient client = LdClient.Init(
            "mob-d451d3a0-8735-4d72-9001-fba93ed1c903",
            context,
            timeSpan
        );

        if (client.Initialized)
        {
            // Console.WriteLine("SDK successfully initialized!");
            var flags = client.AllFlags();
        }
        else
        {
            Console.WriteLine("SDK failed to initialize");
            Environment.Exit(1);
        }

        var flagValue = client.BoolVariation("simTest", false);

        Console.WriteLine(string.Format("\nFeature flag 'simTest' is {0} for this context- SimTest with Role {1} and Location {2} ", flagValue, context.GetValue("Role"), context.GetValue("Locations").List[0]));

        // Here we ensure that the SDK shuts down cleanly and has a chance to deliver analytics
        // events to LaunchDarkly before the program exits. If analytics events are not delivered,
        // the context properties and flag usage statistics will not appear on your dashboard. In
        // a normal long-running application, the SDK would continue running and events would be
        // delivered automatically in the background.
        client.Dispose();
    }
} while (true);

