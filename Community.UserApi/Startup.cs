
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
            //注入的仓储实体
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


            //添加单例类 上下文存取器
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //添加单例类 NLog帮助类
            services.AddSingleton<INLogHelper, NLogHelper>();
            //< !--在Startup类的ConfigureServices()方法中进行配置，DefaultContractResolver() 原样输出，返回的 json 与后台定义一致-- >
            services.AddControllers().AddJsonOptions(option => option.JsonSerializerOptions.PropertyNamingPolicy = null);
            //配置成全局的服务
            //模型视图控制器添加过滤器(CustomExceptionFilter)
            //AddControllers就是只添加控制器过滤器
            services.AddMvc(config => config.Filters.Add(typeof(CustomExceptionFilter)));


            DbFactory.conDb = Configuration.GetConnectionString("UseDb");//在此切换数据库连接
            ConfigHelper.ConnecSqlServer = Configuration.GetConnectionString("SqlServerConStr");
            ConfigHelper.ConnecMySql = Configuration.GetConnectionString("MySqlConStr");

            //跨域
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

                //启用swagger验证功能
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                //添加全局安全条件
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

            #region 注册jwt
                JWTTokenOptions JWTTokenOptions = new JWTTokenOptions();


                //获取appsettings的内容
                services.Configure<JWTTokenOptions>(this.Configuration.GetSection("JWTToken"));
                //将给定的对象实例绑定到指定的配置节
                Configuration.Bind("JWTToken", JWTTokenOptions);

                //注册到Ioc容器
                services.AddSingleton(JWTTokenOptions);

                //redis缓存
                var section = Configuration.GetSection("Redis:Default");
               //连接字符串
                ConfigHelperRedis._conn = section.GetSection("Connection").Value;

                //实例化名称
                ConfigHelperRedis._name = section.GetSection("InstanceName").Value;
                //默认数据库
                ConfigHelperRedis._db = int.Parse(section.GetSection("DefaultDB").Value ?? "0");

                services.AddSingleton(new RedisHelper());

                //【授权】
                services.AddAuthorization(options =>
                {
                    options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
                    options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
                    options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));
                });

                //添加验证服务
                services.AddAuthentication(option =>
                {
                    //默认身份验证模式
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    //默认方案
                    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                }).AddJwtBearer(option =>
                {
                    //设置元数据地址或权限是否需要HTTP
                    option.RequireHttpsMetadata = false;
                    option.SaveToken = true;
                    //令牌验证参数
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        //获取或设置要使用的Microsoft.IdentityModel.Tokens.SecurityKey用于签名验证。
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.
                        GetBytes(JWTTokenOptions.Secret)),
                        //获取或设置一个System.String，它表示将使用的有效发行者检查代币的发行者。 
                        ValidIssuer = JWTTokenOptions.Issuer,
                        //获取或设置一个字符串，该字符串表示将用于检查的有效受众反对令牌的观众。
                        ValidAudience = JWTTokenOptions.Audience,
                        //是否验证发起人
                        ValidateIssuer = false,
                        //是否验证订阅者
                        ValidateAudience = false,
                        ////允许的服务器时间偏移量
                        ClockSkew = TimeSpan.Zero,
                        ////是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                        ValidateLifetime = true
                    };
                    //如果jwt过期，在返回的header中加入Token-Expired字段为true，前端在获取返回header时判断
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

            //取消驼峰命名法配置
            services.AddControllers().AddJsonOptions(option => option.JsonSerializerOptions.PropertyNamingPolicy = null);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                #region Nlog
                //添加NLog
                loggerFactory.AddNLog();
                //加载配置
                NLog.LogManager.LoadConfiguration("NLog.config");
                //调用自定义的中间件
                app.UseLog();
                #endregion

                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Community.UserApi v1"));
            }

            app.UseRouting();

            app.UseSession();



            app.UseStaticFiles();   //引用静态文件

            app.UseAuthentication();  //添加认证

            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseCors("AllRequests");//开启Cors跨域请求中间件

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
