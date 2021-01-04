using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using Banco_De_Vagas_NSG.Configurations;
using Banco_De_Vagas_NSG.Contexts;
using Banco_De_Vagas_NSG.Interfaces;
using Banco_De_Vagas_NSG.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Banco_De_Vagas_NSG
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{

			services.AddCors(cr => {
				cr.AddDefaultPolicy(bd =>
				{
					bd.WithOrigins("*").WithMethods("*").WithHeaders("*");
				});
			});

			services.AddControllers().AddNewtonsoftJson(a => {
				//a.SerializerSettings.Formatting = Formatting.Indented;
				//a.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
				a.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
				a.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
			}).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Banco_De_Vagas_NSG", Version = "v1.0" });
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});

			services.AddScoped<SmtpClient>(op =>
			{

				SmtpClient smtp = new SmtpClient()
				{
					Host = Configuration.GetValue<string>("Email:ServerSMTP"),
					Port = Configuration.GetValue<int>("Email:ServerPort"),
					UseDefaultCredentials = false,
					Credentials = new NetworkCredential(Configuration.GetValue<string>("Email:Username"), Configuration.GetValue<string>("Email:Password")),
					EnableSsl = true
				};

				return smtp;
			});


			services.
			AddAuthentication(op => {
				op.DefaultAuthenticateScheme = "JwtBearer";
				op.DefaultChallengeScheme = "JwtBearer";
			})
			.AddJwtBearer("JwtBearer", op =>
			{
				op.RequireHttpsMetadata = false;
				op.SaveToken = true;
				op.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("BancoDeVagas-autenticacao")),
					ClockSkew = TimeSpan.FromMinutes(30),
					ValidIssuer = "Banco_De_Vagas_Back_End",
					ValidAudience = "Banco_De_Vagas_Back_End"
				};
			});



			services.AddScoped<EnvioDeEmail>();
			services.AddScoped<Validations>();
			services.AddScoped<UploadArchive>();
			services.AddScoped<Randomize>();
			services.AddScoped<IVagaRepository, VagaRepository>();
			services.AddScoped<IUsuarioRepository, UsuarioRepository>();
			services.AddScoped<ICandidatoRepository, CandidatoRepository>();
			services.AddScoped<IEmpresaRepository, EmpresaRepository>();
			services.AddScoped<ICurriculoRepository, CurriculoRepository>();
			services.AddScoped<ICandidaturaRepository, CandidaturaRepository>();
			services.AddScoped<IBancoDeTalentos, BancoDeTalentos>();

		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}


			app.UseStaticFiles(
				new StaticFileOptions { 
				FileProvider = new PhysicalFileProvider(
					Path.Combine(Directory.GetCurrentDirectory(),"imagens")),
				RequestPath = "/imagens"
			});


			app.UseCors();

			app.UseSwagger();
			
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Banco_De_Vagas_NSG");
			});

			app.UseAuthentication();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
