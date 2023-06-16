using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using _153504_Klimkovich.Domain.Abstractions;
using _153504_Klimkovich.Persistense.Repository;
using _153504_Klimkovich.Application.Service;
using _153504_Klimkovich.Application.Abstractions;
using _153504_Klimkovich.UI.ViewModels;
using _153504_Klimkovich.UI.Pages;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using _153504_Klimkovich.Persistense.Data;
using Microsoft.EntityFrameworkCore;
using _153504_Klimkovich.Domain.Entities;

namespace _153504_Klimkovich.UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        // app settings
        string settingsStream = "_153504_Klimkovich.UI.appsettings.json";
        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream(settingsStream);

        // builder
        var builder = MauiApp.CreateBuilder();
        builder.Configuration.AddJsonStream(stream);
        builder
            .UseMauiApp<App>()
            .UseMauiApp<App>().UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });


        // Dp
        AddDbContext(builder);

        // Services
        SetupServices(builder.Services);

        SeedData(builder.Services);

        return builder.Build();
    }

    private static void SetupServices(IServiceCollection services)
    {
        //Services
        services.AddSingleton<IUnitOfWork, EfUnitOfWork>();
        services.AddSingleton<ISportTeamService, SportTeamService>();
        services.AddSingleton<IMemberService, MemberService>();
        //Pages
        services.AddSingleton<SportTeamsPage>();
        services.AddTransient<MemberDetailsPage>();
        services.AddTransient<CreateSportTeamPage>();
        services.AddTransient<CreateOrEditMemberPage>();
        //ViewModels
        services.AddSingleton<SportTeamsViewModel>();
        services.AddTransient<MemberDetailsViewModel>();
        services.AddTransient<CreateSportTeamViewModel>();
        services.AddTransient<CreateOrEditMemberViewModel>();
    }

    private static void AddDbContext(MauiAppBuilder builder)
    {
        var connStr = builder.Configuration.GetConnectionString("SqliteConnection");
        string dataDirectory = String.Empty;
#if ANDROID
        dataDirectory = FileSystem.AppDataDirectory+"/";
#endif
        connStr = String.Format(connStr, dataDirectory);
        var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connStr).Options;
        builder.Services.AddSingleton<AppDbContext>((s) => new AppDbContext(options));
    }

    public async static void SeedData(IServiceCollection services)
    {
        using var provider = services.BuildServiceProvider();
        var unitOfWork = provider.GetService<IUnitOfWork>();
        await unitOfWork.RemoveDatbaseAsync();
        await unitOfWork.CreateDatabaseAsync();

        // Add SportTeams
        IReadOnlyList<SportTeam> sportTeams = new List<SportTeam>() { 
            new SportTeam(){ Name="Like football", KindOfSport="Football"}, 
            new SportTeam(){ Name="Like backetball", KindOfSport="Backetball"} };
        foreach (var team in sportTeams)
            await unitOfWork.SportTeamRepository.AddAsync(team);
        await unitOfWork.SaveAllAsync();

        //Add Members
        Random rand = new Random();
        int k = 1;
        foreach (var team in sportTeams)
            for (int j = 0; j < 10; j++)
                await unitOfWork.MemberRepository.AddAsync(new Member()
                {
                    Name = $"Member {k++}",
                    Age = rand.Next() % 100,
                    Points = j,
                    SportTeam = team,
                });
        await unitOfWork.SaveAllAsync();
    }
}
