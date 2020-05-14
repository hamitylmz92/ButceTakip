using ButceTakip.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ButceTakip.DataManager
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Gider>().Wait();
            _database.CreateTableAsync<Kategori>().Wait();
            _database.CreateTableAsync<GelirIslem>().Wait();
            _database.CreateTableAsync<GiderIslem>().Wait();
        }
        #region Kategori Sorguları
        public Task<List<Kategori>> GetKategoriList()
        {
            return _database.Table<Kategori>().ToListAsync();
        }
        public Task<List<Kategori>> GetKategoriList(int tip)
        {
            return _database.QueryAsync<Kategori>("SELECT * FROM Kategori WHERE Tipi=?", tip.ToString());
        }
        public Task<int> DeleteKategoriAsync(object obj, int id)
        {
            return _database.DeleteAsync<Kategori>(id);
        }
        #endregion

        #region Gelir Sorguları
        public Task<List<GelirIslem>> GetGelirIslemList()
        {
            return _database.Table<GelirIslem>().ToListAsync();
        }
        public Task<int> DeleteGeliriAsync(object obj, int id)
        {
            return _database.DeleteAsync<GelirIslem>(id);
        }

        public Task<List<GelirIslem>> GetGelirIslemList(DateTime baslangiTarihi)
        {
            return _database.QueryAsync<GelirIslem>("SELECT * FROM Kategori WHERE KayitTarihi>=? AND KayitTarihi <=?", baslangiTarihi,baslangiTarihi.AddDays(-1));
        }
        #endregion

        #region Gider Sorguları
        public Task<List<GiderIslem>> GetGiderIslemList()
        {
            return _database.Table<GiderIslem>().ToListAsync();
        }
        public Task<int> DeleteGideriAsync(object obj, int id)
        {
            return _database.DeleteAsync<GiderIslem>(id);
        }
        #endregion

        #region Genel
        public Task<int> SaveModelAsync(object obj)
        {
            return _database.InsertAsync(obj);
        }
        public Task<int> UpdateModelAsync(object obj)
        {
            return _database.UpdateAsync(obj);
        }
        #endregion

     
    
 
    }
}
