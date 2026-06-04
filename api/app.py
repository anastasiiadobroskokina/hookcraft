from flask import Flask, request, jsonify
from hook_generator import HookGenerator

app = Flask(__name__)

@app.route('/health', methods=['GET'])
def health():
    return jsonify({"status": "ok", "timestamp": __import__('datetime').datetime.utcnow().isoformat()})

@app.route('/generate', methods=['POST'])
def generate():
    data = request.get_json()
    if not data:
        return jsonify({"error": "Missing JSON body"}), 400
    topic = data.get('topic')
    style = data.get('style')
    if not topic or not style:
        return jsonify({"error": "Missing topic or style"}), 400
    try:
        gen = HookGenerator()
        hook = gen.generate_hook(topic, style)
        return jsonify({"hook": hook, "history": gen.history})
    except (ValueError, TypeError) as e:
        return jsonify({"error": str(e)}), 400

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)