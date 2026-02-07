namespace Data_Transceiver_Center.Interfaces
{
    /// <summary>
    /// 抽象数据提供器，定义CheckHelper需要的数据获取方法
    /// </summary>
    public interface IDataProvider
    {
        // 获取扫码码
        string GetScnCode();

        // 获取打印码
        string GetPrtCode();

        // 获取忽略校验标志
        bool GetIgnoreCheck();

    }
}
