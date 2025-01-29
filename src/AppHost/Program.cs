var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql");

var api = builder.AddProject<Projects.RIMS_API>("api")
    .WithReference(sql)
    .WaitFor(sql);

builder.AddBunApp("web", "../web", "dev")
    .WithBunPackageInstallation()
    .WithReference(api)
    .WaitFor(api);

builder.Build().Run();
