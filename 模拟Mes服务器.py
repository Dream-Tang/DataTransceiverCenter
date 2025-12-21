from http.server import HTTPServer, BaseHTTPRequestHandler
import json
from datetime import datetime  # 使用datetime模块获取时间（更稳定）

class JSONHandler(BaseHTTPRequestHandler):
    def do_GET(self):
        self.send_response(200)
        self.send_header('Content-type', 'text/html; charset=utf-8')
        self.end_headers()
        guide = """
        <html>
            <body>
                <h1>JSON POST服务器（已修复）</h1>
                <p>请发送JSON格式的POST请求，示例：</p>
                <pre>
curl -X POST ^
  -H "Content-Type: application/json" ^
  -d '{"name":"测试","data":[1,2,3]}' ^
  http://你的IP:8000
                </pre>
            </body>
        </html>
        """
        self.wfile.write(guide.encode('utf-8'))

    def do_POST(self):
        try:
            # 1. 检查Content-Type是否为JSON
            if self.headers.get('Content-Type') != 'application/json':
                self.send_response(415)
                self.send_header('Content-type', 'application/json')
                self.end_headers()
                response = json.dumps({
                    "status": "error",
                    "message": "仅支持application/json格式",
                    "received_type": self.headers.get('Content-Type') or "未指定"
                })
                self.wfile.write(response.encode('utf-8'))
                return

            # 2. 检查并获取Content-Length（修复可能的KeyError）
            content_length_header = self.headers.get('Content-Length')
            if not content_length_header:
                self.send_response(400)
                self.send_header('Content-type', 'application/json')
                self.end_headers()
                response = json.dumps({
                    "status": "error",
                    "message": "缺少Content-Length头"
                })
                self.wfile.write(response.encode('utf-8'))
                return

            content_length = int(content_length_header)
            if content_length <= 0:
                self.send_response(400)
                self.send_header('Content-type', 'application/json')
                self.end_headers()
                response = json.dumps({
                    "status": "error",
                    "message": "Content-Length必须大于0"
                })
                self.wfile.write(response.encode('utf-8'))
                return

            # 3. 读取并解析JSON数据
            post_data = self.rfile.read(content_length).decode('utf-8')
            json_data = json.loads(post_data)

            # 从解析后的json数据中，提取需要的字段值
            specific_value = json_data.get("input").get("panelId")
            if not specific_value or specific_value == "":
                specific_value = "Test001"

            # 4. 构建成功响应（使用datetime替代log_date_time_string）
            self.send_response(200)
            self.send_header('Content-type', 'application/json')
            self.end_headers()
            response = json.dumps({
                "code":200,
                "info":"上传成功",
                "received_at": datetime.now().strftime('%Y-%m-%d %H:%M:%S'),  # 更稳定的时间格式
                "data": specific_value
            },indent=2,ensure_ascii=False)
            self.wfile.write(response.encode('utf-8'))

        except json.JSONDecodeError as e:
            # JSON解析错误
            self.send_response(400)
            self.send_header('Content-type', 'application/json')
            self.end_headers()
            response = json.dumps({
                "status": "error",
                "message": f"无效的JSON格式: {str(e)}"
            })
            self.wfile.write(response.encode('utf-8'))
        except Exception as e:
            # 捕获所有其他异常，并打印到控制台（方便调试）
            print(f"服务器错误详情: {str(e)}")  # 关键：在控制台显示错误原因
            self.send_response(500)
            self.send_header('Content-type', 'application/json')
            self.end_headers()
            response = json.dumps({
                "status": "error",
                "message": f"服务器内部错误: {str(e)}"
            })
            self.wfile.write(response.encode('utf-8'))

if __name__ == '__main__':
    import sys
    port = int(sys.argv[1]) if len(sys.argv) > 1 else 8000
    server_address = ('', port)
    httpd = HTTPServer(server_address, JSONHandler)
    print(f"修复版JSON服务器启动在端口 {port}...")
    print(f"控制台将显示详细错误信息，方便调试")
    httpd.serve_forever()


# cmd命令测试

# 一行写法
# curl -X POST -H "Content-Type: application/json" -d "{\"name\":\"测试\",\"value\":123}" http://你的服务器IP:8000

# 或者使用分行写法
# curl -X POST ^
#   -H "Content-Type: application/json" ^
#   -d "{\"name\":\"测试\",\"value\":123,\"items\":[\"a\",\"b\"]}" ^
#   http://你的服务器IP:8000

