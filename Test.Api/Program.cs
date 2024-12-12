
using SwaggerThemes;
using Test.Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddCors(options => 
{
    options.AddPolicy("San-Test-Api", policy => 
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(Theme.OneDark);
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("San-Test-Api");
app.Run();
