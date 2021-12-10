# Installatie instructies

# GitHub URL: https://github.com/TwisterTies/HerkansingEindcase-BDM-

1. Zorg ervoor dat EF6, NET 6, VS2022 en latest versie van Angular CLI geïnstalleerd staat

2. Zorg ervoor dat er een LocalDB SQLServer draait met daarin een Database met de naam 'CaseContext'.

3. In de appsettings.json van de back-end, staat er een connection string naar de LocalDB.

4. In startup.cs van de back-end, staat er 'options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))'. Vervang deze string met 'LocalDbConnection'.

5. Zorg er eerst voor dat Add-Migration in de terminal geruned wordt met een naam, kan bijvoorbeeld 'TestCreate' oid zijn.

6. Run daarna Update-Database in de terminal. Dan wordt alles richting de SQL-server gepusht.

7. Run de back-end solution, kijk in de console / web-browser welk localhost-adres deze heeft gekregen.

8. In de Front-end, vervang de baseURL binnen course-overview.service.ts met het juiste localhost-adres van de backend.

9. In de Front-End, vervang de baseURL binnen de file-upload.component.ts met het juiste localhost-adres van de backend.

10. Run de Front-End solution, deze zou nu een connectie met de back-end moeten hebben. :)

11. Importeer eerst een cursus. Anders komt er geen data binnen bij cursusoverzicht.
