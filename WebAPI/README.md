# hesapizi.api
Add-Migration migration_description -Context PgDbContext -OutputDir Migrations/Pg

dotnet ef database update --context PgDbContext --project ./Hesapizi/DataAccess --startup-project ./Hesapizi/WebAPI