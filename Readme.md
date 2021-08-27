<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128568164/12.1.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4142)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [MainPage.xaml](./CS/CustomSeriesPointTooltip/MainPage.xaml) (VB: [MainPage.xaml](./VB/CustomSeriesPointTooltip/MainPage.xaml))
* [MainPage.xaml.cs](./CS/CustomSeriesPointTooltip/MainPage.xaml.cs) (VB: [MainPage.xaml.vb](./VB/CustomSeriesPointTooltip/MainPage.xaml.vb))
<!-- default file list end -->
# How to provide a custom tooltip for a series point


<p>This example shows how to implement custom tooltip that displays another chart with a GDP history for the selected country when hovering over a bar. </p><p><br />
To accomplish this, it is necessary to create the <strong>System.Windows.DataTemplate</strong> object that specifies the custom tooltip appearance, and assign it to the <a href="http://help.devexpress.com/#Silverlight/DevExpressXpfChartsSeries_ToolTipPointTemplatetopic"><u>Series.ToolTipPointTemplate</u></a> property.  </p><p>You also need to bind both charts to the GDP datasource and write the GetDataSource() and GetGDPs() methods. These methods allow you to get the GDP data from a datasource  for each selected country to display it on a chart tooltip. </p><br />


<br/>


