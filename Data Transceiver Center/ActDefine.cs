public class ActDefine
{
    /// CPU TYPE
    // QnACPU
    public const int CPU_Q2ACPU = 0x0011;           // Q2A
    public const int CPU_Q2AS1CPU = 0x0012;         // Q2AS1
    public const int CPU_Q3ACPU = 0x0013;           // Q3A
    public const int CPU_Q4ACPU = 0x0014;           // Q4A
                                                    // QCPU Q
    public const int CPU_Q02CPU = 0x0022;           // Q02(H) Q
    public const int CPU_Q06CPU = 0x0023;           // Q06H   Q
    public const int CPU_Q12CPU = 0x0024;           // Q12H   Q
    public const int CPU_Q25CPU = 0x0025;           // Q25H   Q
    public const int CPU_Q00JCPU = 0x0030;          // Q00J   Q
    public const int CPU_Q00CPU = 0x0031;           // Q00    Q
    public const int CPU_Q01CPU = 0x0032;           // Q01    Q
    public const int CPU_Q12PHCPU = 0x0041;         // Q12PHCPU Q
    public const int CPU_Q25PHCPU = 0x0042;         // Q25PHCPU Q
    public const int CPU_Q12PRHCPU = 0x0043;            // Q12PRHCPU Q
    public const int CPU_Q25PRHCPU = 0x0044;            // Q25PRHCPU Q
    public const int CPU_Q25SSCPU = 0x0055;         // Q25SS
    public const int CPU_Q03UDCPU = 0x0070;         // Q03UDCPU
    public const int CPU_Q04UDHCPU = 0x0071;            // Q04UDHCPU
    public const int CPU_Q06UDHCPU = 0x0072;            // Q06UDHCPU
    public const int CPU_Q02UCPU = 0x0083;          // Q02UCPU
    public const int CPU_Q03UDECPU = 0x0090;            // Q03UDECPU
    public const int CPU_Q04UDEHCPU = 0x0091;           // Q04UDEHCPU
    public const int CPU_Q06UDEHCPU = 0x0092;           // Q06UDEHCPU
    public const int CPU_Q13UDHCPU = 0x0073;            // Q13UDHCPU
    public const int CPU_Q13UDEHCPU = 0x0093;           // Q13UDEHCPU
    public const int CPU_Q26UDHCPU = 0x0074;            // Q26UDHCPU
    public const int CPU_Q26UDEHCPU = 0x0094;           // Q26UDEHCPU
    public const int CPU_Q02PHCPU = 0x0045;         // Q02PH  Q
    public const int CPU_Q06PHCPU = 0x0046;         // Q06PH  Q
    public const int CPU_Q00UJCPU = 0x0080;         // Q00UJCPU
    public const int CPU_Q00UCPU = 0x0081;          // Q00UCPU
    public const int CPU_Q01UCPU = 0x0082;          // Q01UCPU
    public const int CPU_Q10UDHCPU = 0x0075;            // Q10UDHCPU
    public const int CPU_Q20UDHCPU = 0x0076;            // Q20UDHCPU
    public const int CPU_Q10UDEHCPU = 0x0095;           // Q10UDEHCPU
    public const int CPU_Q20UDEHCPU = 0x0096;           // Q20UDEHCPU
    public const int CPU_Q50UDEHCPU = 0x0098;           // Q50UDEHCPU
    public const int CPU_Q100UDEHCPU = 0x009A;          // Q100UDEHCPU
    public const int CPU_Q03UDVCPU = 0x00D1;            // Q03UDVCPU
    public const int CPU_Q04UDVCPU = 0x00D2;            // Q04UDVCPU
    public const int CPU_Q04UDPVCPU = 0x0047;           // Q04UDPV
    public const int CPU_Q06UDVCPU = 0x00D3;            // Q06UDVCPU
    public const int CPU_Q06UDPVCPU = 0x0048;           // Q06UDPV
    public const int CPU_Q13UDVCPU = 0x00D4;            // Q13UDVCPU
    public const int CPU_Q13UDPVCPU = 0x0049;           // Q13UDPV
    public const int CPU_Q26UDVCPU = 0x00D5;            // Q26UDVCPU
    public const int CPU_Q26UDPVCPU = 0x004A;           // Q26UDPV
                                                        // ACPU
    public const int CPU_A0J2HCPU = 0x0102;         // A0J2H
    public const int CPU_A1FXCPU = 0x0103;          // A1FX
    public const int CPU_A1SCPU = 0x0104;           // A1S,A1SJ
    public const int CPU_A1SHCPU = 0x0105;          // A1SH,A1SJH
    public const int CPU_A1NCPU = 0x0106;           // A1(N)
    public const int CPU_A2CCPU = 0x0107;           // A2C,A2CJ
    public const int CPU_A2NCPU = 0x0108;           // A2(N),A2S
    public const int CPU_A2SHCPU = 0x0109;          // A2SH
    public const int CPU_A3NCPU = 0x010A;           // A3(N)
    public const int CPU_A2ACPU = 0x010C;           // A2A
    public const int CPU_A3ACPU = 0x010D;           // A3A
    public const int CPU_A2UCPU = 0x010E;           // A2U,A2US
    public const int CPU_A2USHS1CPU = 0x010F;           // A2USHS1
    public const int CPU_A3UCPU = 0x0110;           // A3U
    public const int CPU_A4UCPU = 0x0111;           // A4U
                                                    // QCPU A
    public const int CPU_Q02CPU_A = 0x0141;         // Q02(H)
    public const int CPU_Q06CPU_A = 0x0142;         // Q06H
                                                    // LCPU
    public const int CPU_L02CPU = 0x00A1;           // L02CPU
    public const int CPU_L26CPUBT = 0x00A2;         // L26CPU-BT
    public const int CPU_L02SCPU = 0x00A3;          // L02SCPU
    public const int CPU_L26CPU = 0x00A4;           // L26CPU
    public const int CPU_L06CPU = 0x00A5;           // L06CPU
                                                    // C Controller
    public const int CPU_Q12DC_V = 0x0058;          // Q12DCCPU-V
    public const int CPU_Q24DHC_V = 0x0059;         // Q24DHCCPU-V
    public const int CPU_Q24DHC_LS = 0x005B;			// Q24DHC LS
    public const int CPU_Q24DHC_VG = 0x005C;	        // Q24DHCCPU-VG
    public const int CPU_Q26DHC_LS = 0x005D;            // Q26DHCCPU-LS
                                                        // Q MOTION
    public const int CPU_Q172CPU = 0x0621;          // Q172CPU
    public const int CPU_Q173CPU = 0x0622;          // Q173CPU
    public const int CPU_Q172HCPU = 0x0621;         // Q172HCPU
    public const int CPU_Q173HCPU = 0x0622;         // Q173HCPU
    public const int CPU_Q172DCPU = 0x0625;         // Q172DCPU
    public const int CPU_Q173DCPU = 0x0626;         // Q173DCPU
    public const int CPU_Q172DSCPU = 0x062A;            // Q172DSCPU
    public const int CPU_Q173DSCPU = 0x062B;            // Q173DSCPU
                                                        // QSCPU
    public const int CPU_QS001CPU = 0x0060;         // QS001
                                                    // FXCPU
    public const int CPU_FX0CPU = 0x0201;           // FX0/FX0S
    public const int CPU_FX0NCPU = 0x0202;          // FX0N
    public const int CPU_FX1CPU = 0x0203;           // FX1
    public const int CPU_FX2CPU = 0x0204;           // FX2/FX2C
    public const int CPU_FX2NCPU = 0x0205;          // FX2N/FX2NC
    public const int CPU_FX1SCPU = 0x0206;          // FX1S
    public const int CPU_FX1NCPU = 0x0207;          // FX1N/FX1NC
    public const int CPU_FX3SCPU = 0x020A;          // FX3S
    public const int CPU_FX3UCCPU = 0x0208;         // FX3U/FX3UC
    public const int CPU_FX3GCPU = 0x0209;          // FX3G/FX3GC
                                                    // BOARD
    public const int CPU_BOARD = 0x0401;            // NETWORK BOARD
                                                    // MOTION
    public const int CPU_A171SHCPU = 0x0601;            // A171SH
    public const int CPU_A172SHCPU = 0x0602;            // A172SH
    public const int CPU_A273UHCPU = 0x0603;            // A273UH
    public const int CPU_A173UHCPU = 0x0604;            // A173UH
                                                        // GOT
    public const int CPU_A900GOT = 0x0701;          // A900GOT

    // iQ-R CPU
    public const int CPU_R04CPU = 0x1001;			// R04CPU
    public const int CPU_R08CPU = 0x1002;			// R08CPU
    public const int CPU_R16CPU = 0x1003;			// R16CPU
    public const int CPU_R32CPU = 0x1004;			// R32CPU
    public const int CPU_R120CPU = 0x1005;			// R120CPU
    public const int CPU_R00CPU = 0x1201;			// R00CPU
    public const int CPU_R01CPU = 0x1202;			// R01CPU
    public const int CPU_R02CPU = 0x1203;			// R02CPU

    // iQ-R PROCESS CPU
    public const int CPU_R08PCPU = 0x1102;			// R08PCPU
    public const int CPU_R16PCPU = 0x1103;			// R16PCPU
    public const int CPU_R32PCPU = 0x1104;			// R32PCPU
    public const int CPU_R120PCPU = 0x1105;			// R120PCPU

    // iQ-R SAFE CPU
    public const int CPU_R08SFCPU = 0x1122;			// R08SFCPU
    public const int CPU_R16SFCPU = 0x1123;			// R16SFCPU
    public const int CPU_R32SFCPU = 0x1124;			// R32SFCPU
    public const int CPU_R120SFCPU = 0x1125;			// R120SFCPU

    // iQ-R EN CPU
    public const int CPU_R04ENCPU = 0x1008;	        // R04ENCPU
    public const int CPU_R08ENCPU = 0x1009;	        // R08ENCPU
    public const int CPU_R16ENCPU = 0x100A;	        // R32ENCPU
    public const int CPU_R32ENCPU = 0x100B;	        // R64ENCPU
    public const int CPU_R120ENCPU = 0x100C;            // R120ENCPU

    // iQ-R Motion
    public const int CPU_R16MTCPU = 0x1011;			// R16MTCPU.
    public const int CPU_R32MTCPU = 0x1012;			// R32MTCPU.

    // iQ-R CCONTROLLER
    public const int CPU_R12CCPU_V = 0x1021;           // R12CCPU-V.

    // iQ-R PSF CPU
    public const int CPU_R08PSFCPU = 0x1111;				// R08PSFCPU
    public const int CPU_R16PSFCPU = 0x1112;				// R16PSFCPU
    public const int CPU_R32PSFCPU = 0x1113;				// R32PSFCPU
    public const int CPU_R120PSFCPU = 0x1114;			// R120PSFCPU

    // iQ-L CPU
    public const int CPU_L04HCPU = 0x1211;				// L04HCPU
    public const int CPU_L08HCPU = 0x1212;				// L08HCPU
    public const int CPU_L16HCPU = 0x1213;              // L16HCPU

    // iQ-F CPU
    public const int CPU_FX5UCPU = 0x0210;			//  FX5U CPU
    public const int CPU_FX5UJCPU = 0x0211;         //  FX5UJ CPU

    /// PORT
    public const int PORT_1 = 0x01;         // CommunicationPort1
    public const int PORT_2 = 0x02;         // CommunicationPort2
    public const int PORT_3 = 0x03;         // CommunicationPort3
    public const int PORT_4 = 0x04;         // CommunicationPort4
    public const int PORT_5 = 0x05;         // CommunicationPort5
    public const int PORT_6 = 0x06;         // CommunicationPort6
    public const int PORT_7 = 0x07;         // CommunicationPort7
    public const int PORT_8 = 0x08;         // CommunicationPort8
    public const int PORT_9 = 0x09;         // CommunicationPort9
    public const int PORT_10 = 0x0A;            // CommunicationPort10


    /// BAUDRATE
    public const int BAUDRATE_300 = 300;            // 300bps
    public const int BAUDRATE_600 = 600;            // 600bps
    public const int BAUDRATE_1200 = 1200;              // 1200bps
    public const int BAUDRATE_2400 = 2400;              // 2400bps
    public const int BAUDRATE_4800 = 4800;              // 4800bps
    public const int BAUDRATE_9600 = 9600;              // 9600bps
    public const int BAUDRATE_19200 = 19200;            // 19200bps
    public const int BAUDRATE_38400 = 38400;            // 38400bps
    public const int BAUDRATE_57600 = 57600;            // 57600bps
    public const int BAUDRATE_115200 = 115200;          // 115200bps

    /// DATA BIT
    public const int DATABIT_7 = 7;         // DATA BIT 7
    public const int DATABIT_8 = 8;         // DATA BIT 8

    /// PARITY
    public const int NO_PARRITY = 0;            // NO PARITY
    public const int ODD_PARITY = 1;            // ODD PARITY
    public const int EVEN_PARITY = 2;           // EVEN PARITY

    /// STOP BITS
    public const int STOPBIT_ONE = 0;           // 1 STOP BIT
    public const int STOPBIT_TWO = 2;           // 2 STOP BIT


    /// SERIAL CONTROL
    public const int TRC_DTR = 0x01;        // DTR
    public const int TRC_RTS = 0x02;        // RTS
    public const int TRC_DTR_AND_RTS = 0x07;        // DTR and RTS
    public const int TRC_DTR_OR_RTS = 0x08;     // DTR or RTS


    /// SUM CHECK
    public const int SUM_CHECK = 1;         // Sum Check
    public const int NO_SUM_CHECK = 0;          // No Sum Check


    ///PACKET TYPE
    public const int PACKET_ASCII = 0x02;       //PACKET TYPE ASCII
    public const int PACKET_BINARY = 0x03;		//PACKET TYPE BINARY

    ///DELIMITER
    public const int CRLF_NONE = 0;		    // CR/LF None
    public const int CRLF_CR = 1;		    // CR ONLY
    public const int CRLF_CRLF = 2;		    // CR/LF

    ///CONNECT WAY
	public const int TEL_AUTO_CONNECT = 0x00;   // AUTO LINE CONNECT	
    public const int TEL_AUTO_CALLBACK = 0x01;  // AUTO LINE CONNECT(CALLBACK FIXATION)
    public const int TEL_AUTO_CALLBACK_NUMBER = 0x02;   // AUTO LINE CONNECT(CALLBACK NUMBER SPECIFICATION)
    public const int TEL_CALLBACK = 0x03;   // CALLBACK CONNECT(FIXATION)
    public const int TEL_CALLBACK_NUMBER = 0x04;    // CALLBACK CONNECT(NUMBER SPECIFICATION)
    public const int TEL_CALLBACK_REQUEST = 0x05;   // CALLBACK REQUEST(FIXATION)
    public const int TEL_CALLBACK_REQUEST_NUMBER = 0x06;    // CALLBACK REQUEST(NUMBER SPECIFICATION)
    public const int TEL_CALLBACK_WAIT = 0x07;  // CALLBACK RECEPTION WAITING

    ///LINE TYPE
    public const int LINETYPE_PULSE = 0x00;         // PULSE
    public const int LINETYPE_TONE = 0x01;          // TONE
    public const int LINETYPE_ISDN = 0x02;          // ISDN

    ///GOT TRANSPARENT PC IF
    public const int GOT_PCIF_USB = 1;              // USB
    public const int GOT_PCIF_SERIAL = 2;               // SERIAL
    public const int GOT_PCIF_ETHERNET = 3;             // ETHERNET

    ///GOT TRANSPARENT PLC IF					
    public const int GOT_PLCIF_SERIAL_QCPUQ = 1;            // SERIAL-QCPU Q
    public const int GOT_PLCIF_SERIAL_QCPUA = 2;            // SERIAL-QCPU A
    public const int GOT_PLCIF_SERIAL_QNACPU = 3;           // SERIAL-QnACPU
    public const int GOT_PLCIF_SERIAL_ACPU = 4;         // SERIAL-ACPU
    public const int GOT_PLCIF_SERIAL_FXCPU = 5;            // SERIAL-FXCPU
    public const int GOT_PLCIF_SERIAL_LCPU = 6;         // SERIAL-LCPU
    public const int GOT_PLCIF_SERIAL_QJ71C24 = 30;         // SERIAL-QJ71C24
    public const int GOT_PLCIF_SERIAL_LJ71C24 = 31;         // SERIAL-LJ71C24
    public const int GOT_PLCIF_ETHERNET_QJ71E71 = 50;           // ETHERNET-QJ71E71
    public const int GOT_PLCIF_ETHERNET_CCIEFADP = 60;          // ETHERNET-CC IE Field adapter
    public const int GOT_PLCIF_ETHERNET_QCPU = 70;          // ETHERNET-QCPU
    public const int GOT_PLCIF_ETHERNET_LCPU = 71;          // ETHERNET-LCPU
    public const int GOT_PLCIF_BUS = 90;            // BUS

    ///ACTPROGTYPE UNITTYPE
    public const int UNIT_QNCPU = 0x13;			// SERIAL(RS232C)-QCPU Q
    public const int UNIT_FXCPU = 0x0F;			// SERIAL(RS232C)-FXCPU
    public const int UNIT_LNCPU = 0x50;			// SERIAL(RS232C)-LCPU
    public const int UNIT_QNMOTION = 0x1C;			// SERIAL(RS232C)-QMOTION
    public const int UNIT_QJ71C24 = 0x19;			// SERIAL(C24)-QCPU
    public const int UNIT_FX485BD = 0x24;			// SERIAL(FX485BD)-FXCPU
    public const int UNIT_LJ71C24 = 0x54;			// SERIAL(C24)-LCPU
    public const int UNIT_QJ71E71 = 0x1A;			// Ethernet(QJ71E71)
    public const int UNIT_FXENET = 0x26;			// Ethernet(FXENET)
    public const int UNIT_FXENET_ADP = 0x27;			// Ethernet(FX1N-ENET-ADP)
    public const int UNIT_QNETHER = 0x2C;			// Ethernet(QCPU) IP
    public const int UNIT_QNETHER_DIRECT = 0x2D;			// Ethernet(QCPU) DIRECT
    public const int UNIT_LNETHER = 0x52;			// Ethernet(LCPU) IP
    public const int UNIT_LNETHER_DIRECT = 0x53;			// Ethernet(LCPU) DIRECT
    public const int UNIT_NZ2GF_ETB = 0x59;			// NZ2GF-ETB
    public const int UNIT_NZ2GF_ETB_DIRECT = 0x5A;			// NZ2GF-ETB DIRECT
    public const int UNIT_QNUSB = 0x16;			// USB-QCPU
    public const int UNIT_LNUSB = 0x51;			// USB-LCPU
    public const int UNIT_QNMOTIONUSB = 0x1D;			// USB-QMOTION
    public const int UNIT_G4QNCPU = 0x1B;			// G4-QCPU
    public const int UNIT_CCLINKBOARD = 0x0C;			// CC-Link Board
    public const int UNIT_MNETHBOARD = 0x1E;			// MELSECNET/H Board
    public const int UNIT_MNETGBOARD = 0x2B;			// CC-Link IE Control Board
    public const int UNIT_CCIEFBOARD = 0x2F;			// CC-Link IE Field Board
    public const int UNIT_SIMULATOR = 0x0B;         // GX Simulator
    public const int UNIT_SIMULATOR2 = 0x30;			// GX Simulator2
    public const int UNIT_QBF = 0x1F;			// QBF
    public const int UNIT_QSS = 0x20;           // Qn SoftLogic
    public const int UNIT_A900GOT = 0x21;			// GOT
    public const int UNIT_GOT_QJ71E71 = 0x40;			// GOT Transparent QJ71E71
    public const int UNIT_GOT_QNETHER = 0x41;			// GOT Transparent Ethernet(QCPU)
    public const int UNIT_GOT_LNETHER = 0x55;			// GOT Transparent Ethernet(LCPU)
    public const int UNIT_GOT_NZ2GF_ETB = 0x5B;			// GOT Transparent NZ2GF-ETB
    public const int UNIT_GOTETHER_QNCPU = 0x56;			// GOT Transparent ETHERNET-QCPU
    public const int UNIT_GOTETHER_QBUS = 0x58;			// GOT Transparent ETHERNET-QBUS
    public const int UNIT_GOTETHER_LNCPU = 0x57;			// GOT Transparent ETHERNET-LCPU
    public const int UNIT_FXETHER = 0x4A;			// EthernetADP-FXCPU
    public const int UNIT_FXETHER_DIRECT = 0x4B;			// EthernetADP-FXCPU(DIRECT)
    public const int UNIT_GOTETHER_FXCPU = 0x60;			// GOT Transparent Ethernet(FXCPU)
    public const int UNIT_GOT_FXETHER = 0x61;			// GOT Transparent FX3U-ENET-ADP
    public const int UNIT_GOT_FXENET = 0x62;			// GOT FX3U-ENET(-L)
    public const int UNIT_RJ71C24 = 0x1000;		// SERIAL(C24)-RCPU
    public const int UNIT_RJ71EN71 = 0x1001;		// Ethernet(RJ71EN71)
    public const int UNIT_RETHER = 0x1002;		// Ethernet(RCPU) IP
    public const int UNIT_RETHER_DIRECT = 0x1003;		// Ethernet(RCPU) DIRECT
    public const int UNIT_RUSB = 0x1004;		// USB-RCPU
    public const int UNIT_RJ71EN71_DIRECT = 0x1005;		// Ethernet(RJ71EN71) DIRECT
    public const int UNIT_RJ71GN11 = 0x1006;		// RJ71GN11
    public const int UNIT_RJ71GN11_DIRECT = 0x1007;		// RJ71GN11 DIRECT
    public const int UNIT_GOT_RJ71EN71 = 0x1051;		// GOT Transparent RJ71EN71
    public const int UNIT_GOT_RETHER = 0x1052;		// GOT Transparent Ethernet(RCPU)
    public const int UNIT_GOTETHER_RJ71C24 = 0x1061;		// GOT Transparent ETHERNET-SERIAL(C24)-RCPU
    public const int UNIT_FXVCPU = 0x2000;		// SERIAL(RS232C)-FX5CPU
    public const int UNIT_FXVETHER = 0x2001;		// Ethernet(FX5CPU) IP
    public const int UNIT_FXVETHER_DIRECT = 0x2002;		// Ethernet(FX5CPU) DIRECT
    public const int UNIT_SIMULATOR3 = 0x31;			// GX Simulator3
    public const int UNIT_GOT_FXVCPU = 0x2005;		// GOT Transparent SERIAL(FX5U)
    public const int UNIT_GOTETHER_FXVCPU = 0x2006;		// GOT Transparent Ethernet-FX5U
    public const int UNIT_GOT_FXVETHER = 0x2007;		// GOT Transparent ETHERNET(FX5U)
    public const int UNIT_LJ71E71 = 0x5C;			// Ethernet(LJ71E71)
    public const int UNIT_GOT_LJ71E71 = 0x5D;			// GOT Transparent LJ71E71
    public const int UNIT_GOTETHER_QN_ETHER = 0x6F;			// GOT Transparent(Ether-GOT-Ether-QnCPU)
    public const int UNIT_LHETHER = 0x1002;		// Ethernet(LHCPU) IP
    public const int UNIT_LHETHER_DIRECT = 0x1003;		// Ethernet(LHCPU) DIRECT
    public const int UNIT_LHUSB = 0x1004;		// USB-LHCPU
    public const int UNIT_FXVUSB = 0x200C;      // USB-FX5CPU

    ///ACTPROGTYPE PROTOCOLTYPE
    public const int PROTOCOL_SERIAL = 0x04;			// Protocol Serial
    public const int PROTOCOL_USB = 0x0D;			// Protocol USB
    public const int PROTOCOL_TCPIP = 0x05;			// Protocol TCP/IP
    public const int PROTOCOL_UDPIP = 0x08;			// Protocol UDP/IP
    public const int PROTOCOL_MNETH = 0x0F;			// Protocol MELSECNET/H
    public const int PROTOCOL_MNETG = 0x14;			// Protocol CC IE Control Board
    public const int PROTOCOL_CCIEF = 0x15;			// Protocol CC IE Field Board
    public const int PROTOCOL_CCLINK = 0x07;			// Protocol CC-LINK Board
    public const int PROTOCOL_SERIALMODEM = 0x0E;			// Protocol MODEM
    public const int PROTOCOL_TEL = 0x0A;			// Protocol TEL
    public const int PROTOCOL_QBF = 0x10;			// Protocol QBF
    public const int PROTOCOL_QSS = 0x11;			// Protocol QSS
    public const int PROTOCOL_USBGOT = 0x13;			// Protocol GOT TRANSPARENT USB
    public const int PROTOCOL_SHAREDMEMORY = 0x06;			// Protocol Simulator

    ///ACTPROGTYPE INVERTER PROTOCOLTYPE
    public const int COMM_RS232C = 0x00;	        // Serial INVERTER
    public const int COMM_USB = 0x01;           // USB INVERTER

    ///ACTPROGTYPE ROBOTCONTROLLER PROTOCOLTYPE
    public const int RC_PROTOCOL_SERIAL = 0x01;				// Serial Robot Controller
    public const int RC_PROTOCOL_USB = 0x04;				// USB Robot Controller
    public const int RC_PROTOCOL_TCPIP = 0x02;              // TCP/IP Robot Controller

    ///INVERTER
    public const int INV_A800 = 0x1E60;         // A800
    ///ROBOT
    public const int RT_CRD700 = 0x0003;            // CRnD-7xx
    ///PACKET
    public const int PACKET_PLC1 = 0x0001;			// CPU Protocol Type

}
