using System;
using System.Collections.Generic;
using HookCraft.Core;
using Xunit;

namespace HookCraft.Tests
{
    public class HookGeneratorTests
    {
        private HookGenerator CreateGenerator() => new HookGenerator();

        // ---------- Тести для ValidateStyle ----------

        [Fact]
        public void ValidateStyle_ValidIntriguing_ReturnsTrue()
        {
            var generator = CreateGenerator();
            bool result = generator.ValidateStyle("інтригуючий");
            Assert.True(result);
        }

        [Fact]
        public void ValidateStyle_ValidHumorous_ReturnsTrue()
        {
            var generator = CreateGenerator();
            Assert.True(generator.ValidateStyle("гумористичний"));
        }

        [Fact]
        public void ValidateStyle_ValidSerious_ReturnsTrue()
        {
            var generator = CreateGenerator();
            Assert.True(generator.ValidateStyle("серйозний"));
        }

        [Fact]
        public void ValidateStyle_InvalidUnknown_ReturnsFalse()
        {
            var generator = CreateGenerator();
            Assert.False(generator.ValidateStyle("незнайомий"));
        }

        [Fact]
        public void ValidateStyle_Null_ThrowsArgumentNullException()
        {
            var generator = CreateGenerator();
            // Використовуємо null! для приглушення попередження CS8625
            Assert.Throws<ArgumentNullException>(() => generator.ValidateStyle(null!));
        }

        [Fact]
        public void ValidateStyle_EmptyString_ThrowsArgumentException()
        {
            var generator = CreateGenerator();
            Assert.Throws<ArgumentException>(() => generator.ValidateStyle(""));
        }

        [Fact]
        public void ValidateStyle_WhitespaceString_ThrowsArgumentException()
        {
            var generator = CreateGenerator();
            Assert.Throws<ArgumentException>(() => generator.ValidateStyle("   "));
        }

        // ---------- Тести для GenerateHook ----------

        [Fact]
        public void GenerateHook_EmptyTopic_ThrowsArgumentException()
        {
            var generator = CreateGenerator();
            Assert.Throws<ArgumentException>(() => generator.GenerateHook("", "інтригуючий"));
        }

        [Fact]
        public void GenerateHook_PastaTopic_HumorousStyle_ReturnsExpectedHook()
        {
            var generator = CreateGenerator();
            string hook = generator.GenerateHook("паста", "гумористичний");
            Assert.Contains("Готуєш пасту?", hook);
        }

        [Fact]
        public void GenerateHook_MarketingTopic_SeriousStyle_ContainsSMM()
        {
            var generator = CreateGenerator();
            string hook = generator.GenerateHook("маркетинг", "серйозний");
            Assert.Contains("SMM", hook);
        }

        [Fact]
        public void GenerateHook_InvalidStyle_ThrowsArgumentException()
        {
            var generator = CreateGenerator();
            var ex = Assert.Throws<ArgumentException>(() => generator.GenerateHook("тема", "фантастичний"));
            Assert.Contains("Невідомий стиль", ex.Message);
        }

        [Fact]
        public void GenerateHook_AddsToHistory()
        {
            var generator = CreateGenerator();
            generator.GenerateHook("спорт", "серйозний");
            Assert.Single(generator.History);
            Assert.Equal("спорт", generator.History[0].Topic);
        }

        // ---------- Тести для BulkGenerate ----------

        [Fact]
        public void BulkGenerate_NullTopics_ThrowsArgumentException()
        {
            var generator = CreateGenerator();
            // Використовуємо null! для приглушення попередження CS8625
            Assert.Throws<ArgumentException>(() => generator.BulkGenerate(null!, "серйозний"));
        }

        [Fact]
        public void BulkGenerate_EmptyTopics_ThrowsArgumentException()
        {
            var generator = CreateGenerator();
            Assert.Throws<ArgumentException>(() => generator.BulkGenerate(new List<string>(), "серйозний"));
        }

        [Fact]
        public void BulkGenerate_ValidTopics_ReturnsListOfSameLength()
        {
            var generator = CreateGenerator();
            var topics = new List<string> { "паста", "маркетинг" };
            var results = generator.BulkGenerate(topics, "гумористичний");
            Assert.Equal(2, results.Count);
            Assert.All(results, r => Assert.IsType<string>(r));
        }

        [Fact]
        public void BulkGenerate_WithInvalidTopic_ReturnsErrorMessageForThatTopic()
        {
            var generator = CreateGenerator();
            var topics = new List<string> { "паста", "", "маркетинг" };
            var results = generator.BulkGenerate(topics, "серйозний");
            Assert.Contains(results, r => r.Contains("Помилка для теми ''"));
        }
    }
}