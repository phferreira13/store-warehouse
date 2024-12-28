using warehouse.service.api.Ioc;
using warehouse.service.business.UseCases.Items;
using warehouse.service.entityframework.Bootstrapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
    {
        c.SwaggerDoc("v1", new() { Title = "warehouse.service.api", Version = "v1" });
    }
);

builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(GetItemsQuery).Assembly));


builder.Services.AddRepositories();

builder.Services.AddEntityFramework(builder.Configuration);

var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
//if (app.Environment.IsDevelopment())
//{
//}

app.Services.ApplyMigration();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
