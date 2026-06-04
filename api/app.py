from flask import Flask, request, jsonify
import json

app = Flask(__name__)

@app.route('/health', methods=['GET'])
def health():
    return jsonify({"status": "ok"})

@app.route('/generate', methods=['POST'])
def generate():
    data = request.get_json()
    if not data:
        return jsonify({"error": "Missing JSON body"}), 400
    topic = data.get('topic')
    style = data.get('style')
    if not topic or not style:
        return jsonify({"error": "Missing topic or style"}), 400
    # Формуємо рядок з нормальними літерами
    hook_text = f"Ваш гачок: {topic} - {style}"
    # Використовуємо json.dumps з ensure_ascii=False
    response = json.dumps({"hook": hook_text}, ensure_ascii=False)
    return response, 200, {'Content-Type': 'application/json; charset=utf-8'}

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)
