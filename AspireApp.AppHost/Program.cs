var builder = DistributedApplication.CreateBuilder(args);

// Add API service with the secret from AppHost configuration
var apiService = builder.AddProject<Projects.AspireApp_ApiService>("apiservice")
    .WithEnvironment("gh_key", builder.Configuration["gh_key"]); // Pass the secret as an environment variable

builder.AddProject<Projects.AspireApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
