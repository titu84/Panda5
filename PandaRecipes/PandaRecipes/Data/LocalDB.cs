using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PandaRecipes.Model.Sqlite;
using System.Linq;

namespace PandaRecipes.Data
{
    public class LocalDB
    {
        readonly SQLiteAsyncConnection database;

        public LocalDB(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Recipe>().Wait();
        }

        public async Task<List<T>> GetItems<T>() where T : class, new()
        {
            return await database.Table<T>().ToListAsync();
        }

        public async Task<List<Recipe>> GetRecipeByCategory(string category)
        {
            return await database.Table<Recipe>().Where(x => x.Category.Contains(category)).ToListAsync();
        }
        public async Task<List<String>> GetCategories()
        {
                var result = new List<String>();
                var temp = await database.Table<Recipe>().Where(x => x.Category != null).ToListAsync();
                foreach (var c in temp)
                {
                    c.Category.Split(' ').ToList().ForEach(a =>
                    {
                        if (a.Length > 0)
                            result.Add(a);
                    });
                }
                return result.Distinct().OrderBy(a => a).ToList();           
        }

        public async Task<T> GetItemByID<T>(int id) where T : class, ISqliteModel, new()
        {
            return await database.Table<T>().Where(x => x.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItem<T>(T item)
        {
            var result = await database.UpdateAsync(item);

            if (result == 0)
                result = await database.InsertAsync(item);

            return result;
        }

        public async Task<int> DeleteItem<T>(T item)
        {
            return await database.DeleteAsync(item);
        }
    }
}
