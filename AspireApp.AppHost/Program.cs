var builder = DistributedApplication.CreateBuilder(args);

// Get the secret from the AppHost's configuration
var secretTesti = builder.Configuration["SecretTesti"];

// Add API service with the secret from AppHost configuration
var apiService = builder.AddProject<Projects.AspireApp_ApiService>("apiservice")
    .WithEnvironment("SecretTesti", secretTesti); // Pass the secret as an environment variable

builder.AddProject<Projects.AspireApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
