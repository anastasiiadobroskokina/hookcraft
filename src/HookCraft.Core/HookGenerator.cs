using System;
using System.Collections.Generic;
using System.Linq;   // Додано для LINQ

namespace HookCraft.Core
{
    public class HookGenerator
    {
        public static readonly List<string> ValidStyles = new List<string>
        {
            "інтригуючий",
            "гумористичний",
            "серйозний"
        };

        public List<GenerationRecord> History { get; private set; }

        public HookGenerator()
        {
            History = new List<GenerationRecord>();
        }

        public bool ValidateStyle(string style)
        {
            if (style == null)
                throw new ArgumentNullException(nameof(style), "Стиль не може бути null");
            if (string.IsNullOrWhiteSpace(style))
                throw new ArgumentException("Стиль не може бути порожнім або складатися з пробілів", nameof(style));

            // Виправлено: використання LINQ для регістронезалежного порівняння
            return ValidStyles.Any(s => string.Equals(s, style.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public string GenerateHook(string topic, string style)
        {
            if (string.IsNullOrWhiteSpace(topic))
                throw new ArgumentException("Тема не може бути порожньою або null", nameof(topic));
            if (!ValidateStyle(style))
                throw new ArgumentException($"Невідомий стиль: {style}. Допустимі: {string.Join(", ", ValidStyles)}");

            string topicLower = topic.ToLower();
            string hook;

            if (topicLower.Contains("рецепт") || topicLower.Contains("паста"))
            {
                if (style.Equals("інтригуючий", StringComparison.OrdinalIgnoreCase))
                    hook = "Секрет ідеальної пасти, про який мовчать кухарі...";
                else if (style.Equals("гумористичний", StringComparison.OrdinalIgnoreCase))
                    hook = "Готуєш пасту? Забудь про воду з-під крана!";
                else
                    hook = $"Як приготувати {topic} за 5 хвилин.";
            }
            else if (topicLower.Contains("маркетинг") || topicLower.Contains("smm"))
            {
                hook = style.Equals("серйозний", StringComparison.OrdinalIgnoreCase)
                    ? "3 фішки, які піднімуть твій SMM на новий рівень."
                    : "Чому твої пости ніхто не читає? Відповідь у першому реченні.";
            }
            else
            {
                hook = $"{char.ToUpper(style[0]) + style.Substring(1).ToLower()} гачок на тему: {topic}";
            }

            History.Add(new GenerationRecord { Topic = topic, Style = style, Hook = hook });
            return hook;
        }

        public List<string> BulkGenerate(List<string> topics, string style)
        {
            if (topics == null || topics.Count == 0)
                throw new ArgumentException("Список тем не може бути null або порожнім", nameof(topics));

            var results = new List<string>();
            foreach (var topic in topics)
            {
                try
                {
                    results.Add(GenerateHook(topic, style));
                }
                catch (ArgumentException ex)
                {
                    results.Add($"Помилка для теми '{topic}': {ex.Message}");
                }
            }
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