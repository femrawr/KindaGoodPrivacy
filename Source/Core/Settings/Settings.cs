using IniParser;
using IniParser.Model;
using KindaGoodPrivacy.Source.Core.SaveManager;

namespace KindaGoodPrivacy.Source.Core.Settings
{
    public class Settings
    {
        public static int Iterations { get; set; } = 35;
        public static int MemorySize { get; set; } = 65536;
        public static int Parallelism { get; set; } = Environment.ProcessorCount;

        private static readonly string section = "SETTINGS";

        public static void Save()
        {
            var parser = new FileIniDataParser();
            var data = new IniData();

            data[section]["Iterations"] = Iterations.ToString();
            data[section]["MemorySize"] = MemorySize.ToString();
            data[section]["Parallelism"] = Parallelism.ToString();

            parser.WriteFile(Variables.settingsFilePath, data);
        }

        public static void Load()
        {
            var parser = new FileIniDataParser();
            var data = parser.ReadFile(Variables.settingsFilePath);

            if (!data.Sections.ContainsSection(section))
                return;

            _ = int.TryParse(data[section]["Iterations"], out int iterations);
            _ = int.TryParse(data[section]["MemorySize"], out int memorySize);
            _ = int.TryParse(data[section]["Parallelism"], out int parallelism);

            Iterations = iterations > 0 ? iterations : Iterations;
            MemorySize = memorySize > 0 ? memorySize : MemorySize;
            Parallelism = parallelism > 0 ? parallelism : Parallelism;
        }
    }
}
