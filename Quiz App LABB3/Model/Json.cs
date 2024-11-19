using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Quiz_App_LABB3.Model
{
    //public class Json
    //{
    //    private readonly string appDataPath;
    //    private readonly string filePath;
    //    private readonly JsonSerializerOptions options;

    //    public Json()
    //    {
    //        // Tilldela värdet till fältvariabeln `appDataPath`
    //        appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

    //        string appFolder = Path.Combine(appDataPath, "Quiz-App-Labb3");
    //        Directory.CreateDirectory(appFolder);

    //        // Tilldela värdet till fältvariabeln `filePath`
    //        filePath = Path.Combine(appFolder, "Quiz-App-Labb3.json");

    //        options = new JsonSerializerOptions
    //        {
    //            IncludeFields = true,
    //            PropertyNameCaseInsensitive = true
    //        };
    //    }

    //    public async Task SaveJson(List<QuestionPack> packs)
    //    {
    //        string json = JsonSerializer.Serialize(packs, new JsonSerializerOptions { WriteIndented = true });
    //        Debug.WriteLine($"Saving to file: {filePath}");  // Lägg till debug-meddelande
    //        await File.WriteAllTextAsync(filePath, json);
    //        Debug.WriteLine("Save completed!");
    //    }

    //    public async Task<List<QuestionPack>> LoadJson()
    //    {
    //        try
    //        {
    //            if (File.Exists(filePath))
    //            {
    //                string json = await File.ReadAllTextAsync(filePath);
    //                return JsonSerializer.Deserialize<List<QuestionPack>>(json, options);
    //            }
    //            else
    //            {
    //                Debug.WriteLine("File not found! Path: " + filePath);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Debug.WriteLine($"Error loading Json: {ex.Message}");
    //        }

    //        return new List<QuestionPack>();
    //    }
    //}
}
