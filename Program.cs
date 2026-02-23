using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();






// ... (после builder.Services.AddEndpointsApiExplorer())

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


// Swagger + Bearer JWT
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter token ONLY."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// JWT
 app.UseAuthentication(); // who you are
app.UseAuthorization(); // can you сome here 

app.MapControllers();
app.Run();


/* Mål, ditt API ska vara “användbart för andra”, inte bara för dig

Krav
Swagger JWT Auth funkar
Lägg till Bearer auth i Swagger

Testa minst 2 skyddade endpoints via Swagger

Dokumentera 6 endpoints snyggt
Varje endpoint ska ha summary + response codes

Visa exempel på request body för minst 2 POST

Standardisera fel
Använd ProblemDetails eller egen Error DTO

Minst dessa cases ska ge tydliga svar

400 vid valideringsfel

401 utan token

403 fel roll, eller saknar access

409 conflict, t ex dubbel enrollment

Leverans
README sektion “API Usage”
Screenshot i repo som visar Swagger med Authorize + ett lyckat protected call
1 kort text, “3 vanligaste fel jag löste idag” */
