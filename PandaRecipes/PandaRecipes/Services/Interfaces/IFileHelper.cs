using System;
using System.Collections.Generic;
using System.Text;

namespace PandaRecipes.Services.Interfaces
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
