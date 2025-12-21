namespace Data_Transceiver_Center
{
    // 处理ini配置的接口
    public interface IIniConfigurable
    {
        void LoadIni(string iniFilePath);
    }

    // 处理json配置的接口
    public interface IJsonConfigurable
    {
        void LoadJson(string jsonFilePath);
    }

    // 主窗体配置接口（处理Checkbox等主窗体专属配置）
    public interface IMainConfigurable
    {
        void LoadMainConfig(string iniFilePath);
    }

    // 新增接口定义（可放在单独的接口文件中）
    public interface IIniSavable
    {
        void SaveIni(string iniFilePath);
    }

    public interface IJsonSavable
    {
        void SaveJson(string jsonFilePath);
    }
}
