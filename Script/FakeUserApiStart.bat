docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=p97B98100' -e 'MSSQL_PID=Express' -p 1433:1433 -v sqlvolume:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2017-latest-ubuntu
timeout /t 3
docker run --rm -it -p 8000:80 -p 8001:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=8001 -e ASPNETCORE_ENVIRONMENT=Development -e ASPNETCORE_Kestrel__Certificates__Default__Password="devCertPassword" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/FakeUserApi.pfx -v /c/certs:/https/ fakeuser
