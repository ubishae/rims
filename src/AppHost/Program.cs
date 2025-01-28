var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql");

builder.AddProject<Projects.RIMS_API>("api")
    .WithReference(sql)
    .WaitFor(sql);

builder.Build().Run();
