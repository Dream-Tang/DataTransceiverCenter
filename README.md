# DataTransceiverCenter
C#自动化设备 上位机小程序  
星源西部装备-Mes自动打标机  
DTC实现的功能：  
1.视觉识别玻璃码，存入csv文件。DTC读取csv中的数据。  
2.将visionCode通过Mes通信发送，获取Mes回复的FogID。  
3.将FogID通过ZPL生成斑马打印机所需的指令文件，发送给打印机打印成条码。  
4.通知PLC已打印。PLC控制机械手取条码，并进行贴付。  
![image](https://github.com/Dream-Tang/DataTransceiverCenter/assets/24229806/847b8977-4d31-4ce2-b81c-380a38fc1380)
![软件操作](https://github.com/Dream-Tang/DataTransceiverCenter/assets/24229806/bfdd90b9-5542-4a53-a82b-691a800acec1)
