# AShow
一款简易的隐蔽的A股实时股价监控桌面程序，简单配置后即可使用

## 配置文件如下所示
height：窗体的高
width：窗体的宽
opacity：窗体透明度，可按需配置，隐蔽性的保证
interval：调用间隔，单位秒
codes：股票代码，前缀sh代表上证，sz代表深证
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.6.1" />
    </startup>
    <appSettings>
      <add key="height" value ="40"/>
      <add key="width" value ="250"/>
      <add key="opacity" value ="1.0"/>
      <add key="interval" value ="2"/>
      <add key="codes" value ="sh600879"/>
    </appSettings>
</configuration>
