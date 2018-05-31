using System.Linq;
using GolfGear.Clubs;
using GolfGear.Clubs.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;

namespace GolfGear
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
            services.AddSingleton<IDocumentStore>(_ => {
                var docStore = new DocumentStore {
                    Database = this.Configuration["Database:Name"],
                    Urls = new[] { this.Configuration["Database:Url"] },
                    
                };
                
                docStore.Initialize();

                this.LoadSeedData(docStore);

                return docStore;
            });

            services.AddCors(options => {
                options.AddPolicy("CorsPolicy", builder => {
                    builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowCredentials();
                });
            });
            
            services.AddMvc();
            
        }

        private void LoadSeedData(DocumentStore documentStore)
        {
            var urls = string.Join(", ", documentStore.Urls);
            
            using (var session = documentStore.OpenSession()){
                if (!session.Advanced.LoadStartingWith<GolfClub>("GolfClubs").Any()){
                    var callawayRogue = new GolfClub {
                        Type = ClubType.Driver,
                        Make = "Callaway",
                        Model = "Rogue",
                        Loft = 10.5m,
                        Shaft = new Shaft {
                            Type = ShaftType.Graphite,
                            Make = "Project X",
                            Model = "HZRDUS Yellow",
                            Flex = Flex.Stiff,
                            Weight = 65
                        }
                    };

                    var taylormadeM1 = new GolfClub {
                        Type = ClubType.Driver,
                        Make = "Taylormade",
                        Model = "M1",
                        Loft = 10.5m,
                        Shaft = new Shaft {
                            Type = ShaftType.Graphite,
                            Make = "Aldila",
                            Model = "Rogue Silver",
                            Flex = Flex.Stiff,
                            Weight = 65
                        }
                    };

                    var clubs = new[] { callawayRogue, taylormadeM1 };

                    using (var bulkOperation = documentStore.BulkInsert()){
                        foreach (var club in clubs) {
                            bulkOperation.Store(club);
                        }
                    }
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy")
               .UseMvc();
        }
    }
}
