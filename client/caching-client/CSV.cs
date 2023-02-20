using System.Collections.Generic;
using System.IO;

public class CsvWriter<T>
{
    private string _filePath;
    
    public CsvWriter(string filePath)
    {
        _filePath = filePath;
    }
    
    public void WriteListToCsv(List<T> list)
    {
        var directory = Path.GetDirectoryName(_filePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        using (var writer = new StreamWriter(_filePath))
        {
            var properties = typeof(T).GetProperties();
            var header = string.Join(",", properties.Select(p => p.Name));
            writer.WriteLine(header);
            
            foreach (var item in list)
            {
                var row = string.Join(",", properties.Select(p => p.GetValue(item)?.ToString() ?? ""));
                writer.WriteLine(row);
            }
        }

        Console.WriteLine($"written {list.Count} lines in csv file: {_filePath} ");
    }
}
