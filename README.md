# DataTransceiverCenter
C#自动化设备 上位机小程序  
西部装备-Mes自动打标机  
DTC实现的功能：  
1.相机拍摄玻璃二维码，其结果通过TCP传输给软件。  
2.玻璃码通过http协议，与Mes通信，获取Mes回复的FogID。  
3.FogID按照ZPL模板，生成斑马打印机所需的指令文件，发送给打印机打印成条码。  
4.通知PLC已打印。PLC控制机械手取条码，并进行贴付。  
![image](https://github.com/Dream-Tang/DataTransceiverCenter/blob/dev2/操作演示.gif)
5.扫码枪校验打印的条码，输出校验结果给PLC。  
  
  
  
使用的C#知识点记录：  
1.文件操作，读取，修改，保存，复制移动（ZPL生成功能，发送打印机功能）  
2.http数据收发，Json数据解析（MES通信功能）  
3.PLC通信，三菱官方组件方案MX component（PLC通信）  
4.串口通信，system.IO（扫码枪验码）  
5.TCP数据接收，MS官方例程TcpListener（接收相机数据）  
6.多线程，task（后台多线程）  
7.委托，Action，methodInvoker（跨线程更新界面）  
8.lamda表达式。（task，action，methodInvoker等  
9.ini配置文件，读和写（配置加载和保存功能）  
10.自动流程，同步多线程，异步多线程，async await task  
11.二维码生成，zxing库  
12.代码打包部署工具，VS扩展工具Microsoft visual studio installer projects  
