from flask import Flask, request, jsonify

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
    # Повертаємо тестову відповідь (без реальної генерації)
    return jsonify({"hook": f"Ваш гачок: {topic} - {style}"}), 200
