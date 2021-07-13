
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Community.Common;
using Community.IRepository;
using Community.Repository;
namespace Community.UserApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //ע��Ĳִ�ʵ��
            services.AddSingleton<IGoodsRepository, GoodsRepository>();
            services.AddSingleton<IGoodsTypeRepository, GoodsTypeRepository>();
            services.AddSingleton<IDemoRepository,DemoRepository>();
            services.AddSingleton<IColonelRepository,ColonelRepository>();
            services.AddSingleton<ISeckillConfignRepository, SeckillConfignRepository>();
            services.AddSingleton<ISeckillGoodsRepository, SeckillGoodsRepository>();

            services.AddSingleton<IGoodsTypeRepository, GoodsTypeRepository>();
            services.AddSingleton<IGoodsRepository, GoodsRepository>();
            services.AddSingleton<IColonelGradeRepository, ColonelGradeRepository>();

            services.AddSingleton<IGoodsEvaluateRepository,GoodsEvaluateRepository>();
            services.AddSession();

            services.AddSingleton<ISalespersonRepository, SalespersonRepository>();
            services.AddSingleton<ICommunityGroupRepository, CommunityGroupRepository>();
            services.AddSingleton<IWareHouseRepository, WareHouseRepository>();
            services.AddSingleton<ICommissionRepository, CommissionRepository>();


            //��ӵ����� �����Ĵ�ȡ��
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //��ӵ����� NLog������
            services.AddSingleton<INLogHelper, NLogHelper>();
            //< !--��Startup���ConfigureServices()�����н������ã�DefaultContractResolver() ԭ����������ص� json ���̨����һ��-- >
            services.AddControllers().AddJsonOptions(option => option.JsonSerializerOptions.PropertyNamingPolicy = null);
            //���ó�ȫ�ֵķ���
            //ģ����ͼ��������ӹ�����(CustomExceptionFilter)
            //AddControllers����ֻ��ӿ�����������
            services.AddMvc(config => config.Filters.Add(typeof(CustomExceptionFilter)));


            DbFactory.conDb = Configuration.GetConnectionString("UseDb");//�ڴ��л����ݿ�����
            ConfigHelper.ConnecSqlServer = Configuration.GetConnectionString("SqlServerConStr");
            ConfigHelper.ConnecMySql = Configuration.GetConnectionString("MySqlConStr");

            //����
            services.AddCors(c =>
            {
                c.AddPolicy("AllRequests", policy =>
                {
                    policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            services.AddControllers();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Community.UserApi", Version = "v1" });

                //����swagger��֤����
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "���¿�����������ͷ����Ҫ���Jwt��ȨToken��Bearer Token",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header,//jwtĬ�ϴ��authorization��Ϣ��λ��(����ͷ��)
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                //���ȫ�ְ�ȫ����
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"}
                        },new string[] { }
                    }
                });
            });

            #region ע��jwt
                JWTTokenOptions JWTTokenOptions = new JWTTokenOptions();


                //��ȡappsettings������
                services.Configure<JWTTokenOptions>(this.Configuration.GetSection("JWTToken"));
                //�������Ķ���ʵ���󶨵�ָ�������ý�
                Configuration.Bind("JWTToken", JWTTokenOptions);

                //ע�ᵽIoc����
                services.AddSingleton(JWTTokenOptions);

                //redis����
                var section = Configuration.GetSection("Redis:Default");
               //�����ַ���
                ConfigHelperRedis._conn = section.GetSection("Connection").Value;

                //ʵ��������
                ConfigHelperRedis._name = section.GetSection("InstanceName").Value;
                //Ĭ�����ݿ�
                ConfigHelperRedis._db = int.Parse(section.GetSection("DefaultDB").Value ?? "0");

                services.AddSingleton(new RedisHelper());

                //����Ȩ��
                services.AddAuthorization(options =>
                {
                    options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
                    options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
                    options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));
                });

                //�����֤����
                services.AddAuthentication(option =>
                {
                    //Ĭ�������֤ģʽ
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    //Ĭ�Ϸ���
                    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                }).AddJwtBearer(option =>
                {
                    //����Ԫ���ݵ�ַ��Ȩ���Ƿ���ҪHTTP
                    option.RequireHttpsMetadata = false;
                    option.SaveToken = true;
                    //������֤����
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        //��ȡ������Ҫʹ�õ�Microsoft.IdentityModel.Tokens.SecurityKey����ǩ����֤��
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.
                        GetBytes(JWTTokenOptions.Secret)),
                        //��ȡ������һ��System.String������ʾ��ʹ�õ���Ч�����߼����ҵķ����ߡ� 
                        ValidIssuer = JWTTokenOptions.Issuer,
                        //��ȡ������һ���ַ��������ַ�����ʾ�����ڼ�����Ч���ڷ������ƵĹ��ڡ�
                        ValidAudience = JWTTokenOptions.Audience,
                        //�Ƿ���֤������
                        ValidateIssuer = false,
                        //�Ƿ���֤������
                        ValidateAudience = false,
                        ////����ķ�����ʱ��ƫ����
                        ClockSkew = TimeSpan.Zero,
                        ////�Ƿ���֤Token��Ч�ڣ�ʹ�õ�ǰʱ����Token��Claims�е�NotBefore��Expires�Ա�
                        ValidateLifetime = true
                    };
                    //���jwt���ڣ��ڷ��ص�header�м���Token-Expired�ֶ�Ϊtrue��ǰ���ڻ�ȡ����headerʱ�ж�
                    option.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            #endregion

            //ȡ���շ�����������
            services.AddControllers().AddJsonOptions(option => option.JsonSerializerOptions.PropertyNamingPolicy = null);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                #region Nlog
                //���NLog
                loggerFactory.AddNLog();
                //��������
                NLog.LogManager.LoadConfiguration("NLog.config");
                //�����Զ�����м��
                app.UseLog();
                #endregion

                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Community.UserApi v1"));
            }

            app.UseRouting();

            app.UseSession();



            app.UseStaticFiles();   //���þ�̬�ļ�

            app.UseAuthentication();  //�����֤

            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseCors("AllRequests");//����Cors���������м��

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
