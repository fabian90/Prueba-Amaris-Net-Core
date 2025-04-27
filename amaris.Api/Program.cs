using amaris.Api.IoC;
using amaris.Infrastructure.Data;
using FluentValidation.AspNetCore;

var allowSpecificOrigins = "AllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
string[] lOrigins = builder.Configuration.GetValue<string>("Origins").Split(",");
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins(lOrigins);// Permitir cualquier origen
                          policy.AllowAnyMethod();// Permitir cualquier método (GET, POST, PUT, DELETE, etc.)
                          policy.AllowAnyHeader();// Permitir cualquier encabezado
                      });
});
// Add services to the container.
// Registrar los servicios
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<TransaccionRequestValidator>());

DependencyContainer.RegisterServices(builder.Services);
//Configure Automapper
builder.Services.AddScoped<DynamoDbContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FabianVargasTovar.Api v1"));
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(allowSpecificOrigins); // Aplicar la política CORS
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
