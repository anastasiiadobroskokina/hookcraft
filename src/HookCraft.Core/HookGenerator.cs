using System;
using System.Collections.Generic;
using System.Linq;

namespace HookCraft.Core
{
    public class HookGenerator
    {
        // 1. Погане іменування (однолітерна змінна)
        public static readonly List<string> VS = new List<string> { "інтригуючий", "гумористичний", "серйозний" };

        public List<GenerationRecord> History { get; private set; }

        public HookGenerator()
        {
            History = new List<GenerationRecord>();
        }

        // 2. Довгий метод (> 30 рядків) + 3. Магічні числа та рядки
        public bool ValidateStyle(string s)
        {
            // 4. Надмірна вкладеність
            if (s != null)
            {
                if (s != "")
                {
                    // 5. Дублювання коду (повторювана логіка)
                    var trimmed = s.Trim();
                    if (trimmed == "інтригуючий") return true;
                    if (trimmed == "гумористичний") return true;
                    if (trimmed == "серйозний") return true;
                    return false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // 6. Магічне число 0.05, 0.10, 0.15 та дублювання
        public string GenerateHook(string topic, string style)
        {
            if (topic == null) throw new ArgumentNullException();
            if (style == null) throw new ArgumentNullException();

            var discount = 0.0;
            if (style == "інтригуючий")
                discount = 0.05;
            else if (style == "гумористичний")
                discount = 0.10;
            else if (style == "серйозний")
                discount = 0.15;

            // 7. Мертвий код (закоментований блок)
            // var oldWay = topic + " - " + style;

            return $"{style} гачок зі знижкою {discount}: {topic}";
        }

        public List<string> BulkGenerate(List<string> topics, string style)
        {
            if (topics == null || topics.Count == 0)
                throw new ArgumentException("Список тем не може бути null або порожнім");
            var results = new List<string>();
            foreach (var topic in topics)
                results.Add(GenerateHook(topic, style));
            return results;
        }
    }

    public class GenerationRecord
    {
        public string Topic { get; set; } = "";
        public string Style { get; set; } = "";
        public string Hook { get; set; } = "";
    }
}