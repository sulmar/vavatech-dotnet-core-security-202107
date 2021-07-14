# dotnet-core-security-tutorial



## Ukrycie serwera


### IIS

Dodaj do projektu plik _web.config_ 

~~~ xml
<?xml version="1.0" encoding="UTF-8"?>
<configuration>
	<system.web>
		<httpRuntime enableVersionHeader="false"/>
	</system.web>
	<system.webServer>
		<security>
			<requestFiltering removeServerHeader="true" />
		</security>
		<httpProtocol>
			<customHeaders>
				<remove name="X-Powered-By"/>
			</customHeaders>
		</httpProtocol>
	</system.webServer>
</configuration>
~~~

### Kestrel
~~~ csharp
public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    .ConfigureKestrel(options => options.AddServerHeader = false)
                    .UseStartup<Startup>();
            });
~~~ 

