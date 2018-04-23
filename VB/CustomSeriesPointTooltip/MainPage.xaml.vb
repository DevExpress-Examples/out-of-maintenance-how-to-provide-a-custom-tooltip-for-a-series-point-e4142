Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Resources
Imports System.Globalization
Imports System.Windows.Controls
Imports System.Xml.Linq

Namespace CustomSeriesPointTooltip
	Partial Public Class MainPage
		Inherits UserControl
		Public Sub New()
			InitializeComponent()
			chart.DataSource = GetDataSource()
		End Sub

		Private Function GetDataSource() As List(Of G8Member)
			Dim GDPs As List(Of GDP) = GetGDPs()
			Dim countries As New List(Of G8Member)()
			Const yearsInDecade As Integer = 10
			For countryCounter As Integer = 0 To 7
				Dim countryGDPs As New List(Of GDP)()
				For countryValuesCounter As Integer = 0 To yearsInDecade - 1
					countryGDPs.Add(GDPs(countryCounter * yearsInDecade + countryValuesCounter))
				Next countryValuesCounter
				countries.Add(New G8Member(countryGDPs))
			Next countryCounter
			Return countries
		End Function

		Private Function GetGDPs() As List(Of GDP)
			Dim document As XDocument = DataLoader.LoadXmlFromResources("/Data/GDPofG8.xml")
			Dim result As New List(Of GDP)()
			If document IsNot Nothing Then
				For Each element As XElement In document.Element("G8GDPs").Elements()
					Dim country As String = element.Element("Country").Value
					Dim year As Integer = Integer.Parse(element.Element("Year").Value)
					Dim product As Decimal = Convert.ToDecimal(element.Element("Product").Value, CultureInfo.InvariantCulture)
					result.Add(New GDP(country, year, product))
				Next element
			End If
			Return result
		End Function
		Public NotInheritable Class DataLoader
			Private Sub New()
			End Sub
			Public Shared Function LoadXmlFromResources(ByVal fileName As String) As XDocument
				Try
					fileName = "/CustomSeriesPointTooltip;component" & fileName
					Dim uri As New Uri(fileName, UriKind.RelativeOrAbsolute)
					Dim info As StreamResourceInfo = Application.GetResourceStream(uri)
					Return XDocument.Load(info.Stream)
				Catch
					Return Nothing
				End Try
			End Function
		End Class

		Private Sub ChartToolTipController_ToolTipOpening(ByVal sender As Object, ByVal e As DevExpress.Xpf.Charts.ChartToolTipEventArgs)
			Dim toolTipData As ToolTipData = TryCast(e.Hint, ToolTipData)
			Dim colorNumber As Integer = e.Series.Points.IndexOf(e.SeriesPoint)
			Dim seriesColor As Color = e.ChartControl.Palette(colorNumber)
			toolTipData.SeriesBrush = New SolidColorBrush(seriesColor)
		End Sub
	End Class

	Public Class GDP
		Private privateCountry As String
		Public Property Country() As String
			Get
				Return privateCountry
			End Get
			Private Set(ByVal value As String)
				privateCountry = value
			End Set
		End Property
		Private privateYear As Integer
		Public Property Year() As Integer
			Get
				Return privateYear
			End Get
			Private Set(ByVal value As Integer)
				privateYear = value
			End Set
		End Property
		Private privateProduct As Decimal
		Public Property Product() As Decimal
			Get
				Return privateProduct
			End Get
			Private Set(ByVal value As Decimal)
				privateProduct = value
			End Set
		End Property

		Public Sub New(ByVal country As String, ByVal year As Integer, ByVal product As Decimal)
			Country = country
			Year = year
			Product = product
		End Sub
	End Class

	Public Class G8Member
		Private privateGDPin2010 As Decimal
		Public Property GDPin2010() As Decimal
			Get
				Return privateGDPin2010
			End Get
			Private Set(ByVal value As Decimal)
				privateGDPin2010 = value
			End Set
		End Property
		Private privateCountryName As String
		Public Property CountryName() As String
			Get
				Return privateCountryName
			End Get
			Private Set(ByVal value As String)
				privateCountryName = value
			End Set
		End Property
		Private privateToolTipData As ToolTipData
		Public Property ToolTipData() As ToolTipData
			Get
				Return privateToolTipData
			End Get
			Set(ByVal value As ToolTipData)
				privateToolTipData = value
			End Set
		End Property

		Public Sub New(ByVal GDPs As List(Of GDP))
			ToolTipData = New ToolTipData(GDPs, GDPs(0).Country)
			CountryName = GDPs(0).Country
			GDPin2010 = GDPs(9).Product
		End Sub
	End Class

	Public Class ToolTipData
		Private privateGDPs As List(Of GDP)
		Public Property GDPs() As List(Of GDP)
			Get
				Return privateGDPs
			End Get
			Private Set(ByVal value As List(Of GDP))
				privateGDPs = value
			End Set
		End Property
		Private privateSeriesBrush As SolidColorBrush
		Public Property SeriesBrush() As SolidColorBrush
			Get
				Return privateSeriesBrush
			End Get
			Set(ByVal value As SolidColorBrush)
				privateSeriesBrush = value
			End Set
		End Property
		Private privateTitle As String
		Public Property Title() As String
			Get
				Return privateTitle
			End Get
			Private Set(ByVal value As String)
				privateTitle = value
			End Set
		End Property

		Public Sub New(ByVal gdps As List(Of GDP), ByVal countryName As String)
			GDPs = gdps
			Title = countryName & " GDP History"
		End Sub

	End Class
End Namespace
