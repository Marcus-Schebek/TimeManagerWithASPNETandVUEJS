using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using api.Data;
using api.Repositories;
using api.Services;
using System.Text.Json.Serialization;

//Carrega as configurações da aplicação
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
//Configura o contexto do Banco de Dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configura a autenticação JWT
// Configura a autenticação baseada em tokens JWT. A chave usada para assinar os tokens é obtida
// das configurações, e várias opções de validação de token são definidas, como validar a chave de assinatura.
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; 
    options.SaveToken = true;            
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,    
        IssuerSigningKey = new SymmetricSecurityKey(key),  
        ValidateIssuer = false,            
        ValidateAudience = false            
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers()
    .AddJsonOptions(options => 
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

// Configura a injeção de dependência para os repositórios e serviços
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();
builder.Services.AddScoped<IWorkIntervalRepository, WorkIntervalRepository>();
builder.Services.AddScoped<AuthService>(sp => new AuthService(sp.GetRequiredService<IUserRepository>(), builder.Configuration["Jwt:Key"]));
builder.Services.AddScoped<TodoService>();
builder.Services.AddScoped<WorkIntervalService>();

var app = builder.Build();

// Configura o middleware para o ambiente de desenvolvimento
// Se o aplicativo estiver em modo de desenvolvimento, usa a página de exceção do desenvolvedor
// para mostrar detalhes de erros no navegador. Em produção, usa o manipulador de exceção padrão.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Adiciona os cabeçalhos HSTS para maior segurança em produção.
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => "Hello World!");

app.Run();