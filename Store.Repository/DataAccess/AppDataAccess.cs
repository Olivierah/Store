using Store.Repository.Context;
using Store.Domain.Entities;
using Store.Domain.Dtos;

namespace Store.Repository.DataAccess
{
    public class AppDataAccess 
    {
        //Recupera todos os aplicativos do banco
        public static List<App> GetAllApps()
        {
            using (var context = new StoreDataContext())
            {
                var entityAppList = context.Apps.Select(x => x).ToList();
                return entityAppList;
            }
        }

        // Salva os aplicativos gerados no banco
        public async static void SaveRandomAppsInDb(List<AppsDto> appList)
        {
            using (var context = new StoreDataContext())
            {
                foreach (var item in appList)
                {
                    var entityApp = new App
                    {
                        Id = item.Id,
                        AppName = item.AppName
                    };
                    await context.Apps.AddAsync(entityApp);
                    await context.SaveChangesAsync();
                }
            }
            
        }
    }
}
