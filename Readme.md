<!-- default file list -->
*Files to look at*:

* [MainPage.xaml](./CS/CustomSeriesPointTooltip/MainPage.xaml) (VB: [MainPage.xaml](./VB/CustomSeriesPointTooltip/MainPage.xaml))
* [MainPage.xaml.cs](./CS/CustomSeriesPointTooltip/MainPage.xaml.cs) (VB: [MainPage.xaml](./VB/CustomSeriesPointTooltip/MainPage.xaml))
<!-- default file list end -->
# How to provide a custom tooltip for a series point


<p>This example shows how to implement custom tooltip that displays another chart with a GDP history for the selected country when hovering over a bar. </p><p><br />
To accomplish this, it is necessary to create the <strong>System.Windows.DataTemplate</strong> object that specifies the custom tooltip appearance, and assign it to the <a href="http://help.devexpress.com/#Silverlight/DevExpressXpfChartsSeries_ToolTipPointTemplatetopic"><u>Series.ToolTipPointTemplate</u></a> property.  </p><p>You also need to bind both charts to the GDP datasource and write the GetDataSource() and GetGDPs() methods. These methods allow you to get the GDP data from a datasource  for each selected country to display it on a chart tooltip. </p><br />


<br/>


