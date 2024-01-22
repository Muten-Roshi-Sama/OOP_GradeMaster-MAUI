using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeMasterMAUI.Services
{
    public class EntityRepository
    {
        public static async Task<T> UnpackAsync<T>(string filename, Func<string, Task<T>> unpackFunction)
        {
            var SaveFilename = Path.Combine(Config.Dir, filename);
            return await unpackFunction(SaveFilename);
        }

        public static async Task<List<T>> UnpackAllAsync<T>(string filePattern, Func<string, Task<T>> unpackFunction)
        {
            var items = new List<T>();
            var files = Directory.EnumerateFiles(Config.Dir, filePattern);
            foreach (var file in files)
            {
                var item = await UnpackAsync(Path.GetFileName(file), unpackFunction);
                items.Add(item);
            }
            return items;
        }
    }

}
