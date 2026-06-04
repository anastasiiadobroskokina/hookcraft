class HookGenerator:
    VALID_STYLES = ["інтригуючий", "гумористичний", "серйозний"]

    def __init__(self):
        self.history = []

    def validate_style(self, style: str) -> bool:
        if not isinstance(style, str):
            raise TypeError("Стиль має бути рядком")
        if not style or style.isspace():
            raise ValueError("Стиль не може бути порожнім")
        return style.strip().lower() in [s.lower() for s in self.VALID_STYLES]

    def generate_hook(self, topic: str, style: str) -> str:
        if not topic or not isinstance(topic, str):
            raise ValueError("Тема має бути непорожнім рядком")
        if not self.validate_style(style):
            raise ValueError(f"Невідомий стиль: {style}. Допустимі: {self.VALID_STYLES}")

        topic_lower = topic.lower()
        if "рецепт" in topic_lower or "паста" in topic_lower:
            if style == "інтригуючий":
                hook = "Секрет ідеальної пасти, про який мовчать кухарі..."
            elif style == "гумористичний":
                hook = "Готуєш пасту? Забудь про воду з-під крана!"
            else:
                hook = f"Як приготувати {topic} за 5 хвилин."
        elif "маркетинг" in topic_lower or "smm" in topic_lower:
            hook = "3 фішки, які піднімуть твій SMM на новий рівень." if style == "серйозний" else "Чому твої пости ніхто не читає? Відповідь у першому реченні."
        else:
            hook = f"{style.capitalize()} гачок на тему: {topic}"

        self.history.append({"topic": topic, "style": style, "hook": hook})
        return hook