Namespace Utilities.Conversions

  Public Module Conversions

    Public ReadOnly Property KilogramsToPounds() As Double
      Get
        Return 2.2046
      End Get
    End Property

    Public ReadOnly Property GramsToPounds() As Double
      Get
        Return KilogramsToPounds / 1000
      End Get
    End Property

    Public ReadOnly Property GramsToOunces() As Double
      Get
        Return GramsToPounds * 16
      End Get
    End Property

    Public ReadOnly Property PoundsToKilograms() As Double
      Get
        Return 0.4536
      End Get
    End Property

    Public ReadOnly Property PoundsToGrams() As Double
      Get
        Return PoundsToKilograms * 1000
      End Get
    End Property

    Public ReadOnly Property OuncesToGrams As Double
      Get
        Return PoundsToGrams / 16
      End Get
    End Property

    Public ReadOnly Property LitersToGallonsUK() As Double
      Get
        Return 0.219969
      End Get
    End Property

    Public ReadOnly Property LitersToGallonsUS() As Double
      Get
        Return 0.264172
      End Get
    End Property

    Public ReadOnly Property GallonsUSToLiters() As Double
      Get
        Return 3.785412
      End Get
    End Property

    Public ReadOnly Property GallonsUKToLiters() As Double
      Get
        Return 4.54609
      End Get
    End Property

    Public ReadOnly Property GallonsUKToGallonsUS() As Double
      Get
        Return 1.20094
      End Get
    End Property

    Public ReadOnly Property GallonsUSToGallonsUK() As Double
      Get
        Return 0.83267
      End Get
    End Property

    Public ReadOnly Property GallonsUSToPounds() As Double
      Get
        Return GallonsUSToLiters * KilogramsToPounds
      End Get
    End Property

    Public ReadOnly Property PoundsPerGallonUSToGramsPerLiter() As Double
      Get
        Return PoundsToGrams / GallonsUSToLiters
      End Get
    End Property

    Public ReadOnly Property MetersToYards() As Double
      Get
        Return 1.0936
      End Get
    End Property

    Public ReadOnly Property YardsToMeters() As Double
      Get
        Return 0.9144
      End Get
    End Property

    Public ReadOnly Property BarToPsi() As Double
      Get
        Return 14.5037738
      End Get
    End Property

    Public ReadOnly Property PsiToBar() As Double
      Get
        Return 1 / BarToPsi
      End Get
    End Property

    Public Function CentigradeToFarenheit(centigrade As Double) As Double
      ' Assumes tenths
      Return ((centigrade * 9) / 5) + 320
    End Function

    Public Function CentigradeToFarenheit(centigrade As Short) As Short
      ' Assumes tenths
      Dim farenheit As Double = CentigradeToFarenheit(CDbl(centigrade))
      Return CShort(farenheit)
    End Function

    Public Function FarenheitToCentigrade(farenheit As Double) As Double
      ' Assumes tenths
      Return ((farenheit - 320) / 9) * 5
    End Function

    Public Function FarenheitToCentigrade(farenheit As Short) As Short
      ' Assumes tenths 
      Dim centrigrade As Double = FarenheitToCentigrade(CDbl(farenheit))
      Return CShort(centrigrade)
    End Function

    ' 1 Gram per square meter = 0.02949 ounces per square yard
    Public Function GramsPerSquareMeterToOuncesPerSquareYard(gramsPerSquareMeter As Double) As Double
      Return (gramsPerSquareMeter * GramsToOunces) / (MetersToYards ^ 2)
    End Function

    ' 1 Ounce per square yard = 33.906 grams per square meter
    Public Function OuncesPerSquareYardToGramsPerSquareMeter(ouncesPerSquareYard As Double) As Double
      Return (ouncesPerSquareYard * OuncesToGrams) / (YardsToMeters ^ 2)
    End Function



  End Module

End Namespace
