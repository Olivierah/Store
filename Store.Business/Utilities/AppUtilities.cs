using Store.Domain.Dtos;
using Store.Domain.Entities;
using Store.Repository.DataAccess;

namespace Store.Business.AppUtilities
{
    public class AppUtilities
    {
        // Mock de aplicativos (Gera uma lista de apps aleatórios)
        private static List<AppsDto> GenerateAppsList()
        {
            try
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
            catch (Exception)
            {
                throw;
            }
        }

        // Verifica se já existem aplicativos no banco. 
        public static List<AppsDto> ValidateIfDataAlreadyExist()
        {
            try
            {
                var apps = AppDataAccess.GetAllApps();

                if (apps.Count == 0 || apps == null)
                {
                    return GenerateAppsList();
                }
                return AppListEntityToAppDto(apps);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Converte a lista de aplicativos de Entidade para DTO
        private static List<AppsDto> AppListEntityToAppDto(List<App> apps)
        {
            try
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
