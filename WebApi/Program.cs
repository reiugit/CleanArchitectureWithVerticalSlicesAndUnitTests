var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// For brevity here is no DI, no service implementations, no endpoints...
// This project is just for running the unit tests.
// (See sub-project "UnitTests" for the actual tests.)

app.Run();
