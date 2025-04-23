using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ShopMagic
    {
        private static ShopManager _shopManager = new ShopManager();
        private static string _xmlFile = "antique.xml";
        private static string _jsonFile = "modern.json";
        private static string _textFile = "legends.txt";

        public static void Main(string[] args)
        {
            CreateSampleData();

            _shopManager.LoadAllData(_xmlFile, _jsonFile, _textFile);

            while (true)
            {
                Console.WriteLine("\nМагазин артефактов:");
                Console.WriteLine("1 - Создать отчет");
                Console.WriteLine("2 - Найти проклятые артефакты");
                Console.WriteLine("3 - Сгруппировать артефакты по редкости");
                Console.WriteLine("4 - Показать самые сильные артефакты");
                Console.WriteLine("5 - Сохранить артефакты в файл");
                Console.WriteLine("0 - Выход");

                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                if (choice == "0")
                {
                    break;
                }

                ProcessChoice(choice);
            }

            Cleanup();

            Console.WriteLine("Программа завершена.");
        }

        static void ProcessChoice(string choice)
        {
            switch (choice)
            {
                case "1":
                    _shopManager.GenerateReport("report.txt");
                    Console.WriteLine("Отчет создан: report.txt");
                    break;

                case "2":
                    List<LegendaryArtifact> cursed = _shopManager.FindCursedArtifacts();
                    Console.WriteLine("Проклятые артефакты:");
                    foreach (var artifact in cursed)
                    {
                        Console.WriteLine(artifact.Serialize());
                    }
                    break;

                case "3":
                    Dictionary<Rarity, int> rarityGroups = _shopManager.GroupByRarity();
                    Console.WriteLine("Артефакты по редкости:");
                    foreach (var group in rarityGroups)
                    {
                        Console.WriteLine($"{group.Key}: {group.Value}");
                    }
                    break;

                case "4":
                    Console.Write("Сколько самых сильных артефактов показать? ");
                    if (int.TryParse(Console.ReadLine(), out int count))
                    {
                        List<Artifact> top = _shopManager.TopByPower(count);
                        Console.WriteLine($"Топ {count} самых сильных артефактов:");
                        foreach (var artifact in top)
                        {
                            Console.WriteLine($"{artifact.Name}: {artifact.PowerLevel}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неправильный ввод числа!");
                    }
                    break;

                case "5":
                    Console.Write("В каком формате сохранить (xml или json)? ");
                    string format = Console.ReadLine();
                    _shopManager.ExportArtifacts(_shopManager.Artifacts, "artifacts." + format, format);
                    break;

                default:
                    Console.WriteLine("Неправильный выбор! Попробуйте еще раз.");
                    break;
            }
        }

        static void CreateSampleData()
        {
            List<AntiqueArtifact> xmlArtifacts = new List<AntiqueArtifact>
        {
            new AntiqueArtifact { Id = 1, Name = "Амулет", PowerLevel = 95, Rarity = Rarity.Legendary, Age = 1200, OriginRealm = "Аркадия" }
        };
            XmlProcessor xmlProcessor = new XmlProcessor();
            xmlProcessor.SaveData(xmlArtifacts, _xmlFile);

            List<ModernArtifact> jsonArtifacts = new List<ModernArtifact>
        {
            new ModernArtifact { Id = 2, Name = "Бластер", PowerLevel = 88, Rarity = Rarity.Epic, TechLevel = 9.5, Manufacturer = "TechMage" }
        };
            JsonProcessor jsonProcessor = new JsonProcessor();
            jsonProcessor.SaveData(jsonArtifacts, _jsonFile);

            List<LegendaryArtifact> textArtifacts = new List<LegendaryArtifact>
        {
            new LegendaryArtifact { Id = 3, Name = "Меч судьбы", PowerLevel = 100, Rarity = Rarity.Legendary, CurseDescription = "Забирает жизнь", IsCursed = true }
        };
            LegendaryProcessor legendaryProcessor = new LegendaryProcessor();
            legendaryProcessor.SaveData(textArtifacts, _textFile);
        }

        static void Cleanup()
        {
            try
            {
                File.Delete(_xmlFile);
                File.Delete(_jsonFile);
                File.Delete(_textFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении файлов: {ex.Message}");
            }
        }
    }