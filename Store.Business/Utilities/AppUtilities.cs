using Store.Repository.DataAccess;
using Store.Domain.Entities;
using Store.Domain.Dtos;

namespace Store.Business.AppUtilities
{
    public class AppUtilities
    {
        // Mock de aplicativos (Gera uma lista de apps aleatórios)
        private static List<AppsDto> GenerateAppsList()
        {
            List<AppsDto> apps = new List<AppsDto>();

            for (int i = 1; i < 10; i++)
            {
                string appName = $"Aplicativo - {i}";

                AppsDto app = new AppsDto
                {
                    Id = Guid.NewGuid(),
                    AppName = appName
                };
                apps.Add(app);
            }
            AppDataAccess.SaveRandomAppsInDb(apps);
            return apps;
        }

        // Verifica se já existem aplicativos no banco. 
        public static List<AppsDto> ValidateIfDataAlreadyExist()
        {
            var apps = AppDataAccess.GetAllApps();

            if (apps.Count == 0 || apps == null)
            {
                return GenerateAppsList();
            }
            return AppListEntityToAppDto(apps);
        }

        //Converte a lista de aplicativos de Entidade para DTO
        private static List<AppsDto> AppListEntityToAppDto(List<App> apps)
        {
            var appListDto = new List<AppsDto>();

            foreach (var item in apps)
            {
                var appDto = new AppsDto
                {
                    Id = item.Id,
                    AppName = item.AppName,
                };
                appListDto.Add(appDto);
            }
            return appListDto;
        }
    }
}
