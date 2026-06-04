from flask import Flask, request, jsonify
from hook_generator import HookGenerator

app = Flask(__name__)

# Створюємо екземпляр генератора (можна використовувати один на весь застосунок)
generator = HookGenerator()

@app.route('/health', methods=['GET'])
def health():
    """Endpoint для перевірки стану сервісу."""
    return jsonify({"status": "ok"}), 200

@app.route('/generate', methods=['POST'])
def generate():
    """Endpoint для генерації гачка за темою та стилем."""
    # Перевіряємо, чи є JSON у тілі запиту
    if not request.is_json:
        return jsonify({"error": "Missing JSON body"}), 400
    
    data = request.get_json()
    topic = data.get('topic')
    style = data.get('style')
    
    # Валідація обов'язкових полів
    if not topic:
        return jsonify({"error": "Missing 'topic' field"}), 400
    if not style:
        return jsonify({"error": "Missing 'style' field"}), 400
    
    try:
        # Генеруємо гачок
        hook = generator.generate_hook(topic, style)
        # Повертаємо результат разом з історією (опціонально)
        return jsonify({
            "hook": hook,
            "history": generator.history[-5:]  # останні 5 записів
        }), 200
    except (ValueError, TypeError) as e:
        return jsonify({"error": str(e)}), 400
    except Exception as e:
        # На випадок непередбачених помилок
        return jsonify({"error": f"Internal server error: {str(e)}"}), 500

if __name__ == '__main__':
    # Тільки для локального запуску; на продакшені використовується gunicorn
    app.run(host='0.0.0.0', port=5000, debug=False)
