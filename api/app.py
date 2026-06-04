from flask import Flask, request, jsonify
from hook_generator import HookGenerator

app = Flask(__name__)
generator = HookGenerator()

@app.route('/health', methods=['GET'])
def health():
    return jsonify({"status": "ok"}), 200

@app.route('/generate', methods=['POST'])
def generate():
    try:
        data = request.get_json()
        if not data:
            return jsonify({"error": "Missing JSON body"}), 400
        
        topic = data.get('topic')
        style = data.get('style')
        
        if not topic or not style:
            return jsonify({"error": "Missing 'topic' or 'style'"}), 400
        
        hook = generator.generate_hook(topic, style)
        return jsonify({
            "hook": hook,
            "history": generator.history[-5:]
        }), 200
    except Exception as e:
        return jsonify({"error": str(e)}), 400

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)
