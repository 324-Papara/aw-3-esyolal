create migration SQL Server
     dotnet ef migrations add InitialCreate -s ../Para.Api/ --context ParaDbContext
create migration PostgreSQL Server
     dotnet ef migrations add InitialCreate -s ../Para.Api/ --context ParaDbContext    
  
db guncelleme SQL 
     dotnet ef database update --project "./Para.Data" --startup-project "Para.Api/" --context ParaDbContext
db guncelleme Postgre
     dotnet ef database update --project "./Para.Data" --startup-project "Para.Api/" --context ParaDbContext

     6	Emre	Yolal	12321321	esyolal@gmail.com	34233812	1997-03-01 00:00:00.000	Emre	2024-07-25 00:00:00.000	1
8	Emre	Yolalaa	121212121	esyolal1@gmail.com	0	2024-07-25 13:36:00.128		0001-01-01 00:00:00.000	0