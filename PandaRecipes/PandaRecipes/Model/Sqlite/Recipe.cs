using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using PandaRecipes.Data;

namespace PandaRecipes.Model.Sqlite
{
    public class Recipe : ISqliteModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Title { get; set; }
        public string PhotoPath { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}
