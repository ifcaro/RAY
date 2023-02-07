using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RAY
{
    internal class Config : IDisposable
    {
        private Dictionary<string, Dictionary<string, string>> _config = new Dictionary<string, Dictionary<string, string>>();
        
        public void ReadConfig(string filename)
        {
            if (File.Exists(filename))
            {
                char[] separator = new char[] { '=' };
                string currentGroup = null;

                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string line = sr.ReadLine().Trim();

                        while(line != null)
                        {

                            if (line.StartsWith("[") && line.EndsWith("]"))
                            {
                                currentGroup = line.Substring(1, line.Length - 2);
                                _config.Add(currentGroup, new Dictionary<string, string>());
                            }
                            else
                            {
                                var split = line.Split(separator, 2);

                                if (split.Length == 2)
                                {
                                    Set(currentGroup, split[0], split[1]);
                                }
                            }

                            line = sr.ReadLine()?.Trim();
                        }
                    }
                }
            }
        }

        public void SaveConfig(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    foreach(var group in _config)
                    {
                        sw.WriteLine($"[{group.Key}]");

                        foreach(var value in group.Value)
                        {
                            sw.WriteLine($"{value.Key}={value.Value}");
                        }

                        sw.WriteLine();
                    }
                }
            }
        }

        public string Get(string group, string key)
        {
            if (_config.ContainsKey(group) && _config[group].ContainsKey(key))
            {
                return _config[group][key];
            }

            return null;
        }

        public string GetByValue(string group, string value)
        {
            if (_config.ContainsKey(group) && _config[group].ContainsValue(value))
            {
                return _config[group].First(x => x.Value == value).Key;
            }

            return null;
        }

        public void Set(string group, string key, string value)
        {
            if (!_config.ContainsKey(group))
            {
                _config.Add(group, new Dictionary<string, string>());
            }

            if (!_config[group].ContainsKey(key))
            {
                _config[group].Add(key, value);
            }
            else
            {
                _config[group][key] = value;
            }
        }

        public void Dispose()
        {
            _config.Clear();
        }
    }

}
