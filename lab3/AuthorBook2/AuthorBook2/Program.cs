using AuthorBook2.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var modelBuilder = new ODataConventionModelBuilder(); 
modelBuilder.EntitySet<Author>("Authors");
modelBuilder.EntitySet<Book>("Books");
modelBuilder.EntitySet<Loan>("Loans");
builder.Services.AddControllers().AddOData(options => options.Select().Filter().OrderBy().Expand().Count()
    .SetMaxTop(null).AddRouteComponents(
        "odata", modelBuilder.GetEdmModel())
    .EnableQueryFeatures());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PostgresContext>(options=> 
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();

// my odata endpoints
// /odata/Books?$filter=PublishedYear eq 2019
// /odata/Books?$apply=aggregate($count as Total)
// /odata/Loans?$filter=Returned eq false
// /odata/Loans?$filter=BorrowerId eq 2&$expand=Books($select=Title)
// /odata/Books?$filter=PublishedYear eq 2019&$orderby= Title asc
// /odata/Authors?$apply=groupby(BirthDate)
// /odata/Books?$top=2&$skip=1
