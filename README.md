# DataTransceiverCenter
C#自动化设备 上位机小程序  
星源西部装备-Mes自动打标机  
DTC实现的功能：  
1.视觉识别玻璃码，存入csv文件。DTC读取csv中的数据。  
2.将visionCode通过Mes通信发送，获取Mes回复的FogID。  
3.将FogID通过ZPL生成斑马打印机所需的指令文件，发送给打印机打印成条码。  
4.通知PLC已打印。PLC控制机械手取条码，并进行贴付。  
![image](https://github.com/Dream-Tang/DataTransceiverCenter/blob/dev2/操作演示.gif)
