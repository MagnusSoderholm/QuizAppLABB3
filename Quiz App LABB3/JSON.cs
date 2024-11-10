using Quiz_App_LABB3.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quiz_App_LABB3
{
    //public class Json : INotifyPropertyChanged
    //{
    //    private string filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    //    public event PropertyChangedEventHandler? PropertyChanged;
    //    public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //    }


    //    public async void SaveQuestionPack(ObservableCollection<QuestionPackViewModel> questionPack)
    //    {
    //        string folderName = @"QuizAppLABB3";
    //        string folderPath = Path.Combine(filePath, folderName);
    //        string fullPath = Path.Combine(folderPath, "questions.Json");
    //        var options = new JsonSerializerOptions
    //        {
    //            WriteIndented = true,
    //            IgnoreReadOnlyFields = true,
    //            IgnoreReadOnlyProperties = true
    //        };
    //        if (!Directory.Exists(folderPath))
    //        {
    //            Directory.CreateDirectory(folderPath);
    //        }
    //        string json = JsonSerializer.Serialize(questionPack, options);
    //        if (!File.Exists(fullPath))
    //        //{
    //        //    File.Create(fullPath);
    //        //}
    //        await File.WriteAllTextAsync(fullPath, json);
    //    }


    //    public async Task<ObservableCollection<QuestionPackViewModel>> LoadQuestionPack()
    //    {
    //        string folderName = @"QuizAppLABB3";
    //        string folderPath = Path.Combine(filePath, folderName);
    //        string fullPath = Path.Combine(folderPath, "questions.Json");
    //        if (File.Exists(fullPath))
    //        {
    //            if (new FileInfo(fullPath).Length != 0)
    //            {
    //                string json = await File.ReadAllTextAsync(fullPath);
    //                var QPack = JsonSerializer.Deserialize<ObservableCollection<QuestionPackViewModel>>(json);
    //                return QPack;
    //            }
    //            else
    //            {
    //                return new ObservableCollection<QuestionPackViewModel>();
    //            }
    //        }
    //        else
    //        {
    //            return new ObservableCollection<QuestionPackViewModel>();
    //        }
    //    }
    //}



}
