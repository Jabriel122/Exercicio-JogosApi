using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);




//Adcionar o servi�o de Jwt Bearer (forma de autentica��o)
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})

.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //Valida quem est� solicitando
        ValidateIssuer = true,

        //valida quem est� recebendo 
        ValidateAudience = true,

        //define se o tempo de expira��o ser� valido
        ValidateLifetime = true,

        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("inlock-chave-autenticacao-manha-dev-api")),

        //forma de criptografar e valida a chave de autentica��o
        ClockSkew = TimeSpan.FromMinutes(5),

        //nome da issuer (De onde est� vindo)
        ValidIssuer = "senai.inlock.webApi",

        //nome do audinece (para onde est� indo)
        ValidAudience = "senai.inlock.webApi"


    };

});

//Adcionar Swagger
builder.Services.AddSwaggerGen(options =>
{
    //Aqui adciona informa��es sobre a API no Swagger
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API JOGOS GABS (senai.inlock.webApi)",
        Description = "APi para gerenciamento de jogos - Introdu��o a Sprint 2 - Backend API",
        Contact = new OpenApiContact
        {
            Name = "Senai Inform�tica - Turma Manh� - Gabriel Marchetti",
            Url = new Uri("https://github.com/Jabriel122")
        },

        //Configure o Swagger para usar o arquivo XML gerado
        // using System.Reflection;
        // var xmlJogosname = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlJogosname));
});


  


    //Usando a autentica�ao no Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Value: Bearer TokenJWT ",
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
            new string[] {}
        }
    });


});

//adiciona o servico de controller
builder.Services.AddControllers();

var app = builder.Build();

//comeca a configuracaos do swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

//finaliza a configuracao do swagger

//adiciona mapeamento dos controllers
app.MapControllers();


//Adciona Autentica��o
app.UseAuthentication();

//adciona autoriza��o
app.UseAuthorization();

app.UseHttpsRedirection();

app.Run();


