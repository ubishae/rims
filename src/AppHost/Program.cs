var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql");

var api = builder.AddProject<Projects.RIMS_API>("api")
    .WithReference(sql)
    .WaitFor(sql);

builder.AddBunApp("web", "../web", "dev")
    .WithBunPackageInstallation()
    .WithReference(api)
    .WaitFor(api)
    .WithEnvironment("BROWSER", "none")
    .WithEnvironment("VITE_API_URL", api.GetEndpoint("api"))
    .WithHttpEndpoint(env: "VITE_PORT")
    .PublishAsDockerFile();

builder.Build().Run();
