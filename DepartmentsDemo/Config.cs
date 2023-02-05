namespace DepartmentsDemo;

public static class Config
{
	public static void Init(IWebHostEnvironment env)
	{
		var builder = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
			.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
		// This one needs to be last
		//.AddEnvironmentVariables();
        
		AppConfiguration = builder.Build();
	}

	public static IConfiguration AppConfiguration { get; private set; }
}